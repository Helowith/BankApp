using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.DTOs
{
    public class OperacjaReadDTO
    {
        public int Id { get; set; }
        public int? IdRachOdbiorcy { get; set; }
        public string NrRachOdbiorcy { get; set; }
        public string NazwaIAdresOdbiorcy { get; set; }
        public int? IdRachNadawcy { get; set; }
        public string NrRachNadawcy { get; set; }
        public string NazwaIAdresNadawcy { get; set; }
        public string Tytul { get; set; }
        public double Kwota { get; set; }
        public DateTime Data { get; set; }
        public string TypOperacji { get; set; }
        public string Status { get; set; }
    }
}
