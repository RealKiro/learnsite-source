using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Student_Scm : System.Web.UI.MasterPage
{
    protected string Cbanner = "";
    LearnSite.Model.Cook cook = new LearnSite.Model.Cook();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowListMenu();
        }
    }

    private void AddLessonFirst(string CurWay, string Cid, string Ctitle)
    {
        MenuItem mic = new MenuItem();
        mic.Text = Ctitle;
        mic.ImageUrl = "~/images/home.gif";
        mic.SeparatorImageUrl = "../images/separate.gif";
        mic.NavigateUrl = "~/student/showcourse.aspx?cid=" + Cid;
        Menuact.Items.Add(mic);//添加本课导学菜单
    }

    private void AddReturn()
    {
        MenuItem ms = new MenuItem();
        ms.Text = "返回";
        ms.ImageUrl = "~/images/return.png";
        ms.NavigateUrl = "~/student/myinfo.aspx";
        Menuact.Items.Add(ms);
    }

    private void ShowListMenu()
    {
        string myCid = "";
        string Lidstr = "";
        if (Request.QueryString["cid"] != null)
        {
            myCid = Request.QueryString["cid"].ToString();
        }
        else
        {
            if (Request.QueryString["lid"] != null)
            {
                Lidstr = Request.QueryString["lid"].ToString();
                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
                lmodel = lbll.GetModel(Int32.Parse(Lidstr));
                myCid = lmodel.Lcid.ToString();
            }
        }
        if (LearnSite.Common.WordProcess.IsNum(myCid))
        {
            string Uploadmode = LearnSite.Common.XmlHelp.GetUploadMode();
            string mUrl;
            switch (Uploadmode)
            {
                case "0":
                    mUrl = "mission";
                    break;
                case "1":
                    mUrl = "task";
                    break;
                default:
                    mUrl = "task";
                    break;
            }

            string CurWay = "";

            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            Cbanner = cbll.GetBanner(Int32.Parse(myCid)).Replace("~", "../..");

            string Ctitle = " 首页 "; //cbll.GetTitle(Int32.Parse(Cid));

            AddLessonFirst(CurWay, myCid.ToString(), Ctitle);
            LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
            DataTable dt = lbll.GetShowedMenu(Int32.Parse(myCid)).Tables[0];
            int dcount = dt.Rows.Count;

            if (dcount > 0)
            {
                string sepUrl = "../images/separate.gif";
                string urlarrow = "~/images/arrow.png";
                string urllocker = "~/images/locker.png";
                string urlfinish = "~/images/finish.png";
                //string urlfinishing = "~/images/finishing.png";
                LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();

                bool ispass = false;
                LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
                ispass = rbll.GetRpass(cook.Sgrade, cook.Sclass);//获取闯关模式开关
                int lcount = dcount;
                if (ispass)
                {
                    //如果课堂活动为闯关模式
                    LearnSite.BLL.MenuWorks kbll = new LearnSite.BLL.MenuWorks();
                    string lidall = "";
                    for (int i = 0; i < dcount; i++)
                    {
                        string Lid = dt.Rows[i]["lid"].ToString();
                        lidall = lidall + "'" + Lid + "'";
                        if (i < dcount - 1)
                            lidall = lidall + ",";
                    }
                    //获取已经完成的关卡数
                    lcount = kbll.GetMyLidCount(cook.Sid, lidall);
                }
                for (int i = 0; i < dcount; i++)
                {
                    string Lid = dt.Rows[i]["lid"].ToString();
                    string Lsort = dt.Rows[i]["Lsort"].ToString();
                    string Ltype = dt.Rows[i]["Ltype"].ToString();
                    string Lxidstr = dt.Rows[i]["Lxid"].ToString();
                    string Ltitlestr = dt.Rows[i]["Ltitle"].ToString();

                    MenuItem ma = new MenuItem();
                    ma.Text = Ltitlestr;
                    ma.SeparatorImageUrl = sepUrl;
                    ma.ImageUrl = urlarrow;

                    switch (Ltype)
                    {
                        case "1"://活动
                            ma.ImageUrl = "~/images/mission.png";
                            ma.NavigateUrl = "~/student/show" + mUrl + ".aspx?lid=" + Lid;
                            break;
                        case "2"://调查
                            ma.ImageUrl = "~/images/survey.png";
                            ma.NavigateUrl = "~/student/myexam.aspx?lid=" + Lid;
                            break;
                        case "3"://讨论
                            ma.ImageUrl = "~/images/topic.png";
                            ma.NavigateUrl = "~/student/topicdiscuss.aspx?lid=" + Lid;
                            break;
                        case "4"://表单
                            ma.ImageUrl = "~/images/inquiry.png";
                            ma.NavigateUrl = "~/student/txtform.aspx?lid=" + Lid;
                            break;
                        case "5"://编程
                            ma.ImageUrl = "~/images/program.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "6"://描述
                            ma.ImageUrl = "~/images/description.png";
                            ma.NavigateUrl = "~/student/description.aspx?lid=" + Lid;
                            break;
                        case "8"://编程
                            ma.ImageUrl = "~/images/python.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "9"://交互式python测评
                            ma.ImageUrl = "~/images/console.png";
                            ma.NavigateUrl = "~/student/console.aspx?lid=" + Lid;
                            break;
                        case "10"://流程图
                            ma.ImageUrl = "~/images/mxgraph.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "11"://像素画
                            ma.ImageUrl = "~/images/pixel.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "12"://单网页
                            ma.ImageUrl = "~/images/html.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "13"://拼图编程
                            ma.ImageUrl = "~/images/pythonblock.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "14"://积木编程
                            ma.ImageUrl = "~/images/blockpy.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "15"://思维导图
                            ma.ImageUrl = "~/images/kitymind.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "16"://表格处理
                            ma.ImageUrl = "~/images/excel.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "17"://二维码
                            ma.ImageUrl = "~/images/qrcode.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "18"://在线文档
                            ma.ImageUrl = "~/images/word.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "19"://演示文稿
                            ma.ImageUrl = "~/images/pptist.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "20"://海报设计
                            ma.ImageUrl = "~/images/poster.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "21"://风格迁移
                            ma.ImageUrl = "~/images/style.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "22"://图像分类
                            ma.ImageUrl = "~/images/mlimg.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "23"://人脸识别
                            ma.ImageUrl = "~/images/face.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "24"://物联网mqtt
                            ma.ImageUrl = "~/images/mqtt.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "25"://手绘画布
                            ma.ImageUrl = "~/images/excalidraw.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "26"://推箱子地图
                            ma.ImageUrl = "~/images/sokoban.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "27"://人工智能对话
                            ma.ImageUrl = "~/images/ai.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "28"://语音合成
                            ma.ImageUrl = "~/images/speek.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "29"://文字识别
                            ma.ImageUrl = "~/images/ocr.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "30"://声音分析
                            ma.ImageUrl = "~/images/sound.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "31"://井字棋
                            ma.ImageUrl = "~/images/tic-tac-toe.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "32"://手写数字识别
                            ma.ImageUrl = "~/images/handnum.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "33"://Markdown写作
                            ma.ImageUrl = "~/images/markdown.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "34"://iframe嵌入网页
                            ma.ImageUrl = "~/images/iframe.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                        case "35"://文生图
                            ma.ImageUrl = "~/images/text-to-image.png";
                            ma.NavigateUrl = "~/student/program.aspx?lid=" + Lid;
                            break;
                    }
                    if (ispass)
                    {
                        if (i < lcount)
                        {
                            bool codepass = wbll.WorkPass(cook.Sid, Int32.Parse(Lxidstr));
                            if(codepass)
                                ma.ImageUrl = urlfinish;
                        }
                        if (i > lcount)
                        {
                            ma.ImageUrl = urllocker;
                            if (cook.Sid > 0)
                                ma.NavigateUrl = "#";//如果不是模拟学生则显示链接
                        }
                    }
                    if (Lidstr == Lid)
                    {
                        CurWay = Ltitlestr;
                        ma.Selected = true;
                    }
                    Menuact.Items.Add(ma);//添加活动菜单
                }
            }
            dt.Dispose();
            AddReturn();

            int timepass = LearnSite.Common.Computer.TimePassed();
            this.Page.Title = HttpUtility.UrlDecode(cook.Sname) + " " + cook.Snum + " (" + timepass + "分钟)";

        }

    }
}
