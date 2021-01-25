
using dBankAPIMySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Data
{
    public interface IUzytkownikRepo
    {
        Uzytkownik GetUserById(int id);
        Uzytkownik GetUserByEmailToLogin(string email);
        void CreateUser(Uzytkownik uzytkownik);
        void UpdateUser(Uzytkownik uzytkownik);
        void DeleteUser(Uzytkownik uzytkownik);
        IEnumerable<Object> GetUserByEmail(string email);
        void SaveChanges();
    }
}
