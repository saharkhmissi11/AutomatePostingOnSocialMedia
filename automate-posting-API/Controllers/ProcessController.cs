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
using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using static Dropbox.Api.Account.PhotoSourceArg;

namespace PostingOnSociallMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly SocialMediaDbContext _context;


        public ProcessController(SocialMediaDbContext context)
        {
            _context = context;
        }
        [HttpPost("begin")]
        public async Task<ActionResult<string>> BeginProcess([FromBody] ProcessRequestModel requestModel)
        {
            HttpClient httpClient = new HttpClient();
            string primaryProduct = requestModel.primaryProduct;
            string secondaryProducts = requestModel.secondaryProducts;
            string uniqueFileName = Guid.NewGuid().ToString("N") + "." + "jpg";
            string imagePathFacebook = getImagePath(requestModel.imagePathFacebook,"Facebook",uniqueFileName);
            string imagePathInstagram = getImagePath(requestModel.imagePathInstagram,"Instagram", uniqueFileName);
            string imagePathTwitter = getImagePath(requestModel.imagePathTwitter,"Twitter", uniqueFileName);
            var camunda = new CamundaEngineClient(new System.Uri("http://localhost:8080/engine-rest/engine/default/"), null, null);
            string processInstanceId = camunda.BpmnWorkflowService.StartProcessInstance("PostingProcess", new Dictionary<string, object>() {
                    {"imagePathFacebook", imagePathFacebook },
                     {"imagePathInstagram", imagePathInstagram },
                      {"imagePathTwitter", imagePathTwitter },
                    {"primaryProductReference",primaryProduct},
                    {"secondaryProductsReferences",secondaryProducts},
                });

            /* Thread.Sleep(3000);

             string requestUrl = $"{"http://localhost:8080/engine-rest"}/process-instance/{processInstanceId}/variables/imageName";
             HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
             if (response.IsSuccessStatusCode)
             {

                 var variable = await response.Content.ReadFromJsonAsync<CamundaClient.Dto.Variable>();
                 var imageName = variable.Value.ToString();
                 Console.WriteLine($"Variable value is  {imageName}");
                 var imageNameResponse = await httpClient.PostAsJsonAsync($"https://localhost:7147/api/DropBox/Upload/{imageName}" , imageName);
                 if (imageNameResponse.IsSuccessStatusCode)
                 {

                 }
                 }*/

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

        [HttpPut("SendUrl/{platform}/{url}")]
        public async Task<ActionResult> SendUrl(string platform, string url)
        {
            try
            {
                var imageUrl = await _context.ImageUrls.FirstOrDefaultAsync(i => i.Platform == platform);

                if (imageUrl == null)
                {
                    return NotFound();
                }

                imageUrl.Url = url;

                await _context.SaveChangesAsync();
                Console.WriteLine(imageUrl.Url);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the URL.");
            }
        }
        static Bitmap ConvertByteArrayToBitmap(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                return new Bitmap(stream);
            }
        }
        static string getImagePath(string path64,string platform,string uniqueFileName)
        {
            string imageData = path64.Substring(path64.IndexOf(',') + 1);
            byte[] decodedImage = Convert.FromBase64String(imageData);

            MemoryStream ms = new MemoryStream(decodedImage);
            
            // Create Bitmap from the memory stream
            Bitmap bitmap = new Bitmap(ms);
            

            string savePath = @"C:/automate-posting/ProductsCropped/"+platform+"/"+uniqueFileName;  // Update file extension if needed

            // Ensure the directory exists

            // Save the Bitmap as an image file
            bitmap.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);  // Update ImageFormat if needed
            return savePath;
        }
        

    }
}


