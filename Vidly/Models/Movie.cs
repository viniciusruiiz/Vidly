using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vidly.Models
{
    public class Movie
    {
        public Movie()
        {
            Customers = new List<Customer>();
        }

        public virtual int Id { get; set; } //autoset
        [DisplayName("Nome")]
        [Required]
        public virtual string Name { get; set; }
        [DisplayName("Gênero")]
        [Required]
        public virtual string Genre { get; set; }
        [DisplayName("Classificação Indicativa")]
        [Required]
        public virtual int Rating { get; set; }
        [DisplayName("Data de Lançamento")]
        [DataType(DataType.Date)]
        public virtual DateTime ReleaseDate { get; set; }
        
        public virtual bool Disponibility()
        {
            return Customers.Count >= 50 ? false : true;
        }
        public virtual IList<Customer> Customers { get; set; }
    }
}