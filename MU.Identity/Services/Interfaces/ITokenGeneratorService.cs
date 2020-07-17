using MU.Identity.Data.Models;
using System.Collections.Generic;
namespace MU.Identity.Services.Interfaces
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(User user, IEnumerable<string> roles = null);
    }
}
