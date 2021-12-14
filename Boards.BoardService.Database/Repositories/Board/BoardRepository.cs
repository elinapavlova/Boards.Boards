using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.Auth.Common.Options;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boards.BoardService.Database.Repositories.Board
{
    public class BoardRepository : BaseRepository, IBoardRepository
    {
        private readonly AppDbContext _context;
        public BoardRepository(AppDbContext context, PagingOptions options) : base(context, options)
        {
            _context = context;
        }

        public async Task<ICollection<BoardModel>> GetByCategoryId(Guid id)
        {
            return await _context.Set<BoardModel>()
                .AsNoTracking()
                .Where(b => b.CategoryId == id)
                .ToListAsync();
        }

        public async Task<BoardModel> GetByName(string name)
        {
            return await _context.Set<BoardModel>()
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Name == name);
        }
    }
}