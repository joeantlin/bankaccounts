using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class AccountView
    {
        public string FirstName {get;set;}
        public int Balance {get;set;}
        public List<Transaction> UserTransactions {get;set;}
        public Transaction NewTransaction {get;set;}
    }
}
