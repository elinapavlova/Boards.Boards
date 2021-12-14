using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Boards.BoardService.Core.Dto.File;
using Microsoft.AspNetCore.Http;

namespace Boards.BoardService.Core.Services.FileStorage
{
    public interface IFileStorageService
    {
        Task<List<FileResponseDto>> Upload(IFormFileCollection files, Guid threadId);
        Task<Collection<Uri>> GetByThreadId(Guid id);
    }
}