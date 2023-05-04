using TesteApi.Context;
using TesteApi.Models;

namespace TesteApi.Repository;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(MyContext context) : base(context)
    {

    }
}
