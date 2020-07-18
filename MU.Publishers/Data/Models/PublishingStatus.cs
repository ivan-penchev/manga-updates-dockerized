using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Data.Models
{
    public enum PublishingStatus
    {
        Ongoing = 1,
        Completed = 2,
        Hiatus = 3,
        Discontinued = 4,
        ToBePublished = 5
    }
}
