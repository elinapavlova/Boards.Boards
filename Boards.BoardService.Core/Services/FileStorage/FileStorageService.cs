using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Boards.BoardService.Core.Dto.File;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Boards.BoardService.Core.Services.FileStorage
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        public FileStorageService
        (
            IHttpClientFactory clientFactory,
            IMapper mapper,
            IFileRepository fileRepository
        )
        {
            _clientFactory = clientFactory;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task<Collection<FileContentResult>> GetByThreadId(Guid id)
        {
            var result = new Collection<FileContentResult>();
            var files = await _fileRepository.GetByThreadId(id);
            if (files == null)
                return null;

            foreach (var file in files)
            {
                using var client = _clientFactory.CreateClient("FileStorage");
                var response = await client.GetAsync($"{file.Id}");
                result.Add(new FileContentResult
                    (await response.Content.ReadAsByteArrayAsync(), response.Content.Headers.ContentType.MediaType));
            }

            return result;
        }
        
        public async Task<List<FileResponseDto>> Upload(IFormFileCollection files, Guid threadId)
        {
            var content = await CreateContent(files);
            if (content == null)
                return null;

            using var client = _clientFactory.CreateClient("FileStorage");
            var response = await client.PostAsync("Upload", content);
            var responseMessage = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<FileResponseDto>>(responseMessage);

            if (result == null)
                return null;

            await AddFilesToDatabase(threadId, result);

            return result;
        }
        
        private static async Task<MultipartFormDataContent> CreateContent(IFormFileCollection files)
        {
            var multiContent = new MultipartFormDataContent();

            if (files.Count > 10)
                return null;
            
            foreach (var file in files)
            {
                // Если файл пустой или его размер больше 100 мб
                if (file.Length is <= 0 or > 104857600)
                    return null;

                var streamContent = new StreamContent(file.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                multiContent.Add(streamContent, file.Name, file.FileName);
            }

            return multiContent;
        }

        private async Task AddFilesToDatabase(Guid threadId, IEnumerable<FileResponseDto> files)
        {
            foreach (var file in files)
            {
                var newFile = _mapper.Map<FileModel>(file);
                newFile.ThreadId = threadId;
                
                await _fileRepository.Create(newFile);
            }
        }
    }
}