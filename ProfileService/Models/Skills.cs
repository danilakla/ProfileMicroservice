using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models;

public class Skills
{
    [Key]
    public int SkillId { get; set; }
    public string Text { get; set; }
    public int ProfilesId { get; set; }
    public Profiles Profiles { get; set; }
}
