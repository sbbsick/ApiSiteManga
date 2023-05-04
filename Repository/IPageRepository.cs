using Microsoft.AspNetCore.Mvc;
using TesteApi.Models;

namespace TesteApi.Repository;

public interface IPageRepository : IRepository<Page>
{
    //public Task PostMultiFileAsync(List<PageUpload> fileData, [FromForm] Page page);
    public void UploadFiles(List<IFormFile> files, string subDirectory, string mangaName);

    public void ReadFilesAndCreatePages(List<IFormFile> files, string subDirectory, Chapter chapter, string mangaName);

    public void ReplaceFiles(List<IFormFile> files, string subDirectory, string mangaName, Chapter chapter);

    public void DeletePageFiles(string mangaName, string subDirectory);

    public Task GetImages(string mangaName, string subDirectory, Chapter chapter);
}