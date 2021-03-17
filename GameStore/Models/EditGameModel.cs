﻿using DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class EditGameModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public Categories Category { get; set; }

        [Required]
        public Rating Rating { get; set; }       
        public IFormFile Logo { get; set; }       
        public IFormFile Background { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Count { get; set; }              
    }
}
