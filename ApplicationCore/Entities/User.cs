using ApplicationCore.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class User : IdentityUser<Guid>
    {
      [PersonalData]
      public string Name { get; set; }
      [PersonalData]
      public DateTime DOB { get; set; }
      public UserStatus? Status { get; set; }
      public string Address { get; set; }
    }
}
