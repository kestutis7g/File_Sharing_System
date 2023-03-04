using System.ComponentModel.DataAnnotations;

namespace Forum.API.Models;

public class GroupUserModel
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }


    [Range(typeof(bool), "false", "true", ErrorMessage = "Must be valid boolean.")]
    public bool IsAdmin { get; set; }

}
