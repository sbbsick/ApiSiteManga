using System.ComponentModel.DataAnnotations;

namespace TesteApi.Models;

public class Page
{
    [Key]
    public int Id { get; set; }

    //[NotMapped]
    //public List<IFormFile>? Files { get; set; }
    public string? PageNumber { get; set; }

    public string? PageUrl { get; set; }

    public int ChapterId { get; set; }

    public Chapter? Chapter { get; set; }
}