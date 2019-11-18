using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.DrawingCore.Text;
using System.IO;
using System.Linq;
using System.Text;

namespace cts.web.core.Librs
{
    /// <summary>
    /// 图片处理类
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="image"></param>
        /// <param name="proportion"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image CreateThumb(Image image, ThumbnailProportion proportion, int width, int height)
        {
            if (image == null)
                throw new ArgumentNullException("图片为空image");
            int x = 0, y = 0, ow = image.Width, oh = image.Height, towidth = width, toheight = height;
            switch (proportion)
            {
                case ThumbnailProportion.WIDTH_HEIHT:
                    break;
                case ThumbnailProportion.WIDTH:
                    toheight = image.Height * width / image.Width;
                    break;
                case ThumbnailProportion.HEIHT:
                    towidth = image.Width * height / image.Height;
                    break;
                case ThumbnailProportion.CUT:
                    //剪切图片高比宽长
                    if ((double)image.Width / (double)image.Height > (double)towidth / (double)toheight)
                    {
                        oh = image.Height;
                        ow = image.Height * towidth / toheight;
                        y = 0;
                        x = (image.Width - ow) / 2;
                    }
                    else
                    {
                        ow = image.Width;
                        oh = image.Width * height / towidth;
                        x = 0;
                        y = (image.Height - oh) / 2;
                    }
                    break;
            }
            //指定位图大小
            Bitmap bmp = new Bitmap(towidth, toheight);
            //创建画布
            using (Graphics gs = Graphics.FromImage(bmp))
            {
                gs.SmoothingMode = SmoothingMode.HighQuality;
                gs.CompositingQuality = CompositingQuality.HighQuality;
                gs.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //清空画布并以透明背景色填充 
                gs.Clear(Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分 
                gs.DrawImage(image, new Rectangle(0, 0, towidth, toheight),
                    new Rectangle(x, y, ow, oh),
                    GraphicsUnit.Pixel);
                return bmp;
            }
        }

        /// <summary>
        /// 图片添加文字水印
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="text">文字内容</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="emSize">字体大小px</param>
        /// <param name="fontColor">字体颜色，：#ffffff</param>
        /// <param name="position">位置</param>
        /// <param name="opacity">透明度：1-100</param>
        /// <returns></returns>
        public static Image WaterMarkText(Image image, string text, string fontFamily, int emSize, string fontColor, WaterPosition position, int opacity)
        {
            return AddWaterText(image, text, fontFamily, emSize, fontColor, position, opacity, 0);
        }

        /// <summary>
        /// 图片添加文字水印
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="text">文字内容</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="emSize">字体大小px</param>
        /// <param name="fontColor">字体颜色，：#ffffff</param>
        /// <param name="position">位置</param>
        /// <param name="opacity">透明度：1-100</param>
        /// <param name="rotate">倾斜度 正负180</param>
        /// <returns></returns>
        public static Image AddWaterText(Image image, string text, string fontFamily, int emSize, string fontColor, WaterPosition position, int opacity, int rotate)
        {
            using (Graphics gs = Graphics.FromImage(image))
            {
                //文本格式
                Font font = new Font(fontFamily, emSize, FontStyle.Regular, GraphicsUnit.Pixel);
                SizeF crSize = gs.MeasureString(text, font);
                //倾斜度
                gs.RotateTransform(rotate);
                float xpos = 0;
                float ypos = 0;
                switch (position)
                {
                    case WaterPosition.左上:
                        xpos = (float)image.Width * (float).01;
                        ypos = (float)image.Height * (float).01;
                        break;
                    case WaterPosition.中上:
                        xpos = ((float)image.Width * (float).50) - (crSize.Width / 2);
                        ypos = (float)image.Height * (float).01;
                        break;
                    case WaterPosition.右上:
                        xpos = ((float)image.Width * (float).99) - crSize.Width;
                        ypos = (float)image.Height * (float).01;
                        break;
                    case WaterPosition.左中:
                        xpos = (float)image.Width * (float).01;
                        ypos = ((float)image.Height * (float).50) - (crSize.Height / 2);
                        break;
                    case WaterPosition.中中:
                        xpos = ((float)image.Width * (float).50) - (crSize.Width / 2);
                        ypos = ((float)image.Height * (float).50) - (crSize.Height / 2);
                        break;
                    case WaterPosition.右中:
                        xpos = ((float)image.Width * (float).99) - crSize.Width;
                        ypos = ((float)image.Height * (float).50) - (crSize.Height / 2);
                        break;
                    case WaterPosition.左下:
                        xpos = (float)image.Width * (float).01;
                        ypos = ((float)image.Height * (float).99) - crSize.Height;
                        break;
                    case WaterPosition.中下:
                        xpos = ((float)image.Width * (float).50) - (crSize.Width / 2);
                        ypos = ((float)image.Height * (float).99) - crSize.Height;
                        break;
                    case WaterPosition.右下:
                        xpos = ((float)image.Width * (float).99) - crSize.Width;
                        ypos = ((float)image.Height * (float).99) - crSize.Height;
                        break;
                }
                var color = ColorTranslator.FromHtml(fontColor);
                gs.DrawString(text, font, new SolidBrush(Color.FromArgb(opacity / 100 * 255, color)), xpos, ypos);
                return image;
            }

        }


        /// <summary>
        /// 图片添加水印
        /// </summary>
        /// <param name="image"></param>
        /// <param name="watermark">水印图片</param>
        /// <param name="position">位置</param>
        /// <param name="opacity">透明度。1-100，越小透越透明</param>
        /// <returns></returns>
        public static Image AddWaterPic(Image image, Image watermark, WaterPosition position, int opacity)
        {
            //如果水印图片大于图片，则不添加水印
            if (watermark.Height >= image.Height || watermark.Width >= image.Width)
                return image;

            using (Graphics g = Graphics.FromImage(image))
            {
                ImageAttributes imageAttributes = new ImageAttributes();
                ColorMap colorMap = new ColorMap();

                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float transparency = 0.5F;
                if (opacity >= 1 && opacity <= 100)
                    transparency = (opacity / 100.0F);

                float[][] colorMatrixElements = {
                                                  new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                  new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                  new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                  new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
                                                  new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                              };
                ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                int xpos = 0;
                int ypos = 0;
                switch (position)
                {
                    case WaterPosition.左上:
                        xpos = (int)(image.Width * (float).01);
                        ypos = (int)(image.Height * (float).01);
                        break;
                    case WaterPosition.中上:
                        xpos = ((int)(image.Width * (float).50) - (watermark.Width / 2));
                        ypos = (int)(image.Height * (float).01);
                        break;
                    case WaterPosition.右上:
                        xpos = ((int)(image.Width * (float).99) - watermark.Width);
                        ypos = (int)(image.Height * (float).01);
                        break;
                    case WaterPosition.左中:
                        xpos = (int)(image.Width * (float).01);
                        ypos = (int)((image.Height * (float).50) - (watermark.Height / 2));
                        break;
                    case WaterPosition.中中:
                        xpos = (int)((image.Width * (float).50) - (watermark.Width / 2));
                        ypos = (int)((image.Height * (float).50) - (watermark.Height / 2));
                        break;
                    case WaterPosition.右中:
                        xpos = (int)((image.Width * (float).99) - watermark.Width);
                        ypos = (int)((image.Height * (float).50) - (watermark.Height / 2));
                        break;
                    case WaterPosition.左下:
                        xpos = (int)(image.Width * (float).01);
                        ypos = (int)((image.Height * (float).99) - watermark.Height);
                        break;
                    case WaterPosition.中下:
                        xpos = (int)((image.Width * (float).50) - (watermark.Width / 2));
                        ypos = (int)((image.Height * (float).99) - watermark.Height);
                        break;
                    case WaterPosition.右下:
                        xpos = (int)((image.Width * (float).99) - watermark.Width);
                        ypos = (int)((image.Height * (float).99) - watermark.Height);
                        break;
                }
                g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            return image;
        }


        /// <summary>
        /// argb颜色字符串，返还a/r/g/b顺序的值数组
        /// </summary>
        /// <param name="rgbstring"></param>
        /// <returns></returns>
        public static int[] ARGBStringToArray(string rgbstring)
        {
            if (string.IsNullOrEmpty(rgbstring))
                return null;
            if (rgbstring.StartsWith("#"))
                rgbstring = rgbstring.Substring(1);
            string[] array = { string.Empty, string.Empty, string.Empty, string.Empty };
            switch (rgbstring.Length)
            {
                case 3:
                    array[1] = rgbstring.Substring(0, 1);
                    array[2] = rgbstring.Substring(1, 1);
                    array[3] = rgbstring.Substring(2);
                    break;
                case 6:
                    array[1] = rgbstring.Substring(0, 2);
                    array[2] = rgbstring.Substring(2, 2);
                    array[3] = rgbstring.Substring(4);
                    break;
                case 8:
                    array[0] = rgbstring.Substring(0, 2);
                    array[1] = rgbstring.Substring(2, 2);
                    array[2] = rgbstring.Substring(4, 2);
                    array[3] = rgbstring.Substring(6);
                    break;
                default:
                    return null;
            }
            return array.Select(one => !String.IsNullOrEmpty(one) ? Convert.ToInt32(one, 16) : 0).ToArray();
        }

        /// <summary>
        /// 生成文字图片 默认文字不透明
        /// </summary>
        /// <returns></returns>
        public static Image FontMarkPicture(string fonttext, int fontsize, string fontfamily, string fontcolor,string backColor)
        {
            return FontMarkPicture(fonttext, fontsize, fontfamily, fontcolor, backColor,1);
        }

        /// <summary>
        /// 生成文字图片（包含透明度）
        /// </summary>
        /// <param name="fonttext">文字</param>
        /// <param name="fontsize">文字大小 px单位</param>
        /// <param name="fontfamily">字体</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="opacity">文字透明度,0 ~ 1</param>
        /// <returns></returns>
        public static Image FontMarkPicture(string fonttext, int fontsize, string fontfamily,string fontcolor,string backColor, float opacity)
        {
            Bitmap image = null;
            using (Bitmap testImg = new Bitmap(1, 1))
            {
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                Font font = new Font(fontfamily, fontsize, GraphicsUnit.Pixel);
                using (Graphics tgp = Graphics.FromImage(testImg))
                {
                    SizeF stringFlag = tgp.MeasureString(fonttext, font, PointF.Empty, format);

                    image = new Bitmap(Convert.ToInt32(stringFlag.Width+1), Convert.ToInt32(stringFlag.Height+1));
                    using (Graphics gp = Graphics.FromImage(image))
                    {
                       var backcolor = ARGBStringToArray(backColor);
                        gp.Clear(Color.FromArgb(255,backcolor[1], backcolor[2], backcolor[3]));
                        var argbArray = ARGBStringToArray(fontcolor);
                        if (argbArray == null || !argbArray.Any())
                            return null;
                        SolidBrush sb = new SolidBrush(Color.FromArgb((int)(opacity * 255), argbArray[1], argbArray[2], argbArray[3]));
                        gp.DrawString(fonttext, font, sb, new PointF(0, 0));
                    }
                }
            }
            return image;
        }
         
        /// <summary>
        /// 压缩图片，先定宽度最大1920px
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="compression">压缩质量 1-100(数字越小压缩率越高)</param>
        /// <returns></returns>
        public static MemoryStream Compress(MemoryStream ms, int compression = 100)
        {
            using (Bitmap srcimg = new Bitmap(ms))
            {
                int width = srcimg.Width;
                int height = srcimg.Height;
                if (width > 1920)
                {
                    width = 1920;
                    height = 1920 * srcimg.Height / srcimg.Width;
                }

                using (Bitmap dstimg = new Bitmap(width, height))//图片压缩质量
                {
                    //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
                    using (Graphics gr = Graphics.FromImage(dstimg))
                    {
                        //把原始图像绘制成上面所设置宽高的缩小图
                        Rectangle rectDestination = new Rectangle(0, 0, width, height);
                        gr.Clear(Color.WhiteSmoke);
                        gr.CompositingQuality = CompositingQuality.HighQuality;
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr.DrawImage(srcimg, rectDestination, 0, 0, srcimg.Width, srcimg.Height, GraphicsUnit.Pixel);

                        EncoderParameters ep = new EncoderParameters(1);
                        long[] qy = new long[1];
                        qy[0] = compression;//设置压缩的比例1-100
                        ep.Param[0] = new EncoderParameter(System.DrawingCore.Imaging.Encoder.Quality, qy);//设置压缩的比例1-100
                        ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                        ImageCodecInfo jpegICIinfo = arrayICI.FirstOrDefault(t => t.FormatID == ImageFormat.Jpeg.Guid);
                        MemoryStream memoryStream = new MemoryStream();
                        if (jpegICIinfo != null)
                        {
                            dstimg.Save(memoryStream, jpegICIinfo, ep);
                        }
                        else
                        {
                            dstimg.Save(memoryStream, srcimg.RawFormat);//保存到内存里
                        }
                        return memoryStream;
                    }
                }
            }
        }

    }


    /// <summary>
    /// 缩略图方式
    /// </summary>
    public enum ThumbnailProportion
    {
        /// <summary>
        /// 指定宽，高按比例   
        /// </summary>
        WIDTH,

        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        HEIHT,

        /// <summary>
        /// 指定高宽缩放（可能变形）
        /// </summary>
        WIDTH_HEIHT,

        /// <summary>
        /// 指定高宽裁减（不变形）
        /// </summary>
        CUT

    }

    /// <summary>
    /// 图片水印位置
    /// </summary>
    public enum WaterPosition
    {
        /// <summary>
        /// 左上
        /// </summary>
        左上,

        /// <summary>
        /// 中上
        /// </summary>
        中上,

        /// <summary>
        /// 右上
        /// </summary>
        右上,

        /// <summary>
        /// 左中
        /// </summary>
        左中,

        /// <summary>
        /// 中中
        /// </summary>
        中中,

        /// <summary>
        /// 右中
        /// </summary>
        右中,

        /// <summary>
        /// 左下
        /// </summary>
        左下,

        /// <summary>
        /// 中下
        /// </summary>
        中下,

        /// <summary>
        /// 右下
        /// </summary>
        右下

    }
}
