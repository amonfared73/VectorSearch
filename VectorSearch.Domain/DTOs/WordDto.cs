﻿namespace VectorSearch.Domain.DTOs
{
    public record WordDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
