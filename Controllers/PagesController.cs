using Microsoft.AspNetCore.Mvc;

namespace TesteApi.Controllers;

[Route("api/page")]
[ApiController]
public class PagesController : Controller
{
    [HttpGet("get-pages-by-chapter/{chapterId:int}")]
    public Task<IActionResult> GetPagesByChapter(int chapterId)
    {
        throw new NotImplementedException();
    }
}