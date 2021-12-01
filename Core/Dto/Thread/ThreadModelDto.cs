using System;
using System.Collections.Generic;
using Common.Base;
using Core.Dto.File;

namespace Core.Dto.Thread
{
    public class ThreadModelDto : BaseModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid BoardId { get; set; }
        
        public ICollection<FileResponseDto> Files { get; set; }
    }
}