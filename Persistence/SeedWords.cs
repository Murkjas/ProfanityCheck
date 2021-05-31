using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class SeedWords
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Words.Any()) return;
            
            var words = new List<Word>
            {
                new Word
                {
                    Text = "Helsinki"
                },
                new Word
                {
                   Text = "Seed"
                },
                new Word
                {
                    Text = "Dotnet"
                }
            };

            await context.Words.AddRangeAsync(words);
            await context.SaveChangesAsync();
        }
    }
}