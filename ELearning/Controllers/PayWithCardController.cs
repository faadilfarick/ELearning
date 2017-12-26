using PayPal.Api;
using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using System.Data.Entity;
using ELearning;
using Microsoft.AspNet.Identity;
using ELearning.DAL;

namespace ELearning.Controllers
{
    [Authorize]
    public class PayWithCardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: PayWithCard
        public ActionResult Index( int? Cid)
        {
            //TempData["Name"] = course.Name;
            //TempData["City"] = course.Name;

            if(Cid != null)
            {
                var course = db.Courses.Find(Cid);
                var pamentInfo = new Paymentinfo();
                pamentInfo.CourseID = course.ID;
                pamentInfo.ItemPrice = course.Price;
                pamentInfo.ItemName = course.Name;
                ViewBag.courseInfo = course;
                return View(pamentInfo);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        //[Bind(Include = "ID,ItemName,ItemPrice,ItemQuantity,cvv,month,year,fname,lname,cardnumber,cardtype,fee,Subtotal,Total,Shipping,Tax")]
        public ActionResult PaymentWithCreditCard(Paymentinfo paymentinfo)
        {
            //Course course = new Course();
            if (paymentinfo != null)
            {
                Item item = new Item();
                item.name = paymentinfo.ItemName;
                item.currency = "USD";
                //if (course != null)
                //{
                    //item.price = "20";
                //}
                //else
                //{
                    item.price = paymentinfo.ItemPrice.ToString();
                //}
                item.quantity = "1";
                item.sku = "sku";


                List<Item> itms = new List<Item>();
                itms.Add(item);
                ItemList itemList = new ItemList();
                itemList.items = itms;


                Address billingAddress = new Address();
                billingAddress.city = "NewYork";
                billingAddress.country_code = "US";
                billingAddress.line1 = "23rd street kew gardens";
                billingAddress.postal_code = "43210";
                billingAddress.state = "NY";



                CreditCard crdtCard = new CreditCard();
                crdtCard.billing_address = billingAddress;
                crdtCard.cvv2 = paymentinfo.cvv.ToString();  // CVV here
                crdtCard.expire_month = paymentinfo.month;
                crdtCard.expire_year = paymentinfo.year;
                crdtCard.first_name = paymentinfo.fname;
                crdtCard.last_name = paymentinfo.lname;
                crdtCard.number = paymentinfo.cardnumber.ToString(); //Card Number Here
                crdtCard.type = paymentinfo.cardtype;


                Details details = new Details();
                details.shipping = "0";
                details.subtotal = item.price;
                details.tax = "0";

                Amount amnt = new Amount();
                amnt.currency = "USD";

                amnt.total = details.subtotal;
                amnt.details = details;


                Transaction tran = new Transaction();
                tran.amount = amnt;
                tran.description = "Description about the payment amount.";
                tran.item_list = itemList;
                tran.invoice_number = User.Identity.Name + DateTime.Now.Date + DateTime.Now.TimeOfDay;



                List<Transaction> transactions = new List<Transaction>();
                transactions.Add(tran);



                FundingInstrument fundInstrument = new FundingInstrument();
                fundInstrument.credit_card = crdtCard;



                List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
                fundingInstrumentList.Add(fundInstrument);


                Payer payr = new Payer();
                payr.funding_instruments = fundingInstrumentList;
                payr.payment_method = "credit_card";


                Payment pymnt = new Payment();
                pymnt.intent = "sale";
                pymnt.payer = payr;
                pymnt.transactions = transactions;

                try
                {
                    APIContext apiContext = PaypalConfiguration.GetAPIContext();


                    Payment createdPayment = pymnt.Create(apiContext);


                    if (createdPayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                    else
                    {
                        var userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                        string query = "addToPurchasedCourse '" + userID + "','" + paymentinfo.CourseID + "'";
                        bool res = new SystemDAL().executeNonQuerys(query);

                        return RedirectToAction("details", "Courses", new { id = paymentinfo.CourseID });
                        //return View("Success");
                    }
                }
                catch (PayPal.PayPalException ex)
                {
                    Logger.Log("Error: " + ex.Message);
                    return View("Failure");
                }
                //if (TempData != null)
                //{
                //    course.Name = TempData["Name"].ToString();
                //    course.Name = TempData["City"].ToString();
                //}


                //specify here anything should saved in the db
                //if (business != null)
                //{
                //    if (ModelState.IsValid)
                //    {
                //        db.Businesses.Add(business);
                //        db.SaveChanges();
                //        return RedirectToAction("Index", "Businesses");
                //    }

                //    return View(business);
                //}


            }
            else
            {
                return View(paymentinfo);
            }

        }
    }
}