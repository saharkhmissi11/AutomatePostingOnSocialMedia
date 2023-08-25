﻿using CamundaClient.Dto;
using CamundaClient.Worker;
using Dropbox.Api.Files;
using Dropbox.Api;
using System.Web;
using static PostingOnSocialMedia.Controllersvisu.DropBoxController;

namespace PostingOnSociallMedia.Workers
{

    [ExternalTaskTopic("Upload-Image")]
    //With this annotation, the system knows that this class  is a camunda task worker
    [ExternalTaskVariableRequirements("mediaName", "projectName","productName", "imagePathToUpload","platform")] //required variables in order to execute the task worker
    public class UploadImage : IExternalTaskAdapter
    {

        private HttpClient httpClient = new HttpClient();
        private DropboxClient dropboxClient = new DropboxClient("sl.Bkw2e-Glp19ZPPe95XpPbDyakbMznEPm8D9TwFjF2BMRk1cF5PICmpVMMppzwQkVT6P8i-GNSB69y478_-V5pjPiKnceUOPKPFET5YLhykspSv9RQ1_O6DJe-k8WHYJxg1ILyrbE4BRSn16gkv2s32Y");
        public  void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
           
            Console.WriteLine("UploadImage starts");
            string mediaName = Convert.ToString(externalTask.Variables["mediaName"].Value);
            string platform = Convert.ToString(externalTask.Variables["platform"].Value);
            string projectName = Convert.ToString(externalTask.Variables["projectName"].Value);
            string productName = Convert.ToString(externalTask.Variables["productName"].Value);
            string imagePathToUpload = Convert.ToString(externalTask.Variables["imagePathToUpload"].Value);
            UploadRequestModel model= new UploadRequestModel();
            model.ImageName = mediaName;
            model.ProjectName = projectName;
            model.ProductName = productName;
            model.Platform = platform;
            Task.Run(() => UploadImageAsync(model)).Wait();
           
        }
        private async Task UploadImageAsync(UploadRequestModel model)
        {
            string imageName = model.ImageName;
            string platform = model.Platform;
            string projectName = model.ProjectName;
            string productName = model.ProductName;

            string filePath = "C:/automate-posting/ProductsMD/" + platform + "/" + HttpUtility.UrlDecode(imageName);
            Console.WriteLine(filePath);
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

        }
    }
}
