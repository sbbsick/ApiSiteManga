using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteApi.Models;

public class Manga
{
    public Manga()
    {
        Chapters = new Collection<Chapter>();
        Genres = new Collection<Genre>();
    }

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(80, MinimumLength = 1, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(80, MinimumLength = 1, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
    public string? Author { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
    public string? Description { get; set; }

    public bool Status { get; set; }

    public DateTime Published { get; set; }
    public DateTime Updated { get; set; }
    public DateTime LastUpdated { get; set; }

    public string? CoverUrl { get; set; }
    public ICollection<Chapter>? Chapters { get; set; }
    public ICollection<Genre>? Genres { get; set; }

    [NotMapped]
    public int[] GenresId { get; set; }
}