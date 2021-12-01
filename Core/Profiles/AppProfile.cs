using System.Collections.Generic;
using AutoMapper;
using Common.Result;
using Core.Dto.Board;
using Core.Dto.Board.Create;
using Core.Dto.Category;
using Core.Dto.Category.Create;
using Core.Dto.File;
using Core.Dto.Thread;
using Core.Dto.Thread.Create;
using Database.Models;

namespace Core.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CreateBoardModelDto, BoardModel>();

            CreateMap<CreateCategoryModelDto, CategoryModel>();
            
            
            CreateMap<CreateThreadRequestDto, ThreadModel>()
                .ForMember(x => x.Files, opt 
                    => opt.Ignore());
            
            
            CreateMap<FileResponseDto, FileModel>();
            CreateMap<FileModel, FileResponseDto>();
            

            CreateMap<BoardModel, BoardModelDto>();
            CreateMap<BoardModel, BoardResponseDto>();
            CreateMap<BoardModel, ResultContainer<BoardModelDto>>()
                .ForMember(x => x.Data, opt =>
                    opt.MapFrom(b => b));
            CreateMap<ICollection<BoardModel>, ResultContainer<ICollection<BoardResponseDto>>>()
                .ForMember(x => x.Data, opt => 
                    opt.MapFrom(b => b));

            
            CreateMap<CategoryModel, CategoryModelDto>();
            CreateMap<CategoryModel, ResultContainer<CategoryModelDto>>()
                .ForMember(x => x.Data, opt =>
                    opt.MapFrom(c => c));
            
            
            CreateMap<ThreadModel, ThreadModelDto>();
            CreateMap<ThreadModel, ThreadResponseDto>();
            CreateMap<ThreadModel, CreateThreadResponseDto>();
            CreateMap<ThreadModel, ResultContainer<ThreadResponseDto>>()
                .ForMember(x => x.Data, opt =>
                    opt.MapFrom(t => t));
            CreateMap<ThreadModel, ResultContainer<CreateThreadResponseDto>>()
                .ForMember(x => x.Data, opt =>
                    opt.MapFrom(t => t));
            CreateMap<ThreadModel, ResultContainer<ThreadModelDto>>()
                .ForMember(x => x.Data, opt =>
                    opt.MapFrom(t => t));
            CreateMap<ICollection<ThreadModel>, ResultContainer<ICollection<ThreadModelDto>>>()
                .ForMember(x => x.Data, opt =>
                    opt.MapFrom(t => t));
        }
    }
}