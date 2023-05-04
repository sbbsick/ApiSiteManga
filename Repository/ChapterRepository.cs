using TesteApi.Context;
using TesteApi.Models;

namespace TesteApi.Repository;

public class ChapterRepository : Repository<Chapter>, IChapterRepository
{
    private readonly IWebHostEnvironment _environment;

    public ChapterRepository(MyContext context, IWebHostEnvironment environment) : base(context)
    {
        _environment = environment;
    }

    public Tuple<int, string> SaveImage(IFormFile imageFile)
    {
        try
        {
            var path = Path.Combine(_environment.ContentRootPath, "wwwroot/uploads");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            // check the allowed extenstions
            var ext = Path.GetExtension(imageFile.FileName);
            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };

            if (!allowedExtensions.Contains(ext))
            {
                string msg = $"Only {string.Join(",", allowedExtensions)} extensions are allowed";
                return new Tuple<int, string>(0, msg);
            }

            string uniqueString = Guid.NewGuid().ToString();

            // create a unique filename here
            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            var stream = new FileStream(fileWithPath, FileMode.Create);
            imageFile.CopyTo(stream);
            stream.Close();
            return new Tuple<int, string>(1, newFileName);

        }
        catch (Exception ex)
        {
            return new Tuple<int, string>(0, $"Error has occured - {ex.Message}");
        }

    }
}