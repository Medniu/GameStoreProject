using BLL.DTO;
using BLL.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IS3Service
    {
        Task<AwsResponse> UploadPictureToAws(IFormFile gamesInfoDTO);        
    }
}
