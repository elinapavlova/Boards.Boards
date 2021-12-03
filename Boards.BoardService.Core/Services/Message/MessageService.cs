using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Boards.BoardService.Core.Dto.Message;
using Common.Error;
using Common.Result;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Thread;
using Newtonsoft.Json;

namespace Boards.BoardService.Core.Services.Message
{
    public class MessageService : IMessageService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IThreadRepository _threadRepository;

        public MessageService
        (
            IHttpClientFactory clientFactory, 
            IThreadRepository threadRepository
        )
        {
            _clientFactory = clientFactory;
            _threadRepository = threadRepository;
        }

        public async Task<ResultContainer<ICollection<MessageResponseDto>>> GetByThreadId(Guid id)
        {
            var result = new ResultContainer<ICollection<MessageResponseDto>>();
            var thread = await _threadRepository.GetById<ThreadModel>(id);
            if (thread == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            using var client = _clientFactory.CreateClient("MessageService");
            var response = await client.PostAsync($"By-Thread-Id/{id}", null!);
            
            var responseMessage = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<ICollection<MessageResponseDto>>(responseMessage);

            result.Data = responseJson;
            return result;
        }
    }
}