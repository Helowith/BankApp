using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.DTOs
{
    public class OperacjaMoneyTransferDTO
    {
        public int IdRachNadawcy { get; set; }
        public string NrRachOdbiorcy { get; set; }
        public string NazwaIAdresOdbiorcy { get; set; }
        public string Tytul { get; set; }
        public double Kwota { get; set; }
    }
}
