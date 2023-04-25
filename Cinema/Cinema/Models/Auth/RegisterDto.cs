using System.ComponentModel.DataAnnotations;

namespace Cinema.Models;

public class RegisterDto
{
    [Required(ErrorMessage = "Не указано имя пользователя")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина имени должна быть в диапазоне от {3} до {50} символов")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Не указан логин пользователя")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина логина должна быть в диапазоне от {4} до {50} символов")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [StringLength(32, MinimumLength = 8, ErrorMessage = "Недопустимая длина пароля")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Не указана почта")]
    [EmailAddress(ErrorMessage = "Введите почту")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Не указана дата рождения")]
    public DateTime BirthDate { get; set; }

    [Required] public Gender Gender { get; set; }
}