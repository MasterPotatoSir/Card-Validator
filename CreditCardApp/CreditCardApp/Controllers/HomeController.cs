using System;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using CredApp.Models;
using CreditCardValidator;
using System.Collections.Generic;

namespace CredApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }

        public IActionResult CredApp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetDetails()
        {

            try
            {

                CardDetails cmodel = new CardDetails();
                cmodel.CardNum = HttpContext.Request.Form["txtCardNum"].ToString();
                CreditCardDetector detector = new CreditCardDetector(cmodel.CardNum);
                cmodel.CardProv = detector.BrandName;

                if (detector.IsValid())
                {
                    int result = cmodel.SaveDetails();
                    if (result > 0)
                    {
                        ViewBag.Result = "Card Saved Saved Successfully";
                    }
                    else
                    {
                        ViewBag.Result = "Something Went Wrong";
                    }

                }
                else
                {

                    ViewBag.Result = "Invalid Card Number";
                }

            }
            catch (System.ArgumentException ex)
            {

                ViewBag.Result = ex.Message;
            }

            return View("Index");
        }

        [HttpGet]
        public ActionResult Details()
        {
            String connectionString = GetConString.ConString();
            String sql = "SELECT * FROM CredDetails"; ;

            var cmodel = new CardModel();
            cmodel.CardDetailsList = new List<CardDetails>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var cardDets = new CardDetails();
                    cardDets.CardNum = rdr["CardNum"].ToString();
                    cardDets.CardProv = rdr["CardProv"].ToString();
                    cmodel.CardDetailsList.Add(cardDets);
                }

            }

            return View(cmodel);
        }
    }
}