using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Register1.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Register1.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LoginController : ControllerBase 
  {

    private readonly IConfiguration _config;
    public LoginController(IConfiguration config)
    {
      _config = config;
      
    }
    // GET: api/<LoginController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<LoginController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<LoginController>
    [HttpPost]
    //public IActionResult  Post([FromBody] Login login)
    public IActionResult  Post(Login login)
    {
      //string MyDBConnection = "Data Source=DESKTOP-8924CO7\\SQLEXPRESS;Initial Catalog=OtpManager;Integrated Security=True";
      try
      {
          SqlConnection con = new SqlConnection(_config.GetConnectionString("MyDBConnection"));
        //  con.Open();
        //  string Query = "SELECT COUNT(*) FROM dbo.Users WHERE Email='@username' AND Pwd='@password'";
        //  //string Query = "SELECT COUNT(*) FROM Users WHERE email='" + user.Email + "' AND password='" + user.Password + "'";

        //  SqlCommand cmd = new SqlCommand(Query, con);
        //  cmd.Parameters.AddWithValue("@username", login.Email);
        //  cmd.Parameters.AddWithValue("@password", login.Pwd);
        //  cmd.ExecuteNonQuery();
        //  //return Ok(user.Email+user.Password);
        //  return Ok("Success");
        //}
        //catch (Exception)
        //{

        //  return Ok("Failure");
        //}
        //SqlConnection con = new SqlConnection(MyDBConnection);
        con.Open();
        string Query = "SELECT COUNT(*) FROM Users WHERE email='" + login.Email + "' AND pwd='" + login.Pwd + "'";
        //string Query = "SELECT COUNT(*) FROM Users WHERE email='" + user.Email + "' AND password='" + user.Password + "'";
        SqlDataAdapter sda = new SqlDataAdapter(Query, con);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows[0][0].ToString() == "1")
        {
          /* I have made a new page called home page. If the user is successfully authenticated then the form will be moved to the next form */
          return Ok("Success");
        }
        else
          return Ok("Failure");

        //SqlCommand cmd = new SqlCommand(Query,con);
        //cmd.Parameters.AddWithValue("@username", user.Email);
        //cmd.Parameters.AddWithValue("@password", user.Password);
        //cmd.ExecuteNonQuery();
        ////return Ok(user.Email+user.Password);
        //return Ok("Success");
      }
      catch (Exception e)
      {

        return Ok(e);
      }
    }
    

    // PUT api/<LoginController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<LoginController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
