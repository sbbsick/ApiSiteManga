﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteApi.DTOs;
using TesteApi.Models;
using TesteApi.Repository;

namespace TesteApi.Controllers;

[Route("api/chapter")]
[ApiController]
public class ChaptersController : ControllerBase
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;

    public ChaptersController(IUnitOfWork unit, IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<ChapterDTO>>> GetChapters()
    {
        var chapters = await _unit.ChapterRepository
            .Get()
            .Include(c => c.Manga)
            .Include(c => c.Pages)
            .ToListAsync();

        if (chapters is null)
            return BadRequest(new { message = "Manga sem capítulos." });

        return Ok(chapters);
    }

    [HttpGet("get-chapters-by-mangaId/{id:int}")]
    public async Task<ActionResult<IEnumerable<Chapter>>> GetChaptersByManga(int id)
    {
        var chapters = await _unit.ChapterRepository
            .Get()
            .Where(c => c.MangaId == id)
            .Include(c => c.Pages)
            .ToListAsync();

        if (chapters is null)
            return BadRequest(new { message = "Manga sem capítulos." });

        return Ok(chapters);
    }

    [HttpGet("get-by-id/{id:int}", Name = "ById")]
    public async Task<ActionResult<Chapter>> GetChapter(int id)
    {
        var chapter = await _unit.ChapterRepository
            .Get()
            .Where(c => c.Id == id)
            .Include(c => c.Pages)
            .FirstOrDefaultAsync();

        if (chapter is null)
            return BadRequest(new { message = "Capítulo não encontrado." });

        return Ok(chapter);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("create-new-chapter")]
    public async Task<ActionResult<ChapterDTO>> PostChapter([FromForm] ChapterDTO chapterDto, List<IFormFile> files)
    {
        var chapter = _mapper.Map<Chapter>(chapterDto);

        _unit.ChapterRepository.Add(chapter);

        var manga = await _unit.MangaRepository.GetById(m => m.Id == chapter.MangaId);

        if (manga is null) return BadRequest(new { message = "Manga não encontrado." });
        if (chapter is null) return BadRequest(new { message = "Capítulo não encontrado." });

        //_unit.PageRepository?.ReadFilesAndCreatePages(files, chapter.ChapterNumber.ToString(), chapter,
        // manga.Name);

        _unit.PageRepository.CreatePages(files, chapter, manga.Name);

        manga.Chapters?.Add(chapter);

        await _unit.Commit();

        chapterDto = _mapper.Map<ChapterDTO>(chapter);

        return new CreatedAtRouteResult("ById", new { id = chapter.Id }, chapterDto);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("update-chapter/{id:int}")]
    public async Task<ActionResult> UpdateChapter(int id, [FromForm] ChapterDTO chapterDto)
    {
        if (id != chapterDto.Id)
            return BadRequest(new { message = "Capítulo inválido." });

        var chapter = _mapper.Map<Chapter>(chapterDto);

        if (!ModelState.IsValid)
            return BadRequest(new { message = "Capítulo inválido." });

        _unit.ChapterRepository.Update(chapter);

        await _unit.Commit();

        return Ok(new { message = "Capítulo atualizado com sucesso." });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("update-pages/{chapterNumber:int}")]
    public async Task<ActionResult> UpdatePages(int chapterNumber, List<IFormFile> files)
    {

        var chapter = await _unit.ChapterRepository.GetById(c => c.ChapterNumber == chapterNumber);

        if (chapter is null)
            return BadRequest(new { message = "Capítulo não encontrado." });

        var manga = await _unit.MangaRepository.GetById(m => m.Id == chapter.MangaId);
        //_unit.PageRepository.ReplaceFiles(files, chapterNumber.ToString(), manga.Name, chapter);
        if (manga is not null)
            _unit.PageRepository.CreatePages(files, chapter, manga.Name);

        await _unit.Commit();

        return Ok(new { message = "Capítulo atualizado com sucesso." });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("remove-chapter/{mangaId:int}/{chapterNumber:int}")]
    public async Task<ActionResult<Chapter>> RemoveChapter(int mangaId, int chapterNumber)
    {
        var chapter = await _unit.ChapterRepository
            .Get()
            .Include(c => c.Pages)
            .FirstOrDefaultAsync(c => c.Manga.Id == mangaId);

        if (chapter is null)
            return BadRequest(new { message = "Capítulo não encontrado." });

        var manga = await _unit.MangaRepository.GetById(m => m.Id == chapter.MangaId);

        if (mangaId != manga.Id)
            return BadRequest(new { manga.Name });

        _unit.ChapterRepository.Remove(chapter);

        await _unit.Commit();

        return Ok(new { message = "Capítulo removido com sucesso." });
    }
}

//[HttpPost]
//public async Task<ActionResult<Chapter>> PostChapter([FromForm] ChapterDTO chapterDto)
//{
//    var chapter = _mapper.Map<Chapter>(chapterDto);

//    var fileResult = _unit.ChapterRepository.SaveImage(chapterDto.PagesUpload);

//    if (fileResult.Item1 == 1)
//        chapter.Pages = Encoding.ASCII.GetBytes(fileResult.Item2);

//    if (!ModelState.IsValid)
//        return BadRequest(new { message = "Capítulo inválido." });

//    _unit.ChapterRepository.Add(chapter);
//    await _unit.Commit();

//    var chapterDTO = _mapper.Map<ChapterDTO>(chapter);

//    return new CreatedAtRouteResult("Listt", new { id = chapter.Id }, chapterDTO);
//}