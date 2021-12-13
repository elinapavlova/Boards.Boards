using System;
using System.Collections.Generic;
using Boards.Auth.Common.Base;
using Boards.BoardService.Core.Dto.Message;
using Microsoft.AspNetCore.Mvc;

namespace Boards.BoardService.Core.Dto.Thread
{
    public class ThreadResponseDto : BaseModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid BoardId { get; set; }
        
        public ICollection<FileContentResult> Files { get; set; }
        public ICollection<MessageResponseDto> Messages { get; set; }
    }
}