using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.BoardService.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Boards.BoardService.Database.Repositories.File
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;
        public FileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FileModel>> GetByThreadId(Guid id)
        {
            return _context.Set<FileModel>()
                .AsNoTracking()
                .AsEnumerable()
                .Where(f => f.ThreadId == id && f.MessageId == null)
                .ToList();
        }

        public async Task<FileModel> Create(FileModel file)
        {
            file.DateCreated = DateTime.Now;
            await _context.Set<FileModel>().AddAsync(file);
            await _context.SaveChangesAsync();
            return file;
        }
    }
}