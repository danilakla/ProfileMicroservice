using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Data;
using ProfileService.DTO;
using ProfileService.Infrastructure.Repository;
using ProfileService.Infrastructure;
using ProfileService.Models;
using ProfileService.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProfileService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SkillController : ControllerBase
{

    private readonly IRepository<CreateSkillDto, Skills> _repository;
    private readonly IProfileService _profileService;
    private readonly IdentityService _identityService;
    private readonly ApplicationDbContext _applicationDbContext;

    public SkillController(IRepository<CreateSkillDto, Skills> repository,
        IProfileService profileService,
        IdentityService identityService,
        ApplicationDbContext applicationDbContext)
    {
        _repository = repository;
        _profileService = profileService;
        _identityService = identityService;
        _applicationDbContext = applicationDbContext;
    }


    [HttpGet("/get-skill/{id:int}")]
    [Authorize]
    public async Task<SkillResponseDTO> GetSkill(int id)
    {
        try
        {
            var skill = await _repository.GetProjects(id);

            return new SkillResponseDTO
            {
              SkillId= skill.SkillId,
              Text=skill.Text
            };
        }
        catch (Exception)
        {

            throw;
        }
    }


    [HttpPost("/create-skill")]
    [Authorize]
    public async Task<IActionResult> CreateSkill(CreateSkillDto  createSkillDto)
    {
        try
        {
            var claims = User.Identities.First();
            var id = _identityService.GetUserId(claims);
            var role = _identityService.GetUserRole(claims);

            var skill= await _repository.CreateProject(createSkillDto);
            var profile = await _profileService.GetProfile(id, role);
            profile.Skills.Add(skill);
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception)
        {

            throw;
        }
    }




    [HttpPut("/update-skill")]
    [Authorize]
    public async Task<IActionResult> UpdateSkill(UpdateSkillDTO updateSkillDTO)
    {
        try
        {
            var projectUpdate = new CreateSkillDto
            {
               Text= updateSkillDTO.Text
               
            };
            await _repository.UpdateProject(projectUpdate, updateSkillDTO.SkillId);
            return Ok();
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpDelete("/delete-skill/{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteSkill(int id)
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
