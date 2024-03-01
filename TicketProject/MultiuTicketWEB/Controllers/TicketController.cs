using Application.AllQuery.TicketCQRS.Commend;
using Application.AllQuery.TicketCQRS.Querry;
using Application.DTO;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace MultiuTicketWEB.Controllers
{
    public class TicketController : ApiControllerBase
    {

        [HttpPost("Ticketdetail")]
        public async Task<IActionResult> Ticketdetail([FromBody] TicketDto ticket)
        {
           var details= await Mediator.Send(new PostTicketProperty { ticketDto = ticket });
            return Ok(details);

        }

        [HttpGet("GetAllData")]
        public  async Task<IActionResult> GetAllData()
        {
            var details = await Mediator.Send(new GetTicketDetail { });
            return Ok(details);
        }

        [HttpPut("UpdateTicketDetail{Id}")]

        public async Task<IActionResult> UpdateTicketDetail(int Id , TicketModel ticketModel)
        {
            var details = await Mediator.Send(new PutTicketProperty { Id = Id, TicketModels = ticketModel });
            return Ok(details);
        }

        [HttpPut("UpdateComment{Id}")]

        public async Task<IActionResult> UpdateComment(int Id, TicketModel ticketModel)
        {
            var details = await Mediator.Send(new UpdateCommentProperty { Id = Id, TicketModels = ticketModel });
            return Ok(details);
        }
        
        
        [HttpDelete("DeleteTicketDetail{Id}")]

        public async Task<IActionResult> DeleteTicketDetail(int Id)
        {
            var details = await Mediator.Send(new DeleteTicketProperty { Id = Id });
            return Ok(details);
        }
    }
}
