using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OTF.Core
{
    public enum CuisineType
    {
         None,
         Mexican,
         Japanese        
    }

    public class Restaurant
    {
        [Required, StringLength(80)]
        public string Name {get; set;}
        public string Location {get; set;}
        public int Id {get; set;}
        public CuisineType cusine {get; set;}
    }
}