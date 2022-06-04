using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Elmah.EFCoreContext
{
    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
        {
        }

        public EFDbContext(DbContextOptions<EFDbContext> options)
            : base(options)
        {
        }

        public DbSet<ElmahError> ELMAH_Error { get; set; } = null!;

        public DbSet<ElmahApplication> ElmahApplication { get; set; } = null!;

        public DbSet<ElmahHost> ElmahHost { get; set; } = null!;

        public DbSet<ElmahSource> ElmahSource { get; set; } = null!;

        public DbSet<ElmahStatusCode> ElmahStatusCode { get; set; } = null!;

        public DbSet<ElmahType> ElmahType { get; set; } = null!;

        public DbSet<ElmahUser> ElmahUser { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=Localhost;Initial Catalog=Elmah;UID=sa;PWD=abcd1234;", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region #1.1 ElmahModel.ElmahError

            modelBuilder.Entity<ElmahError>(entity =>
            {

                entity.ToTable("ELMAH_Error", "dbo");

                entity.HasKey(e => new { e.ErrorId })
                    .HasName("PK_ElmahError_ErrorId");

                entity.Property(e => e.ErrorId)
                    .HasColumnName("ErrorId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Application)
                    .HasColumnName("Application")
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Host)
                    .HasColumnName("Host")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasColumnName("Type")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Source)
                    .HasColumnName("Source")
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Message)
                    .HasColumnName("Message")
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.User)
                    .HasColumnName("User")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StatusCode)
                    .HasColumnName("StatusCode");

                entity.Property(e => e.TimeUtc)
                    .HasColumnName("TimeUtc")
                    .HasColumnType("DateTime");

                entity.Property(e => e.Sequence)
                    .HasColumnName("Sequence")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AllXml)
                    .HasColumnName("AllXml")
                    .IsRequired();

                entity.HasOne(d => d.ElmahApplication)
                    .WithMany(p => p.ELMAH_Error)
                    .HasForeignKey(d => d.Application)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ElmahHost)
                    .WithMany(p => p.ELMAH_Error)
                    .HasForeignKey(d => d.Host)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ElmahSource)
                    .WithMany(p => p.ELMAH_Error)
                    .HasForeignKey(d => d.Source)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ElmahStatusCode)
                    .WithMany(p => p.ELMAH_Error)
                    .HasForeignKey(d => d.StatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ElmahType)
                    .WithMany(p => p.ELMAH_Error)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ElmahUser)
                    .WithMany(p => p.ELMAH_Error)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            #endregion #1.1 ElmahModel.ElmahError

            #region #1.2 ElmahModel.ElmahApplication

            modelBuilder.Entity<ElmahApplication>(entity =>
            {

                entity.ToTable("ElmahApplication", "dbo");

                entity.HasKey(e => new { e.Application })
                    .HasName("PK_ElmahApplication_Application");

                entity.Property(e => e.Application)
                    .HasColumnName("Application")
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasMaxLength(60);

            });

            #endregion #1.2 ElmahModel.ElmahApplication

            #region #1.3 ElmahModel.ElmahHost

            modelBuilder.Entity<ElmahHost>(entity =>
            {

                entity.ToTable("ElmahHost", "dbo");

                entity.Ignore(t=>t.SpatialLocation);

                entity.HasKey(e => new { e.Host })
                    .HasName("PK_ElmahHost_Host");

                entity.Property(e => e.Host)
                    .HasColumnName("Host")
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasMaxLength(50);

                entity.Property(e => e.SpatialLocation____)
                    .HasColumnName("SpatialLocation");

            });

            #endregion #1.3 ElmahModel.ElmahHost

            #region #1.4 ElmahModel.ElmahSource

            modelBuilder.Entity<ElmahSource>(entity =>
            {

                entity.ToTable("ElmahSource", "dbo");

                entity.HasKey(e => new { e.Source })
                    .HasName("PK_ElmahSource_Source");

                entity.Property(e => e.Source)
                    .HasColumnName("Source")
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasMaxLength(60);

            });

            #endregion #1.4 ElmahModel.ElmahSource

            #region #1.5 ElmahModel.ElmahStatusCode

            modelBuilder.Entity<ElmahStatusCode>(entity =>
            {

                entity.ToTable("ElmahStatusCode", "dbo");

                entity.HasKey(e => new { e.StatusCode })
                    .HasName("PK_ElmahStatusCode_StatusCode");

                entity.Property(e => e.StatusCode)
                    .HasColumnName("StatusCode")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(50);

            });

            #endregion #1.5 ElmahModel.ElmahStatusCode

            #region #1.6 ElmahModel.ElmahType

            modelBuilder.Entity<ElmahType>(entity =>
            {

                entity.ToTable("ElmahType", "dbo");

                entity.HasKey(e => new { e.Type })
                    .HasName("PK_ElmahType_Type");

                entity.Property(e => e.Type)
                    .HasColumnName("Type")
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasMaxLength(100);

            });

            #endregion #1.6 ElmahModel.ElmahType

            #region #1.7 ElmahModel.ElmahUser

            modelBuilder.Entity<ElmahUser>(entity =>
            {

                entity.ToTable("ElmahUser", "dbo");

                entity.HasKey(e => new { e.User })
                    .HasName("PK_ElmahUser_User");

                entity.Property(e => e.User)
                    .HasColumnName("User")
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasMaxLength(50);

            });

            #endregion #1.7 ElmahModel.ElmahUser

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}

