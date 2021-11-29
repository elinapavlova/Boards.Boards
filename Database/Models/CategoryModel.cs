using System.Collections.Generic;
using Common.Base;

namespace Database.Models
{
    public class CategoryModel : BaseModel
    {
        public string Name { get; set; }

        public ICollection<BoardModel> Boards { get; set; }
    }
}