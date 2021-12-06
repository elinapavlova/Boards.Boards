using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Boards.BoardService.Core.Dto.Message;
using Common.Error;
using Common.Result;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Thread;
using Common.Filter;
using Common.Options;
using Newtonsoft.Json;

namespace Boards.BoardService.Core.Services.Message
{
    public class MessageService : IMessageService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IThreadRepository _threadRepository;
        private readonly PagingOptions _pagingOptions;

        public MessageService
        (
            IHttpClientFactory clientFactory, 
            IThreadRepository threadRepository,
            PagingOptions pagingOptions
        )
        {
            _clientFactory = clientFactory;
            _threadRepository = threadRepository;
            _pagingOptions = pagingOptions;
        }

        public async Task<ResultContainer<ICollection<MessageResponseDto>>> GetByThreadId(Guid id, FilterPagingDto filter)
        {
            var result = new ResultContainer<ICollection<MessageResponseDto>>();
            var thread = await _threadRepository.GetById<ThreadModel>(id);
            if (thread == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            if (filter.PageNumber == 0)
                filter.PageNumber = _pagingOptions.DefaultPageNumber;
            if (filter.PageSize == 0)
                filter.PageSize = _pagingOptions.DefaultPageSize;

            using var client = _clientFactory.CreateClient("MessageService");
            var response = await client.PostAsJsonAsync($"By-Thread-Id/{id}", filter);

            switch (response.IsSuccessStatusCode)
            {
                case false when filter.PageNumber > _pagingOptions.DefaultPageNumber:
                    result.ErrorType = ErrorType.NotFound;
                    return result;
                case false:
                    return result;
            }

            var responseMessage = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<ICollection<MessageResponseDto>>(responseMessage);

            foreach (var message in responseJson)
                if (message.ReferenceToMessage.Id == Guid.Empty)
                    message.ReferenceToMessage = null;

            result.Data = responseJson;
            return result;
        }
    }
}