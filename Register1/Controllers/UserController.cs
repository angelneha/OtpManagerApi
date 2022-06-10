
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Register1.Models;
using System;
using System.Data;
using System.Linq;

namespace Register1.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  //[EnableCors("MyPolicy")]
  public class UserController : ControllerBase
  {
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;
    public UserController(IConfiguration config, IWebHostEnvironment env)
    {
      _config = config;
      // _context = context;
      _env = env;
    }

    
[HttpGet]
public JsonResult Get()
{
  string query = @"select * from Users";
  DataTable table = new DataTable();
  string sqlDataSource = _config.GetConnectionString("MyDBConnection");
  SqlDataReader myReader;
  try
  {
    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
    {
      myCon.Open();
      using (SqlCommand myCommand = new SqlCommand(query, myCon))
      {
        myReader = myCommand.ExecuteReader();
        table.Load(myReader); ;

        myReader.Close();
        myCon.Close();
      }
    }
  }
  catch (Exception ex)
  {
    return new JsonResult(ex.Message);
  }

  return new JsonResult(table);
}

[HttpPost]
public JsonResult Post(User user)
{
  string query = @"
                insert into Users values 
                ('" + user.FirstName + @"','" + user.LastName + @"','" + user.Email + @"','" + user.Mobile + @"','" + user.Gender + @"','" + user.Pwd + @"')
                ";
  DataTable table = new DataTable();
  string sqlDataSource = _config.GetConnectionString("MyDBConnection");
  SqlDataReader myReader;
  try
  {
    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
    {
      myCon.Open();
      using (SqlCommand myCommand = new SqlCommand(query, myCon))
      {
        myReader = myCommand.ExecuteReader();
        table.Load(myReader); ;

        myReader.Close();
        myCon.Close();
      }
    }
  }
  catch (Exception ex)
  {
    Console.WriteLine(ex.Message);
    //return ""
  }


  return new JsonResult("Added successfully");
}
}
}

/*
 https://localhost:44367/ */



