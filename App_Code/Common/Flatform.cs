using System;
using System.Collections.Generic;
using System.Web;
namespace LearnSite.Common
{
    /// <summary>
    ///Flatform 的摘要说明
    /// </summary>
    public class Flatform
    {
        public Flatform()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        
       public static bool isUnix {
           get
           {
               return Environment.OSVersion.Platform == PlatformID.Unix;
           }       
       }

       /// <summary>
       /// 检验物理路径c:\最后一个字符是否缺少"\"，缺少则添加
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static string Checkbdir(string str)
       {
           if (!str.EndsWith("\\"))
           {
               if (Flatform.isUnix)
               {
                   str = str + "/";
               }
               else
               {
                   str = str + "\\";
               }
           }
           return str;
       }

    }


}