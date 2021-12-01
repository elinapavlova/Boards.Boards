﻿using System;
using System.Collections.Generic;
using Common.Base;

namespace Database.Models
{
    public class ThreadModel : BaseModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid BoardId { get; set; }
        
        public BoardModel Board { get; set; }
        public ICollection<MessageModel> Messages { get; set; }
        public ICollection<FileModel> Files { get; set; }
    }
}