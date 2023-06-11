using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace AlphaImageTrimmer.Models
{
    public class Trimmer
    {
        /// <summary>
        /// 入力した Bmp の透明部分を除いた矩形を取得します
        /// </summary>
        /// <param name="bmp">と透明部分を含む Bitmap を入力します</param>
        /// <returns>透明部分を除いた矩形。ただし、入力した　Bitmap が透明色を含まない場合は Rect のデフォルト値を返します。</returns>
        public static Rectangle GetRect(Bitmap bmp)
        {
            if (bmp.PixelFormat != PixelFormat.Format32bppArgb)
            {
                return default;
            }

            // 画像のピクセルを byte[] にコピーする
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            var bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            var rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);
            bmp.UnlockBits(bmpData);

            int x0 = bmp.Width;
            int y0 = bmp.Height;
            int x1 = 0;
            int y1 = 0;

            // 透明でないピクセルを探す
            for (int i = 3; i < rgbValues.Length; i += 4)
            {
                // Aの値が0なら透明ピクセル
                if (rgbValues[i] != 0)
                {
                    int x = (i / 4) % bmp.Width;
                    int y = i / 4 / bmp.Width;

                    if (x0 > x)
                    {
                        x0 = x;
                    }

                    if (y0 > y)
                    {
                        y0 = y;
                    }

                    if (x1 < x)
                    {
                        x1 = x;
                    }

                    if (y1 < y)
                    {
                        y1 = y;
                    }
                }
            }

            return new Rectangle(x0, y0, x1 - x0, y1 - y0);
        }

        /// <summary>
        /// 入力した bmp を rect に沿って切り抜き、fileName に入力した名前で保存します。
        /// </summary>
        /// <param name="bmp">切り抜く BitmapImage を入力します</param>
        /// <param name="rect">切り抜く矩形を入力します</param>
        /// <param name="fileName">切り抜いた画像を保存する際の名前を入力します</param>
        public static void Crop(Bitmap bmp, Rectangle rect, string fileName)
        {
            var newBmp = new Bitmap(rect.Width, rect.Height);
            using (var g = Graphics.FromImage(newBmp))
            {
                g.DrawImage(bmp, 0, 0, rect, GraphicsUnit.Pixel);
            }

            newBmp.Save(fileName);
        }
    }
}