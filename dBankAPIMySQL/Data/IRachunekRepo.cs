using dBankAPIMySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Data
{
    public interface IRachunekRepo
    {
        void CreateAccount(Rachunek rachunek);
        Rachunek GetAllUserAccounts(string email);
        void SaveChanges();
        Uzytkownik GetUserByEmail(string email);

    }
}
