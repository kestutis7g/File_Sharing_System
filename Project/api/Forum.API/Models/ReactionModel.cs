using System.ComponentModel.DataAnnotations;

namespace Forum.API.Models;
public class ReactionModel
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Picture { get; set; }

}
