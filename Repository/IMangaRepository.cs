using TesteApi.Models;

namespace TesteApi.Repository;

public interface IMangaRepository : IRepository<Manga>
{
    Task<IEnumerable<Manga>> GetMangaGenres();

    //void UploadCover(IFormFile mangaCover, string mangaName);

    //void DeleteMangaPages(string mangaName);
}