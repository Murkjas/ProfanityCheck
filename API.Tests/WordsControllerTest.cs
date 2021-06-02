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
        public void AddWord_ReturnsWord()
        {
            Word testWord = new Word
            {
                Id = new Guid(),
                Text = "TestWord"
            };
            var result = _controller.AddWord(testWord);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetWords_ReturnsWords()
        {
            var result = _controller.GetWords();
            Assert.NotNull(result);
            var okResult = new OkObjectResult(result);
            Assert.Equal(200, okResult.StatusCode);
        }
        [Fact]
        public async Task GetWord_ReturnsWord()
        {
            Word word = new Word()
            {
                Id = new Guid(),
                Text = "TestWord"
            };
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(w => w.Words.FindAsync(word.Id)).ReturnsAsync(word);
            WordsController controller = new WordsController(mockContext.Object);
            var result = await controller.GetWord(word.Id);
            var receivedWord = result.Value as Word;
            Assert.Equal(receivedWord.Text, word.Text);
        }
    }
}