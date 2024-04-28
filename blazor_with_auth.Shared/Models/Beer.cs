using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blazor_with_auth.Data;

namespace blazor_with_auth.Shared.Models
{
    public class Beer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UnRegisteredAppUserId { get; set; }
        public int Amount { get; set; } = 1;
        //  registered user
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
