﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Data.Entities
{
    [Table("Tracks")]
    public class Track
    {
        [Key]
        public int TrackId { get; set; }

        public string Name { get; set; }

        private int _AlbumId;

        public int AlbumId { get; set; }

        public int MediTypeId { get; set; }

        public int GenreId { get; set; }

        private string _Composer;

        public string Composer
        {
            get
            {
                return _Composer;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Composer = null;
                }
                else
                {
                    _Composer = value;
                }
            }
        }


        public int Milliseconds { get; set; }

        public int Bytes { get; set; }

        public double UnitPrice { get; set; }



        public virtual Genre Genre { get; set; }
        public virtual MediaType MediaType { get; set; }

    }
}
