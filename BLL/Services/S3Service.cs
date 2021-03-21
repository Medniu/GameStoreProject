using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Response;
using BLL.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;
        private readonly IOptions<AwsSettings> _config;

        public S3Service(IAmazonS3 client, IOptions<AwsSettings> config)
        {
            _client = client;
            _config = config;
        }

        public async Task<AwsResponse> UploadPictureToAws(IFormFile formFile)
        {
            try
            {
                using (var newMemoryStream = new MemoryStream())
                {

                    formFile.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = formFile.FileName,
                        BucketName = _config.Value.BucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(_client);
                    await fileTransferUtility.UploadAsync(uploadRequest);

                    var expiryUrlRequest = new GetPreSignedUrlRequest()
                    {
                        BucketName = _config.Value.BucketName,
                        Key = formFile.FileName,
                        Expires = DateTime.Now.AddHours(1)
                    };

                    string logoUrl = _client.GetPreSignedURL(expiryUrlRequest);
                    return new AwsResponse
                    {
                        PictureUrl = logoUrl
                    };
                }
            }
            catch (AmazonS3Exception amazonException)
            {
                return new AwsResponse
                {
                    Message = amazonException.Message,
                    Status = amazonException.StatusCode,
                    PictureUrl = null
                };
            }        
        }               
    }
}
