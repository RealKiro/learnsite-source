using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
public partial class Teacher_start : System.Web.UI.Page
{
    LearnSite.Model.TeaCook tcook = new LearnSite.Model.TeaCook();
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            if (!IsPostBack)
            {
                Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "上课开始页面";
                Btnstudent.Attributes.Add("onclick", "this.form.target='_black'");
                Btnrefresh.Attributes.Add("onclick", "this.form.target='_self'");
                BtnaAllQuit.Attributes["OnClick"] = "return confirm('您确定要将当前上课班级学生全体下线吗？');";
                GradeClass();
                showCid();//放在签到显示前，以便绑定
                Showkc();
                showhouse();
                ShowSigin();
                ShowNoSigin();
                HowManyWork();
                showLock();
                showwtUrl();
                showMenu();
            }
        }
    }

    private void showhouse()
    {
        string pcroom = tcook.Hroom;
        if (!LearnSite.Common.XmlHelp.GetHouseMode())
        {
            LearnSite.BLL.Computers cbll = new LearnSite.BLL.Computers();
            DDLhouse.DataSource = cbll.CmpRoom();
            DDLhouse.DataTextField = "Pm";
            DDLhouse.DataValueField = "Pm";
            DDLhouse.DataBind();
            HyperLinkSeat.Visible = false;
        }
        else
        {
            HyperLinkSeat.Visible = true;
            LearnSite.BLL.House hbll = new LearnSite.BLL.House();
            DDLhouse.DataSource = hbll.GetListHouse();
            DDLhouse.DataTextField = "Hname";
            DDLhouse.DataValueField = "Hid";
            DDLhouse.DataBind();
        }
        if (DDLhouse.Items.FindByValue(pcroom) != null)
        {
            DDLhouse.SelectedValue = pcroom;
        }
    }
    private void showLock()
    {
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        LearnSite.Model.Room rmodel = new LearnSite.Model.Room();
        rmodel = rm.GetModel(Rgrade, Rclass);
        CheckBoxip.Checked = rmodel.Rlock;
        CheckBoxOpen.Checked = rmodel.Ropen;
        CheckBoxRgauge.Checked = rmodel.Rgauge;
        CheckBoxPwd.Checked = rmodel.Rpwdsee;
        CheckBoxShare.Checked = rmodel.Rshare;
        CheckBoxGroupShare.Checked = rmodel.Rgroupshare;
        CheckBoxScratch.Checked = rmodel.Rscratch;
        CheckBoxLogin.Checked = rmodel.Rlogin;
        CheckBoxPass.Checked = rmodel.Rpass;
        string reat = rmodel.Rseat.Value.ToString();
        TBpwd.Text = rmodel.Rpwd;
        TBpwd.ToolTip = "班级模式：学生使用生成的班级密码登录";
        if (LearnSite.Common.XmlHelp.LoginMode() == 0) //如果是0，个人模式，1为班级模式
        {
            TBpwd.Text = "个人模式";
            CheckBoxLogin.Visible = false;
            TBpwd.ToolTip = "个人模式：学生使用个人密码登录";
        }

        /*
        if (LearnSite.Common.XmlHelp.GetSingleLogin())//如果是单点登录
        {
            BtnaAllQuit.Visible = true;
        }
         */

    }

    private void showCid()
    {
        int Chid = tcook.Hid;
        int Cobj = Int32.Parse(DDLgrade.SelectedValue);
        int Cterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
        LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
        DDLCid.DataSource = cbll.ShowCidCtitle(Chid, Cobj, Cterm);
        DDLCid.DataTextField = "Ctitle";
        DDLCid.DataValueField = "Cid";
        DDLCid.DataBind();
    }

    /// <summary>
    /// 显示本班级已学和未学学案
    /// </summary>
    private void Showkc()
    {
        int Rhid = tcook.Hid;
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
        int Syear = sbll.GetYear(Rgrade, Rclass);
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        int Wterm = Int32.Parse(LearnSite.Common.XmlHelp.GetTerm());
        string Wcids = wbll.ShowDoneWorkCids(Rgrade, Rclass, Wterm, Syear);//作品
        LearnSite.BLL.TopicReply Tbll = new LearnSite.BLL.TopicReply();
        string Tcids = Tbll.ShowDoneReplyCids(Rgrade, Rclass, Wterm, Syear);//讨论回复
        LearnSite.BLL.SurveyFeedback fbll = new LearnSite.BLL.SurveyFeedback();
        string Fcids = fbll.ShowFeedbackCids(Rgrade, Rclass, Wterm, Syear);//调查测验
        LearnSite.BLL.Solves vbll = new LearnSite.BLL.Solves();
        string vcids = vbll.ShowDoneSovleCids(Rgrade.ToString(), Rclass.ToString(), Wterm.ToString(), Syear.ToString());//python测评

        string rrr = Syear.ToString() + Rgrade.ToString() + Rclass.ToString();
        int simiSid = 0 - Int32.Parse(rrr);//将入学年度和班级号作为模拟学生的ID
        LearnSite.BLL.MenuWorks kbll = new LearnSite.BLL.MenuWorks();
        string kcids = kbll.readCids(simiSid);//阅读任务

        LearnSite.BLL.TxtFormBack txbll = new LearnSite.BLL.TxtFormBack();
        string txcids = txbll.ShowDoneBackCids(Rgrade, Rclass, Wterm, Syear);//填表

        string allCids = Wcids + Tcids + Fcids + vcids + kcids + txcids;
        allCids = LearnSite.Common.WordProcess.SimpleWords(allCids);
        LearnSite.BLL.Courses cs = new LearnSite.BLL.Courses();
        DLdonekc.DataSource = cs.ShowClassDoneCourse(Rgrade, Rhid, allCids);
        DLdonekc.DataBind();
        DLnewkc.DataSource = cs.ShowClassnewCourse(Rgrade, Rhid, allCids);
        DLnewkc.DataBind();
    }

    protected void Btnset_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid =  tcook.Hid.ToString();
            int Rhid = Int32.Parse(Hid);//教师编号
            int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
            int Rclass = Int32.Parse(DDLclass.SelectedValue);
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            string cid = DDLCid.SelectedValue;
            if (!string.IsNullOrEmpty(cid))
                rm.UpdateRcid(Rgrade, Rclass, Int32.Parse(cid));
            if (LearnSite.Common.XmlHelp.LoginMode() == 1) //如果是0，个人模式，1为班级模式
            {
                int pwdlen = 3;
                TBpwd.Text = rm.TeachingRoomSet(Rhid, Rgrade, Rclass, pwdlen);//返回班级密码
                TBpwd.ToolTip = "班级模式下学生登录的密码！";
            }
            else
            {
                TBpwd.Text = "个人模式";
                int pwdlen = 6;
                rm.TeachingRoomSet(Rhid, Rgrade, Rclass, pwdlen);//设置一下配合黄池祥老师的管理软件
                TBpwd.ToolTip = "学生登录请使用自己的个人密码！";
            }
            Session[Hid + "grade"] = DDLgrade.SelectedValue;
            Session[Hid + "StartTime"] = DateTime.Now.ToString();
            Btnset.Enabled = false;
            DDLgrade.Enabled = false;
            DDLclass.Enabled = false;
            DDLhouse.Enabled = false;
            Btnstudent.Enabled = true;
            Btnrefresh.Enabled = true;
            LearnSite.BLL.Students stu = new LearnSite.BLL.Students();
            stu.ThisClassTeamScoresNew(Rgrade, Rclass);//批量更新group新方法
            ShowNoSigin();
            DDLCid.Enabled = false;
            //LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
            //vbll.SetClose(Rhid);//开始上课时，先将测验处于关闭状态
        }
    }

    private void ShowSigin()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid =  tcook.Hid.ToString();
            int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
            int Rclass = Int32.Parse(DDLclass.SelectedValue);
            DateTime dt = DateTime.Now;
            int Qyear = dt.Year;
            int Qmonth = dt.Month;
            int Qday = dt.Day;
            string SignSort = RBsort.SelectedValue;//获取上课页面签到排序方法
            LabelToday.Text = " 服务器日期校准：" + Qyear.ToString() + "年" + Qmonth.ToString() + "月" + Qday.ToString() + "日";
            LearnSite.BLL.Signin sg = new LearnSite.BLL.Signin();
            string pcroom = DDLhouse.SelectedValue;
            if (SignSort.Equals("3"))
            {
                LearnSite.Model.SeatCollect sctm = sg.StartSignTable(Rgrade, Rclass, Qyear, Qmonth, Qday, pcroom);
                DLonline.DataSource = sctm.Dt;
                DLonline.DataBind();
                DLonline.RepeatColumns = sctm.Column;
                Labelsigin.Text = sctm.Online.ToString();
            }
            else
            {
                DLonline.DataSource = sg.StartSignClass(Rgrade, Rclass, Qyear, Qmonth, Qday, SignSort);
                DLonline.DataBind();
                DLonline.RepeatColumns = 8;
                Labelsigin.Text = DLonline.Items.Count.ToString();
            }
        }
    }
    /// <summary>
    /// 设置作品展示和汇总表、座位表 链接
    /// </summary>
    private void showwtUrl()
    {
        string Rgrade = DDLgrade.SelectedValue;
        string Rclass = DDLclass.SelectedValue;
        string Rcid = DDLCid.SelectedValue;
        string Houseid = DDLhouse.SelectedValue;
        if (!string.IsNullOrEmpty(Rcid))
        {
            HylkDiskGroup.NavigateUrl = "~/teacher/sharegview.aspx?wgrade=" + Rgrade + "&wclass=" + Rclass;
            HylkDiskstu.NavigateUrl = "~/teacher/shareview.aspx?wgrade=" + Rgrade + "&wclass=" + Rclass;
            HLworkshow.NavigateUrl = "~/teacher/workshow.aspx?wgrade=" + Rgrade + "&wclass=" + Rclass + "&wcid=" + Rcid;
            HLtotal.NavigateUrl = "~/teacher/coursetotal.aspx?wgrade=" + Rgrade + "&wclass=" + Rclass + "&wcid=" + Rcid;
            HLrate.NavigateUrl = "~/teacher/learnrate.aspx?wgrade=" + Rgrade + "&wclass=" + Rclass + "&wcid=" + Rcid;

            if (Houseid != "")
                HyperLinkSeat.NavigateUrl = "~/seat/seatshow.aspx?hid=" + Houseid + "&sgrade=" + Rgrade + "&sclass=" + Rclass;
        }
    }
    private void ShowNoSigin()
    {
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
        int Syear = sbll.GetYear(Rgrade, Rclass);
        DateTime dt = DateTime.Now;
        int Qyear = dt.Year;
        int Qmonth = dt.Month;
        int Qday = dt.Day;
        LearnSite.BLL.Signin sg = new LearnSite.BLL.Signin();
        DLnotline.DataSource = sg.StartNoSignClassTwo(Rgrade, Rclass, Syear, Qyear, Qmonth, Qday);
        DLnotline.DataBind();
        Labelsigno.Text = DLnotline.Items.Count.ToString();
    }

    private void GradeClass()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            LearnSite.BLL.Room room = new LearnSite.BLL.Room();

            string Hid =  tcook.Hid.ToString();
            DDLgrade.DataSource = room.GetGrade(Int32.Parse(Hid));
            DDLgrade.DataTextField = "Rgrade";
            DDLgrade.DataValueField = "Rgrade";
            DDLgrade.DataBind();
            if (Session[Hid + "grade"] != null)
                DDLgrade.SelectedValue = Session[Hid + "grade"].ToString();

            int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            DDLclass.DataSource = rm.GetLimitClass(Rgrade);
            DDLclass.DataTextField = "Rclass";
            DDLclass.DataValueField = "Rclass";
            DDLclass.DataBind();
            if (Session[Hid + "class"] != null)
                DDLclass.SelectedValue = Session[Hid + "class"].ToString();
        }
    }

    private void HowManyWork()
    {
        int i = 0;
        foreach (DataListItem item in this.DLonline.Items)
        {
            if (Int32.Parse(((Label)item.FindControl("Labelwork")).Text) > 0)
            {
                i++;
            }
        }
        Labelcount.Text = "已经提交作品共" + i.ToString() + "位";
    }

    protected void DLonline_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Color cr = Color.Silver;
        Label hl = new Label();
        hl = (Label)e.Item.FindControl("HyperSname");
        HyperLink lk = (HyperLink)e.Item.FindControl("Groupflag");
        LinkButton lbunlock = (LinkButton)e.Item.FindControl("Lunlock");
        if (!string.IsNullOrEmpty(hl.Text))
        {
            Label lb = new Label();
            lb = (Label)e.Item.FindControl("Labelwork");
            int Qwork = Int32.Parse((lb.Text));
            switch (Qwork)
            {
                case 1:
                    hl.BackColor = Labelone.BackColor;
                    break;
                case 2:
                    hl.BackColor = Labeltwo.BackColor;
                    break;
                case 3:
                    hl.BackColor = Labelthree.BackColor;
                    break;
                case 4:
                    hl.BackColor = Labelfour.BackColor;
                    break;
            }
            if (Qwork > 4)
            {
                hl.BackColor = Labelmore.BackColor;
            }
            string sleader = ((Label)e.Item.FindControl("LabelSleader")).Text.ToLower();
            string sgroup = ((Label)e.Item.FindControl("LabelSgroup")).Text;
            string sgtitle = ((Label)e.Item.FindControl("LabelSgtitle")).Text;
            string vpath = "~/images/gcard.gif";
            string Qid = this.DLonline.DataKeys[e.Item.ItemIndex].ToString();
            string curCid = DDLCid.SelectedValue;
            if (sgroup == "" || sgroup == "0")
            {
                lk.ToolTip = "未分组";
                vpath = "~/images/ncard.gif";//如果未分组,换图标
            }
            else
            {
                int sgp = Int32.Parse(sgroup);
                ((Label)e.Item.FindControl("Labelcolor")).ForeColor = LearnSite.Common.ColorDeel.GroupColor(sgp);//设置组颜色标志，以便签到页面查找

                if (sleader == "true" && sgp > 0)
                {
                    vpath = "~/images/gflag.gif";//如果是组长的话,换图标
                    lk.ToolTip = sgroup + "小组组长";
                    if (sgtitle != "")
                        lk.ToolTip = sgtitle + "组长";
                    if (curCid != "")
                    {
                        string jslk = "attitudegroup('" + sgroup + "', '" + hl.Text + "', '" + Qid + "', '" + curCid + "');";
                        lk.Attributes.Add("onclick", jslk);
                    }
                }
                else
                {
                    lk.ToolTip = sgroup + "小组成员";
                    if (sgtitle != "")
                        lk.ToolTip = sgtitle + "小组成员";
                }
            }
            lk.ImageUrl = vpath + "?temp=" + DateTime.Now.Millisecond.ToString();
            int Qattitude = Int32.Parse(((Label)e.Item.FindControl("Labelattitude")).Text);
            if (curCid != "")
            {
                string jsstr = "attitude('" + Qid + "', '" + Server.UrlEncode(hl.Text) + "', '" + Qattitude + "', '" + curCid + "');";
                hl.Attributes.Add("onclick", jsstr);
            }
            string Qnote = ((Label)e.Item.FindControl("Labelnote")).Text;
            Label lbqnum = (Label)e.Item.FindControl("Labelqnum");
            string myqnum = lbqnum.Text;
            string pp = LearnSite.Common.Photo.ExistStuPhoto(myqnum);
            if (pp != "none")
            {
                lbqnum.Attributes.Add("class", "tooltip");
                lbqnum.ToolTip = pp;
                lbqnum.BackColor = Color.Bisque;
            }
            if (Qattitude != 0)
            {
                hl.ToolTip = "学习表现：" + Qnote + "\n评价得分：  " + Qattitude;
            }
            else
            {
                hl.ToolTip = "点击评价学习表现";
            }
            if (!LearnSite.Common.App.IsLogin(myqnum))
            {
                lbunlock.Visible = false;
            }
            else
            {
                lbunlock.Attributes.Add("onclick", "return   confirm('您确定要将该生踢下线吗？');");
            }
        }
        else
        {
            lbunlock.Visible = false;
            lk.ImageUrl = "~/images/nocomputer.gif" + "?temp=" + DateTime.Now.Millisecond.ToString();
            ((Label)e.Item.FindControl("Labelcolor")).Visible = false;
            ((Label)e.Item.FindControl("HyperSname")).Visible = false;
            ((Label)e.Item.FindControl("LabelQmachine")).Visible = false;
            ((Label)e.Item.FindControl("Labelqnum")).Visible = false;

        }
    }

    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            if (DDLgrade.SelectedValue != null)
            {
                int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
                LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
                DDLclass.DataSource = rm.GetLimitClass(Rgrade);
                DDLclass.DataBind();
                Showkc();
                ShowSigin();
                ShowNoSigin();
                HowManyWork();
                string Hid = tcook.Hid.ToString();
                Session[Hid + "grade"] = DDLgrade.SelectedValue;
                Session[Hid + "class"] = DDLclass.SelectedValue;
                BtnaAllQuit.Enabled = true;
                showLock();
                showCid();
                showwtUrl();
                showMenu();
            }
        }
    }
    protected void Btnstudent_Click(object sender, EventArgs e)
    {
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        LearnSite.Common.CookieHelp.SimulationStudentCookies(Rgrade, Rclass);//模拟该班级学生登录cookies设置
        System.Threading.Thread.Sleep(200);
        Response.Redirect("~/index.aspx", true);
    }
    protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLclass.SelectedValue != null)
        {
            string Hid = tcook.Hid.ToString();
            Session[Hid + "class"] = DDLclass.SelectedValue;
            Showkc();
            ShowSigin();
            ShowNoSigin();
            HowManyWork();
            BtnaAllQuit.Enabled = true;
            showLock();
            showwtUrl();
            showMenu();
        }
    }
    protected void DLdonekc_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        int Wcid = Int32.Parse(DLdonekc.DataKeys[e.Item.ItemIndex].ToString());
        LearnSite.BLL.Works bll = new LearnSite.BLL.Works();
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        ((Label)e.Item.FindControl("wk")).Text = bll.HowCourseWorks(Wcid, Rgrade, Rclass);
    }
    protected void DLnotline_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label hl = new Label();
        hl = (Label)e.Item.FindControl("lbQname");
        Label lb = new Label();
        lb = (Label)e.Item.FindControl("LabelNnum");
        string jsstr = "notsg('" + lb.Text + "', '" + DDLgrade.SelectedValue + "', '" + Server.UrlEncode(hl.Text) + "');";
        hl.Attributes.Add("onclick", jsstr);

        string pp = LearnSite.Common.Photo.ExistStuPhoto(lb.Text);
        if (pp != "none")
        {
            lb.Attributes.Add("class", "tooltip");
            lb.ToolTip = pp;
            lb.BackColor = Color.Bisque;
        }

        LearnSite.BLL.NotSign bll = new LearnSite.BLL.NotSign();
        string getnote = bll.GetNoteToday(lb.Text);
        if (getnote.Trim() != "")
            hl.ToolTip = getnote;
        else
            hl.ToolTip = "点击请给未签到备注";
    }
    protected void DLnewkc_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        CheckBox cb = new CheckBox();
        cb = (CheckBox)e.Item.FindControl("Ck");
        ImageButton imgbtn = new ImageButton();
        imgbtn = (ImageButton)e.Item.FindControl("PubSet");
        if (cb.Checked)
        {
            cb.ToolTip = "已发布";
            imgbtn.ToolTip = "点击隐藏";
        }
        else
        {
            cb.ToolTip = "已隐藏";
            imgbtn.ToolTip = "点击发布";
        }
    }
    protected void DLonline_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string myqnum = ((Label)e.Item.FindControl("Labelqnum")).Text;
        if (e.CommandName == "UnLock")
        {
            LearnSite.Common.App.AppUserRemove(myqnum);//在线列表中踢除
            LearnSite.Common.App.AppKickUserAdd(myqnum);//踢除列表中增加
            ((LinkButton)e.Item.FindControl("Lunlock")).Enabled = false;
            System.Threading.Thread.Sleep(500);
        }
        ShowSigin();
    }
    protected void BtnaAllQuit_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            int sgrade = 0;
            int sclass = 0;
            if (DDLclass.SelectedValue != "")
                sclass = Int32.Parse(DDLclass.SelectedValue);
            if (DDLgrade.SelectedValue != "")
                sgrade = Int32.Parse(DDLgrade.SelectedValue);
            LearnSite.Common.App.GradeClassRemove(sgrade, sclass);
            BtnaAllQuit.Enabled = false;
            ShowSigin();
        }
    }
    protected void RBsort_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSigin();
    }
    protected void CheckBoxip_CheckedChanged(object sender, EventArgs e)
    {
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        bool rlock = CheckBoxip.Checked;
        rm.UpdateLock(Rgrade, Rclass, rlock);
    }
    protected void CheckBoxFace_CheckedChanged(object sender, EventArgs e)
    {
        ShowSigin();
        ShowNoSigin();
    }
    protected void DLnewkc_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int Cid = Int32.Parse(DLnewkc.DataKeys[e.Item.ItemIndex].ToString());
        if (e.CommandName == "P" && Btnset.Enabled)
        {
            LearnSite.BLL.Courses cbll = new LearnSite.BLL.Courses();
            cbll.UpdateCpublish(Cid);
            System.Threading.Thread.Sleep(200);
            Showkc();
            showCid(); 
            showMenu();
        }
    }
    protected void CheckBoxOpen_CheckedChanged(object sender, EventArgs e)
    {
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        bool ropen = CheckBoxOpen.Checked;
        rm.UpdateRopen(Rgrade, Rclass, ropen);
    }
    protected void Btnrefresh_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            if (!Btnset.Enabled)
            {
                string Hid = tcook.Hid.ToString();
                Btnset.Enabled = false;
                DDLgrade.Enabled = false;
                DDLclass.Enabled = false;
                Btnstudent.Enabled = true;
                ShowSigin();
                ShowNoSigin();
                HowManyWork();
                //if (Session[Hid + "StartTime"] != null)
                //{         注释掉是因为某些服务器时间转换出错
                //    DateTime oldtime = DateTime.Parse(Session[Hid + "StartTime"].ToString()); 
                //    DateTime nowtime = DateTime.Now;
                //    Labelfresh.Text = "上课时间已经过去：" + LearnSite.Common.Computer.DatagoneMinute(oldtime, nowtime) + "分钟";
                //}
            }
        }
    }
    protected void CheckBoxRgauge_CheckedChanged(object sender, EventArgs e)
    {
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        bool isgauge = CheckBoxRgauge.Checked;
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        rbll.UpdateMyRgauge(Rgrade, Rclass, isgauge);//选择班级上课时，默认将互评自动设置为关，方便适当使用
    }
    protected void CheckBoxPwd_CheckedChanged(object sender, EventArgs e)
    {
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        bool ispwdsee = CheckBoxPwd.Checked;
        rbll.UpdateRpwdsee(Rgrade, Rclass, ispwdsee);
    }
    protected void DDLCid_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Rgrade = DDLgrade.SelectedValue;
        string Rclass = DDLclass.SelectedValue;
        string Rcid = DDLCid.SelectedValue;
        if (Rcid != "")
        {
            HLworkshow.NavigateUrl = "~/teacher/workshow.aspx?wgrade=" + Rgrade + "&wclass=" + Rclass + "&wcid=" + Rcid;
            HLtotal.NavigateUrl = "~/teacher/coursetotal.aspx?wgrade=" + Rgrade + "&wclass=" + Rclass + "&wcid=" + Rcid;
            HLrate.NavigateUrl = "~/teacher/learnrate.aspx?wgrade=" + Rgrade + "&wclass=" + Rclass + "&wcid=" + Rcid;
            ShowSigin();
            showMenu();
        }
    }
    protected void DDLhouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSigin();
        string pcroom = DDLhouse.SelectedValue;
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null && pcroom != "")
        {
            string Hid =  tcook.Hid.ToString();
            LearnSite.BLL.Teacher tbll = new LearnSite.BLL.Teacher();
            tbll.updateHroom(Int32.Parse(Hid), pcroom);//记录上课的电脑室名称
        }
    }
    protected void CheckBoxShare_CheckedChanged(object sender, EventArgs e)
    {
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        bool isshare = CheckBoxShare.Checked;
        rbll.UpdateRshare(Rgrade, Rclass, isshare);
        if (!isshare)
        {
            if (CheckBoxGroupShare.Checked)
            {
                CheckBoxGroupShare.Checked = false;
                rbll.UpdateRgroupshare(Rgrade, Rclass, isshare);//网盘总开关，同步一下小组网盘
            }
        }
    }
    protected void CheckBoxGroupShare_CheckedChanged(object sender, EventArgs e)
    {
        bool isshare = CheckBoxShare.Checked;
        if (isshare)
        {
            bool isgroupshare = CheckBoxGroupShare.Checked;
            LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
            int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
            int Rclass = Int32.Parse(DDLclass.SelectedValue);
            rbll.UpdateRgroupshare(Rgrade, Rclass, isgroupshare);
        }
        else
        {
            CheckBoxGroupShare.Checked = false;
            LearnSite.Common.WordProcess.Alert("请先选中前面的网盘开关，启用网盘系统！", this.Page);
        }
    }
    protected void CheckBoxScratch_CheckedChanged(object sender, EventArgs e)
    {
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        bool isScratch = CheckBoxScratch.Checked;
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        rbll.UpdateMyRscratch(Rgrade, Rclass, isScratch);//可以在上课页面控制编程开始或暂停，不用进入模拟学生
    }
    protected void CheckBoxLogin_CheckedChanged(object sender, EventArgs e)
    {
        LearnSite.BLL.Room rbll = new LearnSite.BLL.Room();
        bool isLogin = CheckBoxLogin.Checked;
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        rbll.UpdateMyRlogin(Rgrade, Rclass, isLogin);//可以在上课页面控制编程开始或暂停，不用进入模拟学生

    }

    private void showMenu()
    {
        string cid = DDLCid.SelectedValue;
        if (!String.IsNullOrEmpty(cid))
        {
            LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
            System.Data.DataTable dt = lbll.GetMenu(Int32.Parse(cid)).Tables[0];
            dt.Columns.Add("Limgurl");
            int dcount = dt.Rows.Count;
            if (dcount > 0)
            {
                for (int i = 0; i < dcount; i++)
                {
                    string Ltype = dt.Rows[i]["Ltype"].ToString();
                    string imgurl = "";
                    switch (Ltype)
                    {
                        case "1"://活动
                            imgurl = "~/images/mission.png";
                            break;
                        case "2"://调查
                            imgurl = "~/images/survey.png";
                            break;
                        case "3"://讨论
                            imgurl = "~/images/topic.png";
                            break;
                        case "4"://表单
                            imgurl = "~/images/inquiry.png";
                            break;
                        case "5"://编程
                            imgurl = "~/images/program.png";
                            break;
                        case "6"://描述
                            imgurl = "~/images/description.png";
                            break;
                        case "8"://编程
                            imgurl = "~/images/python.png";
                            break;
                        case "9"://交互式python测评
                            imgurl = "~/images/console.png";
                            break;
                        case "10"://流程图
                            imgurl = "~/images/mxgraph.png";
                            break;
                        case "11"://像素画
                            imgurl = "~/images/pixel.png";
                            break;
                        case "12"://单网页
                            imgurl = "~/images/html.png";
                            break;
                        case "13"://拼图
                            imgurl = "~/images/pythonblock.png";
                            break;
                        case "14"://积木
                            imgurl = "~/images/blockpy.png";
                            break;
                        case "15"://导图
                            imgurl = "~/images/kitymind.png";
                            break;
                        case "16"://表格处理
                            imgurl = "~/images/sheet.png";
                            break;
                        case "17"://二维码
                            imgurl = "~/images/qrcode.png";
                            break;
                        case "18"://在线文档
                            imgurl = "~/images/word.png";
                            break;
                        case "19"://在线演示文稿
                            imgurl = "~/images/pptist.png";
                            break;
                        case "20"://在线海报设计
                            imgurl = "~/images/poster.png";
                            break;
                        case "21"://风格迁移
                            imgurl = "~/images/style.png";
                            break;
                        case "22"://图像分类
                            imgurl = "~/images/mlimg.png";
                            break;
                        case "23"://人脸识别
                            imgurl = "~/images/face.png";
                            break;
                        case "24"://物联网
                            imgurl = "~/images/mqtt.png";
                            break;
                        case "25"://手绘画布
                            imgurl = "~/images/excalidraw.png";
                            break;
                        case "26"://推箱子地图
                            imgurl = "~/images/sokoban.png";
                            break;
                        case "27"://人工智能对话
                            imgurl = "~/images/ai.png";
                            break;
                        case "28"://人工智能对话
                            imgurl = "~/images/speek.png";
                            break;
                        case "29"://文字识别
                            imgurl = "~/images/ocr.png";
                            break;
                        case "30"://声音分析
                            imgurl = "~/images/sound.png";
                            break;
                        case "31"://井字棋
                            imgurl = "~/images/tic-tac-toe.png";
                            break;
                        case "32"://手写数字识别
                            imgurl = "~/images/handnum.png";
                            break;
                        case "33"://Markdown写作
                            imgurl = "~/images/markdown.png";
                            break;
                        case "34"://iframe嵌入网页
                            imgurl = "~/images/iframe.png";
                            break;
                        case "35"://文生图
                            imgurl = "~/images/text-to-image.png";
                            break;
                        case "36"://素材库
                            imgurl = "~/images/web.png";
                            break;
                        case "37"://网站设计
                            imgurl = "~/images/website.png";
                            break;

                        default://默认
                            imgurl = "~/images/lesson.png";
                            break;
                    }
                    dt.Rows[i]["Limgurl"] = imgurl;
                }
            }
            DataListMenu.DataSource = dt;
            DataListMenu.DataBind();
            DataListMenu.Visible = true;
        }
        else
        {
            DataListMenu.Visible = false;
        }
    }

    protected void DataListMenu_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        CheckBox cb = new CheckBox();
        cb = (CheckBox)e.Item.FindControl("CheckBoxShow");
        ImageButton imgbtn = new ImageButton();
        imgbtn = (ImageButton)e.Item.FindControl("imgBtn");
        Label lt = new Label();
        lt = (Label)e.Item.FindControl("lableTitle");
        if (cb.Checked)
        {
            imgbtn.ToolTip = "点击隐藏";
            lt.ToolTip = "已发布";
            lt.ForeColor = System.Drawing.Color.Black;
        }
        else
        {
            imgbtn.ToolTip = "点击发布";
            lt.ToolTip = "已隐藏";
            lt.ForeColor = System.Drawing.Color.Gainsboro;
        }
    }
    protected void DataListMenu_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int Lid = Int32.Parse(DataListMenu.DataKeys[e.Item.ItemIndex].ToString());
        if (e.CommandName == "P" )
        {
            LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
            lbll.UpdateLshow(Lid);
            System.Threading.Thread.Sleep(200);
            showMenu();
        }
    }
    protected void CheckBoxPass_CheckedChanged(object sender, EventArgs e)
    {
        int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
        int Rclass = Int32.Parse(DDLclass.SelectedValue);
        LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
        bool rpass = CheckBoxPass.Checked;
        rm.UpdateRpass(Rgrade, Rclass, rpass);
    }
}
