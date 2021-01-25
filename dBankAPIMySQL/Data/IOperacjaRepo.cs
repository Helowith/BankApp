using dBankAPIMySQL.DTOs;
using dBankAPIMySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Data
{
    public interface IOperacjaRepo
    {
        OperacjaReadDTO AddFounds(OperacjaAddMoneyDTO operacjaAddMoneyDTO);
        List<Operacja> GetAllOperations(int id);
        void SaveChanges();
        OperacjaReadDTO MoneyTransfer(OperacjaMoneyTransferDTO operacjaMoneyTransferDTO);
        void GetNewPaymentsToAddToAccounts();

        //Uzytkownik GetUzytkownikByEmail(string email);
    }
}
