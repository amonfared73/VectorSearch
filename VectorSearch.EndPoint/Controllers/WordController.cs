using Microsoft.AspNetCore.Mvc;
using VectorSearch.ApplicationService.Services;
using VectorSearch.Core.Models;
using VectorSearch.Core.ViewModels;

namespace VectorSearch.EndPoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IWordService _wordService;
        public WordController(IWordService wordService)
        {
            _wordService = wordService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string? word)
        {
            try
            {
                var result = await _wordService.GetAll(word);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _wordService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> VectorSearch(string word)
        {
            try
            {
                var result = await _wordService.VectorSearch(word);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(string word)
        {
            try
            {
                await _wordService.Insert(word);
                return Ok($"Word: {word} inserted successfully!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string word)
        {
            try
            {
                await _wordService.Delete(word);
                return Ok($"Word: {word} deleted successfully!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> BulkInsert(IEnumerable<WordViewModel> words)
        {
            try
            {
                await _wordService.BulkInsert(words);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> ClearAllWords()
        {
            try
            {
                await _wordService.ClearAllWords();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
