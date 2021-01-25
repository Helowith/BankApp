using dBankAPIMySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Data
{
    public class SqlRachunekRepo : IRachunekRepo
    {
        private bankContext _context;
        public SqlRachunekRepo(bankContext bankContext)
        {
            _context = bankContext;
        }
        public void CreateAccount(Rachunek rachunek)
        {
            _context.Rachuneks.Add(rachunek);
        }

        public Rachunek GetAllUserAccounts(string email)
        {
            throw new NotImplementedException();
        }

        public Uzytkownik GetUserByEmail(string email)
        {
            return _context.Uzytkowniks.FirstOrDefault(p => p.Email == email);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
