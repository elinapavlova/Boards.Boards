using System.Collections.Generic;
using AutoMapper;
using Common.Result;
using Core.Dto;
using Core.Dto.Board;
using Core.Dto.Board.Create;
using Core.Dto.Category;
using Database.Models;

namespace Core.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CreateBoardModelDto, BoardModel>();
            CreateMap<BoardModel, BoardModelDto>();
            CreateMap<BoardModel, BoardResponseDto>();

            CreateMap<CreateCategoryModelDto, CategoryModel>();
            CreateMap<CategoryModel, CategoryModelDto>();

            CreateMap<BoardModel, ResultContainer<BoardModelDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(b => b));
            CreateMap<ICollection<BoardModel>, ResultContainer<ICollection<BoardResponseDto>>>()
                .ForMember("Data", opt => 
                    opt.MapFrom(b => b));

            CreateMap<CategoryModel, ResultContainer<CategoryModelDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(c => c));
            
            
        }
    }
}