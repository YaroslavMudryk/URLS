using Microsoft.EntityFrameworkCore;
using URLS.Data.Entities;

namespace URLS.Data;

public class UrlsContext(DbContextOptions<UrlsContext> options) : DbContext(options)
{
    public DbSet<Test> Tests { get; set; }
}
