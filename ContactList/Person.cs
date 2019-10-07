using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList
{
    public class Person
    {

        [Required]
        public int id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        [Required]
        public string email { get; set; }
    }
}
