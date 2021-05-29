using API.DTOs;
using API.Entities;
using API.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
	public interface IMessageRepository
	{
		void AddGroup(Group group);

		void RemoveConnection(Connection connection);

		Task<Connection> GetConnection(string connectionId);

		Task<Group> GetMessageGroup(string groupName);

		Task<Group> GetGroupForConnetion(string connectionId);

		void AddMessage(Message message);

		void DeleteMessage(Message message);

		Task<Message> GetMessage(int id);

		Task<PagedList<MessageDto>> GetMessageForUser(MessageParams messageParams);

		Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string reciepientUsername);

		Task<bool> SaveAllAsync();
	}
}