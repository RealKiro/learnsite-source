using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Student_mysurvey : System.Web.UI.Page
{
    LearnSite.Model.Cook cook = new LearnSite.Model.Cook();

    protected void Page_init(object sender, EventArgs e)
    {
        showSurvey();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.Common.CookieHelp.KickStudent();
            if (!IsPostBack)
            {

                Btnshow.Attributes.Add("onclick", "this.form.target='_blank'");
                Btnok.Attributes.Add("onclick", "return   confirm('您确定要提交答卷吗？');");
            }
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
            LabelLid.Text = Lid;
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
                LabelVtotal.Text = vmodel.Vtotal.ToString();
                vcontent.InnerHtml = HttpUtility.HtmlDecode(vmodel.Vcontent);
                int vtype = vmodel.Vtype.Value;
                Lbtype.Text = vtype.ToString();
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
                bool isClose = vmodel.Vclose;
                if (isClose)
                {
                    Btnclock.ImageUrl = "~/images/clockred.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                    Btnclock.ToolTip = "暂停，请咨询老师！";
                }
                else
                {
                    Btnclock.ImageUrl = "~/images/clock.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
                    Btnclock.ToolTip = "开启，请开始回答！";
                    showQuestion();
                }
                

                int myscore = GetMyScore(Vid, fnum);
                if (myscore != -1024)
                {
                    //如果已经回答过调查
                    Btnok.Visible = false;
                    Lbcheck.Text = "已完成！";
                    Lbcheck.BackColor = System.Drawing.Color.Green;
                    Lbfscore.Text = myscore.ToString();
                    int syear = cook.Syear;
                    int sgrade = cook.Sgrade;
                    int sclass = cook.Sclass;

                    LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
                    int cns = sbll.CountClassMate(sgrade, sclass);
                    LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
                    int dcn = fbll.GetSurveyStu(sgrade, sclass, Cid, Vid);
                    if (dcn == cns)
                        Btnshow.Visible = true;
                }
                else
                {
                    Lbfscore.Text = "0";
                    if (isClose)
                    {
                        Btnok.Visible = false;//如果关闭调查，则提交按钮无效
                    }
                    else
                    {
                        Btnok.Visible = true;
                    }
                    Lbcheck.Text = "未完成！";
                    Lbcheck.BackColor = System.Drawing.Color.Red;
                    Btnshow.Visible = false; 
                    ImgBtnClear.Visible = false;
                }


                if (fnum.IndexOf('s') > -1)
                {
                    Btnshow.Visible = true;
                    //Btnok.Visible = false;
                    Btnclock.Enabled = true;
                    ImgBtnClear.Visible = true;
                }
                else
                {
                    ImgBtnClear.Visible = false;
                    Btnclock.Enabled = false;
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
                for(int i=0;i<count;i++)
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
    protected void Btnok_Click(object sender, EventArgs e)
    {
        if (LearnSite.Common.WordProcess.IsNum(LabelVid.Text))
        {
            int Lid = Int32.Parse(LabelLid.Text);
            int Cid = Int32.Parse(LabelCid.Text);
            int Vid = Int32.Parse(LabelVid.Text);
            string fnum = cook.Snum;
            string Wtime = cook.LoginTime;
            DateTime Wdate = DateTime.Now;
            if (GetMyScore(Int32.Parse(LabelVid.Text), fnum) == -1024)
            {
                int qcount = Int32.Parse(LabelVtotal.Text);//考题数
                if (qcount > 0)
                {
                    //如果有题目则分析
                    ArrayList midselect = new ArrayList();
                    ArrayList bselect = new ArrayList();
                    int scount = 0;//选择题数
                    int bcount = 0;//填空题数
                    int bdone = 0;//已完成的填空数量
                    foreach (DataListItem item in this.DataListonly.Items)
                    {
                        RadioButtonList rblm = (RadioButtonList)item.FindControl("RBLselect");
                        if (rblm.SelectedIndex > -1)
                        {
                            midselect.Add(rblm.SelectedValue);
                            scount++;
                        }
                        Panel pl = (Panel)item.FindControl("Blanks");
                        int pcount = pl.Controls.Count;
                        if (pcount > 0)
                        {
                            foreach (Control c in pl.Controls)
                            {
                                if (c is TextBox)
                                {
                                    string txt = ((TextBox)c).Text.Trim();
                                    bselect.Add(txt);
                                    bcount++;
                                    if (!String.IsNullOrEmpty(txt))
                                        bdone++;//如果不为空，说明已填写
                                }
                            }
                        }
                    }

                    int allcount = scount + bcount;

                    string msg = "";

                    int syear = cook.Syear;
                    int sgrade = cook.Sgrade;
                    int sclass = cook.Sclass;
                    int sid = cook.Sid;

                    LearnSite.Model.SurveyFeedback fmodel = new LearnSite.Model.SurveyFeedback();
                    fmodel.Fnum = fnum;
                    fmodel.Fyear = syear;
                    fmodel.Fgrade = sgrade;
                    fmodel.Fclass = sclass;
                    fmodel.Fterm = cook.ThisTerm;
                    fmodel.Fcid = Cid;
                    fmodel.Fvid = Vid;
                    fmodel.Fvtype = Int32.Parse(Lbtype.Text);

                    LearnSite.BLL.SurveyItem mbll = new LearnSite.BLL.SurveyItem();
                    int myscore = mbll.GetItemScore(midselect);
                    string[] bbarray = mbll.GetItembbScore(bselect, Vid);
                    string bok = bbarray[1].ToString();
                    string bstr = string.Join(",", (string[])bselect.ToArray(typeof(string)));

                    fmodel.Fselect = string.Join(",", (string[])midselect.ToArray(typeof(string)));
                    if (!string.IsNullOrEmpty(bok))
                        fmodel.Fselect = fmodel.Fselect + "," + bok;

                    int ppscore=Int32.Parse(bbarray[0]);
                    int allscore = myscore + ppscore;

                    fmodel.Fscore = allscore;
                    fmodel.Fdate = DateTime.Now;
                    fmodel.Fsid = sid;
                    fmodel.Flid = Lid;

                    LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
                    if (fbll.Add(fmodel) > 0)
                    {
                        //添加课堂活动记录
                        LearnSite.Model.MenuWorks kmodel = new LearnSite.Model.MenuWorks();
                        kmodel.Klid = Lid;
                        kmodel.Ksid = cook.Sid;
                        kmodel.Ktime = LearnSite.Common.Computer.GoneMinute(DateTime.Parse(Wtime), Wdate);
                        kmodel.Kcheck = false;
                        LearnSite.BLL.MenuWorks kbll = new LearnSite.BLL.MenuWorks();
                        kbll.Add(kmodel);
                    }

                    msg = "试题数量：" + allcount.ToString() + "\r\n" + bstr + "\r\n" + fmodel.Fselect + "\n得分：" + ppscore + "+" + myscore;
                    Lbtime.Text = msg;
                    
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + msg + "')</script>");
                    string myurl = "~/student/mysurvey.aspx?lid=" + Lid.ToString();
                    Response.Redirect(myurl);

                }
                else
                {
                    Btnok.Enabled = false;
                }

            }
        }
    }
    protected void Btnshow_Click(object sender, EventArgs e)
    {
        int Cid = Int32.Parse(LabelCid.Text);
        int Vid = Int32.Parse(LabelVid.Text);
        int syear = cook.Syear;
        int sgrade = cook.Sgrade;
        int sclass = cook.Sclass;
        string fnum = cook.Snum;
        string rnd = LearnSite.Common.WordProcess.RndStrnum(6);
        Session[fnum + "surveyrnd"] = rnd;
        string ty = "12";
        string url = "mysurveyclass.aspx?vid=" + Vid + "&cid=" + Cid + "&syear=" + syear + "&sgrade=" + sgrade + "&sclass=" + sclass + "&rnd=" + rnd + "&type=" + ty;
        Response.Redirect(url, true);
    }
    protected void Btnclock_Click(object sender, ImageClickEventArgs e)
    {
        string fnum = Server.UrlDecode(cook.Snum);
        if (fnum.IndexOf('s') > -1)
        {
            int Vid = Int32.Parse(LabelVid.Text);
            LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
            vbll.UpdateVclose(Vid);
            showSurvey();
        }
    }
    protected void ImgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["lid"] != null)
        {
            int Lid = Int32.Parse(LabelLid.Text);
            int Vid = Int32.Parse(LabelVid.Text);
            if (LearnSite.Common.WordProcess.IsNum(LabelVid.Text))
            {
                LearnSite.BLL.MenuWorks kbll = new LearnSite.BLL.MenuWorks();
                kbll.DeleteMenuWork(cook.Sid, Lid);

                LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
                fbll.DeleteSnum(Vid, cook.Snum);
                System.Threading.Thread.Sleep(300);
            }
            
        }
    }
}