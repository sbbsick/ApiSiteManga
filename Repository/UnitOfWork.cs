using Microsoft.Extensions.Hosting;
using System;
using TesteApi.Context;
using TesteApi.Services;

namespace TesteApi.Repository;

public class UnitOfWork : IUnitOfWork
{
    private IMangaRepository _mangaRepository;
    private IChapterRepository _chapterRepository;
    private IPageRepository _pageRepository;
    private IGenreRepository _genreRepository;

    private readonly IWebHostEnvironment _environment;
    private readonly MyContext _context;

    public UnitOfWork(MyContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public IMangaRepository MangaRepository
    {
        get
        {
            _mangaRepository ??= new MangaRepository(_context, _environment);
            return _mangaRepository;
        }
    }

    public IChapterRepository ChapterRepository
    {
        get
        {
            _chapterRepository ??= new ChapterRepository(_context, _environment);
            return _chapterRepository;
        }
    }

    public IPageRepository PageRepository
    {
        get
        {
            _pageRepository ??= new PageRepository(_context, _environment);
            return _pageRepository;
        }
    }

    public IGenreRepository GenreRepository
    {
        get
        {
            _genreRepository ??= new GenreRepository(_context);
            return _genreRepository;
        }
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}