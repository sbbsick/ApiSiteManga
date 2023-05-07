using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TesteApi.DTOs;

namespace TesteApi.Models;

public class Page
{
    [Key]
    public int Id { get; set; }

    //[NotMapped]
    //public List<IFormFile>? Files { get; set; }

    public string? PageUrl { get; set; }

    public int ChapterId { get; set; }

    public Chapter? Chapter { get; set; }
}