using Azure.Core;
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

namespace PostingOnSocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("AllowOrigin")]

    public class DropBoxController : ControllerBase
    {
        private readonly SocialMediaDbContext _context;
        DropboxClient dropboxClient = new DropboxClient("sl.BkRNeARxZfsbkldM3oDcQb1wKp_OLTMycZQOHdXwFlnpv33ymTeuYLfxvpb4le0CvyY3slnAWzED2UwtpemwggklCNfULhzaAqeWsrkn-4Yuechh56PdvLwd7pEF9AOahjfpeCLolnTCcpkQfStAag8");
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
        [HttpPost("Upload/{product}")]
        public async Task<ActionResult<string>> Upload(string product)
        {
            List<string> platforms = new List<string>();
            platforms.Add("Facebook");
            platforms.Add("Instagram");
            platforms.Add("Twitter");
            foreach (string platform in platforms)
            {
                string filePath = "C:/automate-posting/ProductsMD/" + platform + "/" + HttpUtility.UrlDecode(product);
                Console.WriteLine(filePath);
                try
                {
                    string dropboxFolderPath = "/Uploaded_Products/" + platform;
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
            }

            return Ok();
        }

        [HttpGet("GetDisplayPath/{image}")]
        public async Task<ActionResult<string>> GetDisplayPath(string image)
        {
            var link = await dropboxClient.Files.GetTemporaryLinkAsync("/Products" + "/" + image);
            if (link == null) { return NotFound(); }
            return link.Link;

        }
        [HttpGet("GetImagesLinks")]
        public async Task<ActionResult> GetImagesLinks()
        {
            var files = await dropboxClient.Files.ListFolderAsync("/Products");
            List<string> paths = new List<string>();
            foreach (var file in files.Entries)
            {
                paths.Add(file.PathDisplay);
            }
            // return links;
            List<string> links = new List<string>();
            List<object> images = new List<object>();
            foreach (var path in paths)
            {
                var link = await dropboxClient.Files.GetTemporaryLinkAsync(path);
                links.Add(link.Link);
            }
            return Ok(links);
        }

    }
}