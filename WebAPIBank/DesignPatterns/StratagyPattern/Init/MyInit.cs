using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPIBank.Models.Context;
using WebAPIBank.Models.Entities;

namespace WebAPIBank.DesignPatterns.StratagyPattern.Init
{
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            CardInformation ci = new CardInformation();
            ci.CardUserName = "Burcu Ozcan";
            ci.CardNumber = "1111 1111 1111 1111";
            ci.CardExpiryYear = 2027;
            ci.CardExpiryMonth = 10;
            ci.SecurityNumber = "111";
            ci.Limit = 100000;
            ci.Balance = 100000;
            context.Cards.Add(ci);
            context.SaveChanges();
        }
    }
}