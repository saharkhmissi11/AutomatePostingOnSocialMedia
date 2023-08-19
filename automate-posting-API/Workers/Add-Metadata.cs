using Aspose.Imaging.Xmp;
using CamundaClient.Dto;
using CamundaClient.Worker;
using System.Drawing;
using System.Drawing.Imaging;
namespace PostingOnSocialMedia.Workers
{
    [ExternalTaskTopic("Add-Metadata")]
    //With this annotation, the system knows that this class  is a camunda task worker
    [ExternalTaskVariableRequirements("imagePath", "platform", "primaryProductReference", "secondaryProductsReferences")] //required variables in order to execute the task worker
    public class Add_Metadata : IExternalTaskAdapter
    {
        public void AddMD(string platform)
        {
            Image image = Image.FromFile(platform);

        }
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            Console.WriteLine("Add-Metadata starts");
            string imagePath = Convert.ToString(externalTask.Variables["imagePath"].Value);
            string platform = Convert.ToString(externalTask.Variables["platform"].Value);
            string primaryProductReference = Convert.ToString(externalTask.Variables["primaryProductReference"].Value);
            string secondaryProductsReferences = Convert.ToString(externalTask.Variables["secondaryProductsReferences"].Value);
            try
            {
                /*Dictionary<string, string> platforms = new Dictionary<string, string>();
                platforms.Add("Facebook", imagePathFacebook);
                platforms.Add("Instagram", imagePathInstagram);
                platforms.Add("Twitter", imagePathTwitter);*/

                Image image = Image.FromFile(imagePath);
                PropertyItem propertyItem = (PropertyItem)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(PropertyItem));
                string keyWords = primaryProductReference + ";" + secondaryProductsReferences;

                byte[] customDataBytes = System.Text.Encoding.Unicode.GetBytes(keyWords);
                propertyItem.Id = 0x9c9e;
                propertyItem.Len = customDataBytes.Length;
                propertyItem.Type = 1;
                propertyItem.Value = customDataBytes;
                image.SetPropertyItem(propertyItem);
                string newImagePath = $"C:/automate-posting/ProductsMD/{platform}/" + Path.GetFileName(imagePath);
                Console.WriteLine(newImagePath);
                image.Save(newImagePath, ImageFormat.Jpeg);
                string variable = "imagePathToUpload";
                resultVariables.Add(variable, newImagePath);
                resultVariables.Add("imageName", Path.GetFileName(imagePath));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite : " + ex.Message);
            }
        }
    }
}
