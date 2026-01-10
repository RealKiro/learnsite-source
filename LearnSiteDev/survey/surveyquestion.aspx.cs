using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_surveyquestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "调查题添加页面";
        if (Request.QueryString["vid"] != null && Request.QueryString["cid"] != null)
        {
            if (!IsPostBack)
            {
                showQuestion();
            }
        }
        else
        {
            Btnadd.Enabled = false;        
        }
    }
    protected string myCid()
    {
        if (Request.QueryString["cid"] != null)
        {
            return Request.QueryString["cid"].ToString();
        }
        else
        {
            return "";
        }
    }
    private void showQuestion()
    {
        if (Request.QueryString["qid"] != null)
        {
            string qid = Request.QueryString["qid"].ToString();
            LearnSite.Model.SurveyQuestion qmodel = new LearnSite.Model.SurveyQuestion();
            LearnSite.BLL.SurveyQuestion qbll = new LearnSite.BLL.SurveyQuestion();
            qmodel = qbll.GetModel(Int32.Parse(qid));
            mcontent.InnerText = HttpUtility.HtmlDecode(qmodel.Qtitle);
            QBlack.Checked = qmodel.Qblack;
            Btnadd.Text = "修改试题";
        }
    }

    protected void Btnadd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["vid"] != null && Request.QueryString["cid"] != null)
        {
            string fckstr = LearnSite.Common.WordProcess.ClearPP(mcontent.InnerText);
            if (fckstr.Length > 2)
            {
                int cid =Int32.Parse( Request.QueryString["cid"].ToString());
                int vid = Int32.Parse(Request.QueryString["vid"].ToString());
                LearnSite.Model.SurveyQuestion qmodel = new LearnSite.Model.SurveyQuestion();
                qmodel.Qcid = cid;
                qmodel.Qcount = 0;
                string Qtitle = HttpUtility.HtmlEncode(fckstr);
                qmodel.Qtitle = Qtitle;
                qmodel.Qvid = vid;
                qmodel.Qblack = QBlack.Checked;
                LearnSite.BLL.SurveyQuestion qbll = new LearnSite.BLL.SurveyQuestion();
                LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();
                if (Request.QueryString["qid"] != null)
                {
                    int Qid = Int32.Parse(Request.QueryString["qid"].ToString());
                    qbll.UpdateQtitle(Qid, Qtitle,QBlack.Checked);
                    mbll.UpdateItemMblack(Qid, QBlack.Checked);

                    System.Threading.Thread.Sleep(200);
                    string url = "~/survey/survey.aspx?cid=" + cid + "&vid=" + vid;
                    Response.Redirect(url, true);
                }
                else
                {
                    qbll.Add(qmodel);
                    System.Threading.Thread.Sleep(200);
                    string url = "~/survey/survey.aspx?cid=" + cid + "&vid=" + vid;
                    Response.Redirect(url, true);
                }
            }
            else
            {
                Labelmsg.Text = "调查试题文字太少！";
            }
        }
    }
    protected void BtnSurvey_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["vid"] != null && Request.QueryString["cid"] != null)
        {
            string cid = Request.QueryString["cid"].ToString();
            string vid = Request.QueryString["vid"].ToString();
            string url = "~/survey/survey.aspx?cid=" + cid + "&vid=" + vid;
            Response.Redirect(url, true);
        }
    }
}