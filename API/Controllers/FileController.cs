using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FileController : BaseAPIController
    {

        static HttpClient client = new HttpClient();

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                // Get all words in the database
                var streamTask = client.GetStreamAsync("http://localhost:5000/api/words");
                // The words are returned as JSON, so we need to convert it into a C# object
                List<Word> foundWords = await JsonSerializer.DeserializeAsync<List<Word>>(await streamTask);
                // No need to do any processing related to the file if we haven't got any banned words.
                if (foundWords.Count == 0)
                {
                    return Ok("File uploaded successfully!");
                }

                // Assume that the file is sent either through a form, or through Postman as form-data
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                if (file.Length > 0)
                {
                    // Use a list instead of an array, as we don't know the size of this list beforehand
                    List<string> bannedWords = new List<string>();
                    using (var stream = file.OpenReadStream())
                    {
                        using var sr = new StreamReader(stream, Encoding.UTF8);
                        // Read the given file
                        var fileContents = sr.ReadToEnd();
                        // For each word in our database, check if that word appears in the given file
                        foreach (Word word in foundWords)
                        {
                            if(fileContents.Contains(word.Text))
                            {
                                bannedWords.Add(word.Text);
                            } 
                        }
                    }
                    string statusMessage = "File uploaded successfully!\n" + bannedWords.Count + " banned words were found: " + String.Join(",", bannedWords);
                    return Ok(statusMessage);
                }
                else
                {
                    return BadRequest("No files given!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}