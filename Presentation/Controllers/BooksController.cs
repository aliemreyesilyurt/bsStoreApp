using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        // Dependency Injection
        // IOC semasindaki Resolve blogu
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _manager.BookService.GetAllBooksAsync(false);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {
            var book = await _manager
                .BookService
                .GetOneBookByIdAsync(id, false);

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
        {
            if (bookDto is null)
                return BadRequest(); //400

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState); //422

            var book = await _manager.BookService.CreateOneBookAsync(bookDto);

            return StatusCode(201, book);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {
            if (bookDto is null)
                return BadRequest(); //400

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState); //422

            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);

            return NoContent(); //204
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.BookService.DeleteOneBookAsync(id, false);
            //BookManager uzerinde DeleteOneBookAsync metodunda entity kontrolu yapildigi icin burada tekrar yapmaya gerek yok!

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            //JsonPatchDocument<Book> : JSON verilerini bir varlık sinifi(Book) uzerinde uygulamak icin kullanilir

            if (bookPatch is null)
                return BadRequest(); //400

            var result = await _manager.BookService.GetOneBookForPatchAsync(id, false); //(BookForDtoUpdate, Book)

            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState); //gelen JSON yamalarini "entity" nesnesine uygular 

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.BookService.SaveChangesForUpdateAsync(result.bookDtoForUpdate, result.book);

            return NoContent(); //204
        }
    }
}
