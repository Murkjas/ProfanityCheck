using System;
using System.Threading.Tasks;
using API.Controllers;
using Domain;
using Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace API.Tests
{
    public abstract class WordsControllerTest
    {
        protected WordsControllerTest(DbContextOptions<DataContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<DataContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new DataContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var words = new List<Word>
                {
                    new Word
                    {
                        Text = "Test1"
                    },
                    new Word
                    {
                        Text = "Test2"
                    },
                    new Word
                    {
                        Text = "Test3"
                    }
                };

                context.Words.AddRange(words);

                context.SaveChanges();
            }
        }
// Add comment for Azure deployment
        [Fact]
        public async Task Can_get_words()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var controller = new WordsController(context);

                var items = await controller.GetWords();
                Assert.Equal(3, items.Value.Count);
            }
        }
    }
}