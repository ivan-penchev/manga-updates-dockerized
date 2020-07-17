using MU.Common.Services;
using MU.Identity.Data.Models;
using MU.Identity.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Identity.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<User>> Register(UserInputModel userInput);

        Task<Result<UserOutputModel>> Login(UserInputModel userInput);

        Task<Result> ChangePassword(string userId, ChangePasswordInputModel changePasswordInput);
    }
}
