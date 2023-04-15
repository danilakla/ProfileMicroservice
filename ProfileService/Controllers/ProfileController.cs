using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.DTO;
using ProfileService.Infrastructure;
using ProfileService.Models;
using ProfileService.Services;
using System.Security.Claims;

namespace ProfileService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly IdentityService _identityService;

    public ProfileController(IProfileService profileService, IdentityService identityService)
    {
        _profileService = profileService;
        _identityService = identityService;
    }


    [HttpPut("/UpdatePhotoProfile")]
    [Authorize]
    public async Task<IActionResult> UpdatePhotoProfile(IFormFile photo)
    {
        try
        {
            var claims = User.Identities.First();
            var id = _identityService.GetUserId(claims);
            var role = _identityService.GetUserRole(claims);
            await _profileService.ChangePhotoProfile(id, role,photo);
            var user = await _profileService.GetProfile(id, role);
            return Ok(user.Photo);

        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPut("/UpdateProfileBackPhoto")]
    [Authorize]
    public async Task<IActionResult> UpdatePhotoBackroundProfile(IFormFile photo)
    {
        try
        {
            var claims = User.Identities.First();
            var id = _identityService.GetUserId(claims);
            var role = _identityService.GetUserRole(claims);
            await _profileService.ChangeBackgroundPhotoProfile(id, role, photo);
            return Ok();

        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpGet("/GetProfile")]
    [Authorize]
    public async Task<ActionResult<Profiles>> GetProfile()
    {
        try
        {
           
            var claims = User.Identities.First();
            var id= _identityService.GetUserId(claims);
            var role= _identityService.GetUserRole(claims);
            var profile = await _profileService.GetProfile(id, role);
            return profile; 

        }
        catch (Exception)
        {

            throw;
        }
    }
    [HttpPut("/UpdateProfile")]
    [Authorize]
    [DisableRequestSizeLimit]
    public async Task UpdateProfile(UpdateProfileDTO updateProfileDTO)
    {
        try
        {


            var claims = User.Identities.First();
            var id = _identityService.GetUserId(claims);
            var role = _identityService.GetUserRole(claims);
            
            await _profileService.UpdateProfile(id, role, updateProfileDTO);

        }
        catch (Exception)
        {

            throw;
        }
    }



    public record class UpdatePhotoDTO
    {
        public IFormFile Photo { get; set; }
    }


}
