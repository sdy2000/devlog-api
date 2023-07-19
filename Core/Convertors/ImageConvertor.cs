using SixLabors.ImageSharp.Formats.Jpeg;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Core.Convertors
{
    public class ImageConvertor
    {
        public void Image_resize(string input_Image_Path, string output_Image_Path, int new_Width)
        {
            const int quality = 50;


            using (Image image = Image.Load(input_Image_Path))
            {
                double dblWidth_origial = image.Width;

                double dblHeigth_origial = image.Height;

                double relation_heigth_width = dblHeigth_origial / dblWidth_origial;

                int new_Height = (int)(new_Width * relation_heigth_width);

                image.Mutate(x => x.Resize(new_Width, new_Height).AutoOrient());

                // Save the image as JPEG with the specified quality
                var encoder = new JpegEncoder { Quality = quality };
                image.Save(output_Image_Path, encoder);
            }
        }
    }
}
