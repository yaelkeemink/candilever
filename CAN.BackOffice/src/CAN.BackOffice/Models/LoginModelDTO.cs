using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Models
{
    public class LoginModelDTO
    {

        [Display(Name ="Gebruikersnaam")]
        public string UserName { get; set; }
        [Display(Name = "Wachtwoord")]        
        public string Password { get; set; }
    }
}
