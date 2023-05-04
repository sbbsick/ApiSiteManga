using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TesteApi.Models;

namespace TesteApi.Context;

public class MyContext : IdentityDbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }

    public DbSet<Manga>? Mangas { get; set; }
    public DbSet<Chapter>? Chapters { get; set; }
    public DbSet<Page>? Pages { get; set; }
    public DbSet<Genre>? Genres { get; set; }
}