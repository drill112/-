using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите логин")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Логин должен быть от 3 до 50 символов")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Пароль должен быть длиннее 5 символов")]
    public string Pwd { get; set; } = string.Empty;
}


