using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Identity;
using Shop.Domain.Entities.Metadata;

namespace Shop.Persistence;
public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
     => builder.ApplyConfigurationsFromAssembly(AssemblyReference.assembly);

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Village> Villages { get; set; }
    public DbSet<Ward> Ward { get; set; }
    public DbSet<Config> Config { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentDetail> PaymentDetails { get; set; }
    public DbSet<PaymentHistory> PaymentHistories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PaymentSummary> PaymentSummaris { get; set; }
}
