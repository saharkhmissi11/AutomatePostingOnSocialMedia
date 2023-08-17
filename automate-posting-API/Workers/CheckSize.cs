/*using CamundaClient.Dto;
using CamundaClient.Worker;

namespace PostingOnSociallMedia.Workers
{
    [ExternalTaskTopic("Check-Size")]
    [ExternalTaskVariableRequirements("imagePathFacebook", "imagePathInstagram", "imagePathTwitterT")]
    public class CheckSize : IExternalTaskAdapter
    {
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            string imagePathFacebook = Convert.ToString(externalTask.Variables["imagePathFacebook"].Value);
            string imagePathInstagram = Convert.ToString(externalTask.Variables["imagePathInstagram"].Value);
            string imagePathTwitter = Convert.ToString(externalTask.Variables["imagePathTwitter"].Value);
            var imgFacebookSize = GetFileSize(imagePathFacebook);
            var imgInstagramSize= GetFileSize(imagePathInstagram);
            var imgTwitterSize = GetFileSize(imagePathTwitter);
            Console.WriteLine(imgFacebookSize);
        }
        static long GetFileSize(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                return new FileInfo(FilePath).Length;
            }
            return 0;
        }
    }

   
}*/
