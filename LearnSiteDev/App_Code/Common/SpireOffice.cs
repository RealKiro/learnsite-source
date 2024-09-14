using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using Spire.Xls;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Presentation;

namespace LearnSite.Common
{
    /// <summary>
    ///SpireOffice 的摘要说明
    /// </summary>
    public class SpireOffice
    {
        public SpireOffice()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// Office转图片
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool OfficeToPng(int mid, string num) {
            LearnSite.BLL.Works wbll = new BLL.Works();
            string wurl= wbll.GetWorkWurla(mid, num);
            if (string.IsNullOrEmpty(wurl))
                return false;
            else {
                string wfile = HttpContext.Current.Server.MapPath(wurl);
                if (File.Exists(wfile))
                    return OfficeToPdf(wfile);
                else
                    return false;
            }
        }


        /// <summary>
        /// Office转图片
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool OfficeToPicture(string filePath)
        {
            bool result = false;
            string ext = "." + filePath.Split('.')[1];
            string savePath = filePath.Replace(ext, ".png");
            switch (ext)
            {
                case ".xls":
                case ".xlsx":
                    try
                    {
                        Workbook workbook = new Workbook();
                        workbook.LoadFromFile(filePath);
                        Worksheet sheet = workbook.Worksheets[0];
                        sheet.SaveToImage(savePath, System.Drawing.Imaging.ImageFormat.Png);    
                        result = true;
                        workbook.Dispose();
                    }
                    catch (Exception ee)
                    {
                        LearnSite.Common.Log.Addlog("Excel转png失败记录", ee.ToString());
                    }
                    break;

                case ".doc":
                case ".docx":
                    try
                    {
                        Document document = new Document();
                        document.LoadFromFile(filePath);
                        System.Drawing.Image[] img = document.SaveToImages( Spire.Doc.Documents.ImageType.Bitmap);
                        img[0].Save(savePath, System.Drawing.Imaging.ImageFormat.Png);        
                        result = true;
                        document.Dispose();
                    }
                    catch (Exception ee)
                    {
                        LearnSite.Common.Log.Addlog("Word转png失败记录", ee.ToString());
                    }
                    break;
                case ".ppt":
                case ".pptx":
                    try
                    {
                        Presentation ppt = new Presentation();
                        ppt.LoadFromFile(filePath);
                        Image image = ppt.Slides[0].SaveAsImage();
                        image.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);

                        //ppt.SaveToFile(savePath, Spire.Presentation.FileFormat.Tiff);
                        result = true;
                        ppt.Dispose();
                    }
                    catch (Exception ee)
                    {
                        LearnSite.Common.Log.Addlog("PPT转png失败记录", ee.ToString());
                    }
                    break;

            }

            return result;
        }

        /// <summary>
        /// 图片剪切
        /// </summary>
        /// <param name="filepath">源文件路径</param>
        /// <param name="CutHight">需要裁剪上下边框高度</param>
        /// <param name="CutWidth">裁剪宽度</param>
        /// <param name="thumbnailPath">图片输出路径</param>
        /// <param name="imageFormatIn">图片输入格式</param>
        /// <param name="imageFormatOut">图片输出格式</param>
        /// <returns></returns>
        public static bool CutImage(string filepath, int CutHight, int CutWidth, string thumbnailPath, ImageFormat imageFormatIn, ImageFormat imageFormatOut)
        {
            bool result = false;
            try
            {
                System.Drawing.Image originalImage = System.Drawing.Image.FromFile(filepath);
                int allW = originalImage.Width;
                int allH = originalImage.Height;
                // int oW = allW;
                //int oH = allH;
                int towidth = allW - CutHight;
                int toheight = allH - CutWidth;
                //新建一个bmp图片
                System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
                //新建一个画板
                Graphics g = System.Drawing.Graphics.FromImage(bitmap);
                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空画布并以透明背景色填充
                g.Clear(Color.Transparent);
                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, allW, allH), new Rectangle(CutHight, CutHight, towidth, toheight), GraphicsUnit.Pixel);
                //以jpg格式保存缩略图
                string path = thumbnailPath + Path.GetFileNameWithoutExtension(filepath) + "." + imageFormatOut.ToString();
                bitmap.Save(thumbnailPath, imageFormatOut);
                g.Dispose();
                bitmap.Dispose();
                result = true;
            }
            catch (Exception ee)
            {
                LearnSite.Common.Log.Addlog("图片剪切失败：", ee.ToString());
            }
            return result;
        }


        /// <summary>
        /// Office转图片
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool OfficeToPdf(string filePath)
        {
            bool result = false;
            string ext = "." + filePath.Split('.')[1];
            string savePath = filePath.Replace(ext, ".pdf");
            switch (ext)
            {
                case ".xls":
                case ".xlsx":
                    try
                    {
                        Workbook workbook = new Workbook();
                        workbook.LoadFromFile(filePath);
                        workbook.SaveToFile(savePath, Spire.Xls.FileFormat.PDF);
                        result = true;
                        workbook.Dispose();
                    }
                    catch (Exception ee)
                    {
                        LearnSite.Common.Log.Addlog("Excel转pdf失败记录", ee.ToString());
                    }
                    break;

                case ".doc":
                case ".docx":
                    try
                    {
                        Document document = new Document();
                        document.LoadFromFile(filePath);
                        document.SaveToFile(savePath, Spire.Doc.FileFormat.PDF);
                        result = true;
                        document.Dispose();
                    }
                    catch (Exception ee)
                    {
                        LearnSite.Common.Log.Addlog("Word转pdf失败记录", ee.ToString());
                    }
                    break;
                case ".ppt":
                case ".pptx":
                    try
                    {
                        Presentation ppt = new Presentation();
                        ppt.LoadFromFile(filePath);
                        ppt.SaveToFile(savePath, Spire.Presentation.FileFormat.PDF);
                        result = true;
                        ppt.Dispose();
                    }
                    catch (Exception ee)
                    {
                        LearnSite.Common.Log.Addlog("PPT转pdf失败记录", ee.ToString());
                    }
                    break;

            }

            return result;
        }

    }
}