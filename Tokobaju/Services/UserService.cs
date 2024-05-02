using Tokobaju.Dto;
using Tokobaju.Entities;
using Tokobaju.Exceptions;
using Tokobaju.Repositories;
using Tokobaju.Utils;

namespace Tokobaju.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IPersistence _persistence;
    private readonly BcryptUtil _bcryptUtil;

    public UserService(IRepository<User> repository, IPersistence persistence, BcryptUtil bcryptUtil)
    {
        _repository = repository;
        _persistence = persistence;
        _bcryptUtil = bcryptUtil;
    }

    public async Task<bool> ChangePassword(string id, ChangePasswordDto password)
    {
        var user = await GetById(id);
        var validate = _bcryptUtil.Validate(password.OldPassword, user.Password);
        if (!validate)
        {
            throw new UnauthorizedException("oldPassword wrong");
        }

        if (password.NewPassword != password.ConfirmNewPassword)
        {
            throw new BadRequestException("newPassword and confirmNewPassword didn't match");
        }

        user.Password = _bcryptUtil.HashPassword(password.NewPassword);
        user.UpdatedAt = DateTime.Now;
        _repository.Update(user);
        await _persistence.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(string id)
    {
        var user = await GetById(id);
        _repository.Delete(user);

        return true;
    }

    public async Task<List<User>> GetAll()
    {
        var users = await _repository.FindAllAsync();
        foreach (var user in users)
        {
            user.Password = "";
        }

        return users;
    }

    public async Task<User> GetById(string id)
    {
        var user = await _repository.FindByIdAsync(Guid.Parse(id));
        if (user == null)
        {
            throw new NotFoundException($"user with id {id} not found");
        }

        return user;
    }

    public async Task<User> Update(string id, string photo, UpdateUserDto payload)
    {
        var user = await GetById(id);

        if (payload.Name != null) user.Name = payload.Name;
        if (payload.Email != null) user.Email = payload.Email;
        if (photo != "")
        {
            if (!user.Photo.Contains("unknown")) File.Delete(user.Photo);
            user.Photo = photo;
        }
        user.UpdatedAt = DateTime.Now;

        var newUser = _repository.Update(user);
        await _persistence.SaveChangesAsync();
        newUser.Password = "";

        return newUser;
    }
}