using Microsoft.EntityFrameworkCore;

namespace DockFlow
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DocumentSample> DocumentSample => Set<DocumentSample>();
        public DbSet<Object> Object => Set<Object>();
        public DbSet<Parameter> Parameter => Set<Parameter>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseCollation("UTF-8");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DF_db12233.db");
        }
    }

    public class DocumentSample
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public byte[]? File { get; set; }
        public string? ValueParseDoc { get; set; }
    }

    public class Object
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class Parameter
    {
        public Guid Id { get; set; }
        public int ObjectId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public Object? Object { get; set; }
    }
}