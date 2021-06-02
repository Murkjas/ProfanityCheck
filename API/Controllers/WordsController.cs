using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class WordsController : BaseAPIController
    {
        private readonly DataContext _context;
        public WordsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Word>>> GetWords()
        {
            var allWords = await _context.Words.ToListAsync();
            if (allWords.Count == 0)
            {
                return NotFound("No words found!");
            }
            else
            {
                return Ok(allWords);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Word>> GetWord(Guid id)
        {
            var word = await _context.Words.FindAsync(id);
            if (word == null)
            {
                return NotFound("No word found with the given ID!");
            }
            else
            {
                return word;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddWord(Word word)
        {
            _context.Words.Add(word);
            await _context.SaveChangesAsync();
            return Ok("New word " + word.Text + " added successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(Guid id)
        {
            var wordToDelete = await _context.Words.FindAsync(id);
            if (wordToDelete == null)
            {
                return NotFound("Unable to delete word. No word found with the given ID!");
            }
            else
            {
                _context.Remove(wordToDelete);
                await _context.SaveChangesAsync();
                return Ok("Word deleted successfully!");
            }
        }
    }
}