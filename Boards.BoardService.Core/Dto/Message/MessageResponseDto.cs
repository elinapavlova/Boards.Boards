using System;
using System.Collections.Generic;
using Boards.Auth.Common.Base;
using Boards.BoardService.Core.Dto.File;

namespace Boards.BoardService.Core.Dto.Message
{
    public class MessageResponseDto : BaseModel
    {
        public string Text { get; set; }
        public ICollection<FileResultDto> Files { get; set; }
        public ReferenceToMessageDto ReferenceToMessage { get; set; }
        public Guid ThreadId { get; set; }
    }
}