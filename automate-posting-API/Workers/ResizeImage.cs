//using Aspose.Imaging;
/*
using CamundaClient.Dto;
using CamundaClient.Worker;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PostingOnSociallMedia.Workers
{
    [ExternalTaskTopic("Resize-Image")]
    //  With this annotation, the system knows that this class  is a camunda task worker
    [ExternalTaskVariableRequirements("imagePathToResize")] //required variables in order to execute the task worker
    public class ResizeImage : IExternalTaskAdapter
    {
        private static System.Drawing.Image Resize(System.Drawing.Image imgToResize, Size size)
        {
            // Get the image current width
            int sourceWidth = imgToResize.Width;
            // Get the image current height
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            // Calculate width and height with new desired size
            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            nPercent = Math.Min(nPercentW, nPercentH);
            // New Width and Height
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            Console.WriteLine("ResizeImage starts");

            string imagePathToResize = Convert.ToString(externalTask.Variables["imagePathToResize"].Value);
            Console.WriteLine("imagePathToResize :"+ imagePathToResize);
         
            try
            {
                Image image= Image.FromFile(@imagePathToResize);
                Bitmap imgbitmap = new Bitmap(image);
                Image resizedImageFacebook = Resize(imgbitmap, new Size(1200, 630));
                resizedImageFacebook.Save(@"C:/automate-posting/ProductsResized/Facebook/"+Path.GetFileName(imagePathToResize));
                resultVariables.Add("imagePathFacebook", "C:/automate-posting/ProductsResized/Facebook/" + Path.GetFileName(imagePathToResize));
                Image resizedImageInstagram = Resize(imgbitmap, new Size(1080, 1080));
                resizedImageInstagram.Save(@"C:/automate-posting/ProductsResized/Instagram/" + Path.GetFileName(imagePathToResize));
                resultVariables.Add("imagePathInstagram", "C:/automate-posting/ProductsResized/Instagram/" + Path.GetFileName(imagePathToResize));
                Image resizedImageTwitter = Resize(imgbitmap, new Size(1200, 675));
                resizedImageTwitter.Save(@"C:/automate-posting/ProductsResized/Twitter/" + Path.GetFileName(imagePathToResize));
                resultVariables.Add("imagePathTwitter", "C:/automate-posting/ProductsResized/Twitter/" + Path.GetFileName(imagePathToResize));
                
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}*/