﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Data.Entities
{
    [Table("Genres")]
    public class Genre
    {
        [Key]

        public int GenreId { get; set; }

        private string _Name;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Name = null;
                }
                else
                {
                    _Name = value;
                }
            }
        }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}