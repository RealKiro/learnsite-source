using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_pixeledit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.QueryString["mcid"] != null && Request.QueryString["mid"] != null)
        {
            if (!IsPostBack)
            {
                Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "自定义主题修改页面";
                ShowMgid();
                missionview();
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
    protected void Btnedit_Click(object sender, EventArgs e)
    {
        string fckstr = mcontent.InnerText;
        if (Texttitle.Text != "" && fckstr != "")
        {
            if (Request.QueryString["mcid"] != null && Request.QueryString["mid"] != null)
            {
                string Mcid = Request.QueryString["mcid"].ToString();
                string Mid = Request.QueryString["mid"].ToString();

                LearnSite.Model.Mission mission = new LearnSite.Model.Mission();
                mission.Mid = Int32.Parse(Mid);
                mission.Mtitle = HttpUtility.HtmlEncode(Texttitle.Text.Trim());
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

                mission.Mexample = exampleurl;//编程实例

                LearnSite.BLL.Mission missionbll = new LearnSite.BLL.Mission();
                missionbll.Update(mission);

                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();

                lmodel.Lcid = Int32.Parse(Mcid);
                lmodel.Lxid = Int32.Parse(Mid);
                lmodel.Ltype = Int32.Parse(titleValue);//页面类型为 自定义主题
                lmodel.Lshow = CheckPublish.Checked;
                lmodel.Ltitle = Texttitle.Text.Trim();
                lbll.UpdateMenuThree(lmodel);
                System.Threading.Thread.Sleep(500);
                string url = "~/teacher/pixelshow.aspx?mcid=" + Mcid + "&mid=" + Mid;
                Response.Redirect(url, false);
            }
            else
            {
                Labelmsg.Text = "取不到主题编号Mid！";
            }
        }
        else
        {
            Labelmsg.Text = "内容及标题不能为空！";
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
    private void missionview()
    {
        if (Request.QueryString["mid"] != null)
        {
            int Mid = Int32.Parse(Request.QueryString["mid"].ToString());
            LearnSite.Model.Mission mission = new LearnSite.Model.Mission();
            LearnSite.BLL.Mission missionbll = new LearnSite.BLL.Mission();
            mission = missionbll.GetModel(Mid);
            CheckPublish.Checked = mission.Mpublish;
            Texttitle.Text = mission.Mtitle;
            mcontent.InnerText = HttpUtility.HtmlDecode(mission.Mcontent);
            DDLTitle.SelectedValue = mission.Mcategory.ToString();
            string mgid = mission.Mgid.ToString();
                string exampleurl = mission.Mexample;
            if (DDLMgid.Items.FindByValue(mgid) != null)
                DDLMgid.SelectedValue = mgid;
            if (DDLTitle.SelectedValue == "24")
            {
                Ckdevice.Visible = true;
                if (exampleurl != "")
                {
                    FillCheckBoxList(exampleurl, Ckdevice);
                }
            }
            else
            {
                Ckdevice.Visible = false;
            }
            if (DDLTitle.SelectedValue == "34")
            {
                Texturl.Visible = true;
                Texturl.Text = exampleurl;
            }
            else
            {
                Texturl.Visible = false;
            }
        }
    }

    /// <param name="str">字符串，格式要求为“A,B,C”</param>
    /// <param name="checkBoxList">CheckBoxList控件</param>

    public void FillCheckBoxList(string str, CheckBoxList checkBoxList)
    {
        string[] items = str.Split(',');
        //遍历items
        foreach (string item in items)
        {
            //如果值相等，则选中该项
            foreach (ListItem listItem in checkBoxList.Items)
            {
                if (item == listItem.Value)
                    listItem.Selected = true;
                else
                    continue;
            }
        }
    }

    protected void BtnCourse_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["mcid"] != null && Request.QueryString["mid"] != null)
        {
            string Mcid = Request.QueryString["mcid"].ToString();
            string url = "~/teacher/courseshow.aspx?cid=" + Mcid.ToString();
            Response.Redirect(url, false);
        }
    }
}