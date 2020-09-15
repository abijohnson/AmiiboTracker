namespace AmiiboTracker.DAL

{
    using System;
    using System.Data.Entity;
    using AmiiboTracker.Models; 
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AmiiboContext : DbContext
    {
        public AmiiboContext()
            : base("name=AmiiboContext")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Amiibo> Amiiboes { get; set; }
        public virtual DbSet<AmiiboUserBridge> AmiiboUserBridges { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<AmiiboContext>(null);

            modelBuilder.Entity<Amiibo>()
                .HasMany(e => e.AmiiboUserBridges)
                .WithOptional(e => e.Amiibo)
                .HasForeignKey(e => e.AmiiboID);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AmiiboUserBridges)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);
        }
    }
}
