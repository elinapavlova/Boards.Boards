using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Core.Dto.Board;
using Boards.BoardService.Core.Dto.Board.Create;
using Boards.BoardService.Core.Dto.Board.Update;
using Common.Filter;
using Common.Result;

namespace Boards.BoardService.Core.Services.Board
{
    public interface IBoardService
    {
        Task<ResultContainer<BoardModelDto>> Create(CreateBoardModelDto board);
        Task<ResultContainer<BoardModelDto>> GetByName(string name);
        Task<ResultContainer<BoardModelDto>> GetById(Guid id);
        Task<ResultContainer<BoardModelDto>> Delete(Guid id);
        Task<ResultContainer<BoardModelDto>> Update(UpdateBoardRequestDto data);
        Task<ResultContainer<ICollection<BoardModelDto>>> GetByCategoryId(Guid id);
        Task<ResultContainer<BoardResponseDto>> GetByIdWithThreads(Guid id, FilterPagingDto filter);
    }
}