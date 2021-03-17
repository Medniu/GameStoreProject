using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Settings
{
    public class AwsSettings
    {
        public string BucketName { get; set; }
        public string Region { get; set; }
        public string Profile { get; set; }
        public string AWSAccessKeyId { get; set; }
        public string AWSSecretKey { get; set; }
    }
}
