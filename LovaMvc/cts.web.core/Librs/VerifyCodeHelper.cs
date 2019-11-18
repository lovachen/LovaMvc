using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core.Librs
{

    /// <summary>
    /// 
    /// </summary>
    public enum VerifyCodeType
    {
        /// <summary>
        /// 数字验证码
        /// </summary>
        NumberVerifyCode,

        /// <summary>
        /// 字母验证码
        /// </summary>
        AbcVerifyCode,

        /// <summary>
        /// 混合验证码
        /// </summary>
        MixVerifyCode
    };


    /// <summary>
    /// 验证码生成
    /// </summary>
    public class VerifyCodeHelper
    {
        /// <summary>  
        /// 1.数字验证码  
        /// </summary>  
        /// <param name="length"></param>  
        /// <returns></returns>  
        private static string CreateNumberVerifyCode(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值    
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字    
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字    
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码    
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }

        /// <summary>  
        /// 2.字母验证码  
        /// </summary>  
        /// <param name="length">字符长度</param>  
        /// <returns>验证码字符</returns>  
        private static string CreateAbcVerifyCode(int length)
        {
            char[] verification = new char[length];
            char[] dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                verification[i] = dictionary[random.Next(dictionary.Length - 1)];
            }
            return new string(verification);
        }

        /// <summary>  
        /// 3.混合验证码  
        /// </summary>  
        /// <param name="length">字符长度</param>  
        /// <returns>验证码字符</returns>  
        private static string CreateMixVerifyCode(int length)
        {
            char[] verification = new char[length];
            char[] dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                verification[i] = dictionary[random.Next(dictionary.Length - 1)];
            }
            return new string(verification);
        }

        /// <summary>  
        /// 产生验证码
        /// </summary>
        /// <param name="min">最小长度</param>
        /// <param name="max">最大长度</param>
        /// <param name="type">验证码类型：数字，字符，符合</param>  
        /// <returns></returns>  
        public static string CreateVerifyCode(int min, int max, VerifyCodeType type = VerifyCodeType.MixVerifyCode)
        {
            string verifyCode = string.Empty;
            Random random = new Random();
            int length = random.Next(min, max);
            switch (type)
            {
                case VerifyCodeType.NumberVerifyCode:
                    verifyCode = CreateNumberVerifyCode(length);
                    break;
                case VerifyCodeType.AbcVerifyCode:
                    verifyCode = CreateAbcVerifyCode(length);
                    break;
                case VerifyCodeType.MixVerifyCode:
                    verifyCode = CreateMixVerifyCode(length);
                    break;
            }
            return verifyCode;
        }

        /// <summary>  
        /// 产生验证码,默认长度4位
        /// </summary>  
        /// <param name="length"></param>
        /// <param name="type">验证码类型：数字，字符，符合。默认数字</param>  
        /// <returns></returns>  
        public static string CreateVerifyCode(int length = 4, VerifyCodeType type = VerifyCodeType.MixVerifyCode)
        {
            string verifyCode = string.Empty;
            Random random = new Random();
            switch (type)
            {
                case VerifyCodeType.NumberVerifyCode:
                    verifyCode = CreateNumberVerifyCode(length);
                    break;
                case VerifyCodeType.AbcVerifyCode:
                    verifyCode = CreateAbcVerifyCode(length);
                    break;
                case VerifyCodeType.MixVerifyCode:
                    verifyCode = CreateMixVerifyCode(length);
                    break;
            }
            return verifyCode;
        }

    }
}
