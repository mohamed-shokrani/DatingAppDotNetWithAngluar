using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Helper;

namespace DatingApp.Interfaces;
public interface IMessageRepository
{
     void AddMSG (Message message);
     void DeleteMSG(Message message);
    Task<Message> GetMessage(int messageId);
    Task<PageList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
    Task<IEnumerable<MessageDto>> GetMessagesThread(string currentUserName, string RecipientUserName);
    Task<bool> SaveChangesAsync();    



}
