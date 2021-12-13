using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.Auth.Common.Options;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boards.BoardService.Database.Repositories.File
{
    public class FileRepository : BaseRepository, IFileRepository
    {
        private readonly AppDbContext _context;
        public FileRepository(AppDbContext context, PagingOptions options) : base(context, options)
        {
            _context = context;
        }

        public async Task<List<FileModel>> GetByThreadId(Guid id)
        {
            return _context.Set<FileModel>()
                .AsNoTracking()
                .AsEnumerable()
                .Where(f => f.ThreadId == id)
                .ToList();
        }
    }
}