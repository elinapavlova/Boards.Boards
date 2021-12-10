using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.Auth.Common.Options;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;

namespace Boards.BoardService.Database.Repositories.Board
{
    public class BoardRepository : BaseRepository, IBoardRepository
    {
        public BoardRepository(AppDbContext context, PagingOptions options) : base(context, options)
        {
        }

        public async Task<ICollection<BoardModel>> GetByCategoryId(Guid id)
        {
            var boards = Get<BoardModel>(b => b.CategoryId == id).AsEnumerable().ToList();
            return boards;
        }
    }
}