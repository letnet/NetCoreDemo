using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace NetCoreTemplate.Utility
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class Captcha
    {
        /// <summary>
        /// 生成验证码字符串
        /// </summary>
        /// <param name="codeLen">验证码字符长度</param>
        /// <returns>返回验证码字符串</returns>
        public static string CaptchaStr(int codeLen)
        {
            if (codeLen < 1)
            {
                return string.Empty;
            }

            int number;
            string checkCode = string.Empty;
            Random random = new Random();

            for (int index = 0; index < codeLen; index++)
            {
                number = random.Next();

                char ch;
                if (number % 2 == 0)
                {
                    ch = (char)('0' + (char)(number % 10));     //生成数字
                }
                else
                {
                    ch = (char)('A' + (char)(number % 26));     //生成字母
                }

                if (ch == '0' || ch == 'o')
                {
                    index--;
                    continue;
                }
                checkCode += ch;
            }

            return checkCode;
        }

        ///<summary>
        /// 获取验证码图片流
        /// </summary>
        /// <param name="checkCode">验证码字符串</param>
        /// <returns>返回验证码图片流</returns>
        private static MemoryStream CreateCodeImg(string checkCode)
        {
            if (string.IsNullOrEmpty(checkCode))
            {
                return null;
            }
            Bitmap image = new Bitmap((int)Math.Ceiling((checkCode.Length * 13.0)) + 2, 28);
            Graphics graphic = Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                graphic.Clear(Color.White);

                int chx = 1, chy = 5;
                var rectangle = new Rectangle(0, 0, image.Width, image.Height);
                for (int chi = 0; chi < checkCode.Length; chi++)
                {
                    var ch = checkCode[chi];
                    var chfont = new Font("Arial", 14, (FontStyle.Bold | FontStyle.Italic));
                    var color = Color.Black;
                    //淡化字符颜色 
                    using (var brush = new LinearGradientBrush(rectangle, color, color, 90f, true))
                    {
                        brush.SetSigmaBellShape(0.5f);
                        graphic.DrawString(ch.ToString(), chfont, brush, chx + random.Next(-2, -1) * chi, chy + random.Next(-5, 3));
                        chx = chx + image.Width / checkCode.Length;
                    }
                }

                int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
                //干扰线
                for (int index = 0; index < 4; index++)
                {
                    x1 = random.Next(image.Width);
                    x2 = random.Next(image.Width);
                    y1 = random.Next(image.Height);
                    y2 = random.Next(image.Height);
                    graphic.DrawLine(new Pen(Color.White), x1, y1, x2, y2);
                }
                int x = 0;
                int y = 0;
                //画图片的前景噪音点
                for (int i = 0; i < 15; i++)
                {
                    x = random.Next(image.Width);
                    y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.White);
                }
                //画图片的边框线
                graphic.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //将图片验证码保存为流Stream返回
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                return ms;
            }
            finally
            {
                graphic.Dispose();
                image.Dispose();
            }
        }

        public static byte[] GetBytes(int codeLen, out string code)
        {
            code = CaptchaStr(codeLen);
            MemoryStream ms = CreateCodeImg(code);
            if (null != ms)
            {
                var bytes = ms.ToArray();
                ms.Close();
                return bytes;
            }
            else
            {
                return null;
            }
        }

        public static string GetBase64String(int codeLen, out string code)
        {
            var bytes = GetBytes(codeLen, out code);
            var base64string = Convert.ToBase64String(bytes);
            return $"data:image/jpeg;base64,{base64string}";
        }
    }
}
