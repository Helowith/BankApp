using System;
using System.Collections.Generic;

#nullable disable

namespace dBankAPIMySQL.Models
{
    public partial class Uzytkownik
    {
        public Uzytkownik()
        {
            Rachuneks = new HashSet<Rachunek>();
        }

        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string MiejsceUr { get; set; }
        public DateTime DataUr { get; set; }
        public string Pesel { get; set; }
        public string NrTel { get; set; }
        public string Email { get; set; }
        public string NrDokumentuTozsamosci { get; set; }
        public DateTime DataWaznosciDokumentuTozsamosci { get; set; }
        public string Haslo { get; set; }
        public bool CzyAktywne { get; set; }

        public virtual ICollection<Rachunek> Rachuneks { get; set; }
    }
}
