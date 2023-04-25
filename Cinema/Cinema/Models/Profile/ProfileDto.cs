using System.ComponentModel.DataAnnotations;

namespace Cinema.Models;

public class ProfileDto
{
    public ProfileDto(Guid id, string? nickName, string email, string? avatarLink, string name, DateTime birthDate,
        Gender gender)
    {
        Id = id;
        NickName = nickName;
        Email = email;
        AvatarLink = avatarLink;
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
    }

    public ProfileDto()
    {
        Email = "";
        Name = "";
    }

    public ProfileDto(User user)
    {
        Id = user.Id;
        Email = user.Email;
        Name = user.Name;
        BirthDate = user.BirthDate;
        Gender = user.Gender;
        AvatarLink = user.AvatarLink;
        NickName = user.NickName;
    }

    public Guid Id { get; set; }

    [StringLength(10, MinimumLength = 3, ErrorMessage = "Длина имени должна быть в диапазоне от {3} до {10} символов")]
    public string? NickName { get; set; }

    [Required(ErrorMessage = "Не указана почта")]
    [EmailAddress(ErrorMessage = "Введите почту")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    public string? AvatarLink { get; set; }

    [Required(ErrorMessage = "Не указано имя пользователя")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина имени должна быть в диапазоне от {3} до {50} символов")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Не указана дата рождения")]
    public DateTime BirthDate { get; set; }

    public Gender Gender { get; set; }
}