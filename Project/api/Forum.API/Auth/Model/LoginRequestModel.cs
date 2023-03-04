using NLog.Config;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Forum.API.Auth.Model;
public class LoginRequestModel
{
    [StringLength(32, ErrorMessage = "Length can't be longer than 32.")]
    public string? Login { get; set; }


    [StringLength(32, ErrorMessage = "Length can't be longer than 32.")]
    public string? Password { get; set; }

}
