using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApplication3.Models; // Đảm bảo import namespace chứa lớp Book và DapperRepository

public class BooksController : ApiController
{
    private DapperRepository _repository;

    public BooksController()
    {
       
            // Khởi tạo DapperRepository với chuỗi kết nối của bạn
            string connectionString = "Server=DESKTOP-QLMR95T;Database=bookstore;Integrated Security=True;";
            _repository = new DapperRepository(connectionString);
        
    }

    // GET api/books
    public IHttpActionResult Get()
    {
        var books = _repository.GetAllBooks();
        return Ok(books);
    }

    // GET api/books/5
    public IHttpActionResult Get(int id)
    {
        var book = _repository.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    // GET api/books/search?query=keyword

    [HttpGet]
    [Route("api/books/search")]
    public IHttpActionResult Search([FromUri] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Query parameter is required.");
        }

        var books = _repository.SearchBooks(query);
        if (books.Count == 0)
        {
            return NotFound();
        }

        return Ok(books);
    }






    // POST api/books
    public IHttpActionResult Post([FromBody] Book book)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _repository.InsertBook(book);
        return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
    }

    // PUT api/books/5
    public IHttpActionResult Put(int id, [FromBody] Book book)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != book.Id)
        {
            return BadRequest();
        }

        _repository.UpdateBook(book);
        return Ok(book);
    }

    // DELETE api/books/5
    public IHttpActionResult Delete(int id)
    {
        var book = _repository.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }

        _repository.DeleteBook(id);
        return Ok(book);
    }
}
