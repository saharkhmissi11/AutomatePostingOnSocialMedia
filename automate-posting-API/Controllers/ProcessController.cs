using CamundaClient;
using CamundaClient.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostingOnSocialMedia.Models;
using PostingOnSocialMedia.Workers;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using static Dropbox.Api.Files.ListRevisionsMode;
using PostingOnSociallMedia.Models;
using PostingOnSociallMedia.sf_sezane;
using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using static Dropbox.Api.Account.PhotoSourceArg;
using PostingOnSociallMedia.sf_sezane;

namespace PostingOnSociallMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly SfSezaneContext _context;


        public ProcessController(SfSezaneContext context)
        {
            _context = context;
        }
        [HttpPost("begin")]
        public async Task<ActionResult<string>> BeginProcess([FromBody] ProcessRequestModel requestModel)
        {
            HttpClient httpClient = new HttpClient();
            string primaryProduct = requestModel.primaryProduct;
            string secondaryProducts = requestModel.secondaryProducts;
            string mediaName= requestModel.mediaName;   
            string projectName=requestModel.projectName;
            string productName=requestModel.productName;
            string platform = requestModel.platform;
            string uniqueFileName = Guid.NewGuid().ToString("N") + "." + "jpg";
            string imagePath = getImagePath(requestModel.imagePath, platform, mediaName);

            var camunda = new CamundaEngineClient(new System.Uri("http://localhost:8080/engine-rest/engine/default/"), null, null);
            string processInstanceId = camunda.BpmnWorkflowService.StartProcessInstance("PostingProcess", new Dictionary<string, object>() {
                    {"imagePath", imagePath },
                    {"platform", platform },
                    {"mediaName", mediaName },
                    {"productName", productName },
                    {"projectName", projectName },
                    {"primaryProductReference",primaryProduct},
                    {"secondaryProductsReferences",secondaryProducts},
                });

            return Ok($"Process started with ID: {processInstanceId}");
        }
        static string GetBase64ImageData(string base64String)
        {
            int commaIndex = base64String.IndexOf(',');
            if (commaIndex >= 0 && commaIndex + 1 < base64String.Length)
            {
                return base64String.Substring(commaIndex + 1);
            }
            return base64String;
        }


        static Bitmap ConvertByteArrayToBitmap(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                return new Bitmap(stream);
            }
        }
        static string getImagePath(string path64, string platform, string mediaName)
        {
            string imageData = path64.Substring(path64.IndexOf(',') + 1);
            byte[] decodedImage = Convert.FromBase64String(imageData);

            MemoryStream ms = new MemoryStream(decodedImage);

            // Create Bitmap from the memory stream
            Bitmap bitmap = new Bitmap(ms);


            string savePath = @"C:/automate-posting/ProductsCropped/" + platform + "/" + mediaName;  // Update file extension if needed
            // Ensure the directory exists

            // Save the Bitmap as an image file
            bitmap.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);  // Update ImageFormat if needed
            return savePath;
        }
        [HttpGet("projects")]

        public async Task<ActionResult<IEnumerable<PostingOnSociallMedia.sf_sezane.Project>>> getProjects()
        {
            return await _context.Projects.ToListAsync();

        }
    }
}


