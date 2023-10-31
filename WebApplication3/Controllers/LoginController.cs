using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication3.Models;
using Dapper;

namespace WebApplication3.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("api/auth/login")]
        public HttpResponseMessage Login([FromBody] Login login)
        {
            try
            {
                if (login == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid login data");
                }

                // Kiểm tra tên đăng nhập và mật khẩu
                if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Username and password are required");
                }

                // Kết nối đến cơ sở dữ liệu sử dụng Dapper
                using (IDbConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    dbConnection.Open();

                    // Thực hiện truy vấn kiểm tra tên đăng nhập và mật khẩu
                    var query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                    var user = dbConnection.QueryFirstOrDefault<User>(query, new { login.Username, login.Password });

                    if (user == null)
                    {
                        // Trả về lỗi nếu tên đăng nhập hoặc mật khẩu không hợp lệ
                        return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid username or password");
                    }

                    // Đã xác thực thành công, bạn có thể trả về thông tin đăng nhập hoặc token (tuỳ thuộc vào cách bạn thiết kế ứng dụng)

                    // Ví dụ:
                    return Request.CreateResponse(HttpStatusCode.OK, new { user.Id, user.Username, user.FirstName, user.LastName });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error");
            }
        }
    }
}