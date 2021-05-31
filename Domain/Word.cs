using System;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Word
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}  