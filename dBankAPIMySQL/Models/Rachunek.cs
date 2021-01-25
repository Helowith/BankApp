using System;
using System.Collections.Generic;

#nullable disable

namespace dBankAPIMySQL.Models
{
    public partial class Rachunek
    {
        public Rachunek()
        {
            OperacjaIdRachNadawcyNavigations = new HashSet<Operacja>();
            OperacjaIdRachOdbiorcyNavigations = new HashSet<Operacja>();
        }

        public int Id { get; set; }
        public int IdUzytkownika { get; set; }
        public string Waluta { get; set; }
        public double Saldo { get; set; }
        public string NrRachunku { get; set; }

        public virtual Uzytkownik IdUzytkownikaNavigation { get; set; }
        public virtual ICollection<Operacja> OperacjaIdRachNadawcyNavigations { get; set; }
        public virtual ICollection<Operacja> OperacjaIdRachOdbiorcyNavigations { get; set; }
    }
}
