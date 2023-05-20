using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using TesteApi.Context;

namespace TesteApi.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly MyContext _context;


    protected Repository(MyContext context)
    {
        _context = context;

    }

    //public async Task<string> UploadFile(IFormFile imageFile)
    //{
    //    using (var httpClient = new HttpClient())
    //    {
    //        using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"https://storage.googleapis.com/upload/storage/v1/b/manga-pages/o?uploadType=media&name={imageFile.FileName}"))
    //        {
    //            request.Headers.TryAddWithoutValidation("Authorization", "Bearer ya29.a0AWY7Ckk63GRfmkMX6Z4XngxaUY0x6wpQIYxjXbqDkDSgrCasNTT7_wUTXhVRBoL9UteqGKrhzDySQA2xe4GvJ54Pj4XCb7BD-sZXmWxz_LebS5ymoHlGMRSEPjQyxGIKHv0idVKLRJDFCNDf0vmE6YhrWIoHAEwaCgYKARASARMSFQG1tDrpAKmiVH8sujws60PXvILBwg0166"); 

    //            using (var stream = imageFile.OpenReadStream())
    //            {
    //                request.Content = new StreamContent(stream);
    //                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(imageFile.ContentType); 

    //                var response = await httpClient.SendAsync(request);
    //                var responseString = await response.Content.ReadAsStringAsync();
    //                return responseString;
    //            }
    //        }
    //    }
    //}

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> Get()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public async Task<T> GetById(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Set<T>().Update(entity);
    }

    //ToDo add a method to upload images to google cloud storage
    public async Task<string> ImgurImageUpload(IFormFile imageFile)
    {
        const string imgurApiEndpoint = "https://api.imgur.com/3/upload";
        const string imgurClientId = "1b660fd81e65724";

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", imgurClientId);

        using (var form = new MultipartFormDataContent())
        {
            await using (var stream = imageFile.OpenReadStream())
            {
                var imageContent = new StreamContent(stream);
                imageContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);
                form.Add(imageContent, "image", imageFile.FileName);

                var response = await httpClient.PostAsync(imgurApiEndpoint, form);
                var responseContent = await response.Content.ReadAsStringAsync();

                dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                string imageUrl = jsonResponse.data.link;

                return imageUrl;
            }
        }
    }


}