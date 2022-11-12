using System;
using System.Linq;
using System.Threading.Tasks;
using BookModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Book = BookModels.Book;

namespace Books.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly ILogger _logger;

        public BooksController(IBookRepository repository, ILoggerFactory loggerFactory)
        {
            _repository = repository ?? throw new ArgumentException(nameof(BooksController));
            _logger = loggerFactory.CreateLogger(nameof(BooksController));
        }

        #region Get api/books

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var models = await _repository.GetAllAsync();
                if (!models.Any())
                {
                    return new NoContentResult();
                }

                return Ok(models); // 200 OK

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        #endregion

        #region GetById api/books/1

        [HttpGet("{id:int}", Name = "GetBookById")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var model = await _repository.GetByIdAsync(id);
                if (model == null)
                {
                    return NotFound(); // 204 NotFound
                }

                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        #endregion

        #region AddAsync

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] BookModels.Book dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var temp = new BookModels.Book();
            temp.Title = dto.Title;
            temp.Description = dto.Title;
            temp.Created = DateTime.Now;

            try
            {
                var model = await _repository.AddAsync(temp);
                if (model == null)
                {
                    return BadRequest();
                }

                return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        #endregion

        #region UpdateAsnyc

        // Put api/books/123
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] BookModels.Book dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                dto.Id = id;
                var status = await _repository.UpdateAsync(dto);
                if (!status) BadRequest();
                return NoContent(); // 204 No content
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        #endregion

        #region DeleteAsync

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var status = await _repository.DeleteAsync(id);
                if (!status) BadRequest();
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
        #endregion
    }
}