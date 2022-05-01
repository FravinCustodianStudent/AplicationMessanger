using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplicationMessanger.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace AplicationMessanger.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AplicationMessangerUser class
public class AplicationMessangerUser : IdentityUser
{
    public List<Chat> Chats { get; set; }
}

