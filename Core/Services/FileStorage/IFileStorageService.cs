using System;
using System.Threading.Tasks;
using Common.Result;
using Core.Dto.File.Upload;
using Microsoft.AspNetCore.Http;

namespace Core.Services.FileStorage
{
    public interface IFileStorageService
    {
        Task<ResultContainer<UploadFilesResponseDto>> Upload(IFormFileCollection files, Guid threadId);
    }
}