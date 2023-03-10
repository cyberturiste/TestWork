using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.Reflection;
using TestWork.Models;
using System.Configuration;


namespace TestWork.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MailsController : ControllerBase
    {
        private readonly ILogger<MailsController> _logger;
        private readonly IConfiguration _configuration;
        public MailsController(ILogger<MailsController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                string query = @"
           select mailid,Title,Address from Mails
       ";
                DataTable table = new DataTable();

                string sqlDataSourse = _configuration.GetConnectionString("DefaultConnection").ToString();
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
            catch (Exception e)
            {

                return new JsonResult(e);


            }





        }
        [HttpGet("WithParametr")]
        public JsonResult GetwithParametr(string i)
        {
            try
            {
                string query = @"select mailid,title,address from mails where mailid=" + i;
                DataTable table = new DataTable();
                string sqlDataSourse = _configuration.GetConnectionString("DefaultConnection").ToString();
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
            catch (Exception e)
            {

                return new JsonResult(e);


            }
        }

        [HttpPost]
        public JsonResult Post(Mails dep)
        {
            try
            {
                string query = @"
           insert into mails values(@mailid,@Title,@Address)
       ";
                DataTable table = new DataTable();
                string sqlDataSourse = _configuration.GetConnectionString("DefaultConnection").ToString();
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
            catch (Exception e)
            {

                return new JsonResult(e);


            }
        }


        [HttpPut]
        public JsonResult Put(Mails dep)
        {
            try
            {
                string query = @"
           update mails set Title=@Title,Address=@Address where mailid=@mailid
       ";
                DataTable table = new DataTable();
                string sqlDataSourse = _configuration.GetConnectionString("DefaultConnection").ToString();
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
            catch (Exception e)
            {

                return new JsonResult(e);


            }
        }



    }
}
