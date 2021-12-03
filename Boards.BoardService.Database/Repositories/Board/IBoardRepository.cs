using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;

namespace Boards.BoardService.Database.Repositories.Board
{
    public interface IBoardRepository : IBaseRepository
    {
        Task<ICollection<BoardModel>> GetByCategoryId(Guid id);
    }
}