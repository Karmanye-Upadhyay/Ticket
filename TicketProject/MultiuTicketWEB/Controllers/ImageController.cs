using Application.AllQuery.ImageCQRS.Commend;
using Application.AllQuery.TicketCQRS.Commend;
using Application.DTO;
using InfraStructure.DbFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace MultiuTicketWEB.Controllers
{
    public class ImageController : ApiControllerBase
    {

        private readonly IHostingEnvironment environment; // Change IWebHostEnvironment to IHostingEnvironment
        public ImageController(IHostingEnvironment environment, DbClass context) // Change IWebHostEnvironment to IHostingEnvironment
        {
            this.environment = environment;
        }

        [HttpPut("Imagedetail{Id}")]
        public async Task<IActionResult> CreateImagedetail(IFormFileCollection formfile, int Id)
        {
            string localHost = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            string filePath = GetFilepath();

            var details = await Mediator.Send(new PostImageProperty { Formfile = formfile, Id =Id,LocalHost = localHost, FilePath = filePath });
            return Ok(details);

        }


        [HttpPut("UpdateImagedetail{Id}")]
        public async Task<IActionResult> UpdateImagedetail(IFormFileCollection formfile, int Id)
        {
            string localHost = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            string filePath = GetFilepath();

            var details = await Mediator.Send(new UpdateImageProperty { Formfile = formfile, Id = Id, LocalHost = localHost, FilePath = filePath });
            return Ok(details);

        }

        [HttpDelete("DeleteImage{Name}")]

        public async Task<IActionResult> DeleteImage(string Name)
        {
            var details = await Mediator.Send(new DeleteImageProperty { ImgName = Name });
            return Ok(details);
        }

        [NonAction]
       public string GetFilepath() 
        {
            return this.environment.WebRootPath + "\\Images\\Multi\\";

        }
    }
}
