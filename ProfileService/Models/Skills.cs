using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProfileService.Models;

public class Skills
{
    [Key]
    public int SkillId { get; set; }
    public string Text { get; set; }
    [JsonIgnore]
    public int ProfilesId { get; set; }
    [JsonIgnore]

    public Profiles Profiles { get; set; }
}
