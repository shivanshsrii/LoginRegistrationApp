using LoginRegistrationApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LoginRegistrationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("registration")]
        public string registration(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MyPageCon").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UserName,Password,Email,IsActive)" +
                " VALUES('"+registration.UserName+"','"+registration.Password+"','"+registration.Email+ "','"+registration.IsActive+"')",con);
            con.Open();
            int i=cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Data Inserted";
            }
            else
            {
                return "Error";
            }
        }



        [HttpPost]
        [Route("login")]
        public string login(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MyPageCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Registration WHERE Email='" + registration.Email + "' AND Password='" + registration.Password + "' AND IsActive=1", con);
            DataTable dt=new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return "Valid User";
            }
            else
            {
                return "Invalid User";
            }
        }



    }
}
