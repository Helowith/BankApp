using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace dBankAPIMySQL.Models
{
    public partial class bankContext : DbContext
    {

        public bankContext(DbContextOptions<bankContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Operacja> Operacjas { get; set; }
        public virtual DbSet<Rachunek> Rachuneks { get; set; }
        public virtual DbSet<Uzytkownik> Uzytkowniks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=bank;user=root;treattinyasboolean=true", Microsoft.EntityFrameworkCore.ServerVersion.FromString("10.4.14-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operacja>(entity =>
            {
                entity.ToTable("operacja");

                entity.HasIndex(e => e.IdRachNadawcy, "id_rach_nadawcy");

                entity.HasIndex(e => e.IdRachOdbiorcy, "id_rach_odbiorcy");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Data)
                    .HasColumnType("date")
                    .HasColumnName("data");

                entity.Property(e => e.IdRachNadawcy)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_rach_nadawcy");

                entity.Property(e => e.IdRachOdbiorcy)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_rach_odbiorcy");

                entity.Property(e => e.Kwota)
                    .HasColumnType("double(10,2)")
                    .HasColumnName("kwota");

                entity.Property(e => e.NazwaIAdresNadawcy)
                    .IsRequired()
                    .HasColumnType("varchar(252)")
                    .HasColumnName("nazwa_i_adres_nadawcy")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.NazwaIAdresOdbiorcy)
                    .IsRequired()
                    .HasColumnType("varchar(252)")
                    .HasColumnName("nazwa_i_adres_odbiorcy")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.NrRachNadawcy)
                    .IsRequired()
                    .HasColumnType("varchar(26)")
                    .HasColumnName("nr_rach_nadawcy")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.NrRachOdbiorcy)
                    .IsRequired()
                    .HasColumnType("varchar(26)")
                    .HasColumnName("nr_rach_odbiorcy")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasColumnName("status")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.TypOperacji)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasColumnName("typ_operacji")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.Tytul)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasColumnName("tytul")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.HasOne(d => d.IdRachNadawcyNavigation)
                    .WithMany(p => p.OperacjaIdRachNadawcyNavigations)
                    .HasForeignKey(d => d.IdRachNadawcy)
                    .HasConstraintName("operacja_ibfk_1");

                entity.HasOne(d => d.IdRachOdbiorcyNavigation)
                    .WithMany(p => p.OperacjaIdRachOdbiorcyNavigations)
                    .HasForeignKey(d => d.IdRachOdbiorcy)
                    .HasConstraintName("operacja_ibfk_2");
            });

            modelBuilder.Entity<Rachunek>(entity =>
            {
                entity.ToTable("rachunek");

                entity.HasIndex(e => e.IdUzytkownika, "id_uzytkownika");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdUzytkownika)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_uzytkownika");

                entity.Property(e => e.NrRachunku)
                    .IsRequired()
                    .HasColumnType("varchar(26)")
                    .HasColumnName("nr_rachunku")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.Saldo)
                    .HasColumnType("double(10,2)")
                    .HasColumnName("saldo");

                entity.Property(e => e.Waluta)
                    .IsRequired()
                    .HasColumnType("varchar(8)")
                    .HasColumnName("waluta")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.HasOne(d => d.IdUzytkownikaNavigation)
                    .WithMany(p => p.Rachuneks)
                    .HasForeignKey(d => d.IdUzytkownika)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rachunek_ibfk_1");
            });

            modelBuilder.Entity<Uzytkownik>(entity =>
            {
                entity.ToTable("uzytkownik");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CzyAktywne).HasColumnName("czy_aktywne");

                entity.Property(e => e.DataUr)
                    .HasColumnType("date")
                    .HasColumnName("data_ur");

                entity.Property(e => e.DataWaznosciDokumentuTozsamosci)
                    .HasColumnType("date")
                    .HasColumnName("data_waznosci_dokumentu_tozsamosci");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasColumnName("email")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.Haslo)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasColumnName("haslo")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasColumnName("imie")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");


                entity.Property(e => e.MiejsceUr)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasColumnName("miejsce_ur")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasColumnName("nazwisko")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.NrDokumentuTozsamosci)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasColumnName("nr_dokumentu_tozsamosci")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.NrTel)
                    .IsRequired()
                    .HasColumnType("varchar(9)")
                    .HasColumnName("nr_tel")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");

                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasColumnType("varchar(11)")
                    .HasColumnName("PESEL")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_polish_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
