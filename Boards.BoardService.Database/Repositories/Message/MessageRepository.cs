using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.BoardService.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Boards.BoardService.Database.Repositories.Message
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MessageModel>> GetByThreadId(Guid id, int pageNumber, int pageSize)
        {
            var messages = _context.Set<MessageModel>()
                .AsNoTracking().AsEnumerable()
                .Where(m => m.ThreadId == id)
                .AsQueryable()
                .OrderByDescending(b => b.DateCreated)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return messages;
        }
    }
}