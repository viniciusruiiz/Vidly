using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using vidly.Models;
using Vidly.Models;

namespace vidly.ViewModels
{
    public class CustomerDetailsClassViewModel
    {
        public IList<Movie> Movies { get; set; }
        public virtual string Name { get; set; }
        [Required]
        public virtual string CPF { get; set; }
        [Required]
        [DisplayName("Dia de Nascimento")]
        [DataType(DataType.Date)]
        public virtual DateTime DateOfBirth { get; set; }
        public virtual User User { get; set; }
    }
}