using ProfileService.DTO;
using ProfileService.Models;

namespace ProfileService.Services;

public interface IProfileService
{
    Task<Profiles> GetProfile(int profileId, string role);
    Task UpdateProfile(int profileId, string role, UpdateProfileDTO updateProfileDTO);

    Task ChangePhotoProfile(int profileId, string role, IFormFile photo);
    Task ChangeBackgroundPhotoProfile(int profileId, string role, IFormFile photo);

    

}
