using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication.Modules;

namespace WebApplication.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/Select
        [Route("api/Select")]
        [HttpGet]
        public ActionResult<ArrayList> GetSelect([FromForm] Commons cm)
        {
            DataBase db = new DataBase();
            MySqlConnection conn = db.GetConnetion();
            if (conn == null)
            {
                Console.WriteLine("Error!!");
            }
            else
            {
                Console.WriteLine("Success!!");
            }
            return CRUD.GetSelect();
        }

        // GET api/Insert
        [Route("api/Insert")]
        [HttpPost]
        public ActionResult<ArrayList> GetInsert([FromForm] Commons cm)
        {
            DataBase db = new DataBase();
            MySqlConnection conn = db.GetConnetion();
            if (conn == null)
            {
                Console.WriteLine("Error!!");
            }
            else
            {
                Console.WriteLine("Success!!");
            }
            return CRUD.GetInsert(cm);
        }

        // GET api/Update
        [Route("api/Update")]
        [HttpPost]
        public ActionResult<ArrayList> GetUpdate([FromForm] Commons cm)
        {
            DataBase db = new DataBase();
            MySqlConnection conn = db.GetConnetion();
            if (conn == null)
            {
                Console.WriteLine("Error!!");
            }
            else
            {
                Console.WriteLine("Success!!");
            }
            return CRUD.GetUpdate(cm);
        }

        // GET api/Delete
        [Route("api/Delete")]
        [HttpPost]
        public ActionResult<ArrayList> GetDelete([FromForm] Commons cm)
        {
            DataBase db = new DataBase();
            MySqlConnection conn = db.GetConnetion();
            if (conn == null)
            {
                Console.WriteLine("Error!!");
            }
            else
            {
                Console.WriteLine("Success!!");
            }
            return CRUD.GetDelete(cm);
        }
    }
}
