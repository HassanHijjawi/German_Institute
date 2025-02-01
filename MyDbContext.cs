using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<YourEntity> YourEntities { get; set; } 

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseMySql("server=localhost;database=german_institute;uid=root;password=root;port=3306",
    //            new MySqlServerVersion(new Version(8, 0, 30)));  
    //    }
    //}
}

