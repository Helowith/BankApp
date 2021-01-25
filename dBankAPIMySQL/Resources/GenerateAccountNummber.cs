using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Resources
{
    public class GenerateAccountNummber
    {
        public static string GetAccountNumber()
        {
            //PL
            var kodKraju = "2521";
            //Przypisany z konfiguracji
            var nrBanku = "12402760";

            //generowanie numeru rachunku bankowego
            Random random = new Random();
            string characters = "0123456789";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            result.ToString();
            var numerBezSumyKontrolnej = nrBanku + result + kodKraju;
            // Console.WriteLine(numerBezSumyKontrolnej);
            //algorytm 
            decimal number;

            number = decimal.Parse(numerBezSumyKontrolnej);
            var sk = number % 97;
            var wynik = sk - 98;
            var warBez = Math.Abs(wynik);
            var cyfraKontrolna = Decimal.ToInt32(warBez);

            var nrRachunku = cyfraKontrolna + nrBanku + result;
            if (cyfraKontrolna == 0)
            {
                cyfraKontrolna.ToString();
                nrRachunku = "00" + cyfraKontrolna + nrBanku + result + "cokolwiek";

            }
            else if (cyfraKontrolna <= 9)
            {
                cyfraKontrolna.ToString();
                nrRachunku = "0" + cyfraKontrolna + nrBanku + result;
            }
            else
            {
                cyfraKontrolna.ToString();
                nrRachunku = cyfraKontrolna + nrBanku + result;
            }

            return nrRachunku;
        }
    }
}
