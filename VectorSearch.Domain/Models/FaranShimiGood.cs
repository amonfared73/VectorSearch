﻿using System.ComponentModel.DataAnnotations.Schema;

namespace VectorSearch.Domain.Models
{
    [NotMapped]
    public class FaranShimiGood : IWord
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Vector { get; set; }
        public int DictionaryTypeId { get; set; }
    }
}
