using System.Security.Claims;

namespace ProfileService.Infrastructure;

public class IdentityService
{
    public int GetUserId(ClaimsIdentity claimsIdentity)
    {
        try
        {

            var reqString = claimsIdentity.Claims.Where(c => c.Type == "ProfileId").FirstOrDefault();

            return int.Parse(reqString.Value);
        }
        catch (Exception e)
        {

            throw e;
        }
    }

    public string GetUserRole(ClaimsIdentity claimsIdentity)
    {
        try
        {

            var reqString = claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();

            return reqString.Value;
        }
        catch (Exception e)
        {

            throw e;
        }
    }
}
