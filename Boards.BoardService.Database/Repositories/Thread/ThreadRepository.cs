using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;
using Boards.Common.Filter;
using Boards.Common.Options;

namespace Boards.BoardService.Database.Repositories.Thread
{
    public class ThreadRepository : BaseRepository, IThreadRepository
    {
        public ThreadRepository(AppDbContext context, PagingOptions options) : base(context, options)
        {
        }

        public async Task<ICollection<ThreadModel>> GetByBoardId(Guid id, int pageNumber, int pageSize)
        {
            var filter = new BaseFilter
            {
                Paging = new FilterPagingDto {PageNumber = pageNumber, PageSize = pageSize}
            };
            var source = Get<ThreadModel>(t => t.BoardId == id);
            var threads = await GetFiltered(source, filter);
            if (threads.Count == 0)
                return null;
            
            foreach (var thread in threads)
                thread.Files = Get<FileModel>(f => f.ThreadId == thread.Id && f.MessageId == null)
                    .AsEnumerable().ToList();
            
            return threads;
        }
    }
}