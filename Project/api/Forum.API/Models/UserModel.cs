using NLog.Config;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Forum.API.Models;
public class UserModel
{
    [StringLength(32, ErrorMessage = "Length can't be longer than 32.")]
    public string? Login { get; set; }


    [StringLength(32, ErrorMessage = "Length can't be longer than 32.")]
    public string? Password { get; set; }


    [EnumDataType(typeof(UserRole), ErrorMessage = "Must be valid user role (Admin, User, Guest)")]
    public string? Role { get; set; }


    [StringLength(32, ErrorMessage = "Length can't be longer than 32.")]
    public string? Name { get; set; }


    [StringLength(32, ErrorMessage = "Length can't be longer than 32.")]
    public string? Lastname { get; set; }


    [EmailAddress(ErrorMessage = "Must be valid email address.")]
    public string? Email { get; set; }


    [Phone(ErrorMessage = "Must be valid phone number.")]
    public string? Phone { get; set; }


    public string? ProfilePicture { get; set; }


    [FileExtensions(ErrorMessage = "Must be valid file extention.")]
    public string? FileMime { get; set; }


    [StringLength(1024, ErrorMessage = "Length can't be longer than 1024.")]
    public string? Description { get; set; }


    [Range(typeof(DateTimeOffset), "1950-01-01T00:00:00+00:00", "2200-01-01T00:00:00+00:00", ErrorMessage = "Must be valid date between 1950 and 2200.")]
    public DateTimeOffset? CreatedAt { get; set; }


    [Range(typeof(DateTimeOffset), "1950-01-01T00:00:00+00:00", "2200-01-01T00:00:00+00:00", ErrorMessage = "Must be valid date between 1950 and 2200.")]
    public DateTimeOffset? ModifiedAt { get; set; }


    [Range(typeof(bool), "false", "true", ErrorMessage = "Must be valid boolean.")]
    public bool? Deleted { get; set; }


    [Range(typeof(bool), "false", "true", ErrorMessage = "Must be valid boolean.")]
    public bool? Banned { get; set; }


}

public enum UserRole
{
    Admin,
    User,
    Guest
}
