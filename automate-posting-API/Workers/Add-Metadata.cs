using Aspose.Imaging.Xmp;
using CamundaClient.Dto;
using CamundaClient.Worker;
using System.Drawing;
using System.Drawing.Imaging;
namespace PostingOnSocialMedia.Workers
{
    [ExternalTaskTopic("Add-Metadata")]  
    //With this annotation, the system knows that this class  is a camunda task worker
    [ExternalTaskVariableRequirements("imagePathFacebook", "imagePathInstagram", "imagePathTwitter", "primaryProductReference", "secondaryProductsReferences")] //required variables in order to execute the task worker
    public class Add_Metadata:IExternalTaskAdapter
    {
        public void AddMD(string platform)
        {
            Image image = Image.FromFile(platform);

        }
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            Console.WriteLine("Add-Metadata starts");
            string imagePathFacebook  = Convert.ToString(externalTask.Variables["imagePathFacebook"].Value);
            string imagePathInstagram = Convert.ToString(externalTask.Variables["imagePathInstagram"].Value);
            string imagePathTwitter = Convert.ToString(externalTask.Variables["imagePathTwitter"].Value);
            string primaryProductReference= Convert.ToString(externalTask.Variables["primaryProductReference"].Value);
            string secondaryProductsReferences=Convert.ToString(externalTask.Variables["secondaryProductsReferences"].Value);
            try
            {
                Dictionary<string, string> platforms = new Dictionary<string, string>();
                platforms.Add("Facebook", imagePathFacebook);
                platforms.Add("Instagram", imagePathInstagram);
                platforms.Add("Twitter", imagePathTwitter);
                int j = 0;
                foreach (KeyValuePair<string, string> platform in platforms)
                {
                    Image image = Image.FromFile(platform.Value);
                    PropertyItem propertyItem = (PropertyItem)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(PropertyItem));
                    string keyWords = primaryProductReference + ";" + secondaryProductsReferences;

                    byte[] customDataBytes = System.Text.Encoding.Unicode.GetBytes(keyWords);
                    propertyItem.Id = 0x9c9e;
                    propertyItem.Len = customDataBytes.Length;
                    propertyItem.Type = 1;
                    propertyItem.Value = customDataBytes;
                    image.SetPropertyItem(propertyItem);
                    string newImagePath = $"C:/automate-posting/ProductsMD/{platform.Key}/" + Path.GetFileName(platform.Value);
                    image.Save(newImagePath, ImageFormat.Jpeg);
                    string variable= "imagePathToUpload"+platform.Key;

                    resultVariables.Add(variable, newImagePath);
                    if (j == 0) { resultVariables.Add("imageName", Path.GetFileName(imagePathFacebook)); }
                    j++;
                }

               
                   
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite : " + ex.Message);
            }
        }
    }
}
