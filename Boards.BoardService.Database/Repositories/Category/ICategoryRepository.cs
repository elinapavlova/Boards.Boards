using System.Threading.Tasks;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;

namespace Boards.BoardService.Database.Repositories.Category
{
    public interface ICategoryRepository : IBaseRepository
    {
        Task<CategoryModel> GetByName(string name);
    }
}