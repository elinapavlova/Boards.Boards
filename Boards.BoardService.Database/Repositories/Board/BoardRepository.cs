using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;

namespace Boards.BoardService.Database.Repositories.Board
{
    public class BoardRepository : BaseRepository, IBoardRepository
    {
        public BoardRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ICollection<BoardModel>> GetByCategoryId(Guid id)
        {
            var boards = Get<BoardModel>(b => b.CategoryId == id);
            return boards;
        }
    }
}