using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BankAccounts.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Register", "Login");
            }
            return RedirectToAction("Account");
        }

        [Route("account")]
        [HttpGet]
        public IActionResult Account()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Register", "Login");
            }
            int? userID = HttpContext.Session.GetInt32("UserID");
            User ThisUser = dbContext.Users
                .Include(i => i.UserTransactions)
                .FirstOrDefault(i => i.Id == userID);
            ViewBag.UserInfo = ThisUser;

            float total = 0;
            foreach(Transaction j in ThisUser.UserTransactions)
            {
                total += j.Amount;
            };
            ViewBag.Total = total;
            return View();
        }

        [Route("money")]
        [HttpPost]
        public IActionResult Money(Transaction money)
        {
            float f = money.Amount;
            float truncated = (float)(Math.Truncate((double)f*100.0) / 100.0);
            money.Amount = (float)(Math.Round((double)f, 2));

            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int userID = IntVariable ?? default(int);
            float total = 0;
            User ThisUser = dbContext.Users
                .Include(i => i.UserTransactions)
                .FirstOrDefault(i => i.Id == userID);
            foreach(Transaction j in ThisUser.UserTransactions)
            {
                total += j.Amount;
            };
            if(total + money.Amount < 0)
            {
                ModelState.AddModelError("Amount", "You're too poor to withdraw that much!");
                ViewBag.UserInfo = ThisUser;
                ViewBag.Total = total;
                return View("Account");
            };
            
            money.UserId = userID;
            dbContext.Transactions.Add(money);
            dbContext.SaveChanges();
            return RedirectToAction("Account");
        }

    }
}
