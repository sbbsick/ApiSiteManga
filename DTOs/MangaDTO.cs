using System.ComponentModel;
using TesteApi.Models;

namespace TesteApi.DTOs;

public class MangaDTO
{
    public int Id { get; set; }
    public string? Cover { get; set; }
    public string? Name { get; set; }
    public string? Author { get; set; }
    public string? Description { get; set; }

    public bool Status { get; set; }

    public DateTime Published { get; set; }
    public DateTime Updated { get; set; }
    public DateTime LastUpdated { get; set; }

    public int[]? GenresId { get; set; }
}