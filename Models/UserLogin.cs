using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Login_user.Models;

public partial class UserLogin
{
    [Key]
    public int? Id { get; set; }

    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Gmail { get; set; }
    [Required]
    
    public string? Password { get; set; }
}
