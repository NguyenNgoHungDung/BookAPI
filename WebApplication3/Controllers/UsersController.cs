using System.Web.Http;
using WebApplication3.Models;

public class UsersController : ApiController
{
    private DapperRepository _repository;

    public UsersController()
    {
        // Khởi tạo DapperRepository với chuỗi kết nối của bạn
        string connectionString = "Server=DESKTOP-QLMR95T;Database=bookstore;Integrated Security=True;";
        _repository = new DapperRepository(connectionString);
    }

    // GET api/users
    public IHttpActionResult Get()
    {
        var users = _repository.GetAllUsers(); // Thay thế bằng phương thức lấy danh sách Users
        return Ok(users);
    }

    // GET api/users/5
    public IHttpActionResult Get(int id)
    {
        var user = _repository.GetUserById(id); // Thay thế bằng phương thức lấy User bằng ID
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // POST api/users
    public IHttpActionResult Post([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _repository.InsertUser(user); // Thay thế bằng phương thức thêm User mới
        return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
    }

    // PUT api/users/5
    public IHttpActionResult Put(int id, [FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != user.Id)
        {
            return BadRequest();
        }

        _repository.UpdateUser(user);
        return Ok(user); // Trả về HTTP 200 (OK) và thông tin cập nhật
    }

}
