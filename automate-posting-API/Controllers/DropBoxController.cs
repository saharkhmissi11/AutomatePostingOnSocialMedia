using Azure.Core;
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Dropbox.Api.TeamLog.ActorLogInfo;
using System;
using PostingOnSocialMedia.Models;
using System.Web;
using Microsoft.AspNetCore.Cors;
using PostingOnSociallMedia.sf_sezane;
using PostingOnSociallMedia.Models;

namespace PostingOnSocialMedia.Controllersvisu
{
    [Route("api/[controller]")]
    [ApiController]

    public class DropBoxController : ControllerBase
    {
        private readonly SfSezaneContext _context;
        DropboxClient dropboxClient = new DropboxClient("sl.BlHEhBgNfckqUiN4sYO_D1jvfFj7u-y77ThuMIxMt1lsxFbI53AyO0x3vIfQ39abl6IwYO4-RWEHoJRzOLpxj_GgTqXvpE_zg-BjU9fZp-QwTo-xNXF5oLx5XSfpx4OulmPPc06NtnnNE07HuHntFTc");
        [HttpGet("Download/{product}")]
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
            string projectName= model.ProjectName;
            string productName = model.ProductName;

            string filePath = "C:/automate-posting/ProductsMD/" + platform + "/" + HttpUtility.UrlDecode(imageName);
            try
            {
                string dropboxFolderPath = "/projects/" + projectName + "/social-medias/" + platform + "/" + productName;
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
            public string ProjectName { get; set; }
            public string ProductName { get; set; }
        }
    }
}