namespace TesteApi.Repository;

public interface IUnitOfWork
{
    IMangaRepository MangaRepository { get; }
    IChapterRepository ChapterRepository { get; }
    IPageRepository PageRepository { get; }
    IGenreRepository GenreRepository { get; }
    Task Commit();
    void Dispose();
}