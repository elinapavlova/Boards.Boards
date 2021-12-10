using System.Collections.Generic;
using Boards.Auth.Common.Base;

namespace Boards.BoardService.Database.Models
{
    public class CategoryModel : BaseModel
    {
        public string Name { get; set; }

        public ICollection<BoardModel> Boards { get; set; }
    }
}