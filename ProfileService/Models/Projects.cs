using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models;

public class Projects
{
    [Key]
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string NameUsingTech { get; set; }

    public int ProfilesId { get; set; }
    public Profiles Profiles { get; set; }
}
