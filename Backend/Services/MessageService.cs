using Backend.Exceptions.ApiExceptions;
using Backend.Models;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Backend.Repositories;

namespace Backend.Services;

public class MessageService(MessageRepository repository, ChatRepository chatRepository, UserRepository userRepository)
{
    private readonly MessageRepository _repository = repository;
    private readonly ChatRepository _chatRepository = chatRepository;
    private readonly UserRepository _userRepository = userRepository;

    public async Task<List<MessageResponse>> FindAllUserMessages(Guid id)
    {
        User dbUser = await _userRepository.FindbyId(id) ?? throw new ResourceNotFoundException("User not found");
        List<Message> dbMessages = await _repository.FindAllUserMessages(dbUser);
        List<MessageResponse> messageResponses = dbMessages.Select(MessageResponse.FromMessage).ToList();
        return messageResponses;
    }

    public async Task<List<MessageResponse>> FindAllUserMessagesFromAChat(Guid id, ChatRequest request)
    {
        User dbUser = await _userRepository.FindbyId(id) ?? throw new ResourceNotFoundException("User not found");
        if (dbUser.Id != request.ActualUserId) throw new UnauthorizedException("Invalid request");

        Chat chat = Chat.FromChatRequest(request);

        List<Message> dbMessages = await _repository.FindAllUserMessagesFromAChat(chat);
        List<MessageResponse> messageResponses = dbMessages.Select(MessageResponse.FromMessage).ToList();
        return messageResponses;
    }

    public async Task<MessageResponse> Create(MessageRequest request)
    {
        User actualUser = await this.GetUserOrThrowException(request.ActualUserId, "Actual user not found");
        User otherUser = await this.GetUserOrThrowException(request.OtherUserId, "Other user not found");

        Chat chat1 = await this.FindOrCreateChat(actualUser, otherUser);
        Chat chat2 = await this.FindOrCreateChat(otherUser, actualUser);

        DateTimeOffset now = DateTimeOffset.UtcNow;

        Message actualMessage = new() { Author = actualUser, Content = request.Content,
            Chat = chat1, CreatedAt = now, UpdatedAt = now };
        Message otherMessage = new() { Author = actualUser, Content = request.Content, 
            Chat = chat2, CreatedAt = now, UpdatedAt = now };

        actualMessage = await _repository.Create(actualMessage);
        await _repository.Create(otherMessage);

        return MessageResponse.FromMessage(actualMessage);
    }

    public async Task<MessageResponse> Update(long id, MessageRequest request)
    {
        User actualUser = await this.GetUserOrThrowException(request.ActualUserId, "Actual user not found");
        User otherUser = await this.GetUserOrThrowException(request.OtherUserId, "Other user not found");

        Message message = new()
        {
            Id = id,
            Author = actualUser,
            Content = request.Content,
            Chat = new() { ActualUserId = actualUser.Id, OtherUserId = otherUser.Id },
        };
        message = await _repository.Update(message) ?? throw new ResourceNotFoundException("Message not found");

        return MessageResponse.FromMessage(message);
    }

    public async Task DeleteById(long id, ChatRequest request)
    {
        User actualUser = await this.GetUserOrThrowException(request.ActualUserId, "Actual user not found");
        User otherUser = await this.GetUserOrThrowException(request.OtherUserId, "Other user not found");

        Chat chat = Chat.FromChatRequest(request);
        Message dbMessage = await _repository.FindById(id) ?? throw new ResourceNotFoundException("Message not found");

        if (dbMessage.Chat.ActualUserId != actualUser.Id || dbMessage.Chat.OtherUserId != otherUser.Id)
            throw new UnauthorizedException("Invalid message");

        if (await _repository.DeleteById(id) == null) throw new ResourceNotFoundException("Message not found");
    }

    private async Task<User> GetUserOrThrowException(Guid userId, string message) => await _userRepository.FindbyId(userId)
        ?? throw new ResourceNotFoundException(message);

    private async Task<Chat> FindOrCreateChat(User actual, User other)
    {
        return await _chatRepository.FindByUsers(actual, other) ?? await _chatRepository.Create(actual, other);
    }
}
