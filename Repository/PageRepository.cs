using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TesteApi.Context;
using TesteApi.Models;
using TesteApi.Services;

namespace TesteApi.Repository;

public class PageRepository : Repository<Page>, IPageRepository
{
    private readonly IWebHostEnvironment _environment;

    public PageRepository(MyContext context, IWebHostEnvironment environment) : base(context)
    {
        _environment = environment;
    }

    //public void UploadFiles(List<IFormFile> files, string subDirectory, string mangaName)
    //{
    //    subDirectory ??= string.Empty;
    //    var path = Path.Combine(_environment.ContentRootPath, $"wwwroot/uploads/{mangaName}/chapter - {subDirectory}");
    //    if (!Directory.Exists(path))
    //        Directory.CreateDirectory(path);
    //    files.ForEach(file =>
    //    {
    //        if (file.Length <= 0) return;
    //        var filePath = Path.Combine(path, file.FileName);
    //        using var stream = new FileStream(filePath, FileMode.Create);
    //        file.CopyToAsync(stream);
    //    });
    //}

    //public void ReadFilesAndCreatePages(List<IFormFile> files, string subDirectory, Chapter chapter, string mangaName)
    //{
    //    if (files.Count > 0)
    //    {
    //        files.ForEach(file =>
    //        {
    //            if (file.Length <= 0) return;
    //            var page = new Page
    //            {
    //                PageUrl = Encoding.ASCII.GetBytes(file.FileName),
    //                ChapterId = chapter.Id,
    //                Chapter = chapter
    //            };
    //            UploadFiles(files, subDirectory, mangaName);
    //            Add(page);
    //        });
    //    }
    //}

    //public async Task GetImages(string mangaName, string subDirectory, Chapter chapter)
    //{
    //    subDirectory ??= string.Empty;
    //    var path = Path.Combine(_environment.ContentRootPath, $"wwwroot/uploads/{mangaName}/chapter - {subDirectory}");
    //    var files = Directory.GetFiles(path);
    //    var images = new List<byte[]>();

    //    foreach (var file in files)
    //    {
    //        using (var stream = new FileStream(file, FileMode.Open))
    //        {
    //            var memoryStream = new MemoryStream();
    //            await stream.CopyToAsync(memoryStream);
    //            images.Add(memoryStream.ToArray());
    //        }
    //    }
    //}


    //public void DeletePageFiles(string mangaName, string subDirectory)
    //{
    //    subDirectory ??= string.Empty;
    //    var path = Path.Combine(_environment.ContentRootPath, $"wwwroot/uploads/{mangaName}/chapter - {subDirectory}");

    //    if (Directory.Exists(path))
    //        Directory.Delete(path, true);
    //}

    //public void ReplaceFiles(List<IFormFile> files, string subDirectory, string mangaName, Chapter chapter)
    //{
    //    DeletePageFiles(mangaName, subDirectory);
    //    ReadFilesAndCreatePages(files, subDirectory, chapter, mangaName);
    //}

    public void CreatePages(List<IFormFile> files, Chapter chapter, string mangaName)
    {
        var blobStorageService = new BlobStorageService();
        foreach (var page in files.Select(file => new Page
                 {   PageUrl = blobStorageService.Upload(file, mangaName, chapter.ChapterNumber),
                     PageNumber = file.FileName,
                     ChapterId = chapter.Id,
                     Chapter = chapter
                 }))
        {
            Add(page);
        }
    }

}

