using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.DTOs
{
    public class UzytkownikCreateDTO
    {
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
    }
}
