using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class student_mysurveyresult : System.Web.UI.Page
{
    LearnSite.Model.Cook cook = new LearnSite.Model.Cook();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            showSurvey();
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void showSurvey()
    {
        if (Request.QueryString["lid"] != null)
        {
            string Lid = Request.QueryString["lid"].ToString();
            if (LearnSite.Common.WordProcess.IsNum(Lid))
            {
                LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
                LearnSite.Model.ListMenu lmodel = new LearnSite.Model.ListMenu();
                lmodel = lbll.GetModel(Int32.Parse(Lid));
                int Cid = lmodel.Lcid.Value;
                int Vid = lmodel.Lxid.Value;
                LabelCid.Text = Cid.ToString();
                LabelVid.Text = Vid.ToString();

                string fnum = cook.Snum;

                LearnSite.Model.Survey vmodel = new LearnSite.Model.Survey();
                LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
                vmodel = vbll.GetModel(Vid);
                Lbtitle.Text = vmodel.Vtitle;
                int vtype = vmodel.Vtype.Value;
                if (vtype > 0)
                {
                    Lbtypecn.Text = "测验";
                }
                else
                {
                    Lbtypecn.Text = "调查";
                }
                Lbsname.Text = Server.UrlDecode(cook.Sname);
                Lbsnum.Text = fnum;

                int myscore = GetMyScore(Vid, fnum);
                if (myscore != -1024)
                {
                    //如果已经回答过调查
                    Lbfscore.Text = myscore.ToString();
                }
            }
        }
    }
    private int GetMyScore(int vid, string fnum)
    {
        LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
        return fbll.ExistsScore(vid, fnum);
    }
    private void showQuestion()
    {
        if (LearnSite.Common.WordProcess.IsNum(LabelVid.Text))
        {
            LearnSite.BLL.SurveyQuestion qbll = new LearnSite.BLL.SurveyQuestion();
            DataListonly.DataSource = qbll.GetListByQvid(Int32.Parse(LabelVid.Text));
            DataListonly.DataBind();
        }
    }
    protected void DataListonly_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string qid = DataListonly.DataKeys[e.Item.ItemIndex].ToString();
        if (!string.IsNullOrEmpty(qid))
        {
            RadioButtonList rbl = (RadioButtonList)e.Item.FindControl("RBLselect");
            LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();
            rbl.DataSource = mbll.GetListItemDataTable(Int32.Parse(qid));
            rbl.DataTextField = "Mitem";
            rbl.DataValueField = "Mid";
            rbl.DataBind();

            DataTable blacks = mbll.GetListItemBlackDataTable(Int32.Parse(qid));
            int count = blacks.Rows.Count;
            Panel pl = (Panel)e.Item.FindControl("Blanks");
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    TextBox t = new TextBox();
                    t.ID = "txt" + blacks.Rows[i]["Mid"].ToString();
                    t.Attributes.Add("onpaste", "return  false;");
                    t.Attributes.Add("οndragstart", "return  false;");
                    t.CssClass = "txtblank";
                    pl.Controls.Add(t);
                }
            }
            else
                pl.Visible = false;
        }
    }
}