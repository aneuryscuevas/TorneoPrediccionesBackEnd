using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace Backend.Models
{
    [NotMapped]
    public class TournamentTeamView : TournamentTeam
    {
        [Display(Name="League")]
        public int LeagueId { get; set; }

    }
}