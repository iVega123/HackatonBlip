using AutoMapper;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using PocGPT.Core.Dtos;
using PocGPT.Core.InterfaceService;
using PocGPT.Core.Model;

namespace PocGPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ITicketsService _ticketService;
        private readonly IMapper _mapper;
        private readonly ILogger<WeatherForecastController> _logger;

        public TicketController(ITicketsService ticketService, ILogger<WeatherForecastController> logger, IMapper mapper)
        {
            this._ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService)); ;
            this._logger = logger;
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name = "getTickets")]
        public async Task<IActionResult> BuscarTicketAsync(Guid idTicket)
        {
            try
            {
                var ticketMessages = await _ticketService.GetTickeMessages(idTicket);

                return (IActionResult)ticketMessages;;
            }
            catch (ArgumentException e)
            {

                throw e;
            }

        }

        [HttpPost]
        public async Task<ChatCompletions> ResumeGPTAsync([FromBody] Root entradaObj)
        {
            try
            {

                var itens = entradaObj.Resource.Items.ToList();
                var messages = itens.Select(item => _mapper.Map<Item, Messages>(item)).ToList();
                var result = await _ticketService.GetResultChatGpt(messages);
                return result;
            }
            catch (ArgumentException e)
            {

                throw e;
            }

        }
    }
}
