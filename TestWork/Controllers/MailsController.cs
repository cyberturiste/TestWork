using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.Reflection;
using TestWork.Models;

namespace TestWork.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MailsController : ControllerBase
    {

        String pathToDB = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=postgres";

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                string query = @"
           select mailid,Title,Address from Mails
       ";
                DataTable table = new DataTable();
                string sqlDataSourse = pathToDB;
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myConn = new NpgsqlConnection(sqlDataSourse))
                {
                    myConn.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConn))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myConn.Close();
                    }
                }
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(table);
                if (table.Rows.Count > 0)
                {
                    return new JsonResult(JSONresult);
                }

                else return new JsonResult("datatable empry or does not exist");

            }
            catch (Exception e)
            {

                
                
                    return new JsonResult(e);
                    throw;
                
            }





        }
        [HttpGet("WithParametr")]
        public JsonResult GetwithParametr(string i)
        {
            string query = @"select mailid,title,address from mails where mailid=" + i;
            DataTable table = new DataTable();
            string sqlDataSourse = pathToDB;
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqlDataSourse))
            {
                myConn.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }

            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(table);

            return new JsonResult(JSONresult);

        }

        [HttpPost]
        public JsonResult Post(Mails dep)
        {
            string query = @"
           insert into mails values(@mailid,@Title,@Address)
       ";
            DataTable table = new DataTable();
            string sqlDataSourse = pathToDB;
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqlDataSourse))
            {
                myConn.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@mailid", dep.Id);
                    myCommand.Parameters.AddWithValue("@Title", dep.Title);
                    myCommand.Parameters.AddWithValue("@Address", dep.Address);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }
            }

            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(table);

            return new JsonResult("Added ready");

        }


        [HttpPut]
        public JsonResult Put(Mails dep)
        {
            string query = @"
           update mails set Title=@Title,Address=@Address where mailid=@mailid
       ";
            DataTable table = new DataTable();
            string sqlDataSourse = pathToDB;
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqlDataSourse))
            {
                myConn.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@mailid", dep.Id);
                    myCommand.Parameters.AddWithValue("@Title", dep.Title);
                    myCommand.Parameters.AddWithValue("@Address", dep.Address);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }
            }

            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(table);

            return new JsonResult("Added ready");

        }



    }
}
