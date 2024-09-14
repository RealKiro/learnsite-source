using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class teacher_learnrate : System.Web.UI.Page
{
    protected string colorfast = "#D7F3CD";
    protected string colornormal = "#ACE798";
    protected string colorslow = "#8EDE75";

    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            //Btnreturn.Attributes.Add("onclick", "window.opener=null;window.open('','_self'); window.close()");
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                if (Request.QueryString["wgrade"] != null && Request.QueryString["wclass"] != null && Request.QueryString["wcid"] != null)
                {
                    ShowCid();
                    showrate();
                }
            }
        }

    }
    private void ShowCid()
    {
        LearnSite.Model.TeaCook tcook = new LearnSite.Model.TeaCook();
        int Hid = tcook.Hid;
        LabelGradeClass.Text = Request.QueryString["wgrade"].ToString() + "年级" + Request.QueryString["wclass"].ToString() + "班";
        int Sgrade = Int32.Parse(Request.QueryString["wgrade"].ToString());
        int Sclass = Int32.Parse(Request.QueryString["wclass"].ToString());
        string myCid = Request.QueryString["wcid"].ToString();//直接url传递
        string cterm = LearnSite.Common.XmlHelp.GetTerm();

        LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
        DDLCid.DataSource = cbll.ShowCidCtitle(Hid, Sgrade, Int32.Parse(cterm));
        DDLCid.DataTextField = "Ctitle";
        DDLCid.DataValueField = "Cid";
        DDLCid.DataBind();
        if (myCid != "")
        {
            DDLCid.SelectedValue = myCid;//设置为自动获取的今天本班学案Cid
        }
    }
    private void showrate()
    {
        string cidSelect = DDLCid.SelectedValue;
        if (!string.IsNullOrEmpty(cidSelect))
        {
            int Sgrade = Int32.Parse(Request.QueryString["wgrade"].ToString());
            int Sclass = Int32.Parse(Request.QueryString["wclass"].ToString());
            int Cid = Int32.Parse(cidSelect);
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            string Ctitle = cbll.GetTitle(Cid);
            DateTime dt1 = DateTime.Now;
            DataTable dt = cbll.CourseRate(Cid, Sgrade, Sclass);
            DateTime dt2 = DateTime.Now;
            GridViewclass.DataSource = dt.DefaultView.ToTable();
            GridViewclass.DataBind();

            dt.Dispose();//强制释放
            //Labelmsg.Text = LearnSite.Common.Computer.DatagoneMilliseconds(dt1, dt2) + "毫秒";
            Btnreflash.ToolTip = "刷新";
            this.Page.Title = LabelGradeClass.Text;
        }
    }

    protected void GridViewclass_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            int count = e.Row.Cells.Count;
            for (int i = 2; i < count; i++)
            {
                if (e.Row.Cells[i].Text != "-1")
                {                    
                    int sptime = Int32.Parse(e.Row.Cells[i].Text);
                    if (sptime > 0)
                    {
                        string color = colornormal;
                        if (sptime < 5)
                            color = colorfast;
                        if (sptime > 10)
                            color = colorslow;

                        e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml(color);
                        e.Row.Cells[i].ToolTip = sptime.ToString() + "分钟";
                    }
                    e.Row.Cells[i].BorderColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                }
                e.Row.Cells[i].Text = "";
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //单击行改变行背景颜色 
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }
    protected void Btnreflash_Click(object sender, ImageClickEventArgs e)
    {
        showrate();
    }

    protected void DDLCid_SelectedIndexChanged(object sender, EventArgs e)
    {
        showrate();
    }
}