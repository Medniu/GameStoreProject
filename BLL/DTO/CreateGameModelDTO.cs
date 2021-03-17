using DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class CreateGameModelDTO
    {         
        public IFormFile Background { get; set; }
        public IFormFile Logo { get; set; }
        public string Name { get; set; }
        public Categories Category { get; set; }       
        public Rating Rating { get; set; }             
        public decimal Price { get; set; }       
        public int Count { get; set; }
    }
}
