using ProfileService.Data;
using ProfileService.DTO;
using ProfileService.Models;

namespace ProfileService.Infrastructure.Repository;

public class SkillRepository : IRepository<CreateSkillDto, Skills>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public SkillRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<Skills> CreateProject(CreateSkillDto data)
    {
        try
        {
            var skill = new Skills{Text=data.Text};

            return skill;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task DeleteProject(int id)
    {
        try
        {
            var skilltRemove = await _applicationDbContext.Skills.FindAsync(id);
            _applicationDbContext.Skills.Remove(skilltRemove);
            await _applicationDbContext.SaveChangesAsync();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Skills> GetProjects(int id)
    {


        try
        {
            var skill = await _applicationDbContext.Skills.FindAsync(id);
            return skill;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task UpdateProject(CreateSkillDto data, int id)
    {
        try
        {
            var skill = await _applicationDbContext.Skills.FindAsync(id);
                skill.Text=data.Text;
            
            _applicationDbContext.Skills.Update(skill);
            await _applicationDbContext.SaveChangesAsync();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
