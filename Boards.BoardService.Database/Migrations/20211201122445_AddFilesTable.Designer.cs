// <auto-generated />
using System;
using Boards.BoardService.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Boards.BoardService.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211201122445_AddFilesTable")]
    partial class AddFilesTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Boards.BoardService.Database.Models.BoardModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Boards.BoardService.Database.Models.CategoryModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Boards.BoardService.Database.Models.FileModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<Guid?>("MessageId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("ThreadId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ThreadId");

                    b.ToTable("FileModel");
                });
            
            modelBuilder.Entity("Boards.BoardService.Database.Models.ThreadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BoardId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("Boards.BoardService.Database.Models.BoardModel", b =>
                {
                    b.HasOne("Boards.BoardService.Database.Models.CategoryModel", "Category")
                        .WithMany("Boards")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Boards.BoardService.Database.Models.FileModel", b =>
                {
                    b.HasOne("Boards.BoardService.Database.Models.ThreadModel", "Thread")
                        .WithMany("Files")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Thread");
                });

            modelBuilder.Entity("Boards.BoardService.Database.Models.ThreadModel", b =>
                {
                    b.HasOne("Boards.BoardService.Database.Models.BoardModel", "Board")
                        .WithMany("Threads")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("Boards.BoardService.Database.Models.BoardModel", b =>
                {
                    b.Navigation("Threads");
                });

            modelBuilder.Entity("Boards.BoardService.Database.Models.CategoryModel", b =>
                {
                    b.Navigation("Boards");
                });

            modelBuilder.Entity("Boards.BoardService.Database.Models.ThreadModel", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
