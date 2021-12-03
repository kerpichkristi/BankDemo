using AngAsp.Models;
using BankDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public TransactionsController(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {

            _configuration = configuration;
            _context = applicationDbContext;
        }



        /*[HttpGet]
        [Authorize(Roles = "Administrator")]
        //POST : /api/Transactions

        public async Task<ActionResult<IEnumerable<TransactionModel>>> Transactions()
        {
            List<TransactionModel> TransactionModel = new List<TransactionModel>();
            List<TransactionModel> transactionModels = await _context.TransactionModels.ToListAsync();
            foreach (TransactionModel transactionModel in transactionModels)
            {
                TransactionModel.Add(new TransactionModel()
                {
                    Transactions_Id = transactionModel.Transactions_Id,
                    Date = transactionModel.Date,
                    Debit = transactionModel.Debit,
                    Credit = transactionModel.Credit,
                    Sender = transactionModel.Sender,
                    Receiver = transactionModel.Receiver
                });
            }
            return TransactionModel;
        }*/


        [HttpGet]
        //[Route("Select")]
        //GET : /api/Transactions
        public JsonResult Get()
        {
            string query = @"
                 select transactions_id as ""Transactions_id"",
                     sender as ""Sender"",
                     receiver as ""Receiver"",
                     debit as ""Debit"",
                     credit as ""Credit"",
                     date as ""Date""

                 from transactions";

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

        [HttpPost]
        //[Route("Insert")]
        //POST : /api/Transactions
        public JsonResult Post(TransactionModel model)
        {
            string query = @"
             insert into transactions(sender,receiver,debit,credit)
                     values (@sender,@receiver,@debit,@credit)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@sender", model.Sender);
                    myCommand.Parameters.AddWithValue("@receiver", model.Receiver);
                    myCommand.Parameters.AddWithValue("@debit", model.Debit);
                    myCommand.Parameters.AddWithValue("@credit", model.Credit);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Succesfully");
        }



        [HttpPut]
        //[Route("Update")]
        //PUT : /api/Transactions
        public JsonResult Put(TransactionModel model)
        {
            string query = @"
             update transactions
             set 
             sender = @sender,
             receiver = @receiver,
             debit = @debit,
             credit = @credit,
             data = @data

             where transactions_id = @transactions_id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@transactions_id", model.Transactions_Id);
                    myCommand.Parameters.AddWithValue("@sender", model.Sender);
                    myCommand.Parameters.AddWithValue("@receiver", model.Receiver);
                    myCommand.Parameters.AddWithValue("@debit", model.Debit);
                    myCommand.Parameters.AddWithValue("@credit", model.Credit);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Update Succesfully");
        }
        [HttpDelete]
        //[Route("Delete")]
        //DELETE : /api/Transactions
        public JsonResult Delete(TransactionModel model)
        {
            string query = @"
             delete from transactions

             where transactions_id = @transactions_id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@transactions_id", model.Transactions_Id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Delete Succesfully");
        }


    }
}
