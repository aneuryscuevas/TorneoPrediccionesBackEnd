using System.ComponentModel.DataAnnotations;
using System.Web;
using Domain;

namespace Backend.Models
{
    public class TournamentView: Tournament
    {
        [Display(Name = "Logo")]
        public HttpPostedFileBase LogoToFile { get; set; }
    }
}