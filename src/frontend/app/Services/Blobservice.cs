using System;
using Microsoft.Azure.Storage.Blob;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;

namespace app.Services
{
    public class Blobservice
    {
        public IHostingEnvironment _environment {get; set;}
        public Blobservice(IHostingEnvironment env)
        {
            _environment = env;
        }

        public string UploadToAzureAsync(IFormFile photo)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=lesson07gold;AccouaaaaaaaaaaaMqRSLUBsibHK/eH5FAV1F24pghALDnTHQh1DOvIovgKm+mnXZm/SC43kGhgkqOp6Cqw==;EndpointSuffix=core.windows.net";

            var cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=lesson07gold;AccountKey=yxZ6ZzVBkMpylgvyq4+MqRSLUBsibHK/eH5FAV1F24pghALDnTHQh1DOvIovgKm+mnXZm/SC43kGhgkqOp6Cqw==;EndpointSuffix=core.windows.net");
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("imagesba0bde9d-ae96-4310-a026-bd3d81af150d");
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(photo.FileName);
            cloudBlockBlob.Properties.ContentType = photo.ContentType;

            cloudBlockBlob.UploadFromStream(photo.OpenReadStream());
            var uri = cloudBlockBlob.Uri.AbsoluteUri;
            return uri;
        }
    }
}