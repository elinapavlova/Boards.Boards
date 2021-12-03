using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;

namespace Boards.BoardService.Database.Repositories.Thread
{
    public interface IThreadRepository : IBaseRepository
    {
        Task<ICollection<ThreadModel>> GetByBoardId(Guid id);
    }
}