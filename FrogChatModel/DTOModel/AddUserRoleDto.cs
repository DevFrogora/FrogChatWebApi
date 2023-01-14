﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatModel.DTOModel
{
    public class AddUserRoleDto
    {
        [Required(ErrorMessage ="Email is required")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage ="Role Name is required")]
        public string RoleName { get; set; }

    }
}
