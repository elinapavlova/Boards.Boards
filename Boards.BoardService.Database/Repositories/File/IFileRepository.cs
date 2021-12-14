using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Database.Models;

namespace Boards.BoardService.Database.Repositories.File
{
    public interface IFileRepository
    {
        Task<List<FileModel>> GetByThreadId(Guid id);
        Task<FileModel> Create(FileModel file);
    }
}