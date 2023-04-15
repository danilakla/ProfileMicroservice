using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProfileService.Models;

public class Projects
{
    [Key]
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string NameUsingTech { get; set; }
    [JsonIgnore]

    public int ProfilesId { get; set; }
    [JsonIgnore]
    public Profiles Profiles { get; set; }
}
