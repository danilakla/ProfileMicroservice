using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Data;
using ProfileService.DTO;
using ProfileService.Infrastructure;
using ProfileService.Infrastructure.Repository;
using ProfileService.Models;
using ProfileService.Services;

namespace ProfileService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IRepository<CreateProjectDto, Projects> _repository;
    private readonly IProfileService _profileService;
    private readonly IdentityService _identityService;
    private readonly ApplicationDbContext _applicationDbContext;

    public ProjectController(IRepository<CreateProjectDto, Projects> repository, 
        IProfileService profileService, 
        IdentityService identityService,
        ApplicationDbContext applicationDbContext)
    {
        _repository = repository;
        _profileService = profileService;
        _identityService = identityService;
        _applicationDbContext = applicationDbContext;
    }
    [HttpGet("/get-project/{id:int}")]
    [Authorize]
    public async Task<ProjectResponseDTO> GetProject( int id)
    {
        try
        {
            var project = await _repository.GetProjects(id);

            return new ProjectResponseDTO { 
            Description= project.Description,
            Name= project.Name,
            NameUsingTech= project.NameUsingTech,
            ProjectId= project.ProjectId,
            };
        }
        catch (Exception)
        {

            throw;
        }
    }


    [HttpPost("/create-project")]
    [Authorize]
    public async Task<IActionResult> CreateProject(CreateProjectDto createProjectDto)
    {
        try
        {
            var claims = User.Identities.First();
            var id = _identityService.GetUserId(claims);
            var role = _identityService.GetUserRole(claims);

            var project = await _repository.CreateProject(createProjectDto);
            var profile=await _profileService.GetProfile(id, role);
            profile.Projects.Add(project);
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception)
        {

            throw;
        }
    }




    [HttpPut("/update-project")]
    [Authorize]
    public async Task<IActionResult> UpdateProject(UpdateProjectDTO  updateProjectDTO)
    {
        try
        {
            var projectUpdate = new CreateProjectDto { Description = updateProjectDTO.Description,
                Name = updateProjectDTO.Name,
                NameUsingTech=updateProjectDTO.NameUsingTech,
            };
            await _repository.UpdateProject(projectUpdate, updateProjectDTO.ProjectId);
            return Ok();
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpDelete("/delete-project/{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteProject(int id)
    {
        try
        {
          
            await _repository.DeleteProject(id);
            return Ok();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
