using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Resources
{
    
        public class Payment
        {
            public string DebitedAccountNumber { get; set; }
            public string DebitedNameAndAddress { get; set; }
            public string CreditedAccountNumber { get; set; }
            public string CreditedNameAndAddress { get; set; }
            public string Title { get; set; }
            public double Amount { get; set; }
        }
    
}
