using System;
using System.Collections.Generic;
using Boards.Auth.Common.Base;

namespace Boards.BoardService.Core.Dto.Message
{
    public class MessageResponseDto : BaseModel
    {
        public string Text { get; set; }
        public ICollection<Uri> Files { get; set; }
        public ReferenceToMessageDto ReferenceToMessage { get; set; }
        public Guid ThreadId { get; set; }
    }
}