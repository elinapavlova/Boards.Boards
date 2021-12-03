using Boards.BoardService.Database.Repositories.Base;

namespace Boards.BoardService.Database.Repositories.Thread
{
    public class ThreadRepository : BaseRepository, IThreadRepository
    {
        public ThreadRepository(AppDbContext context) : base(context)
        {
        }
    }
}