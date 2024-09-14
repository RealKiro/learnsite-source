using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_Stud : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LearnSite.Model.Cook cook = new LearnSite.Model.Cook();
            if (cook.IsExist())
            {
                int timepass = LearnSite.Common.Computer.TimePassed();
                this.Page.Title = HttpUtility.UrlDecode(cook.Sname) + " " + cook.Snum+" ("+timepass+")";
            }
        }
    }


}
