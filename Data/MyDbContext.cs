using HienTangToc.Models;
using Microsoft.EntityFrameworkCore;

namespace HienTangToc.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<NguoiMuonModel> NguoiMuon { get; set; }
        public DbSet<NguoiHienModel> NguoiHien { get; set; }
        public DbSet<SalonTocModel> SalonToc { get; set; }
        public DbSet<HTvsSalonModel> HTvsSalon { get; set; }
        public DbSet<MTvsSalonModel> MTvsSalon { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình mối quan hệ nhiều-nhiều giữa NguoiHienModel và SalonTocModel qua HTvsSalonModel
            modelBuilder.Entity<HTvsSalonModel>()
                .HasKey(ht => new { ht.idNH, ht.idSalon });

            modelBuilder.Entity<HTvsSalonModel>()
                .HasOne(ht => ht.NguoiHienModel)
                .WithMany(nh => nh.HTvsSalonModels)
                .HasForeignKey(ht => ht.idNH)
                .OnDelete(DeleteBehavior.Cascade); // Optional: specify the delete behavior

            modelBuilder.Entity<HTvsSalonModel>()
                .HasOne(ht => ht.SalonTocModel)
                .WithMany(st => st.HTvsSalonModels)
                .HasForeignKey(ht => ht.idSalon)
                .OnDelete(DeleteBehavior.Cascade); // Optional: specify the delete behavior

            // Cấu hình mối quan hệ nhiều-nhiều giữa NguoiMuonModel và SalonTocModel qua MTvsSalonModel
            modelBuilder.Entity<MTvsSalonModel>()
                .HasKey(mt => new { mt.idNM, mt.idSalon });

            modelBuilder.Entity<MTvsSalonModel>()
                .HasOne(mt => mt.NguoiMuonModel)
                .WithMany(nm => nm.MTvsSalonModels)
                .HasForeignKey(mt => mt.idNM)
                .OnDelete(DeleteBehavior.Cascade); // Optional: specify the delete behavior

            modelBuilder.Entity<MTvsSalonModel>()
                .HasOne(mt => mt.SalonTocModel)
                .WithMany(st => st.MTvsSalonModels)
                .HasForeignKey(mt => mt.idSalon)
                .OnDelete(DeleteBehavior.Cascade); // Optional: specify the delete behavior
        }
    }
}
