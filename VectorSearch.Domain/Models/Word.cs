﻿namespace VectorSearch.Domain.Models
{
    public class Word : DomainObject
    {
        public string Text { get; set; }
        public string Vector { get; set; }
    }
}
