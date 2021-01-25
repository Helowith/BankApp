using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.DTOs
{
    public class RachunekReadDTO
    {
        public int Id { get; set; }
        public int IdUzytkownika { get; set; }
        public string Waluta { get; set; }
        public double Saldo { get; set; }
        public string NrRachunku { get; set; }
    }
}
