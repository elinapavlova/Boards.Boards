using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Result;
using Core.Dto.Board;
using Core.Dto.Board.Create;
using Core.Dto.Board.Update;

namespace Core.Services.Board
{
    public interface IBoardService
    {
        Task<ResultContainer<BoardModelDto>> Create(CreateBoardModelDto board);
        Task<ResultContainer<BoardModelDto>> GetByName(string name);
        Task<ResultContainer<BoardModelDto>> GetById(Guid id);
        Task<ResultContainer<BoardModelDto>> Delete(Guid id);
        Task<ResultContainer<BoardModelDto>> Update(UpdateBoardRequestDto data);
        Task<ResultContainer<ICollection<BoardResponseDto>>> GetByCategoryId(Guid id);
    }
}