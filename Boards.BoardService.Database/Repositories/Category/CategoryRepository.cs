using Boards.BoardService.Database.Repositories.Base;
using Boards.Common.Options;

namespace Boards.BoardService.Database.Repositories.Category
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context, PagingOptions options) : base(context, options)
        {
        }
    }
}