using Backend.Exceptions.ApiExceptions;
using Backend.Models;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Backend.Repositories;

namespace Backend.Services;

public class UserService(UserRepository repository)
{
    private readonly UserRepository _repository = repository;

    public async Task<UserResponse> FindbyId(Guid id)
    {
        User? user = await _repository.FindbyId(id) ?? throw new ResourceNotFoundException("User not found");
        return UserResponse.FromUser(user);
    }

    public async Task<UserResponse> Create(UserRequest request)
    {
        User user = User.FromUserRequest(request);
        user = await _repository.Create(user);
        return UserResponse.FromUser(user);
    }

    public async Task<UserResponse> Update(Guid id, UserRequest request)
    {
        User dbUser = await _repository.FindbyId(id) ?? throw new ResourceNotFoundException("User not found");
        if (dbUser.PasswordHash != request.Password) throw new UnauthorizedException("Invalid credentials");

        dbUser = User.FromUserRequest(request);
        dbUser = await _repository.Update(id, dbUser) ?? throw new ResourceNotFoundException("User not found");
        return UserResponse.FromUser(dbUser);
    }
}
