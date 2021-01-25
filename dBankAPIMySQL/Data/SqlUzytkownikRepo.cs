using dBankAPIMySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Data
{
    public class SqlUzytkownikRepo : IUzytkownikRepo
    {
        private bankContext _context;
        public SqlUzytkownikRepo(bankContext bankContext)
        {
            _context = bankContext;
        }
        public void CreateUser(Uzytkownik uzytkownik)
        {
            _context.Uzytkowniks.Add(uzytkownik);
        }

        public void DeleteUser(Uzytkownik uzytkownik)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Object> GetUserByEmail(string email)
        {
            //return _context.Uzytkowniks.FirstOrDefault(p => p.Email == email);
            var data = _context.Uzytkowniks.Select(user => new
            {
                Id = user.Id,
                Imie = user.Imie,
                Nazwisko = user.Nazwisko,
                MiejsceUr = user.MiejsceUr,
                DataUr = user.DataUr,
                Pesel = user.Pesel,
                NrTel = user.NrTel,
                Email = user.Email,
                NrDokumentuTozsamosci = user.NrDokumentuTozsamosci,
                DataWaznosciDokumentuTozsamosci = user.DataWaznosciDokumentuTozsamosci,
                Haslo = user.Haslo,
                CzyAktywe = user.CzyAktywne,
                Rachuneks = user.Rachuneks.Select(rachunki => new
                {
                    Id = rachunki.Id,
                    IdUzytkownika = rachunki.IdUzytkownika,
                    Waluta = rachunki.Waluta,
                    Saldo = rachunki.Saldo,
                    NrRachunku = rachunki.NrRachunku
                })

            }).Where(p => p.Email == email).FirstOrDefault();
            yield return data;
        }

        public Uzytkownik GetUserByEmailToLogin(string email)
        {
            return _context.Uzytkowniks.FirstOrDefault(p => p.Email == email);
        }

        public Uzytkownik GetUserById(int id)
        {
            return _context.Uzytkowniks.FirstOrDefault(p => p.Id == id);
            
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdateUser(Uzytkownik uzytkownik)
        {
            throw new NotImplementedException();
        }
    }
}
