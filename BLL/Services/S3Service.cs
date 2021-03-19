using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Response;
using BLL.Settings;
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
        private readonly IMapper _mapper;
        public S3Service(IAmazonS3 client, IOptions<AwsSettings> config, IMapper mapper)
        {
            _client = client;
            _config = config;
            _mapper = mapper;
        }

        public async Task<AwsResponse> UploadPictureToAws(CreateGameModelDTO createGamesModelDTO)
        {           
            var gamesInfoDTO = _mapper.Map<CreateGameModelDTO, GamesInfoDTO>(createGamesModelDTO);

            try
            {
                using (var newMemoryStream = new MemoryStream())
                {

                    createGamesModelDTO.Logo.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = createGamesModelDTO.Logo.FileName,
                        BucketName = _config.Value.BucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(_client);
                    await fileTransferUtility.UploadAsync(uploadRequest);

                    var expiryUrlRequest = new GetPreSignedUrlRequest()
                    {
                        BucketName = _config.Value.BucketName,
                        Key = createGamesModelDTO.Logo.FileName,
                        Expires = DateTime.Now.AddHours(1)
                    };

                    string logoUrl = _client.GetPreSignedURL(expiryUrlRequest);
                    gamesInfoDTO.Logo = logoUrl;

                }

                using (var newMemoryStream = new MemoryStream())
                {

                    createGamesModelDTO.Background.CopyTo(newMemoryStream);
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = createGamesModelDTO.Background.FileName,
                        BucketName = _config.Value.BucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(_client);
                    await fileTransferUtility.UploadAsync(uploadRequest);

                    var expiryUrlRequest = new GetPreSignedUrlRequest()
                    {
                        BucketName = _config.Value.BucketName,
                        Key = createGamesModelDTO.Background.FileName,
                        Expires = DateTime.Now.AddHours(1)
                    };

                    string backUrl = _client.GetPreSignedURL(expiryUrlRequest);
                    gamesInfoDTO.Background = backUrl;
                }
            }
            catch (AmazonS3Exception amazonException)
            {
                return new AwsResponse
                {
                    Message = amazonException.Message,
                    Status = amazonException.StatusCode,
                    gamesInfoDTO = null
                };
            }
            return new AwsResponse
            {
                gamesInfoDTO = gamesInfoDTO
            };
        }

        public async Task<AwsResponse> UpdatePictureOnAws(EditGameModelDTO editGameModelDto)
        {
            var gamesInfoDTO = _mapper.Map<EditGameModelDTO, GamesInfoDTO>(editGameModelDto);
            try
            {
                using (var newMemoryStream = new MemoryStream())
                {

                    editGameModelDto.Logo.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = editGameModelDto.Logo.FileName,
                        BucketName = _config.Value.BucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(_client);
                    await fileTransferUtility.UploadAsync(uploadRequest);

                    var expiryUrlRequest = new GetPreSignedUrlRequest()
                    {
                        BucketName = _config.Value.BucketName,
                        Key = editGameModelDto.Logo.FileName,
                        Expires = DateTime.Now.AddHours(1)
                    };

                    string logoUrl = _client.GetPreSignedURL(expiryUrlRequest);
                    gamesInfoDTO.Logo = logoUrl;

                }
                       
                using (var newMemoryStream = new MemoryStream())
                {

                    editGameModelDto.Background.CopyTo(newMemoryStream);
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = editGameModelDto.Background.FileName,
                        BucketName = _config.Value.BucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(_client);
                    await fileTransferUtility.UploadAsync(uploadRequest);

                    var expiryUrlRequest = new GetPreSignedUrlRequest()
                    {
                        BucketName = _config.Value.BucketName,
                        Key = editGameModelDto.Background.FileName,
                        Expires = DateTime.Now.AddHours(1)
                    };

                    string backUrl = _client.GetPreSignedURL(expiryUrlRequest);
                    gamesInfoDTO.Background = backUrl;
                }
            }
            catch (AmazonS3Exception amazonException)
            {
                return new AwsResponse
                {
                    Message = amazonException.Message,
                    Status = amazonException.StatusCode,
                    gamesInfoDTO = null
                };
            }

            return new AwsResponse
            {
                gamesInfoDTO = gamesInfoDTO
            };
        }
    }
}
