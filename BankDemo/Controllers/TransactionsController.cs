﻿using BankDemo.DataBase;
using BankDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BankDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbcontext;
        public TransactionsController(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _configuration = configuration;
            _applicationDbcontext = applicationDbContext;
        }

        //POST : /api/Transactions
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> Transactions()
        {
            var transactionModels = await _applicationDbcontext.TransactionModels.ToListAsync();       
            return transactionModels;
        }

        //POST : /api/Transactions/PieChart1
        [HttpGet("PieChart1")]
        [Authorize(Roles = "Administrator")]
        public JsonResult PieChart1()
        {
            string query = @"
                select sender, sum(debit) as sumDebit
                from transactions group by sender
                order by SumDebit desc;";
            
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        //POST : /api/Transactions/PieChart2
        [HttpGet("PieChart2")]
        [Authorize(Roles = "Administrator")]    
        public JsonResult PieChart2()
        {
            string query = @"
                select sender, sum(credit) as sumcredit
                from transactions group by sender
                order by SumCredit desc;";
            
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        //POST : /api/Transactions
        [HttpGet("{Transactions_Id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<TransactionModel>> GetTransactions(int transactions_id)
        {
            var transactionModel = await _applicationDbcontext.TransactionModels.FindAsync(transactions_id);

            if (transactionModel == null)
            {
                return NotFound();
            }

            return transactionModel;
        }

        // DELETE: api/Transactions/
        [HttpDelete("{Transactions_Id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<TransactionModel>> DeleteTransaction(int transactions_id)
        {
            var transactionModel = await _applicationDbcontext.TransactionModels.FindAsync(transactions_id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            _applicationDbcontext.TransactionModels.Remove(transactionModel);
            await _applicationDbcontext.SaveChangesAsync();

            return transactionModel;
        }

        // PUT: api/Transactions/
        [HttpPut("{Transactions_Id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> PutUser(int transactions_id, TransactionModel model)
        {
            if (transactions_id != model.Transactions_Id)
            {
                return BadRequest();
            }

            var transactionModel = await _applicationDbcontext.TransactionModels.FindAsync(transactions_id);
            if (transactionModel == null)
            {
                return NotFound();
            }
            _applicationDbcontext.Entry(transactionModel).State = EntityState.Modified;

            return NoContent();
        }
    }
}


