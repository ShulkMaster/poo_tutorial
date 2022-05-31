using Microsoft.EntityFrameworkCore;


namespace Pokedex
{
    public partial class PokedexContext : DbContext
    {
        public PokedexContext(DbContextOptions<PokedexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Entry> Entries { get; set; } = null!;
        public virtual DbSet<Pokemon> Pokemons { get; set; } = null!;
        public virtual DbSet<Sprite> Sprites { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>(entity =>
            {
                entity.ToTable("Entry");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(300);

                entity.Property(e => e.Url).HasMaxLength(500);
            });

            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.ToTable("Pokemon");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(300);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Pokemon)
                    .HasForeignKey<Pokemon>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pokemon_Entry");
            });

            modelBuilder.Entity<Sprite>(entity =>
            {
                entity.ToTable("Sprite");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FrontDefault)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Sprite)
                    .HasForeignKey<Sprite>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sprite_Pokemon");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
