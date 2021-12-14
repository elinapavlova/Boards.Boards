using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.Auth.Common.Filter;
using Boards.Auth.Common.Options;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boards.BoardService.Database.Repositories.Thread
{
    public class ThreadRepository : BaseRepository, IThreadRepository
    {
        private readonly AppDbContext _context;
        public ThreadRepository(AppDbContext context, PagingOptions options) : base(context, options)
        {
            _context = context;
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
                return threads;
            
            foreach (var thread in threads)
                thread.Files = _context.Set<FileModel>()
                    .AsNoTracking().AsEnumerable()
                    .Where(f => f.ThreadId == id && f.MessageId == null)
                    .ToList();

            return threads;
        }
    }
}