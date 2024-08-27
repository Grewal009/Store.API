using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Store.API.Entities;

namespace Store.API.Data;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();
}
