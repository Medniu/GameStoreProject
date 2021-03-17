using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BLL.Response
{
    public class AwsResponse
    {
        public string? Message { get; set; }
        public HttpStatusCode? Status { get; set; }
        public GamesInfoDTO? gamesInfoDTO { get; set; }
    }
}
