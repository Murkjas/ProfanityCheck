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
            return await _context.Words.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Word>> GetWord(Guid id)
        {
            return await _context.Words.FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddWord(Word word)
        {
            _context.Words.Add(word);
            await _context.SaveChangesAsync();
            return Ok("New word " + word.Text + " added successfully!");
        }
    }
}