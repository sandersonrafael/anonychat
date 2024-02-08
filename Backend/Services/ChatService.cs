using Backend.Exceptions.ApiExceptions;
using Backend.Models;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Backend.Repositories;

namespace Backend.Services;

public class ChatService(ChatRepository repository, UserRepository userRepository)
{
    private readonly ChatRepository _repository = repository;
    private readonly UserRepository _userRepository = userRepository;

    public async Task<List<ChatResponse>> FindAllByUserId(Guid userId)
    {
        User user = await _userRepository.FindbyId(userId) ?? throw new ResourceNotFoundException("Invalid user id");
        List<Chat> chats = await _repository.FindAllByUserId(userId);
        List<ChatResponse> chatResponses = chats.Select(ChatResponse.FromChat).ToList();
        return chatResponses;
    }

    public async Task<ChatResponse> Create(ChatRequest request)
    {
        User actual = await _userRepository.FindbyId(request.ActualUserId)
            ?? throw new ResourceNotFoundException("Invalid actual user");
        User other = await _userRepository.FindbyId(request.OtherUserId)
            ?? throw new ResourceNotFoundException("Invalid other user");
        Chat? chat = await _repository.FindByUsers(actual, other);
        if (chat != null) throw new DuplicatedEntityException("Chat already exists");

        chat = await _repository.Create(actual, other);
        return ChatResponse.FromChat(chat);
    }
}
