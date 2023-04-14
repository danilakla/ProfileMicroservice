using ProfileService.Data;
using ProfileService.DTO;
using ProfileService.Models;

namespace ProfileService.Infrastructure.Repository;

public class ProjectRepository : IRepository<CreateProjectDto, Projects>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProjectRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<Projects> CreateProject(CreateProjectDto data)
    {
        try
        {
            var project = new Projects { Description = data.Description, Name = data.Name, NameUsingTech = data.NameUsingTech };

            return project;
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
            var projectRemove=await _applicationDbContext.Projects.FindAsync(id);
             _applicationDbContext.Projects.Remove(projectRemove);
            await _applicationDbContext.SaveChangesAsync();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Projects> GetProjects(int id)
    {
        try
        {
            var project = await _applicationDbContext.Projects.FindAsync(id);
            return project;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task UpdateProject(CreateProjectDto data, int id)
    {
        try
        {
            var project = await _applicationDbContext.Projects.FindAsync(id);
            project.Description=data.Description;
            project.Name=data.Name;
            project.NameUsingTech=data.NameUsingTech;   
             _applicationDbContext.Projects.Update(project);
            await _applicationDbContext.SaveChangesAsync();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
