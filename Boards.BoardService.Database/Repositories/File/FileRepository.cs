using Boards.BoardService.Database.Repositories.Base;

namespace Boards.BoardService.Database.Repositories.File
{
    public class FileRepository : BaseRepository, IFileRepository
    {
        public FileRepository(AppDbContext context) : base(context)
        {
        }
    }
}