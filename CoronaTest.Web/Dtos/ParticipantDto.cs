using CoronaTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaTest.Web.Dtos
{
    public class ParticipantDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Der {0} ist verpflichtend")]
        [DisplayName("Vorname")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Der {0} ist verpflichtend")]
        [DisplayName("Nachname")]
        public string Lastname { get; set; }

        [DisplayName("Name")]
        public string Fullname => $"{Firstname} {Lastname}";

        [Required(ErrorMessage = "Der {0} ist verpflichtend")]
        [DisplayName("Geburtstag")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Das {0} ist verpflichtend")]
        [DisplayName("Geschlecht")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Die {0} ist verpflichtend")]
        [StringLength(10, ErrorMessage = "Die {0} muss genau 10 Zeichen lang sein!", MinimumLength = 10)]
        [DisplayName("Sozialversicherungsnummer")]
        public string SocialSecurityNumber { get; set; }

        [Required(ErrorMessage = "Die {0} ist verpflichtend")]
        [StringLength(16, ErrorMessage = "Die {0} muss zw. {1} und {2} Zeichen lang sein!", MinimumLength = 5)]
        [DisplayName("Handynummer")]
        public string Mobilephone { get; set; }

        [Required(ErrorMessage = "Die {0} ist verpflichtend")]
        [DisplayName("Straße")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Die {0} ist verpflichtend")]
        [DisplayName("Hausnummer")]
        public string HouseNumber { get; set; }

        [DisplayName("Stiege")]
        public string Stair { get; set; }

        [DisplayName("Tür")]
        public string Door { get; set; }

        [Required(ErrorMessage = "Die {0} ist verpflichtend")]
        [DisplayName("PLZ")]
        public string Postalcode { get; set; }

        [Required(ErrorMessage = "Die {0} ist verpflichtend")]
        [DisplayName("Ort")]
        public string City { get; set; }
     
    }
}
