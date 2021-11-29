using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Models;
using Database.Repositories.Base;

namespace Database.Repositories.Board
{
    public interface IBoardRepository : IBaseRepository
    {
        Task<ICollection<BoardModel>> GetByCategoryId(Guid id);
    }
}