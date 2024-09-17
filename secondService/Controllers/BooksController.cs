using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using secondService.Model;
using secondService.Service;

namespace secondService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAll()
        {
            return await _bookService.GetAllAsync();
        }

        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found." });
            }
            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _bookService.AddAsync(book);
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the book.", error = ex.Message });
            }
        }

        // PUT: api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest(new { message = "Book ID mismatch." });
            }

            var existingBook = await _bookService.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound(new { message = "Book not found." });
            }

            try
            {
                await _bookService.UpdateAsync(book);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the book.", error = ex.Message });
            }
        }

        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found." });
            }

            try
            {
                await _bookService.DeleteAsync(id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the book.", error = ex.Message });
            }
        }
    }
}
