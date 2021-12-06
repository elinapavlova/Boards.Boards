using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Common.Options;
using Boards.BoardService.Core.Profiles;
using Boards.BoardService.Core.Services.Board;
using Boards.BoardService.Core.Services.Category;
using Boards.BoardService.Core.Services.FileStorage;
using Boards.BoardService.Core.Services.Message;
using Boards.BoardService.Core.Services.Thread;
using Boards.BoardService.Database;
using Boards.BoardService.Database.Repositories.Base;
using Boards.BoardService.Database.Repositories.Board;
using Boards.BoardService.Database.Repositories.Category;
using Boards.BoardService.Database.Repositories.File;
using Boards.BoardService.Database.Repositories.Thread;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Boards.BoardService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Http Clients
            services.AddHttpClient<IFileStorageService, FileStorageService>("FileStorage", client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseAddress:FileStorage"]);
            });
            services.AddHttpClient<IMessageService, MessageService>("MessageService", client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseAddress:MessageService"]);
            });
            
            var key = Encoding.ASCII.GetBytes(Configuration["AppOptions:Secret"]);
            
            ConfigureSwagger(services);
            
            services.Configure<AppOptions>(Configuration.GetSection(AppOptions.App));
            var tokenOptions = Configuration.GetSection(AppOptions.App).Get<AppOptions>();
            services.AddSingleton(tokenOptions);
            
            services.Configure<PagingOptions>(Configuration.GetSection(PagingOptions.Paging));
            var pagingOptions = Configuration.GetSection(PagingOptions.Paging).Get<PagingOptions>();
            services.AddSingleton(pagingOptions);

            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IThreadRepository, ThreadRepository>();
            services.AddScoped<IFileRepository, FileRepository>();

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBoardService, Core.Services.Board.BoardService>();
            services.AddScoped<IThreadService, ThreadService>();
            services.AddScoped<IFileStorageService, FileStorageService>();

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection,  
                x => x.MigrationsAssembly("Boards.BoardService.Database")));
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AppProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuer = false,
                        RequireExpirationTime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                    x.SaveToken = true;
                });
            
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            
            services.AddControllers();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    opt => {
                        foreach (var description in provider.ApiVersionDescriptions) {
                            opt.SwaggerEndpoint(
                                $"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                        }
                    });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
            
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    BearerFormat = "Bearer {authToken}",
                    Description = "JSON Web Token to access resources. Example: Bearer {token}",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

    }
}