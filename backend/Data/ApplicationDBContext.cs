using Backend.Model;
using Microsoft.EntityFrameworkCore;
namespace Backend.Data;

public class ApplictaionDBContext : DbContext{
    public ApplictaionDBContext(DbContextOptions<ApplictaionDBContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    
}