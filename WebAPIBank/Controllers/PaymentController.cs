using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIBank.DesignPatterns.SingletonPattern;
using WebAPIBank.DTOClasses;
using WebAPIBank.Models.Context;
using WebAPIBank.Models.Entities;

namespace WebAPIBank.Controllers
{
    public class PaymentController : ApiController
    {
        MyContext _db;
        public PaymentController()
        {
            _db = DBTool.DBInstace;

        }

        //[HttpGet]
        //public List<PaymentDTO> GetAll()
        //{
        //    return _db.Cards.Select(x=>new PaymentDTO
        //    { 
        //        CardExpiryMonth=x.CardExpiryMonth,
        //        CardExpiryYear=x.CardExpiryYear,
        //        CardNumber=x.CardNumber,
        //        ID=x.ID,
        //        CardUserName=x.CardUserName,
        //        SecurityNumber=x.SecurityNumber
        //    }).ToList();

        //}

        private void SetBalance(PaymentDTO item, CardInformation ci)
        {

            ci.Balance -= item.ShoppingPrice;
            _db.SaveChanges();
        }

        [HttpPost]
        public IHttpActionResult ReceivePayment(PaymentDTO item)
        {
            CardInformation ci = _db.Cards.FirstOrDefault(x => x.CardNumber == x.CardNumber && x.SecurityNumber == item.SecurityNumber && x.CardUserName == item.CardUserName && x.CardExpiryYear == item.CardExpiryYear && x.CardExpiryMonth == item.CardExpiryMonth);
            if (ci != null)
            {
                if (ci.CardExpiryYear < DateTime.Now.Year)
                {
                    return BadRequest("Card Expired");
                }
                else if (ci.CardExpiryYear == DateTime.Now.Year)
                {
                    if (ci.CardExpiryMonth < item.CardExpiryMonth)
                    {
                        return BadRequest("Card Expired ");
                    }
                    if (ci.Balance >= item.ShoppingPrice)
                    {
                        SetBalance(item, ci);
                        return Ok();

                    }
                    else return BadRequest("Balance Exceeded");

                }

                if (ci.Balance>=item.ShoppingPrice)
                {
                    SetBalance(item,ci);
                    return Ok();

                }
                return BadRequest("Balance Exceeded");


            }
            return BadRequest("Card not found");

        }



    }
}
