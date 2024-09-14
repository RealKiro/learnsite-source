using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profile_mychange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            ShowStudent();
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    private void ShowStudent()
    {
        LearnSite.Model.Cook cook = new LearnSite.Model.Cook(); 
        
        LearnSite.BLL.Students stu = new LearnSite.BLL.Students();        
        Repeater1.DataSource = stu.GetOneStudent(cook.Sid);
        Repeater1.DataBind();
    }
}