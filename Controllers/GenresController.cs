using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteApi.Models;
using TesteApi.Repository;

namespace TesteApi.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GenresController(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public ActionResult GetAll()
        {
            var genres = _unit.GenreRepository
                .Get()
                .Include(g => g.Mangas);

            if (genres is null)
                return NotFound("A lista está vazia");

            return Ok(genres);
        }

        [HttpGet("get-by-id/{id:int}", Name = "GenreById")]
        public ActionResult GetById(int id)
        {
            var genre = _unit.GenreRepository.GetById(g => g.Id == id);

            if (genre is null)
                return NotFound("Gênero não encontrado");

            return Ok(genre);
        }

        [HttpPost("create-new-genre")]
        public async Task<ActionResult> CreateGenre([FromBody] Genre genre)
        {
            if (genre is null)
                return BadRequest("Gênero não pode ser nulo");

            _unit.GenreRepository.Add(genre);
            await _unit.Commit();

            return new CreatedAtRouteResult("GenreById", new { id = genre.Id }, genre);
        }
    }
}
