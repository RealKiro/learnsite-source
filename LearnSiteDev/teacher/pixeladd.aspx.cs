using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_pixeladd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.QueryString["mcid"] != null)
        {
            if (!IsPostBack)
            {
                Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "自定义主题添加";
                ShowMgid();
            }
        }
        else
        {
            Response.Redirect("~/teacher/course.aspx", false);
        }
    }
    protected string myCid()
    {
        if (Request.QueryString["mcid"] != null)
        {
            return Request.QueryString["mcid"].ToString();
        }
        else
        {
            return "";
        }
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        string fckstr = Request.Form["textareaItem"].Trim();
        if (Texttitle.Text != "" && fckstr != "")
        {
            if (Request.QueryString["mcid"] != null)
            {
                string Mcidstr = Request.QueryString["mcid"].ToString();
                int Mcid = Int32.Parse(Mcidstr);
                LearnSite.BLL.Mission missionbll = new LearnSite.BLL.Mission();
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
                int maxSort = lbll.GetMaxLsort(Mcid) + 1;

                LearnSite.Model.Mission mission = new LearnSite.Model.Mission();
                mission.Mcid = Mcid;
                mission.Mtitle = HttpUtility.HtmlEncode(Texttitle.Text.Trim());
                mission.Msort = maxSort;
                mission.Mupload = true;

                string exampleurl = "";//编程实例
                mission.Mpublish = CheckPublish.Checked;
                mission.Mcontent = HttpUtility.HtmlEncode(fckstr);
                string titleValue = DDLTitle.SelectedValue;
                switch (titleValue)
                { 
                   case "17":
                        mission.Mfiletype = "qrcode";//二维码
                        break;
                   case "18":
                        mission.Mfiletype = "word";//在线文档
                        break;
                   case "19":
                        mission.Mfiletype = "pptist";//演示文稿
                        break;
                   case "20":
                        mission.Mfiletype = "poster";//演示文稿
                        break;
                   case "21":
                        mission.Mfiletype = "style";//风格迁移
                        break;
                   case "22":
                        mission.Mfiletype = "mlimg";//图像分类
                        break;  
                   case "23":
                        mission.Mfiletype = "face";//人脸识别
                        break;
                   case "24":
                        mission.Mfiletype = "mqtt";//物联网mqtt
                        foreach (ListItem li in Ckdevice.Items)
                        {
                            if (li.Selected) exampleurl += li.Value + ",";
                        }
                        break;
                   case "25":
                        mission.Mfiletype = "excalidraw";//手绘画布
                        break;
                   case "26":
                        mission.Mfiletype = "sokoban";//推箱子地图
                        break;
                   case "27":
                        mission.Mfiletype = "ai";//人工智能对话
                        break;
                   case "28":
                        mission.Mfiletype = "speek";//语音合成
                        break;
                   case "29":
                        mission.Mfiletype = "ocr";//文字识别
                        break;
                   case "30":
                        mission.Mfiletype = "sound";//声音分析
                        break;
                   case "31":
                        mission.Mfiletype = "tic-tac-toe";//井字棋
                        break;
                   case "32":
                        mission.Mfiletype = "handnum";//手写数字识别
                        break;
                   case "33":
                        mission.Mfiletype = "markdown";//Markdown写作
                        break;
                   case "34":
                        mission.Mfiletype = "iframe";//iframe嵌入网页
                        exampleurl = Texturl.Text.Trim();
                        break;
                   case "35":
                        mission.Mfiletype = "text-to-image";//文生图
                        break;
                   case "36":
                        mission.Mfiletype = "web";//素材库
                        break;
                   case "37":
                        mission.Mfiletype = "website";//网站设计
                        break;
                    default:
                        mission.Mfiletype = "pxl";//像素画
                        break;
                }
                mission.Mcategory = Int32.Parse(titleValue);//自定义主题页面
                mission.Mdate = DateTime.Now;
                mission.Mhit = 0;
                mission.Mgroup = false;
                if (DDLMgid.SelectedValue != "")
                    mission.Mgid = Int32.Parse(DDLMgid.SelectedValue);
                else
                    mission.Mgid = 0;

                mission.Mexample = exampleurl;

                int mid = missionbll.Add(mission);
                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                lmodel.Lcid = Mcid;
                lmodel.Lshow = CheckPublish.Checked;
                lmodel.Lsort = maxSort;
                lmodel.Ltitle = Texttitle.Text.Trim();
                lmodel.Ltype = Int32.Parse(titleValue);//页面类型为 自定义主题
                lmodel.Lxid = mid;
                lbll.Add(lmodel);
                System.Threading.Thread.Sleep(500);
                string url = "~/teacher/courseshow.aspx?cid=" + Mcid.ToString();
                Response.Redirect(url, false);
            }

        }
        else
        {
            Labelmsg.Text = "请填写主题！";
        }
    }
    protected void BtnCourse_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["mcid"] != null)
        {
            string Cid = Request.QueryString["mcid"].ToString();
            string url = "~/teacher/courseshow.aspx?cid=" + Cid;
            Response.Redirect(url, false);
        }
    }

    private void ShowMgid()
    {
        LearnSite.Model.TeaCook tcook = new LearnSite.Model.TeaCook();
        int hid = tcook.Hid;

        LearnSite.BLL.Gauge gbll = new LearnSite.BLL.Gauge();
        DDLMgid.DataSource = gbll.GetListGauge(hid);
        DDLMgid.DataTextField = "Gtitle";
        DDLMgid.DataValueField = "Gid";
        DDLMgid.DataBind();
    }
    protected void DDLTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLTitle.SelectedValue == "24")
        {
            Ckdevice.Visible = true;
        }
        else {
            Ckdevice.Visible = false;
        }
        if (DDLTitle.SelectedValue == "34")
        {
            Texturl.Visible = true;
        }
        else
        {
            Texturl.Visible = false;
        }
    }
}