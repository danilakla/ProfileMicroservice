using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.DTO;
using ProfileService.Models;
using ProfileService.Proto;
using System;

namespace ProfileService.Services;

public class ProfilesService : IProfileService
{


    private readonly ApplicationDbContext _applicationDbContext;
    private readonly Profile.ProfileClient _profileClient;

    public ProfilesService(ApplicationDbContext applicationDbContext, Profile.ProfileClient profileClient)
    {
        _applicationDbContext = applicationDbContext;
        _profileClient = profileClient;
    }



    public async Task ChangeBackgroundPhotoProfile(int profileId, string role, IFormFile photo)
    {
        try
        {
     var user = await _applicationDbContext.Profiles.Where(e => e.UserId == profileId && e.Role == role).FirstOrDefaultAsync();
        
        var photoBytes = SavePhoto(photo);
        user.BackgroundProfile = photoBytes;
        _applicationDbContext.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
   
    }

    public async Task ChangePhotoProfile(int profileId, string role, IFormFile photo)
    {
        try
        {
            var user = await _applicationDbContext.Profiles.Where(e => e.UserId == profileId && e.Role == role).FirstOrDefaultAsync();

            var photoBytes =  SavePhoto(photo);
            await _profileClient.UpdateIconAsync(new Icon() { Email = user.Email, Photo = ByteString.CopyFrom(photoBytes) });

            user.Photo = photoBytes;
            _applicationDbContext.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    
    
    }
    private static byte[] value = new byte[1000];
    public async Task<Profiles> GetProfile(int profileId, string role)
    {
        try
        {
            var user = await _applicationDbContext.Profiles
                .Include(e => e.Skills)
                .Include(e => e.Projects)
                .Where(e => e.UserId == profileId && e.Role == role).FirstOrDefaultAsync();
            
            user.Photo = GetPhoto(Convert.ToBase64String(user.Photo?? value));
            user.BackgroundProfile= GetPhoto(Convert.ToBase64String(user.BackgroundProfile));

            return user;
        }
        catch (Exception)
        {

            throw;
        }
    }

    private byte[] SavePhoto(IFormFile fileObj)
    {
        if(fileObj.Length>0)
        {
            using(var stream = new MemoryStream())
            {
                fileObj.CopyTo(stream);
                var fileBytes=stream.ToArray();

                return fileBytes;
            }

        }
        else
        {
            throw new Exception("doesn't file");
        }
    }

    private byte[] GetPhoto(string sBase64String)
    {
        byte[] bytes = null;
        if (!string.IsNullOrEmpty(sBase64String))
        {
            bytes=Convert.FromBase64String(sBase64String);
        }
        return bytes;
    }

    public async Task UpdateProfile(int profileId, string role, UpdateProfileDTO updateProfileDTO)
    {

        await ChangePhotoProfile(profileId, role, updateProfileDTO.Icon);
        await ChangeBackgroundPhotoProfile(profileId, role, updateProfileDTO.Bg);

    }
}
