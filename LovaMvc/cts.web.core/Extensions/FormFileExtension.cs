using cts.web.core.Librs;
using cts.web.core.MediaItem;
using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 上传文件扩展
    /// </summary>
    public static class FormFileExtension
    {
        //255216是jpg;7173是gif;6677是BMP,13780是PNG
        private static string[] img_exArr = new string[] { "255216", "7173", "6677", "13780" };

        /// <summary>
        /// 文件是否是图片
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static bool IsImage(this IFormFile formFile)
        {
            using (Stream stream = formFile.OpenReadStream())
            {
                using (BinaryReader r = new BinaryReader(stream))
                {
                    StringBuilder sb = new StringBuilder();
                    byte buffer;
                    buffer = r.ReadByte();
                    sb.Append(buffer.ToString());
                    buffer = r.ReadByte();
                    sb.Append(buffer.ToString());
                    return img_exArr.Any(o => o.Contains(sb.ToString().ToLower()));
                }
            }
        }

        /// <summary>
        /// 文件大小超出范围
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="length">字节大小 例 100kb，100*1024 = 102400 </param>
        /// <returns></returns>
        public static bool IsBigSize(this IFormFile formFile, int length)
        {
            return formFile.Length > length;
        }

        /// <summary>
        /// 获取文件的哈希SHA1值
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static string GetSHA1(this IFormFile formFile)
        {
            using (var ms = formFile.OpenReadStream())
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] retval = sha1.ComputeHash(ms);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retval.Length; i++)
                    sb.AppendFormat("{0:X2}", retval[i]);
                return sb.ToString();
            }
        }

        /// <summary>
		/// 获取文件的MD5值
		/// </summary>
		/// <param name="formFile"></param>
		/// <returns></returns>
		public static string GetMD5(this IFormFile formFile)
        {
            using (Stream inputStream = formFile.OpenReadStream())
            {
                byte[] array = MD5.Create().ComputeHash(inputStream);
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < array.Length; i++)
                {
                    stringBuilder.Append(array[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// 创建保存图片文件
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="imageStorage"></param>
        /// <param name="virtualPath">虚拟相对路径 xxx/xxx</param>
        /// <param name="compress">是否压缩图片</param>
        /// <param name="flag">压缩质量 1-100(数字越小压缩率越高)。只有启用压缩是才起作用</param>
        /// <returns></returns>
        public static ImageInfo CreateImagePathFromStream(this IFormFile formFile, IMediaItemStorage imageStorage, string virtualPath, bool compress = false, int flag = 50)
        {
            return CreateImagePathFromStream(formFile, imageStorage, virtualPath, false, compress, flag);
        }

        /// <summary>
        /// 创建保存图片文件
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="imageStorage"></param>
        /// <param name="virtualPath">虚拟相对路径 xxx/xxx</param>
        /// <param name="suffix">是否带后缀</param>
        /// <param name="compress">是否压缩图片</param>
        /// <param name="flag">压缩质量 1-100(数字越小压缩率越高)。只有启用压缩是才起作用</param>
        /// <returns></returns>
        public static ImageInfo CreateImagePathFromStream(this IFormFile formFile, IMediaItemStorage imageStorage, string virtualPath, bool suffix, bool compress, int flag)
        {
            ImageInfo imageInfo = new ImageInfo();
            using (Stream stream = formFile.OpenReadStream())
            {
                imageInfo.FileName = formFile.FileName;
                using (MemoryStream memoryStream2 = new MemoryStream())
                {
                    MemoryStream memoryStream = null;
                    stream.CopyTo(memoryStream2);
                    string obj = suffix ? (Guid.NewGuid() + Path.GetExtension(formFile.FileName)) : Guid.NewGuid().ToString();
                    string fileName = imageInfo.NewFileName = obj;
                    imageInfo.ExtName = Path.GetExtension(formFile.FileName);
                    memoryStream = ((!compress) ? memoryStream2 : ImageHelper.Compress(memoryStream2, flag));
                    imageInfo.IOPath = imageStorage.Storage(memoryStream, virtualPath, fileName);
                    using (Image image = Image.FromStream(memoryStream))
                    {
                        imageInfo.Width = image.Width;
                        imageInfo.Height = image.Height;
                    }
                    imageInfo.Length = memoryStream.Length;
                    memoryStream.Dispose();
                    return imageInfo;
                }
            }
        }

 
    }



    /// <summary>
    /// 上传文件后返回的文件结果 
    /// </summary>
    public class ImageInfo
    {
        /// <summary>
		///
		/// </summary>
		public long Length
        {
            get;
            set;
        }

        /// <summary>
        /// 原文件名
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// 新文件名
        /// </summary>
        public string NewFileName
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public string IOPath
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string ExtName
        {
            get;
            set;
        }

        /// <summary>
        /// 宽
        /// </summary>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// 高
        /// </summary>
        public int Height
        {
            get;
            set;
        }
    }














}
