﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.Models
{
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}