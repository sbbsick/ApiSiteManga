using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TesteApi.Models;

public class Chapter
{
    public Chapter()
    {
        Pages = new Collection<Page>();
    }

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(80, MinimumLength = 5, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int ChapterNumber { get; set; }

    //EF Relation
    public int MangaId { get; set; }
    public Manga? Manga { get; set; }

    public int PageId { get; set; }
    public ICollection<Page>? Pages { get; set; }
}