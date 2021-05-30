using API.DTOs;
using API.Entities;
using API.Helper;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
	public class MessageRepository : IMessageRepository
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;

		public MessageRepository(DataContext dataContext, IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public void AddGroup(Group group)
		{
			_dataContext.Groups.Add(group);
		}

		public void AddMessage(Message message)
		{
			_dataContext.Messages.Add(message);
		}

		public void DeleteMessage(Message message)
		{
			_dataContext.Messages.Remove(message);
		}

		public async Task<Connection> GetConnection(string connectionId)
		{
			return await _dataContext.Connections.FindAsync(connectionId);
		}

		public async Task<Group> GetGroupForConnetion(string connectionId)
		{
			return await _dataContext.Groups
				.Include(c => c.Connections)
				.Where(c => c.Connections.Any(x => x.ConnectionId == connectionId))
				.FirstOrDefaultAsync();
		}

		public async Task<Message> GetMessage(int id)
		{
			return await _dataContext.Messages
				.Include(u => u.Sender)
				.Include(u => u.Recipient)
				.SingleOrDefaultAsync(x => x.Id == id);
		}

		public async Task<PagedList<MessageDto>> GetMessageForUser(MessageParams messageParams)
		{
			var query = _dataContext.Messages
				.OrderByDescending(m => m.MessageSent)
				.ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
				.AsQueryable();
			query = messageParams.Container switch
			{
				"Inbox" => query.Where(u => u.RecipientUserName == messageParams.Username && !u.RecipientDeleted),
				"Outbox" => query.Where(u => u.SenderUserName == messageParams.Username && !u.SenderDeleted),
				_ => query.Where(u => u.RecipientUserName == messageParams.Username && u.DateRead == null && !u.RecipientDeleted)
			};

			return await PagedList<MessageDto>.CreateAsync(query, messageParams.PageNumber, messageParams.PageSize);
		}

		public async Task<Group> GetMessageGroup(string groupName)
		{
			return await _dataContext.Groups
				.Include(x => x.Connections)
				.FirstOrDefaultAsync(x => x.Name == groupName);
		}

		public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string reciepientUsername)
		{
			var messages = await _dataContext.Messages
				.Where(m =>
					m.Recipient.UserName == currentUsername &&
					m.Sender.UserName == reciepientUsername &&
					!m.RecipientDeleted
					||
					m.Recipient.UserName == reciepientUsername &&
					m.Sender.UserName == currentUsername &&
					!m.SenderDeleted
					)
				.OrderByDescending(m => m.MessageSent)
				.ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
				.ToListAsync();

			var unreadMessages = messages.Where(m => m.DateRead == null && m.RecipientUserName == currentUsername).ToList();

			if (unreadMessages.Any())
			{
				foreach (var message in unreadMessages)
					message.DateRead = DateTime.Now;
			}
			return messages;
		}

		public void RemoveConnection(Connection connection)
		{
			_dataContext.Connections.Remove(connection);
		}

	}
}