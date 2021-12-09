using System;
using System.Collections.Generic;
using Boards.BoardService.Core.Dto.File;
using Boards.Common.Base;

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