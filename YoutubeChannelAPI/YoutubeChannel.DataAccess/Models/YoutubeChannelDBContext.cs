using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace YoutubeChannel.DataAccess.Models
{
    public partial class YoutubeChannelDBContext : DbContext
    {
        public YoutubeChannelDBContext()
        {
        }

        public YoutubeChannelDBContext(DbContextOptions<YoutubeChannelDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Video> Videos { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                
               
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin", "YT");

                entity.HasIndex(e => e.Email, "Unique_admin_email")
                    .IsUnique();

                entity.Property(e => e.AdminId).HasColumnName("adminId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("lastName");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Logs", "YT");

                entity.Property(e => e.LogId).HasColumnName("logId");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("action");

                entity.Property(e => e.LogDate)
                    .HasColumnType("datetime")
                    .HasColumnName("logDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("videos", "YT");

                entity.Property(e => e.VideoId)
                    .ValueGeneratedNever()
                    .HasColumnName("videoId");

                entity.Property(e => e.Media)
                    .IsRequired()
                    .HasColumnName("media");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("title");

                entity.Property(e => e.UploadDate)
                    .HasColumnType("datetime")
                    .HasColumnName("uploadDate")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
