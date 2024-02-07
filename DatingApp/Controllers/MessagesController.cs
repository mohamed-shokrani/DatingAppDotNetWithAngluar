using AutoMapper;
using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Extensions;
using DatingApp.Helper;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessagesController(IUserRepository userRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto messageDto)
        {
            var userName = User.GetuserName();
            if (StringComparer.OrdinalIgnoreCase.Equals(userName, messageDto.RecipientUserName))
                return BadRequest("You cannot send a message to yourself."); // Example response
            var sender = await _userRepository.GetUserByNameAsync(userName);
            var recipient = await _userRepository.GetUserByNameAsync(messageDto.RecipientUserName);
            if (recipient is null)
                return NotFound();
            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUserName = sender.UserName,
                RecipientName = recipient.UserName,
                Content = messageDto.Content

            };
            _messageRepository.AddMSG(message);
            return await _messageRepository.SaveChangesAsync()
                                           ? Ok(_mapper.Map<MessageDto>(message))
                                           : BadRequest("Failed to send the message");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>>
            GetMessagesForUsers([FromQuery] MessageParams messageParams)
        {
            messageParams.UserName = User.GetuserName();
            var message = await _messageRepository.GetMessagesForUser(messageParams);
            Response.AddPaginationHeader(message.CurrentPage, message.PageSize, message.TotatCount, message.TotalPage);
            return Ok(message);
        }
        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string userName)
        {
            var currentUserName = User.GetuserName();
            return Ok(await _messageRepository.GetMessagesThread(currentUserName, userName));
        }
    }
}
