using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;

namespace Boards.BoardService.Database.Repositories.Thread
{
    public class ThreadRepository : BaseRepository, IThreadRepository
    {
        public ThreadRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ICollection<ThreadModel>> GetByBoardId(Guid id)
        {
            var threads = Get<ThreadModel>(t => t.BoardId == id);
            if (threads == null)
                return null;
            
            foreach (var thread in threads)
                thread.Files = Get<FileModel>(f => f.ThreadId == thread.Id && f.MessageId == null);
            
            return threads;
        }
    }
}