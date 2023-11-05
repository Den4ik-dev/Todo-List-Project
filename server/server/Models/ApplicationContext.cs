using Microsoft.EntityFrameworkCore;


namespace server.Models;

public class ApplicationContext : DbContext
{
  public DbSet<User> Users => Set<User>();
  public DbSet<Todo> Todoes => Set<Todo>();

  public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
  {
    Database.EnsureCreated();
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // users
    modelBuilder.Entity<User>().ToTable("users");
    modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("user_id");
    modelBuilder.Entity<User>().Property(u => u.Login).HasColumnName("login");
    modelBuilder.Entity<User>().Property(u => u.Password).HasColumnName("password");

    // todoes
    modelBuilder.Entity<Todo>().ToTable("todoes");
    modelBuilder.Entity<Todo>().Property(t => t.Id).HasColumnName("todo_id");
    modelBuilder.Entity<Todo>().Property(t => t.Name).HasColumnName("name");
    modelBuilder.Entity<Todo>().Property(t => t.Complete).HasColumnName("complete");
    modelBuilder.Entity<Todo>().Property(t => t.UserId).HasColumnName("user_id");
  }
}