using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TesteApi.Models;

public class Genre
{
    public Genre()
    {
        Mangas = new Collection<Manga>();
    }

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
    public string? Name { get; set; }

    public ICollection<Manga>? Mangas { get; set; }
}