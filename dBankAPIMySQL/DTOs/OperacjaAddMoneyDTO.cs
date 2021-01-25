using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.DTOs
{
    public class OperacjaAddMoneyDTO
    {
        public int IdRachOdbiorcy { get; set; }
        public double Kwota { get; set; }
    }
}
