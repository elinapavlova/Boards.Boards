using System;
using System.Collections.Generic;
using Boards.Auth.Common.Base;

namespace Boards.BoardService.Core.Dto.Thread
{
    public class ThreadModelDto : BaseModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid BoardId { get; set; }
        
        public ICollection<Uri> Files { get; set; }
    }
}