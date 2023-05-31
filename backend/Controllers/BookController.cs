using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PWAY_ASPNetCore_WebAPI.Filter;
using PWAY_ASPNetCore_WebAPI.Helpers;
using PWAY_ASPNetCore_WebAPI.Models;
using PWAY_ASPNetCore_WebAPI.Services;
using PWAY_ASPNetCore_WebAPI.Wrappers;
using System.Data;
using System.Data.SqlClient;

// Prequisite: DB called BooksDB, 5 var : Id (IDENTITY), StartDis, EndDis, string1, string2

namespace PWAY_ASPNetCore_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookDbContext dbContext;
        private readonly IUriService uriService; 
        public BookController(BookDbContext dbContext, IUriService uriService) 
        {
            this.dbContext = dbContext; // injection
            this.uriService = uriService; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            /* prev:
            var respond = await dbContext.Books.ToListAsync();
            return Ok(respond);*/

            var route = Request.Path.Value; // gets route "api/Book"
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await dbContext.BooksDB //PAGINATION
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await dbContext.BooksDB.CountAsync();
            //without Uri of NextPage, LastPage, FirstPage, LastPage
            //return Ok(new PagedRespond<List<Book>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
            var pagedRespond = PaginationHelper.CreatePagedRespond<Book>(pagedData, validFilter, totalRecords, uriService, route);
            return Ok(pagedRespond);
                
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await dbContext.BooksDB.Where(a => a.Id == id).FirstOrDefaultAsync(); // get from DB
            return Ok(new Respond<Book>(book));
        }
        /* b4 pagination 
        public readonly IConfiguration _configuration;
        public BookController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetOneBook/{id}")] 
        public string GetOneBook(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("BooksDB").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [dbo].[BooksDB] WHERE Id =" + id, con); //using ENTITY
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Book> booksList = new List<Book>();
            Response response = new Response();

            if (dt.Rows.Count > 0)
            {
                Book book = new Book();
                book.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                book.StartDis = Convert.ToInt32(dt.Rows[0]["StartDis"]);
                book.EndDis = Convert.ToInt32(dt.Rows[0]["EndDis"]);
                book.string1 = Convert.ToString(dt.Rows[0]["string1"]);
                book.string2 = Convert.ToString(dt.Rows[0]["string2"]);

                booksList.Add(book);
            }
            if (booksList.Count > 0)
            {
                return JsonConvert.SerializeObject(booksList);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "yo no data";
                return JsonConvert.SerializeObject(response);
            }
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public string GetBooks()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("BooksDB").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [dbo].[BooksDB]", con); //using ENTITY
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Book> booksList = new List<Book>();
            Response response = new Response();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    book.StartDis = Convert.ToInt32(dt.Rows[i]["StartDis"]);
                    book.EndDis = Convert.ToInt32(dt.Rows[i]["EndDis"]);
                    book.string1 = Convert.ToString(dt.Rows[i]["string1"]);
                    book.string2 = Convert.ToString(dt.Rows[i]["string2"]);

                    booksList.Add(book);
                }
            }
            if (booksList.Count > 0)
            {
                return JsonConvert.SerializeObject(booksList);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "yo no data";
                return JsonConvert.SerializeObject(response);
            }
        }
        */
    }
}
