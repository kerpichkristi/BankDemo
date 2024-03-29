﻿using System;

#nullable disable

namespace BankDemo.Models
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
