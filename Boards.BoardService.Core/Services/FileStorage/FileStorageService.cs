﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Boards.BoardService.Core.Dto.File;
using Boards.BoardService.Core.Dto.File.Upload;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.File;
using Boards.Common.Error;
using Boards.Common.Result;
using Microsoft.AspNetCore.Http;
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

        public async Task<ICollection<FileModel>> GetByThreadId(Guid id)
        {
            var files = _fileRepository.Get<FileModel>(f => f.ThreadId == id && f.MessageId == null)
                .AsEnumerable().ToList();
            return files;
        }
        
        public async Task<ResultContainer<UploadFilesResponseDto>> Upload(IFormFileCollection files, Guid threadId)
        {
            var result = new ResultContainer<UploadFilesResponseDto>();
            var content = await CreateContent(files);
            if (content == null)
            {
                result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            using var client = _clientFactory.CreateClient("FileStorage");
            var response = await client.PostAsync("Upload", content);
            var responseMessage = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<UploadFilesResultDto>(responseMessage);

            result = await ValidateResult(responseJson, threadId);
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

        private async Task<ResultContainer<UploadFilesResponseDto>> ValidateResult(UploadFilesResultDto files, Guid threadId)
        {
            var result = new ResultContainer<UploadFilesResponseDto>
            {
                Data = new UploadFilesResponseDto
                {
                    Files = new List<FileResponseDto>()
                }
            };

            if (files.ErrorType != null)
            {
                result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            foreach (var file in files.Data)
            {
                file.ThreadId = threadId;
                switch (file.Extension)
                {
                    case ".mp4" :
                    case ".wav" :
                    case ".txt" :
                    case ".png" :
                    case ".jpg" :
                        var res = await AddFileToDatabase(file);
                        result.Data.Files.Add(res);
                        break;
                }
            }

            return result;
        }

        private async Task<FileResponseDto> AddFileToDatabase(FileResponseDto newFile)
        {
            var file = _mapper.Map<FileModel>(newFile);
            file.MessageId = null;
            var result = _mapper.Map<FileResponseDto>(await _fileRepository.Create(file));

            return result;
        }
    }
}