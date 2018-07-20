using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Calculator> Calculators { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<Item> Items { get; set; }    
        public DbSet<ItemPropertyName> ItemPropertyNames { get; set; }     
        public DbSet<ItemPropertyDescription> ItemPropertyDescriptions { get; set; }     
        public DbSet<ItemTemplate> ItemTemplates { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Project> Projects { get; set; }         
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }  
        public DbSet<Value> Values { get; set; }
        public DbSet<TemplateProperty> TemplateProperties { get; set; }              

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<TemplateProperty>()
                .HasKey(t => new { t.TemplateId, t.PropertyId });    

            modelBuilder.Entity<TemplateProperty>()
                .HasOne(t => t.Template)
                .WithMany(tp => tp.TemplateProperties)
                .HasForeignKey(ti => ti.TemplateId);
            
            modelBuilder.Entity<TemplateProperty>()
                .HasOne(t => t.Property)
                .WithMany(p => p.TemplateProperties)
                .HasForeignKey(pi => pi.PropertyId);
        }
    }
}