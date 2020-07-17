using System;
using System.Collections.Generic;
using System.Text;

namespace MU.Common.Services.Identity
{
    public interface ICurrentUserService
    {
        string UserId { get; }

        bool IsAdministrator { get; }

        bool isTranslator { get;  }
    }
}
