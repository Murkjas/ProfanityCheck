using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API;
using Domain;
using Persistence;


namespace API.Tests
{
    public class WordsControllerAPITest : WordsControllerTest
    {
        public WordsControllerAPITest() : base(
            new DbContextOptionsBuilder<DataContext>()
                .UseSqlite("Filename=words.db")
                .Options)
        {
        }
    }
}