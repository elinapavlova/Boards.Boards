using System;
using Common.Base;

namespace Database.Models
{
    public class BoardModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        
        public CategoryModel Category { get; set; }
    }
}