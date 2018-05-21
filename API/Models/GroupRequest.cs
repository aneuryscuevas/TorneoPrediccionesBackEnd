using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace API.Models
{
    [NotMapped]
    public class GroupRequest : Group
    {
        public byte[] ImageArray { get; set; }
    }
}