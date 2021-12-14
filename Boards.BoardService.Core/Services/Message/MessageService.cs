using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Boards.Auth.Common.Error;
using Boards.Auth.Common.Filter;
using Boards.Auth.Common.Options;
using Boards.Auth.Common.Result;
using Boards.BoardService.Core.Dto.Message;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Message;
using Boards.BoardService.Database.Repositories.Thread;
using Newtonsoft.Json;

namespace Boards.BoardService.Core.Services.Message
{
    public class MessageService : IMessageService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IThreadRepository _threadRepository;
        private readonly PagingOptions _pagingOptions;
        private readonly IMessageRepository _messageRepository;

        public MessageService
        (
            IHttpClientFactory clientFactory, 
            IThreadRepository threadRepository,
            PagingOptions pagingOptions,
            IMessageRepository messageRepository
        )
        {
            _clientFactory = clientFactory;
            _threadRepository = threadRepository;
            _pagingOptions = pagingOptions;
            _messageRepository = messageRepository;
        }

        public async Task<ResultContainer<ICollection<MessageResponseDto>>> GetByThreadId(Guid id, FilterPagingDto filter)
        {
            var result = new ResultContainer<ICollection<MessageResponseDto>>
            {
                Data = new List<MessageResponseDto>()
            };
            
            var thread = await _threadRepository.GetById<ThreadModel>(id);
            if (thread == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }
            
            if (filter.PageNumber <= 0)
                filter.PageNumber = _pagingOptions.DefaultPageNumber;
            if (filter.PageSize <= 0)
                filter.PageSize = _pagingOptions.DefaultPageSize;

            var messages = await _messageRepository.GetByThreadId(id, filter.PageNumber, filter.PageSize);
            if (messages.Count == 0 && filter.PageNumber > _pagingOptions.DefaultPageNumber)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = await GetFiles(messages);
            return result;
        }

        private async Task<ResultContainer<ICollection<MessageResponseDto>>> GetFiles(List<MessageModel> messages)
        {
            var result = new ResultContainer<ICollection<MessageResponseDto>>
            {
                Data = new List<MessageResponseDto>()
            };
            using var client = _clientFactory.CreateClient("MessageService");
            foreach (var message in messages)
            {
                var response = await client.GetAsync($"{message.Id}");
                var content = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<MessageResponseDto>(content);

                result.Data.Add(item);
            }
            return result;
        }
    }
}