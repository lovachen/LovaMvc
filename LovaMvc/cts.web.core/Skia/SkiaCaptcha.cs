using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core.Skia
{
    /// <summary>
    /// 图片验证码
    /// </summary>
    public class SkiaCaptcha
    {
        /// <summary>
        /// 干扰线的颜色集合
        /// </summary>
        private static List<SKColor> Colors = new List<SKColor>()
        {
            SKColors.Orchid ,
            SKColors.Orange,
            SKColors.Olive,
            SKColors.Navy,
            SKColors.Moccasin,
            SKColors.Lime,
            SKColors.Magenta,
            SKColors.Maroon,
            SKColors.Tan,
            SKColors.Teal,
            SKColors.Thistle,
            SKColors.Tomato,
            SKColors.Violet,
            SKColors.Wheat,
            SKColors.Turquoise,
            SKColors.Peru,
            SKColors.Pink,
            SKColors.Plum,
            SKColors.Purple,
            SKColors.Red,
            SKColors.Salmon,
            SKColors.Sienna,
            SKColors.Crimson,
            SKColors.Cyan,
            SKColors.Chocolate,
            SKColors.Aquamarine,
            SKColors.Azure,
            SKColors.Beige,
            SKColors.Bisque,
            SKColors.Coral,
            SKColors.Black,
            SKColors.Blue,
            SKColors.Brown,
            SKColors.Indigo,
            SKColors.Lavender,
            SKColors.Green,
            SKColors.Fuchsia,
        };

        /// <summary>
        /// 创建画笔
        /// </summary>
        /// <param name="color"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        private static SKPaint CreatePaint(SKColor color, float fontSize)
        {
            SkiaSharp.SKTypeface font = SKTypeface.FromFamilyName(null, SKFontStyleWeight.Normal, SKFontStyleWidth.ExtraCondensed, SKFontStyleSlant.Upright);
            SKPaint paint = new SKPaint();
            paint.IsAntialias = true;
            paint.Color = color;
            paint.Typeface = font;
            paint.TextSize = fontSize;
            return paint;
        }
       
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="captchaText">验证码文字</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="lineStrookeWidth">干扰线宽度</param>
        /// <returns></returns>
        public static byte[] GetCaptcha(string captchaText, int width, int height, int lineStrookeWidth = 2)
        {
            //创建bitmap位图
            using (SKBitmap image2d = new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Premul))
            {
                //创建画笔
                using (SKCanvas canvas = new SKCanvas(image2d))
                {
                    //填充背景颜色为白色
                    canvas.DrawColor(SKColors.White);
                    Random random = new Random();
                    var chars = captchaText.ToCharArray();
                    for (int i = 0; i < chars.Length; i++)
                    {
                        //随机旋转
                        var rotate = random.Next(-5, 5);
                        //随机颜色
                        var colorIndex = random.Next(0, Colors.Count);
                        //将文字写到画布上
                        using (SKPaint drawStyle = CreatePaint(Colors[colorIndex], height - 5))
                        {
                            float x = (width / chars.Length) * i;
                            canvas.RotateDegrees(rotate);
                            canvas.DrawText(chars[i].ToString(), x, random.Next(height - 5, height), drawStyle);
                        }
                        //画随机干扰线
                        using (SKPaint drawStyle = new SKPaint())
                        {
                            drawStyle.Color = Colors[colorIndex];
                            drawStyle.StrokeWidth = lineStrookeWidth;
                            canvas.DrawLine(random.Next(0, width), random.Next(0, height), random.Next(0, width), random.Next(0, height), drawStyle);
                        }
                    }

                    //返回图片byte
                    using (SKImage img = SKImage.FromBitmap(image2d))
                    {
                        using (SKData p = img.Encode(SKEncodedImageFormat.Png, 100))
                        {
                            return p.ToArray();
                        }
                    }
                }
            }
        }
    }
}
