using AutoMapper;
using dBankAPIMySQL.DTOs;
using dBankAPIMySQL.Models;
using dBankAPIMySQL.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Data
{
    public class SqlOperacjaRepo : IOperacjaRepo
    {
        private bankContext _context;
        private IMapper _mapper;

        public SqlOperacjaRepo(bankContext bankContext, IMapper mapper)
        {
            _context = bankContext;
            _mapper = mapper;
        }
        public OperacjaReadDTO AddFounds(OperacjaAddMoneyDTO operacjaAddMoneyDTO)
        {
            var bankAccount = _context.Rachuneks.Where(p => p.Id == 0).FirstOrDefault();
            var userAccount = _context.Rachuneks.Where(p => p.Id == operacjaAddMoneyDTO.IdRachOdbiorcy).FirstOrDefault();
            bankAccount.Saldo += operacjaAddMoneyDTO.Kwota;
            userAccount.Saldo += operacjaAddMoneyDTO.Kwota;
            var operacjaBank = new OperacjaCreateDTO
            {
                IdRachOdbiorcy = bankAccount.Id,
                NrRachOdbiorcy = bankAccount.NrRachunku,
                NazwaIAdresOdbiorcy = "-",
                NrRachNadawcy = "-",
                NazwaIAdresNadawcy = "-",
                Tytul = "-",
                Kwota = operacjaAddMoneyDTO.Kwota,
                Data = DateTime.Now,
                TypOperacji = "Wpłata środków na konto",
                Status = "Zakończono"

            };
            var operacjaUser = new OperacjaCreateDTO
            {
                IdRachOdbiorcy = userAccount.Id,
                NrRachOdbiorcy = userAccount.NrRachunku,
                NazwaIAdresOdbiorcy = "-",
                NrRachNadawcy = "-",
                NazwaIAdresNadawcy = "-",
                Tytul = "-",
                Kwota = operacjaAddMoneyDTO.Kwota,
                Data = DateTime.Now,
                TypOperacji = "Wpłata środków na konto",
                Status = "Zakończono"

            };
            var operacjaUserAsOperacja = _mapper.Map<Operacja>(operacjaUser);
            _context.Operacjas.Add(operacjaUserAsOperacja);
            _context.Operacjas.Add(_mapper.Map<Operacja>(operacjaBank));
            _context.SaveChanges();
            var operationUserToSend = _mapper.Map<OperacjaReadDTO>(operacjaUserAsOperacja);
            return operationUserToSend;
        }

        public List<Operacja> GetAllOperations(int id)
        {
            var outgoingOperations = _context.Operacjas.Where(p => p.IdRachNadawcy == id).ToList();
            var incomingOperations = _context.Operacjas.Where(p => p.IdRachOdbiorcy == id).ToList();
            var data = new List<Operacja>();
            foreach(var list in outgoingOperations)
            {
                data.Add(list);
            }
            foreach (var list in incomingOperations)
            {
                data.Add(list);
            }
            return data;
        }

        public OperacjaReadDTO MoneyTransfer(OperacjaMoneyTransferDTO operacjaMoneyTransferDTO)
        {
            var userAccount = _context.Rachuneks.FirstOrDefault(p => p.Id == operacjaMoneyTransferDTO.IdRachNadawcy);
            var userAccountData = _context.Uzytkowniks.FirstOrDefault(p => p.Id == userAccount.IdUzytkownika);
            var reciver = _context.Rachuneks.FirstOrDefault(p => p.NrRachunku == operacjaMoneyTransferDTO.NrRachOdbiorcy);


            if (CheckNumber(operacjaMoneyTransferDTO.NrRachOdbiorcy))
            {
                
                if(reciver == null)
                {
                    var operation = new OperacjaCreateDTO
                    {
                        IdRachOdbiorcy = null,
                        NrRachOdbiorcy = operacjaMoneyTransferDTO.NrRachOdbiorcy,
                        NazwaIAdresOdbiorcy = operacjaMoneyTransferDTO.NazwaIAdresOdbiorcy,
                        IdRachNadawcy = userAccount.Id,
                        NrRachNadawcy = userAccount.NrRachunku,
                        NazwaIAdresNadawcy = $"{userAccountData.Imie} {userAccountData.Nazwisko}",
                        Tytul = operacjaMoneyTransferDTO.Tytul,
                        Kwota = operacjaMoneyTransferDTO.Kwota,
                        Data = DateTime.Now,
                        TypOperacji = "Przelew wewnętrzny",
                        Status = "Błąd! Nie znaleziono użytkownika o podanym numerze konta!"

                    };
                    var operationToSend = _mapper.Map<Operacja>(operation);
                    _context.Operacjas.Add(operationToSend);
                    _context.SaveChanges();
                    return _mapper.Map<OperacjaReadDTO>(operationToSend);
                }
                else
                {
                    var operation = new OperacjaCreateDTO
                    {
                        IdRachOdbiorcy = reciver.Id,
                        NrRachOdbiorcy = operacjaMoneyTransferDTO.NrRachOdbiorcy,
                        NazwaIAdresOdbiorcy = operacjaMoneyTransferDTO.NazwaIAdresOdbiorcy,
                        IdRachNadawcy = userAccount.Id,
                        NrRachNadawcy = userAccount.NrRachunku,
                        NazwaIAdresNadawcy = $"{userAccountData.Imie} {userAccountData.Nazwisko}",
                        Tytul = operacjaMoneyTransferDTO.Tytul,
                        Kwota = operacjaMoneyTransferDTO.Kwota,
                        Data = DateTime.Now,
                        TypOperacji = "Przelew wewnętrzny",
                        Status = "Zakończono"

                    };
                    userAccount.Saldo -= operacjaMoneyTransferDTO.Kwota;
                    reciver.Saldo += operacjaMoneyTransferDTO.Kwota;
                    var operationToSend = _mapper.Map<Operacja>(operation);
                    _context.Operacjas.Add(operationToSend);
                    _context.SaveChanges();
                    return _mapper.Map<OperacjaReadDTO>(operationToSend);
                }

            }
            else
            {
                
                var bankAccount = _context.Rachuneks.FirstOrDefault(p => p.Id == 0);
                var operation = new OperacjaCreateDTO
                {
                    IdRachOdbiorcy = null,
                    NrRachOdbiorcy = operacjaMoneyTransferDTO.NrRachOdbiorcy,
                    NazwaIAdresOdbiorcy = operacjaMoneyTransferDTO.NazwaIAdresOdbiorcy,
                    IdRachNadawcy = userAccount.Id,
                    NrRachNadawcy = userAccount.NrRachunku,
                    NazwaIAdresNadawcy = $"{userAccountData.Imie} {userAccountData.Nazwisko}",
                    Tytul = operacjaMoneyTransferDTO.Tytul,
                    Kwota = operacjaMoneyTransferDTO.Kwota,
                    Data = DateTime.Now,
                    TypOperacji = "Przelew zewnętrzny",
                    Status = "Wysłano"

                };
                var operationBank = new OperacjaCreateDTO
                {
                    IdRachOdbiorcy = null,
                    NrRachOdbiorcy = operacjaMoneyTransferDTO.NrRachOdbiorcy,
                    NazwaIAdresOdbiorcy = operacjaMoneyTransferDTO.NazwaIAdresOdbiorcy,
                    IdRachNadawcy = bankAccount.Id,
                    NrRachNadawcy = userAccount.NrRachunku,
                    NazwaIAdresNadawcy = $"{userAccountData.Imie} {userAccountData.Nazwisko}",
                    Tytul = operacjaMoneyTransferDTO.Tytul,
                    Kwota = operacjaMoneyTransferDTO.Kwota,
                    Data = DateTime.Now,
                    TypOperacji = "Przelew zewnętrzny",
                    Status = "Księgowanie bankowe"

                };

                userAccount.Saldo -= operacjaMoneyTransferDTO.Kwota;
                bankAccount.Saldo -= operacjaMoneyTransferDTO.Kwota;
                _context.Operacjas.Add(_mapper.Map<Operacja>(operationBank));
                var operationToSend = _mapper.Map<Operacja>(operation);
                _context.Operacjas.Add(operationToSend);
                _context.SaveChanges();
                SendPayment(operationToSend);
                return _mapper.Map<OperacjaReadDTO>(operationToSend);
            }
            
        }
        public void GetNewPayments(AllPayments allPayments)
        {
            foreach(var account in allPayments.Payments)
            {
                var userAccount = _context.Rachuneks.FirstOrDefault(p => p.NrRachunku == account.CreditedAccountNumber);
                var bankAccount = _context.Rachuneks.FirstOrDefault(p => p.Id == 0);
                
                if (userAccount == null)
                {
                    var operationBank = new OperacjaCreateDTO
                    {
                        IdRachOdbiorcy = bankAccount.Id,
                        NrRachOdbiorcy = bankAccount.NrRachunku,
                        NazwaIAdresOdbiorcy = "dBank",
                        IdRachNadawcy = null,
                        NrRachNadawcy = account.DebitedAccountNumber,
                        NazwaIAdresNadawcy = account.DebitedNameAndAddress,
                        Tytul = account.Title,
                        Kwota = account.Amount,
                        Data = DateTime.Now,
                        TypOperacji = "Przelew przychodzący",
                        Status = "Księgowanie bankowe - brak numeru konta"

                    };
                    _context.Operacjas.Add(_mapper.Map<Operacja>(operationBank));
                }
                else
                {
                    var userAccountData = _context.Uzytkowniks.FirstOrDefault(p => p.Id == userAccount.IdUzytkownika);
                    var operation = new OperacjaCreateDTO
                    {
                        IdRachOdbiorcy = userAccount.Id,
                        NrRachOdbiorcy = userAccount.NrRachunku,
                        NazwaIAdresOdbiorcy = $"{userAccountData.Imie} {userAccountData.Nazwisko}",
                        IdRachNadawcy = null,
                        NrRachNadawcy = account.DebitedAccountNumber,
                        NazwaIAdresNadawcy = account.DebitedNameAndAddress,
                        Tytul = account.Title,
                        Kwota = account.Amount,
                        Data = DateTime.Now,
                        TypOperacji = "Przelew Przychodzący",
                        Status = "Zakonczono"

                    };
                    var operationBank = new OperacjaCreateDTO
                    {
                        IdRachOdbiorcy = bankAccount.Id,
                        NrRachOdbiorcy = bankAccount.NrRachunku,
                        NazwaIAdresOdbiorcy = "dBank",
                        IdRachNadawcy = null,
                        NrRachNadawcy = account.DebitedAccountNumber,
                        NazwaIAdresNadawcy = account.DebitedNameAndAddress,
                        Tytul = account.Title,
                        Kwota = account.Amount,
                        Data = DateTime.Now,
                        TypOperacji = "Przelew przychodzący",
                        Status = "Księgowanie bankowe - powodzenie"

                    };
                    userAccount.Saldo += account.Amount;
                    bankAccount.Saldo += account.Amount;
                    _context.Operacjas.Add(_mapper.Map<Operacja>(operationBank));
                    _context.Operacjas.Add(_mapper.Map<Operacja>(operation));

                }
                _context.SaveChanges();
            }
        }
        public void GetNewPaymentsToAddToAccounts()
        {
            var getPayments = new GetPayments();
            GetNewPayments(getPayments.AllPayments);
        }
        public void SendPayment(Operacja operacja)
        {
            HttpClient client = new HttpClient();
            var newPayment = new AllPaymentsToSend();

            newPayment.BankNo = "26124027600000000000000000";
            newPayment.PaymentSum = operacja.Kwota;
            newPayment.Payments[0].DebitedAccountNumber = operacja.NrRachNadawcy;
            newPayment.Payments[0].DebitedNameAndAddress = operacja.NazwaIAdresNadawcy;
            newPayment.Payments[0].CreditedAccountNumber = operacja.NrRachOdbiorcy;
            newPayment.Payments[0].CreditedNameAndAddress = operacja.NazwaIAdresOdbiorcy;
            newPayment.Payments[0].Title = operacja.Tytul;
            newPayment.Payments[0].Amount = operacja.Kwota;



            var json = JsonConvert.SerializeObject(newPayment);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://jrozliczajaca.herokuapp.com/przelewy", data);

            var responseString = response.Result.Content.ReadAsStringAsync().Result;
            var serializedPayments =  JsonConvert.DeserializeObject<AllPayments>(responseString);
            GetNewPayments(serializedPayments);

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        private bool CheckNumber(string number)
        {
            var bankNumber = "12402760";
            var transferAccountNumber = (string)default;
            for(int i = 2; i < 10; i++)
            {
                transferAccountNumber += number[i];
            }
            if(bankNumber == transferAccountNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
public Uzytkownik GetUzytkownikByEmail(string email)
{
   return _context.Uzytkowniks.FirstOrDefault(p => p.Email == email);
}
*/
    }
}
