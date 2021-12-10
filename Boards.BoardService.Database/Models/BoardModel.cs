using System;
using System.Collections.Generic;
using Boards.Auth.Common.Base;

namespace Boards.BoardService.Database.Models
{
    public class BoardModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        
        public CategoryModel Category { get; set; }
        public ICollection<ThreadModel> Threads { get; set; }
    }
}