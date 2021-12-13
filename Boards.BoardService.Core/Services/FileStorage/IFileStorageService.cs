using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Boards.BoardService.Core.Dto.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boards.BoardService.Core.Services.FileStorage
{
    public interface IFileStorageService
    {
        Task<List<FileResponseDto>> Upload(IFormFileCollection files, Guid threadId);
        Task<Collection<FileContentResult>> GetByThreadId(Guid id);
    }
}