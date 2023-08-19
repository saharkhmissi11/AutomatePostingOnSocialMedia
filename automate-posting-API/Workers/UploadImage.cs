using CamundaClient.Dto;
using CamundaClient.Worker;

namespace PostingOnSociallMedia.Workers
{

    [ExternalTaskTopic("Upload-Image")]
    //With this annotation, the system knows that this class  is a camunda task worker
    [ExternalTaskVariableRequirements("imageName", "imagePathToUpload","platform")] //required variables in order to execute the task worker
    public class UploadImage : IExternalTaskAdapter
    {
        HttpClient httpClient = new HttpClient();
        public void Upload(string platform)
        {

        }
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            Console.WriteLine("UploadImage starts");
            Thread.Sleep(3000);
            string imageName = Convert.ToString(externalTask.Variables["imageName"].Value);
            string platform = Convert.ToString(externalTask.Variables["platform"].Value);
            string imagePathToUpload = Convert.ToString(externalTask.Variables["imagePathToUpload"].Value);
            var requestContent = new { ImageName = imageName, Platform = platform };
            var imageNameResponse = httpClient.PostAsJsonAsync($"https://localhost:7147/api/DropBox/Upload",requestContent);

        }
    }
}
