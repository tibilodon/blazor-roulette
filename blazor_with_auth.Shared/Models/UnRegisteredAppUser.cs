using System.ComponentModel.DataAnnotations;

namespace blazor_with_auth.Shared.Models
{
    public class UnRegisteredAppUser
    {
        [Key]
        // uuid
        public string? Id { get; set; }
        public string Name { get; set; }
        public ICollection<Beer>? Beers { get; set; }
    }
}
