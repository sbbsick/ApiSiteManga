using System.ComponentModel.DataAnnotations;
using TesteApi.Models;

namespace TesteApi.DTOs;

public class ChapterDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ChapterNumber { get; set; }
    public int MangaId { get; set; }
}