using System.Threading.Tasks;
using Boards.Auth.Common.Options;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boards.BoardService.Database.Repositories.Category
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context, PagingOptions options) : base(context, options)
        {
            _context = context;
        }
        
        public async Task<CategoryModel> GetByName(string name)
        {
            return await _context.Set<CategoryModel>()
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Name == name);
        }
    }
}