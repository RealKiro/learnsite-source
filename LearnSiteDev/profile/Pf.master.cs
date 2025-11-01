using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_Profile_Pf : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (!IsPostBack)
            {
                ShowStudent();
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    private void ShowStudent()
    {
        LearnSite.Model.Cook cook = new LearnSite.Model.Cook();

        int mySid = cook.Sid;
        LearnSite.BLL.Students dbll = new LearnSite.BLL.Students();
        sleadername.Text = Server.UrlDecode(dbll.GetMySgtitle(mySid));
        string ssex = cook.Sex;
        snum.Text = Server.UrlDecode(cook.Snum);

        string murl = LearnSite.Common.Photo.GetStudentPhotoUrl(cook.Snum, ssex);
        Imageface.ImageUrl = murl + "?temp=" + DateTime.Now.Millisecond.ToString();

        string Sgrade = cook.Sgrade.ToString();
        string Sclass = cook.Sclass.ToString();
        sclass.Text = Sgrade + "." + Sclass + "班";
        sname.Text = Server.UrlDecode(cook.Sname);

        LabelRank.Text = Server.UrlDecode(cook.RankImage);
        LabelRank.ToolTip = "你当前的等级为：" + cook.Sscore / 3 + "级  加速升级中…";
    }
}
