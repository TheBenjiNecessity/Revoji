using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace RevojiWebApi.Services
{
    public class AWSFileUploader
    {
        public static readonly string BUCKET_URL = "https://revoji-content.s3-us-west-2.amazonaws.com";
        private static readonly string bucketName = "revoji-content";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest2;

        public static IAmazonS3 s3Client { private get; set; }

        public static async Task<UploadPhotoModel> UploadObject(IFormFile file, string filepath)
        {
            if (filepath == null || filepath == "")
            {
                throw new NullReferenceException("Filepath cannot be empty");
            }

            // get the file and convert it to the byte[]
            byte[] fileBytes = new byte[file.Length];
            file.OpenReadStream().Read(fileBytes, 0, int.Parse(file.Length.ToString()));

            // create unique file name to prevent mess
            var fileName = filepath + "/" + Guid.NewGuid() + file.FileName;

            PutObjectResponse response = null;

            using (var stream = new MemoryStream(fileBytes))
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    InputStream = stream,
                    ContentType = file.ContentType
                };

                response = await s3Client.PutObjectAsync(request);
            }

            return new UploadPhotoModel
            {
                Success = response.HttpStatusCode == HttpStatusCode.OK,
                FileName = fileName,
                Url = BUCKET_URL + "/" + fileName
            };
        }

        public static async Task<UploadPhotoModel> RemoveObject(string fileName)
        {
            var client = new AmazonS3Client(RegionEndpoint.USWest2);

            var request = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = fileName
            };

            var response = await client.DeleteObjectAsync(request);

            return new UploadPhotoModel
            {
                Success = response.HttpStatusCode == HttpStatusCode.OK,
                FileName = fileName,
                Url = BUCKET_URL + "/" + fileName
            };
        }


        public class UploadPhotoModel
        {
            public bool Success { get; set; }
            public string FileName { get; set; }
            public string Url { get; set; }
        }
    }
}
