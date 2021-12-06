using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Core.Dto.File.Upload;
using Boards.BoardService.Database.Models;
using Boards.Common.Result;
using Microsoft.AspNetCore.Http;

namespace Boards.BoardService.Core.Services.FileStorage
{
    public interface IFileStorageService
    {
        Task<ResultContainer<UploadFilesResponseDto>> Upload(IFormFileCollection files, Guid threadId);
        Task<ICollection<FileModel>> GetByThreadId(Guid id);
    }
}