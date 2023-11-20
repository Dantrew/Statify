﻿using System.Text.Json.Serialization;

namespace StatifyAPI.Models
{
    public class Token
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        public DateTime IssueTime { get; set; }
    }
}