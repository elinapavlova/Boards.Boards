using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var threads = await GetFiltered<ThreadModel>
                (t => t.BoardId == id, pageNumber, pageSize);

            if (threads.Count == 0)
                return threads;

            return await GetFiles(threads);
        }

        public async Task<ICollection<ThreadModel>> GetByName(string name, int pageNumber, int pageSize)
        {
            var threads = await GetFiltered<ThreadModel>
                (t => t.Name.Contains(name), pageNumber, pageSize);

            if (threads.Count == 0)
                return threads;
            
            return await GetFiles(threads);
        }

        private async Task<ICollection<ThreadModel>> GetFiles(ICollection<ThreadModel> threads)
        {
            foreach (var thread in threads)
                thread.Files = await _context.Set<FileModel>()
                    .AsNoTracking()
                    .Where(f => f.ThreadId == thread.Id && f.MessageId == null)
                    .ToListAsync();

            return threads;
        }
    }
}