using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;
using BookStoreApp.API.Static;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(BookStoreDbContext context,IMapper mapper, ILogger<BooksController> logger,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            try
            {
                var booksDto = await _context.Books
                    .Include(q => q.Author)
                    .ProjectTo<BookReadOnlyDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return Ok(booksDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing GET in {nameof(GetBooks)}");
                return StatusCode(500, Message.Error500Message);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            try
            {
                var book = await _context.Books
                    .Include(q => q.Author)
                    .ProjectTo<BookDetailsDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(q => q.Id == id);
                if (book == null)
                {
                    return NotFound();
                }
                return book;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing GET in {nameof(GetBook)} ");
                return StatusCode(500, Message.Error500Message);
            }
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(bookDto.ImageData) == false)
            {
                bookDto.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);
                var picName = Path.GetFileName(book.Image);
                var path = $"{_webHostEnvironment.WebRootPath}\\bookcoverimages\\{picName}";
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            _mapper.Map(bookDto,book);

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (! await BookExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, $"Error Performing PUT in {nameof(PutBook)}");
                    return StatusCode(500, Message.Error500Message);
                }
            }
            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Book>> PostBook(BookCreateDto bookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDto);
                book.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBook", new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing POST in {nameof(PostBook)}", bookDto);
                return StatusCode(500, Message.Error500Message);
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteBook)}");
                return StatusCode(500, Message.Error500Message);
            }
        }
        private string CreateFile(string imageBase64,string imageName)
        {
            var url = HttpContext.Request.Host.Value;
            var ext = Path.GetExtension(imageName);
            var fileName = $"{Guid.NewGuid()}{ext}";

            var path = $"{_webHostEnvironment.WebRootPath}\\bookcoverimages\\{fileName}";
            byte[] image = Convert.FromBase64String(imageBase64) ;
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(image,0,image.Length);
            fileStream.Close();
            return $"https://{url}/bookcoverimages/{fileName}";
        }

        private async Task<bool> BookExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(e => e.Id == id);
        }
    }
}
