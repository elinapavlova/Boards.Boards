using System.Collections.Generic;
using AutoMapper;
using Boards.BoardService.Core.Dto.Board;
using Boards.BoardService.Core.Dto.Board.Create;
using Boards.BoardService.Core.Dto.Category;
using Boards.BoardService.Core.Dto.Category.Create;
using Boards.BoardService.Core.Dto.File;
using Boards.BoardService.Core.Dto.Thread;
using Boards.BoardService.Core.Dto.Thread.Create;
using Boards.BoardService.Database.Models;
using Boards.Common.Result;

namespace Boards.BoardService.Core.Profiles
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
            CreateMap<BoardModel, ResultContainer<BoardResponseDto>>()
                .ForMember(x => x.Data, opt =>
                    opt.MapFrom(b => b));
            CreateMap<ICollection<BoardModel>, ResultContainer<ICollection<BoardModelDto>>>()
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