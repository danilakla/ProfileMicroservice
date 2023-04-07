using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models;

public class Profiles
{
    [Key]
    public int ProfileId { get; set; }


    public string Description { get; set; } = "Empty";
    public int UserId { get; set; }
    public byte[] Photo{ get; set; }
    public string LastName { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public string UniversityName { get; set; }
    public  byte[] BackgroundProfile{ get; set; }

    public IQueryable<Projects> Projects { get; set; }
    public IQueryable<Skills>  Skills{ get; set; }


}
