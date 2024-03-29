﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteApi.DTOs;
using TesteApi.Models;
using TesteApi.Repository;

namespace TesteApi.Controllers
{
    [Route("api/manga")]
    [ApiController]
    public class MangasController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public MangasController(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<Manga>>> GetAll()
        {
            var mangas = await _unit.MangaRepository
                .Get()
                .Include(m => m.Chapters)
                .Include(m => m.Genres)
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            if (mangas is null)
                return NotFound("A lista está vazia");

            return Ok(mangas);
        }

        [HttpGet("get-by-id/{id:int}", Name = "MangaById")]
        public async Task<ActionResult<Manga>> GetById(int id)
        {
            var manga = await _unit.MangaRepository
                .Get()
                .Include(m => m.Genres)
                .Include(m => m.Chapters)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (id == 0)
                return Empty;

            if (manga is null)
                return NotFound("O objeto não foi encontrado");

            return Ok(manga);
        }

        [HttpGet("get-by-name/{name}")]
        public async Task<ActionResult<Manga>> GetByName(string name)
        {
            var manga = await _unit.MangaRepository
                .Get()
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Name.ToLower().Contains(name.ToLower()));
            if (name == null)
                return Empty;
            if (manga is null)
                return NotFound("O objeto não foi encontrado");
            return Ok(manga);
        }

        
        [HttpPost("create-new-manga")]
        public async Task<ActionResult<Manga>> Create([FromForm] MangaDTO mangaDto, IFormFile mangaCover)
        {
            if (!ModelState.IsValid)
                return BadRequest("O manga não é válido");

            var manga = _mapper.Map<Manga>(mangaDto);

            if (mangaCover is not null)
                manga.CoverUrl = _unit.MangaRepository.ImgurImageUpload(mangaCover).Result;

            if (manga.GenresId != null && manga.Id == 0)
            {
                foreach (var ids in manga.GenresId)
                {
                    manga.Genres?.Add(await _unit.GenreRepository.GetById(g => g.Id == ids));
                    _unit.MangaRepository.Add(manga);
                }
            }

            _unit.MangaRepository.Add(manga);

            await _unit.Commit();

            return new CreatedAtRouteResult("MangaById", new { id = manga.Id }, mangaDto);
        }

        
        [HttpPut("update-manga/{id:int}")]
        public async Task<ActionResult> Update(int id, [FromForm] MangaDTO mangaDto, IFormFile? mangaCover)
        {
            if (id != mangaDto.Id)
                return BadRequest("Mangá não encontrado.");

            var manga = _mapper.Map<Manga>(mangaDto);

            if (mangaCover is null)
                return BadRequest("Por favor, adicione uma capa");
            
            manga.CoverUrl = _unit.MangaRepository.ImgurImageUpload(mangaCover).Result;
            
            if (manga.GenresId != null && manga.Id != 0) //****// != 0
            {
                foreach (var ids in manga.GenresId)
                {
                    manga.Genres?.Remove(await _unit.GenreRepository.GetById(g => g.Id == ids));
                    manga.Genres?.Add(await _unit.GenreRepository.GetById(g => g.Id == ids));
                    _unit.MangaRepository.Update(manga);
                }
            }

            _unit.MangaRepository.Update(manga);
            await _unit.Commit();
            return Ok(manga);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("remove-manga/{id:int}")]
        public async Task<ActionResult> Remove(int id)
        {
            var manga = await _unit.MangaRepository
                .Get()
                .Include(m => m.Chapters)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (manga is null)
                return NotFound("O manga não foi encontrado");

            _unit.MangaRepository.Remove(manga);

            await _unit.Commit();

            return Ok(new { message = "Manga deletado com sucesso!" });
        }

    }
}
