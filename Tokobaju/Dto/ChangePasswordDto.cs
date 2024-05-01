using System.ComponentModel.DataAnnotations;

namespace Tokobaju.Dto;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "oldPassword Required")]
    [MinLength(6, ErrorMessage = "oldPassword min length 6")]
    public string OldPassword { get; set; } = "";

    [Required(ErrorMessage = "newPassword Required")]
    [MinLength(6, ErrorMessage = "newPassword min length 6")]
    public string NewPassword { get; set;} = "";

    [Required(ErrorMessage = "confirmNewPassword Required")]
    [MinLength(6, ErrorMessage = "confirmNewPassword min length 6")]
    public string ConfirmNewPassword { get; set; } = "";
}