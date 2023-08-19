﻿using Azure.Core;
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Dropbox.Api.TeamLog.ActorLogInfo;
using System;
using PostingOnSocialMedia.Models;
using System.Web;
using Aspose.Imaging.Xmp;
using Microsoft.AspNetCore.Cors;
using PostingOnSociallMedia.sf_sezane;
using PostingOnSociallMedia.Models;

namespace PostingOnSocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("AllowOrigin")]

    public class DropBoxController : ControllerBase
    {
        private readonly SfSezaneContext _context;
        DropboxClient dropboxClient = new DropboxClient("sl.BkefYtEFvUMQNCmxS4A0-CaTyNbvcymPShqsAVVlK0n-HKfbQz2zTW2u7zhCWBELf1uME6kX2VP75rfkOeqJVPlmHKL8aSoP58B_u-QOE2g_4uWkyNIyBU70VeqHSv0fbwQGtH9P8Ht6d2IMvWi2tW8");
        [HttpGet("Download/{product}")]
        //path="/Products/image.jpg"
        public async Task<ActionResult<string>> Download(string product)
        {
            string path;
            try
            {
                product = HttpUtility.UrlDecode(product);
                using (var response = await dropboxClient.Files.DownloadAsync(product))
                {
                    var imageBytes = await response.GetContentAsByteArrayAsync();
                    using (MemoryStream stream = new MemoryStream(imageBytes))
                    {
                        string localFilePath = Path.Combine("C:/automate-posting" + product);
                        System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                        image.Save(localFilePath);
                        path = "C:/automate-posting" + product;
                        return path;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return NotFound();
        }
        [HttpPost("Upload")]
        public async Task<ActionResult<string>> Upload([FromBody] UploadRequestModel model)
        {
            string imageName = model.ImageName;
            string platform = model.Platform;


            string filePath = "C:/automate-posting/ProductsMD/" + platform + "/" + HttpUtility.UrlDecode(imageName);
            Console.WriteLine(filePath);
            try
            {
                string dropboxFolderPath = "/UploadedMedias/" + platform;
                Console.WriteLine(dropboxFolderPath);
                string fileName = Path.GetFileName(filePath);
                string dropboxFilePath = dropboxFolderPath + "/" + fileName;
                using (var fileStream = System.IO.File.Open(filePath, FileMode.Open))
                {
                    var uploadedFile = await dropboxClient.Files.UploadAsync(
                        dropboxFilePath,
                        WriteMode.Overwrite.Instance,
                        body: fileStream
                    );
                    Console.WriteLine($"Uploaded: {uploadedFile.PathDisplay}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"This is an error: {ex.Message}");
            }


            return Ok();
        }

        [HttpGet("GetDisplayPath/{image}")]
        public async Task<ActionResult<string>> GetDisplayPath(string image)
        {
            var link = await dropboxClient.Files.GetTemporaryLinkAsync("/Medias" + "/" + image);
            if (link == null) { return NotFound(); }
            return link.Link;

        }
         [HttpGet("GetImagesLinks")]
         public async Task<ActionResult> GetImagesLinks()
         {
             var files = await dropboxClient.Files.ListFolderAsync("/Medias");
             List<string> paths = new List<string>();
             foreach (var file in files.Entries)
             {
                 paths.Add(file.Name);
             }
             return Ok(paths);
         }
       
        /// 
      /* [HttpGet("GetMediasInProduct/{project}/{product}")]
        public async Task<ActionResult> GetMediasInProduct(string product ,string project)
        {
            var files = await dropboxClient.Files.ListFolderAsync("/projects/"+project +"/medias/"+ product);
            List<string> paths = new List<string>();
            foreach (var file in files.Entries)
            {
                paths.Add(file.PathDisplay);
            }
            // return links;
            List<string> links = new List<string>();
            foreach (var path in paths)
            {
                var link = await dropboxClient.Files.GetTemporaryLinkAsync(path);
                links.Add(link.Link);
            }
            return Ok(links);
        }*/
        [HttpGet("GetMediasInProduct/{project}/{product}")]
        public async Task<ActionResult> GetMediasInProduct(string product, string project)
        {
            List<MediaRequest> mediaRequests = new List<MediaRequest>();
            var files = await dropboxClient.Files.ListFolderAsync("/projects/" + project + "/medias/" + product);
            foreach (var file in files.Entries)
            {
                MediaRequest mediaRequest=new MediaRequest(); ;
                mediaRequest.path = file.PathDisplay;
                mediaRequest.name=file.Name;
                mediaRequests.Add(mediaRequest);
                
            }
            // return links;
            foreach (var media in mediaRequests)
            {  
               
                var link = await dropboxClient.Files.GetTemporaryLinkAsync(media.path);
                media.path = link.Link;
            }
            return Ok(mediaRequests);
        }


        [HttpGet("GetFolders/{folderPath}")]
        public async Task<ActionResult> GetFolders(string folderPath)
        {
            List<string> folders = new List<string>();
            try
            {
                Console.WriteLine(folderPath);
                var listFolderResult = await dropboxClient.Files.ListFolderAsync(HttpUtility.UrlDecode(folderPath));
               
                foreach (var item in listFolderResult.Entries)
                {
                    if (item.IsFolder)
                    {
                        var folder = (FolderMetadata)item;
                        folders.Add(folder.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return Ok(folders);

        }
        public class UploadRequestModel
        {
            public string ImageName { get; set; }
            public string Platform { get; set; }
        }
    }
}