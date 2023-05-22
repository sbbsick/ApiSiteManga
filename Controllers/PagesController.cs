using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteApi.Repository;

namespace TesteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IUnitOfWork _unit;

        public PagesController(IUnitOfWork unit)
        {
            _unit = unit;
        }


        [HttpGet("get-pages-by-chapter/{chapterId:int}")]
        public async Task<ActionResult> GetPagesByChapter(int chapterId)
        {
            //var chapter = _unit.ChapterRepository
            //    .Get()
            //    .Where(c => c.Id == chapterId)
            //    .Include(c => c.Pages)
            //    .Include(c => c.Manga)
            //    .Select(c => c.Manga.Name);

            //if (chapter is null)
            //    return BadRequest(new { message = "Capítulo não encontrado." });

            //return Ok(chapter);

            var pages = await _unit.PageRepository
                .Get()
                .Where(p => p.ChapterId == chapterId)
                .ToListAsync();

            if (pages.Count < 1) 
                return NotFound(new {message = "Páginas não encontradas."});

            return Ok(pages);
        }
    }


}
