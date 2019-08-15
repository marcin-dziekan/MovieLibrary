using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoLib.Models
{
    public class Movie
    {
        public string Path { get; set; }
        public string FullName { get; set; }
        public string WitchDrive { get; set; }
        public DateTime Date { get; set; } 
        public List<Movie> MovieList { get; set; }
    }
}