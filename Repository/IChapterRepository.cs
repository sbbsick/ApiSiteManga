using TesteApi.Models;

namespace TesteApi.Repository;

public interface IChapterRepository : IRepository<Chapter>
{
    public Tuple<int, string> SaveImage(IFormFile imageFile);
}