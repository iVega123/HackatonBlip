using Azure.AI.OpenAI;
using PocGPT.Core.Model;

namespace PocGPT.Core.InterfaceService
{
    public interface ITicketsService
    {
        public Task<IEnumerable<Messages>> GetTickeMessages(Guid IdDoTicket);

        public Task<ChatCompletions> GetResultChatGpt(List<Messages> messages);
    }
}
