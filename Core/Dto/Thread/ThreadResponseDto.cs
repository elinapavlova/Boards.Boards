﻿using System;
using System.Collections.Generic;
using Common.Base;
using Core.Dto.File;
using Database.Models;

namespace Core.Dto.Thread
{
    public class ThreadResponseDto : BaseModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid BoardId { get; set; }
        
        public ICollection<FileResponseDto> Files { get; set; }
        public ICollection<MessageModel> Messages { get; set; }
    }
}