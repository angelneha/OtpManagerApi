using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Register1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Register1.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PasswordController : ControllerBase
  {
    private readonly IConfiguration _config;
    public PasswordController(IConfiguration config)
    {
      _config = config;

    }

    // GET: api/<PasswordController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<PasswordController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<PasswordController>
    [HttpPost ]
    public IActionResult Post(Pwd pwd)
    {
      //string MyDBConnection = "Data Source=DESKTOP-8924CO7\\SQLEXPRESS;Initial Catalog=OtpManager;Integrated Security=True";
      try
      {
        SqlConnection con = new SqlConnection(_config.GetConnectionString("MyDBConnection"));
        
       
        //SqlConnection con = new SqlConnection(MyDBConnection);
        con.Open();
       // return Ok(pwd.Email);
        string Query = "SELECT COUNT(*) FROM Users WHERE email='" + pwd.Email + "' AND mobile='" + pwd.Mobile +  "'";
        //string Query = "SELECT COUNT(*) FROM Users WHERE email='" + user.Email + "' AND password='" + user.Password + "'";
        SqlDataAdapter sda = new SqlDataAdapter(Query, con);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows[0][0].ToString() == "1")
        {
          /* I have made a new page called home page. If the user is successfully authenticated then the form will be moved to the next form */
          return Ok("success");
        }
        else
          return Ok("failure");

        
      }
      catch (Exception e)
      {

        return Ok(e);
      }
    }

    // PUT api/<PasswordController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PasswordController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
