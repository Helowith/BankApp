using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace dBankAPIMySQL.Resources
{
    public class AllPayments
    {
        public string BankNo { get; set; }
        public double PaymentSum { get; set; }
        public Payment[] Payments { get; set; }
    }
}



 
