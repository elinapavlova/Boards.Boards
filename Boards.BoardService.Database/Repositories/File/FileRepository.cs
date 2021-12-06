using Boards.BoardService.Database.Repositories.Base;
using Common.Options;

namespace Boards.BoardService.Database.Repositories.File
{
    public class FileRepository : BaseRepository, IFileRepository
    {
        public FileRepository(AppDbContext context, PagingOptions options) : base(context, options)
        {
        }
    }
}