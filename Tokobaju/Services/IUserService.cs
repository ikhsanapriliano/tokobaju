using Tokobaju.Dto;
using Tokobaju.Entities;

namespace Tokobaju.Services;

public interface IUserService 
{
    Task<User> GetById(string id);
    Task<List<User>> GetAll();
    Task<User> Update(string id, string photo, UpdateUserDto payload);
    Task<bool> ChangePassword(string id, ChangePasswordDto password);
    Task<bool> Delete(string id);
}