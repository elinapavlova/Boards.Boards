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

            CreateMap<ThreadModel, ThreadModelDto>();
            CreateMap<ThreadModel, ThreadResponseDto>();
            CreateMap<ThreadModel, CreateThreadResponseDto>();

            
            CreateMap<CreateCategoryModelDto, CategoryModel>();
            CreateMap<CategoryModel, CategoryModelDto>();

            
            CreateMap<CreateThreadRequestDto, ThreadModel>()
                .ForMember(x => x.Files, opt 
                    => opt.Ignore());
            
            
            CreateMap<FileResponseDto, FileModel>();
            CreateMap<FileModel, FileResponseDto>();

            
            CreateMap<BoardModel, BoardModelDto>();
            CreateMap<BoardModel, BoardResponseDto>();
            CreateMap<BoardModel, ResultContainer<BoardModelDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(b => b));
            CreateMap<ICollection<BoardModel>, ResultContainer<ICollection<BoardResponseDto>>>()
                .ForMember("Data", opt => 
                    opt.MapFrom(b => b));

            
            CreateMap<CategoryModel, ResultContainer<CategoryModelDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(c => c));
            
            
            CreateMap<ThreadModel, ResultContainer<ThreadResponseDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(t => t));
            CreateMap<ThreadModel, ResultContainer<CreateThreadResponseDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(t => t));
            CreateMap<ThreadModel, ResultContainer<ThreadModelDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(t => t));
            CreateMap<ICollection<ThreadModel>, ResultContainer<ICollection<ThreadModelDto>>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(t => t));
        }
    }
}