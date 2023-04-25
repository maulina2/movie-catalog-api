using System.ComponentModel.DataAnnotations;

namespace Cinema.Models;

public class UserShortModel
{
    public UserShortModel(User user)
    {
        UserId = user.Id;
        NickName = user.NickName;
        Avatar = user.AvatarLink;
    }

    [Key] public Guid UserId { get; set; }

    public string? NickName { get; set; }
    public string? Avatar { get; set; }
}