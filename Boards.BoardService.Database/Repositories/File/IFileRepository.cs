using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;

namespace Boards.BoardService.Database.Repositories.File
{
    public interface IFileRepository : IBaseRepository
    {
        Task<List<FileModel>> GetByThreadId(Guid id);
    }
}