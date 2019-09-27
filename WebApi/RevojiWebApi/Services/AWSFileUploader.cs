using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;

namespace RevojiWebApi.Services
{
    public class AWSFileUploader
    {
        public static string BUCKET_URL = "https://revoji-content.s3-us-west-2.amazonaws.com";

        private static string accessKey = "AKIAQVA7QUMV3DODNB6B";
        private static string accessSecret = "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";
        private static string bucket = "revoji-content";

        public static async Task<UploadPhotoModel> UploadObject(IFormFile file, string filepath)
        {
            if (filepath == null || filepath == "")
            {
                throw new NullReferenceException("Filepath cannot be empty");
            }

            // connecting to the client
            var client = new AmazonS3Client(accessKey, accessSecret, RegionEndpoint.USWest2);

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
                    BucketName = bucket,
                    Key = fileName,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                response = await client.PutObjectAsync(request);
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
            var client = new AmazonS3Client(accessKey, accessSecret, RegionEndpoint.EUCentral1);

            var request = new DeleteObjectRequest
            {
                BucketName = bucket,
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
