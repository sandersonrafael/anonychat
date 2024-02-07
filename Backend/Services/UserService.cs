using Backend.Exceptions.ApplicationExceptions;
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
        return new(user.Id, user.Name, user.ProfileImg, user.CreatedAt, user.LastMessageSentAt);
    }

    public async Task<UserResponse> Create(UserRequest request)
    {
        User newUser = new() { Name = request.Name, ProfileImg = request.ProfileImg };
        User dbUser = await _repository.Create(newUser);
        return new UserResponse(dbUser.Id, dbUser.Name, dbUser.ProfileImg, dbUser.CreatedAt, dbUser.LastMessageSentAt);
    }
}
