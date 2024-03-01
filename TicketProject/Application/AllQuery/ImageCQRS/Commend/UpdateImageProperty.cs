using Application.HeplerClass;
using AutoMapper;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.AllQuery.ImageCQRS.Commend
{
    public class UpdateImageProperty : IRequest<ApiResponse>
    {
        public IFormFileCollection Formfile;
        public int Id;
        public string LocalHost;
        public string FilePath;
    }

    public class UpdateImagePropertyHandler : IRequestHandler<UpdateImageProperty, ApiResponse>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;


        public UpdateImagePropertyHandler(IApplicationDBContext ApplicationdbContext, IMapper mapper)
        {
            _context = ApplicationdbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UpdateImageProperty request, CancellationToken cancellationToken)
        {


            ApiResponse response = new ApiResponse();
            try
            {

                
                foreach (var file in request.Formfile)
                {
                    var filename = file.FileName;
                    string[] fileNameParts = file.FileName.Split('.');


                    var modifiedName = fileNameParts[0] + "code" + request.Id + (request.Id + request.Id * request.Id + request.Id) + "." + fileNameParts[1];
                    //  root\Image\batman.jpg
                    var existingMultiAttachment = await _context.MultiAttachmentTable.FirstOrDefaultAsync(m => m.NewName == modifiedName);

                    if (existingMultiAttachment == null)
                    {

                        if (System.IO.File.Exists(modifiedName))
                        {
                            System.IO.File.Delete(modifiedName);

                        }
                        var newimage = request.FilePath + modifiedName;
                        string _ImagePath = request.LocalHost + "/Images/Multi/" + modifiedName;
                        Console.WriteLine(_ImagePath);
                        // for storing the file convert them into file
                        using (FileStream stream = System.IO.File.Create(newimage)) // Creating a file stream to save the image
                        {
                            await file.CopyToAsync(stream);// Copying the uploaded image file to the stream

                            response.ImagePath = filename;
                            response.Extension = fileNameParts[1];
                            response.ResponseCode = 200;// Setting the response code to 200 (success)
                            response.Result = _ImagePath;// Setting the result message to "pass"
                            var addData = new MultiAttachment()

                            {
                                Attachment_Name = _ImagePath,
                                OrignalName = fileNameParts[0],
                                Extension = "." + fileNameParts[1],
                                TickId = request.Id,
                                NewName = modifiedName,

                            };
                            await _context.MultiAttachmentTable.AddAsync(addData);
                            await _context.SaveChangesAsync();
                        }
                        // Return a successful response
                    }

                }
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 500; // Internal Server Error
                response.ErrorMessage = ex.Message;
                return (response); // Return an error response
            }
        }
        
    }


}

