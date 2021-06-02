using System;
using System.Threading.Tasks;
using API.Controllers;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Xunit;

namespace API.Tests
{
    public class WordsControllerTest
    {
        private readonly DataContext _context;
        private readonly WordsController _controller;

        public WordsControllerTest()
        {
            _controller = new WordsController(_context);
        }
        
        [Fact]
        public async void AddWord_ReturnsWord()
        {
            var controller = new WordsController(_context);
            var word = new Word {
                Id = new Guid(),
                Text = "TestWord"
            };

            var result = await controller.AddWord(word);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void GetWords_ReturnsWords()
        {
            var result = await _controller.GetWords();
            var okResult = new OkObjectResult(result);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}