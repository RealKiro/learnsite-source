using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_program : System.Web.UI.Page
{
    LearnSite.Model.Cook cook = new LearnSite.Model.Cook();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (Request.QueryString["lid"] != null)
            {
                if (!IsPostBack)
                {
                    ShowMission();
                    ShowIpWorkDone();
                }
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void ShowMission()
    {
        if (Request.QueryString["lid"] != null)
        {
            string Lid = Request.QueryString["lid"].ToString();
            LabelLid.Text = Lid;
            if (LearnSite.Common.WordProcess.IsNum(Lid))
            {
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                lmodel = lbll.GetModel(Int32.Parse(Lid));
                if (lmodel != null)
                {
                    LabelMcid.Text = lmodel.Lcid.ToString();
                    LabelMid.Text = lmodel.Lxid.ToString();
                    LabelLtype.Text = lmodel.Ltype.ToString();//类型编号

                    LearnSite.Model.Mission model = new LearnSite.Model.Mission();
                    LearnSite.BLL.Mission mn = new LearnSite.BLL.Mission();

                    model = mn.GetModel(lmodel.Lxid.Value);
                    if (model != null)
                    {
                        LabelMtitle.Text = model.Mtitle;
                        Mcontent.InnerHtml = HttpUtility.HtmlDecode(model.Mcontent);
                        LabelUploadType.Text = model.Mfiletype;
                        CheckBack.Checked = model.Microworld;
                        if (model.Mcategory == 13)
                        {
                            CheckBlock.Checked = true;//python拼图编程
                        }
                        if (model.Mcategory == 14)
                        {
                            CheckBlockpy.Checked = true;//python积木编程
                        }
                    }
                }
                else
                {
                    Mcontent.InnerHtml = "此学案活动不存在！" + Lid;
                }
            }
        }
    }

    private void ShowIpWorkDone()
    {
        string Sname = cook.Sname;
        string Snum = cook.Snum;
        VoteLink.NavigateUrl = "~/student/myevaluate.aspx?mid=" + LabelMid.Text + "&cid=" + LabelMcid.Text;

        LearnSite.BLL.Works bll = new LearnSite.BLL.Works();
        LearnSite.Model.Works work = new LearnSite.Model.Works();
        work = bll.GetModelByStu(Int32.Parse(LabelMid.Text), Snum);
        BtnScratch.Text = "开始创作";
        Thumbnail.ImageUrl = "~/images/thumbnail.png";//finished.png
        if (work != null)
        {

            string Wurl = work.Wurl;
            string Wthumbnail = work.Wthumbnail;
            if (!string.IsNullOrEmpty(Wthumbnail))
            {
                Thumbnail.ImageUrl = Wthumbnail + "?temp=" + DateTime.Now.Millisecond.ToString();
                Wtitle.Text = HttpUtility.HtmlDecode(work.Wtitle);
            }
            else {
                Thumbnail.ImageUrl = "~/images/finished.png";
            }
            bool IsCheck = work.Wcheck;
            if (IsCheck)
            {
                Labelmsg.Text = "你的作品已评分！";
                BtnScratch.Text = "查看作品";
                //BtnScratch.Visible = false;
            }
            else
            {
                Labelmsg.Text = "您的作品还未评分!<br/>可以继续修改提交！";
                BtnScratch.Text = "继续创作";
            }
            if (work.Wpass)
            {
                ImagePass.Visible = true;
            }

        }
        if (Snum.StartsWith("s") && cook.Sid < 0)
        {
            BtnBegin.Visible = true;
            ButtonClear.Visible = true;
            BtnScratch.Visible = true;
        }
        else
        {
            BtnBegin.Visible = false;
            ButtonClear.Visible = false;
        }
    }
    protected void BtnScratch_Click(object sender, EventArgs e)
    {
        int Qgrade = cook.Sgrade;
        int Qclass = cook.Sclass;
        string url = "";
        string ext = LabelUploadType.Text;
        string Lid = LabelLid.Text;
        string Ltype = LabelLtype.Text;
        switch (Ltype)
        {
            case "8":
                url = "~/student/python.aspx?lid=" + Lid;
                if (CheckBack.Checked)
                {
                    url = "~/student/turtleidle.aspx?lid=" + Lid;               
                }
                break;
            case "13":
                    url = "~/student/pythonblock.aspx?lid=" + Lid;

                    break;
            case "14":
                    url = "~/student/pythonblockly.aspx?lid=" + Lid;   
         
                break;
            case "5":
                url = "~/student/coding.aspx?lid=" + Lid;
                break;
            case "10":
                url = "~/student/mxgraph.aspx?lid=" + Lid;
                break;
            case "11":
                url = "~/student/pixel.aspx?lid=" + Lid;
                break;
            case "12":
                url = "~/student/htmleditor.aspx?lid=" + Lid;
                break;
            case "15":
                url = "~/student/kitymind.aspx?lid=" + Lid;
                break;
            case "16":
                url = "~/student/excel.aspx?lid=" + Lid;
                break;
            case "17":
                url = "~/student/qrcode.aspx?lid=" + Lid;
                break;
            case "18":
                url = "~/student/word.aspx?lid=" + Lid;
                break;
            case "19":
                url = "~/student/pptist.aspx?lid=" + Lid;
                break;
            case "20":
                url = "~/fabriceditor/poster.aspx?lid=" + Lid;
                break;
            case "21":
                url = "~/student/style.aspx?lid=" + Lid;
                break;
            case "22":
                url = "~/machine/imageclass.aspx?lid=" + Lid;
                break;
            case "23":
                url = "~/faceai/face.aspx?lid=" + Lid;
                break;
            case "24":
                url = "~/student/mqtt.aspx?lid=" + Lid;
                break;
            case "25":
                url = "~/student/draw.aspx?lid=" + Lid;
                break;
            case "26":
                url = "~/student/sokoban.aspx?lid=" + Lid;
                break;
            case "27":
                url = "~/deepseek/deepseek.aspx?lid=" + Lid;
                break;
            case "28":
                url = "~/deepseek/speek.aspx?lid=" + Lid;
                break;
            case "29":
                url = "~/deepseek/ocr.aspx?lid=" + Lid;
                break;
            case "30":
                url = "~/deepseek/soundlab.aspx?lid=" + Lid;
                break;
            case "31":
                url = "~/deepseek/tic-tac-toe.aspx?lid=" + Lid;
                break;
            case "32":
                url = "~/student/handnum.aspx?lid=" + Lid;
                break;
            case "33":
                url = "~/student/markdown.aspx?lid=" + Lid;
                break;
            case "34":
                url = "~/student/iframe.aspx?lid=" + Lid;
                break;
            case "35":
                url = "~/deepseek/aidraw.aspx?lid=" + Lid;
                break;
            default:
                url = "#";
                break;
        }

        string Snum = cook.Snum;

        if (Snum.StartsWith("s"))
            Response.Redirect(url);
        else
        {
            LearnSite.BLL.Room bll = new LearnSite.BLL.Room();
            bool isBegin = bll.IsRscratch(Qgrade, Qclass);
            if (isBegin)
                Response.Redirect(url);
            else
                LearnSite.Common.WordProcess.Alert("活动还未开始，请仔细听讲技术关键点！", this.Page);
        }
    }

    protected void BtnBegin_Click(object sender, EventArgs e)
    {
        int Qgrade = cook.Sgrade;
        int Qclass = cook.Sclass;
        LearnSite.BLL.Room bll = new LearnSite.BLL.Room();
        bool isBegin = bll.IsRscratch(Qgrade, Qclass);
        if (isBegin)
        {
            bll.updateRscratch(Qgrade, Qclass, false);
            Labelscratch.Text = "编程未开始!";
        }
        else
        {
            Labelscratch.Text = "编程已开始!";
            bll.updateRscratch(Qgrade, Qclass, true);
        }
    }
    protected void ButtonClear_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["lid"] != null)
        {
            LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
            string Snum = cook.Snum;
            string Wmid = LabelMid.Text;
            wbll.Delmywork(Int32.Parse(Wmid), Snum);

            LearnSite.BLL.MenuWorks kbll = new LearnSite.BLL.MenuWorks();
            string Lid = Request.QueryString["lid"].ToString();
            kbll.DeleteMenuWork(cook.Sid, Int32.Parse(Lid));

            System.Threading.Thread.Sleep(200);
            ShowIpWorkDone();
        }
    }
}