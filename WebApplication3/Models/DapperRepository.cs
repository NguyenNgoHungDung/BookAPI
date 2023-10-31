using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using WebApplication3.Models;

public class DapperRepository
{
    private IDbConnection _db;

    public DapperRepository(string connectionString)
    {
        _db = new SqlConnection(connectionString);
    }

    public List<Book> GetAllBooks()
    {
        string query = "SELECT * FROM Books";
        return _db.Query<Book>(query).AsList();
    }

    public Book GetBookById(int id)
    {
        string query = "SELECT * FROM Books WHERE Id = @Id";
        return _db.QueryFirstOrDefault<Book>(query, new { Id = id });
    }




    public void InsertBook(Book book)
    {
        string query = "INSERT INTO Books (Title, Author, Price, ImgUrl, Description, Quantity, Status) " +
                       "VALUES (@Title, @Author, @Price, @ImgUrl, @Description, @Quantity, @Status)";
        _db.Execute(query, book);
    }

    public void UpdateBook(Book book)
    {
        string query = "UPDATE Books SET Title = @Title, Author = @Author, " +
                       "Price = @Price, ImgUrl = @ImgUrl, Description = @Description, Quantity = @Quantity, Status = @Status " +
                       "WHERE Id = @Id";
        _db.Execute(query, book);
    }

    public void DeleteBook(int id)
    {
        string query = "DELETE FROM Books WHERE Id = @Id";
        _db.Execute(query, new { Id = id });
    }


    ////////////////////////////////////CART/////////////////////////

    public List<CartItem> GetAllCartItems()
    {
        string query = "SELECT * FROM CartItems";
        return _db.Query<CartItem>(query).AsList();
    }

    public CartItem GetCartItemById(int id)
    {
        string query = "SELECT * FROM CartItems WHERE id = @Id";
        return _db.QueryFirstOrDefault<CartItem>(query, new { Id = id });
    }

    public void InsertCartItem(CartItem cartItem)
    {
        string query = "INSERT INTO CartItems (CartID, BookID, Quantity) " +
                       "VALUES (@CartID, @BookID, @Quantity)";
        _db.Execute(query, cartItem);
    }

    public void UpdateCartItem(CartItem cartItem)
    {
        string query = "UPDATE CartItems SET CartID = @CartID, Quantity = @Quantity " +
                        "WHERE BookID = @BookID AND CartID = @CartID";
        _db.Execute(query, cartItem);
    }



    public void DeleteCartItem(int id)
    {
        string query = "DELETE FROM CartItems WHERE id = @Id";
        _db.Execute(query, new { Id = id });
    }

    ////////////////////////////////Bổ sung////////////////////////

    public Book GetBookInfoByCartItemID(int cartItemID)
    {
        string query = @"
        SELECT b.Title, b.Author, b.Price, b.ImgUrl, b.Quantity, b.Description, b.Status
        FROM CartItems ci
        JOIN Books b ON ci.BookID = b.ID
        WHERE ci.ID = @CartItemID";

        return _db.QueryFirstOrDefault<Book>(query, new { CartItemID = cartItemID });
    }

    public List<Book> SearchBooks(string query)
    {
        string searchQuery = "SELECT * FROM Books WHERE Title LIKE @Query OR Author LIKE @Query";
        return _db.Query<Book>(searchQuery, new { Query = $"%{query}%" }).AsList();
    }

    public CartItem GetCartItemByBookID(int bookID)
    {
        // Thực hiện truy vấn SQL hoặc các thao tác cần thiết để lấy cartItem dựa trên bookID
        // Sau đó, trả về cartItem hoặc null nếu không tìm thấy

        string query = "SELECT TOP 1 * FROM CartItems WHERE BookID = @BookID"; // Lấy chỉ một phần tử đầu tiên nếu có nhiều phần tử thỏa mãn
        return _db.QuerySingleOrDefault<CartItem>(query, new { BookID = bookID });
    }


    //////////////////////////////////////Users

    public List<User> GetAllUsers()
    {
        string query = "SELECT * FROM Users";
        return _db.Query<User>(query).AsList();
    }

    public User GetUserById(int id)
    {
        string query = "SELECT * FROM Users WHERE Id = @Id";
        return _db.QueryFirstOrDefault<User>(query, new { Id = id });
    }

    public void InsertUser(User user)
    {
        string query = "INSERT INTO Users (Username, Password, FirstName, LastName, Email, Address, PhoneNumber) " +
                       "VALUES (@Username, @Password, @FirstName, @LastName, @Email, @Address, @PhoneNumber)";
        _db.Execute(query, user);
    }

    public void UpdateUser(User user)
    {
        string query = "UPDATE Users SET Username = @Username, Password = @Password, " +
                       "FirstName = @FirstName, LastName = @LastName, Email = @Email, Address = @Address, PhoneNumber = @PhoneNumber " +
                       "WHERE Id = @Id";
        _db.Execute(query, user);
    }

    public void DeleteUser(int id)
    {
        string query = "DELETE FROM Users WHERE Id = @Id";
        _db.Execute(query, new { Id = id });
    }



}


