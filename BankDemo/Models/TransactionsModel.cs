using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AngAsp.Models
{


    public partial class TransactionModel
    {

        public int Transactions_Id { get; set; }
        public DateTime Date { get; set; }

        public decimal? Debit { get; set; }

        public decimal? Credit { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

    }
}
