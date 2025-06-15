using System.Numerics;
using BitesPlanner.Data.Entities;
using BitesPlanner.Data.Repositories;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAllAsync() 
    { 
        return await _userRepository.GetAllUsersAsync(); 
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null ) throw new ApplicationException("User not found");
        return user;
    }

    public async Task AddAsync(User user)
    {
        if (user == null) throw new ApplicationException("User cannot be null");
        await ValidateUser(user);
        await _userRepository.AddUserAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        var dbUser = await _userRepository.GetUserByIdAsync(user.Id);
        if (dbUser == null) throw new ApplicationException("User with the provided id not found");

        await ValidateUser(user);
        dbUser.UserName = user.UserName;
        dbUser.Email = user.Email;
        dbUser.Role = user.Role;
        dbUser.IsActive = user.IsActive;

        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        await _userRepository.DeleteUserAsync(id);
    }

    private async Task ValidateUser(User user)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
        if (existingUser != null && existingUser.Id != user.Id) throw new ApplicationException($"User with username {user.UserName} already exists");
        if (user.UserName.Length < 3 || user.UserName.Length > 50) throw new ApplicationException("Username must be between 3 and 50 characters");
        if (string.IsNullOrWhiteSpace(user.UserName)) throw new ApplicationException("User name cannot be empty");
        if (user.Email == null || !user.Email.Contains("@")) throw new ApplicationException("Invalid email address");
        if (!string.IsNullOrWhiteSpace(user.Role))
        {
            var allowedRoles = new[] { "Admin", "User"};
            if (!allowedRoles.Contains(user.Role))throw new ApplicationException($"Invalid role '{user.Role}'");
        }

    }
}
