using System.Net;
using System.Web.Http;
using WebApplication3.Models;

public class CartItemsController : ApiController
{
    private DapperRepository _repository;

    public CartItemsController()
    {
        // Khởi tạo DapperRepository với chuỗi kết nối của bạn
        string connectionString = "Server=DESKTOP-QLMR95T;Database=bookstore;Integrated Security=True;";
        _repository = new DapperRepository(connectionString);
    }

    // GET api/cartitems
    public IHttpActionResult Get()
    {
        // Lấy danh sách các mục trong giỏ hàng (CartItems)
        var cartItems = _repository.GetAllCartItems();
        return Ok(cartItems);
    }

    // GET api/cartitems/5
    public IHttpActionResult Get(int id)
    {
        // Lấy một mục trong giỏ hàng bằng ID
        var cartItem = _repository.GetCartItemById(id);
        if (cartItem == null)
        {
            return NotFound();
        }
        return Ok(cartItem);
    }

    // POST api/cartitems
    public IHttpActionResult Post([FromBody] CartItem cartItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _repository.InsertCartItem(cartItem);
        return CreatedAtRoute("DefaultApi", new { id = cartItem.id }, cartItem);
    }


    // PUT api/cartitems/5
    public IHttpActionResult Put(int id, [FromBody] CartItem cartItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != cartItem.id)
        {
            return BadRequest();
        }

        _repository.UpdateCartItem(cartItem);
        return Ok(cartItem);
    }

    // DELETE api/cartitems/5
    public IHttpActionResult Delete(int id)
    {
        var cartItem = _repository.GetCartItemById(id);
        if (cartItem == null)
        {
            return NotFound();
        }

        _repository.DeleteCartItem(id);
        return Ok(cartItem);
    }

    [HttpGet]
    [Route("api/cartitems/getbookinfo/{cartItemID}")]
    public IHttpActionResult GetBookInfo(int cartItemID)
    {
        // Gọi phương thức từ DapperRepository để lấy thông tin chi tiết về sách dựa trên CartItemID
        Book bookInfo = _repository.GetBookInfoByCartItemID(cartItemID);

        if (bookInfo != null)
        {
            return Ok(bookInfo);
        }

        return NotFound();
    }

    [HttpGet]
    [Route("api/cartitems/getcartitembybookid/{bookID}")]
    public IHttpActionResult GetCartItemByBookID(int bookID)
    {
        // Check if a cart item with the specified bookID exists
        CartItem cartItem = _repository.GetCartItemByBookID(bookID);
        return Ok(cartItem);
    }

  
}
