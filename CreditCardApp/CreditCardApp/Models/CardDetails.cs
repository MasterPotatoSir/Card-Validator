using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Collections.Generic;
using CreditCardValidator;

namespace CredApp.Models
{

    public class CardModel
    {
        public List<CardDetails> CardDetailsList { get; set; }
       
    }

    public class CardDetails
    {
        [Required(ErrorMessage = "Please Enter Card Number")]
        [DisplayName("Potato")]
        public string CardNum { get; set; }
        public string CardProv { get; set; }

        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "INSERT INTO CredDetails(CardNum, CardProv) values ('" + CardNum + "','" + CardProv + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }




}

