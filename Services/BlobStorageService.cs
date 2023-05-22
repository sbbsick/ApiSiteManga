namespace TesteApi.Services;

using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

public class BlobStorageService
{
    readonly string _storageConnectionString =
        "DefaultEndpointsProtocol=https;AccountName=bucketimages;AccountKey=1+FMMoXWr6uFKxNlFyeHxlJ6Xep+0y81fLNAPd0RhpKWO1Boq7xyrFQVo5L2D2gCno7TuPztJCle+AStNk8s8Q==;EndpointSuffix=core.windows.net";

    public string Upload(IFormFile? blob, string mangaName, int chapterNumber)
    {
        if (blob is null)
            return "Blob vázio";

        var containerName = mangaName.ToLower().Replace(" ", "-") + chapterNumber;

        try
        {
            var container = new BlobContainerClient(_storageConnectionString, containerName);
            container.CreateIfNotExists();
            
            var blobClient = container.GetBlobClient(blob.FileName);
            using var stream = blob.OpenReadStream();
            blobClient.Upload(stream);

            var result = blobClient.Uri.AbsoluteUri;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Data);
            throw;
        }
    }
}

