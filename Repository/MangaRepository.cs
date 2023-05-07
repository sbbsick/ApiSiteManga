using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using TesteApi.Context;
using TesteApi.Models;

namespace TesteApi.Repository;

public class MangaRepository : Repository<Manga>, IMangaRepository
{
    private readonly IWebHostEnvironment _environment;

    public MangaRepository(MyContext context, IWebHostEnvironment environment) : base(context)
    {
        _environment = environment;
    }

    public async Task<IEnumerable<Manga>> GetMangaGenres()
    {
        return await Get().Include(m => m.Genres).ToListAsync();
    }

    //public void UploadCover(IFormFile mangaCover, string mangaName)
    //{
    //    var path = Path.Combine(_environment.ContentRootPath, $"wwwroot/uploads/{mangaName}");

    //    if (!Directory.Exists(path)) 
    //        Directory.CreateDirectory(path);

    //    if (mangaCover.Length <= 0) return;

    //    var filePath = Path.Combine(path, mangaCover.FileName);
    //    using var stream = new FileStream(filePath, FileMode.Create);
    //    mangaCover.CopyToAsync(stream);
    //}


    //public void DeleteMangaPages(string mangaName)
    //{
    //    var manga = Get().FirstOrDefault(m => m.Name == mangaName);

    //    if (manga.Chapters == null) return;

    //    var path = Path.Combine(_environment.ContentRootPath, $"wwwroot/uploads/{mangaName}");

    //    if (Directory.Exists(path))
    //        Directory.Delete(path, true);
    //}

    //public async Task<IEnumerable<Manga>> GetMangaChapters()
    //{
    //    return await Get().Include(m => m.Chapters).ToListAsync();
    //}
}