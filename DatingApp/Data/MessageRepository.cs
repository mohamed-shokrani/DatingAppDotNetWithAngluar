using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Helper;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data;

public class MessageRepository : IMessageRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public MessageRepository(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public void AddMSG(Message message)
    {
        _context.Add(message);
    }

    public void DeleteMSG(Message message)
    {
        _context.Remove(message);
    }

    public async Task<Message> GetMessage(int messageId)
    {
      return await  _context.Messages.FindAsync(messageId);
    }

    public async Task<PageList<MessageDto>> GetMessagesForUser(MessageParams messageParams )
    {
        IQueryable<Message> query = _context.Messages
                            .OrderByDescending(m => m.MessageSent); //order them by most recent first
                           
        query = messageParams.Container switch
        {
            "Inbox" => query.Where(u => u.Recipient.UserName == messageParams.UserName),//if we are the recepient of the message and we read it and this what is gonna go back
            "Outbox" => query.Where(s => s.Sender.UserName == messageParams.UserName),
            _ => query.Where(s => s.Sender.UserName == messageParams.UserName && s.DateRead == null)
        };
        var message = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);
        return await PageList<MessageDto>.CreateAsync(message, messageParams.PageNumber, messageParams.PageSize);
    }

    public async Task<IEnumerable<MessageDto>> GetMessagesThread(string currentUserName, string RecipientUserName)
    {
        var messages =  _context.Messages
                               .Include(s => s.Sender).ThenInclude(s => s.Photos)
                               .Include(s => s.Recipient).ThenInclude(s => s.Photos)
                               .Where(m => m.Recipient.UserName == currentUserName
                                       && m.Sender.UserName == RecipientUserName
                                       || m.Recipient.UserName == RecipientUserName
                                       && m.Sender.UserName == currentUserName)
                               .OrderBy(m=>m.MessageSent).ToList();
        var unreadMessages = messages.Where(m => m.DateRead == null).ToList();
        if (unreadMessages.Any())
        {
            foreach (var item in unreadMessages)
            {
                item.DateRead = DateTime.UtcNow;
            }
          await  _context.SaveChangesAsync();
        }
        return _mapper.Map<IEnumerable<MessageDto>>(messages);   

    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() >0;
    }
}
