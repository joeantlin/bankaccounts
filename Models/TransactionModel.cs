using System;
using System.ComponentModel.DataAnnotations;


namespace BankAccounts.Models
{
    public class Transaction
    {
        [Key]
        public int Id {get;set;}
        public float Amount {get;set;}
        public int UserId {get;set;}
        public User UserTransaction {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}