using TesteApi.Models;

namespace TesteApi.Repository;

public interface IMangaRepository : IRepository<Manga>
{
    Task<IEnumerable<Manga>> GetMangaGenres();

    void DeleteMangaPages(string mangaName);
}