using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_mynum : System.Web.UI.Page
{
    protected int loginMode = LearnSite.Common.XmlHelp.LoginMode();
    protected bool isSameNet = LearnSite.Common.Computer.IsSameNet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GradeClass();
            ListSnum(); 
            ShowPwd();
        }

    }
    private void ListSnum()
    {
        if (DDLgrade.SelectedValue != null && DDLclass.SelectedValue != null)
        {
            int Sgrade = Int32.Parse(DDLgrade.SelectedValue);
            int Sclass = Int32.Parse(DDLclass.SelectedValue);
            LearnSite.BLL.Students st = new LearnSite.BLL.Students();
            DataListsnum.DataSource = st.GetNameNum(Sgrade, Sclass);
            DataListsnum.DataBind();
        }
    }

    private void GradeClass()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetAllGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();

        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        DDLclass.DataSource = rm.GetAllClass();
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();
    }
    protected void DataListsnum_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HyperLink hl = new HyperLink();
        hl = (HyperLink)e.Item.FindControl("HLSnum");
        string snum = hl.ToolTip;
        string sname = hl.Text;
        hl.NavigateUrl = "~/index.aspx?mysnum=" + snum + "&myname=" + sname;
        string ssex = "无";
        Image img = new Image();
        img = (Image)e.Item.FindControl("ImageStu"); 
        if (isSameNet)
        {
            img.ImageUrl = LearnSite.Common.Photo.GetStudentPhotoUrl(snum, ssex) + "?temp=" + DateTime.Now.Millisecond.ToString();
        }
        else
        {
            //如果同一网段，显示头像，否则不显示
            img.ImageUrl = "~/images/nothing.gif";
            img.ToolTip = "头像隐藏";
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        ListSnum();
        ShowPwd();
        System.Threading.Thread.Sleep(200);
    }
    private void ShowPwd()
    {
        if (loginMode == 1)
        {
            //LoginMode为1 表示班级模式，则显示密码，否则不执行下面代码
            int Sgrade = Int32.Parse(DDLgrade.SelectedValue);
            int Sclass = Int32.Parse(DDLclass.SelectedValue);
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            TextBoxPwd.Text = rm.GetRoomPwd(Sgrade, Sclass);
        }
    }
    protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListSnum();
        ShowPwd();

    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListSnum();
        ShowPwd();
    }
}