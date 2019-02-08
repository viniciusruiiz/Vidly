using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace vidly.Models
{
    public class Customer
    {
        public Customer()
        {
            Movies = new List<Movie>();
            User = null;
        }
        public virtual int Id { get; set; } //autoset
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string CPF { get; set; }
        [Required]
        [DisplayName("Dia de Nascimento")]
        [DataType(DataType.Date)]
        public virtual DateTime DateOfBirth { get; set; }
        public virtual bool CanReserveMore()
        {
            return Movies.Count >= 5 ? false : true;
        }
        public virtual IList<Movie> Movies { get; set; }
        [Required]
        public virtual User User { get; set; }
        public virtual int Age()
        {
            var today = DateTime.Today;

            var age = today.Year - DateOfBirth.Year;

            if (DateOfBirth > today.AddYears(-age))
                age--;

            return age;
        }
    }
}