using Microsoft.EntityFrameworkCore;
using Team_5.Models.Auth;
using Team_5.Models.Clinic;
using Team_5.Models.Pharmacy;

namespace Team_5.Context
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<Breeds> Breeds { get; set; }
        public virtual DbSet<Examinations> Examinations { get; set; }
        public virtual DbSet<Hospitalizations> Hospitalizations { get; set; }
        public virtual DbSet<Owners> Owners { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Drawers> Drawers { get; set; }
        public virtual DbSet<Lockers> Lockers { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }



        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {
        }


    }
}
