using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Teacher_courseshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学案显示页面";
            showcourse();
            showmenu();
            if (Request.QueryString["cold"] != null)
            {
                BtnEdit.Enabled = false;
                LinkBtnAdd.Enabled = false;
                LinkBtnAddSurvey.Enabled = false;
                LinkBtnAddTopic.Enabled = false;
                LinkBtnAddTxtForm.Enabled = false;
            }
        }
    }

    private void showcourse()
    {
        if (Request.QueryString["cid"] != null)
        {
            string Cid = Request.QueryString["cid"].ToString();
            LearnSite.Model.Courses model = new LearnSite.Model.Courses();
            LearnSite.BLL.Courses cs = new LearnSite.BLL.Courses();
            model = cs.GetModel(Int32.Parse(Cid));
            if (model != null)
            {
                LabelCtitle.Text = model.Ctitle;
                LabelCdate.Text = model.Cdate.ToString();
                LabelCclass.Text = model.Cclass;
                LabelCobj.Text = model.Cobj.ToString();
                LabelCterm.Text = model.Cterm.ToString();
                LabelCks.Text = model.Cks.ToString();
                Ccontent.InnerHtml = HttpUtility.HtmlDecode(model.Ccontent);
                if (model.Cbanner != "")
                {
                    Imagebanner.ImageUrl = model.Cbanner;
                }
                else
                    Imagebanner.Visible = false;
            }
        }
    }
    private void showmenu()
    {
        if (Request.QueryString["cid"] != null)
        {
            string Cid = Request.QueryString["cid"].ToString();
            LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
            GVlistmenu.DataSource = lbll.GetMenu(Int32.Parse(Cid));
            GVlistmenu.DataBind();
        }
    }
    protected void LinkBtnAdd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Mcid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/missionadd.aspx?mcid=" + Mcid;
            Response.Redirect(url, false);
        }
    }
    protected void LinkBtnReturn_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cold"] != null)
        {
            Response.Redirect("~/teacher/courseold.aspx", false);
        }
        else
        {
            Response.Redirect("~/teacher/course.aspx", false);
        }
    }
    protected void LinkBtnAddTopic_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Mcid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/topicadd.aspx?mcid=" + Mcid;
            Response.Redirect(url, true);
        }
    }
    protected void LinkBtnAddSurvey_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Vcid = Request.QueryString["cid"].ToString();
            string url = "~/survey/surveyadd.aspx?cid=" + Vcid;
            Response.Redirect(url, true);
        }
    }
    protected void LinkBtnAddTxtForm_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Vcid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/txtformadd.aspx?mcid=" + Vcid;
            Response.Redirect(url, true);
        }
    }
    protected void LinkBtnProgram_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Vcid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/programadd.aspx?mcid=" + Vcid;
            Response.Redirect(url, true);
        }
    }

    protected void BtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Cid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/courseedit.aspx?cid=" + Cid;
            Response.Redirect(url, true);
        }
    }
    protected void GVlistmenu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int RowIndex = Convert.ToInt32(e.CommandArgument);
        int Lid = Convert.ToInt32(((Label)GVlistmenu.Rows[RowIndex].FindControl("LabelLid")).Text);
        int lxid = Convert.ToInt32(((Label)GVlistmenu.Rows[RowIndex].FindControl("LabelLxid")).Text);
        string ltype = ((Label)GVlistmenu.Rows[RowIndex].FindControl("LabelLtype")).Text;
        //int lsort = Convert.ToInt32(((Label)GVlistmenu.Rows[RowIndex].FindControl("LabelLsort")).Text);        
        int Lcid = Int32.Parse(Request.QueryString["cid"].ToString());
        LearnSite.BLL.ListMenu lbll = new LearnSite.BLL.ListMenu();
        if (e.CommandName == "P")
        {
            lbll.UpdateLshow(Lid);
        }
        if (e.CommandName == "D")
        {
            switch (ltype)
            {
                case "1"://活动
                case "5"://编程
                case "6"://描述
                case "8"://编程
                case "10"://流程图
                case "11"://像素画
                case "12"://网页
                case "13"://编程
                case "14"://编程
                case "15"://导图
                case "16"://在线表格
                case "17"://二维码
                case "18"://在线文档
                case "19"://演示文稿
                case "20"://海报设计
                case "21"://风格迁移
                case "22"://图像分类
                case "23"://人脸识别
                case "24"://物联网MQTT
                case "25"://手绘画布
                case "26"://推箱子地图
                case "27"://人工智能对话
                case "28"://语音合成
                case "29"://文字识别
                case "30"://声音分析
                case "31"://井字棋
                case "32"://手写数字识别
                case "33"://Markdown写作
                case "34"://iframe嵌入网页
                case "35"://文生图
                    LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
                    mbll.DeleteMission(lxid);//假删除任务
                    lbll.Delete(Lid);//删除导航
                    break;
                case "2"://调查
                    LearnSite.BLL.Survey vbll = new LearnSite.BLL.Survey();
                    LearnSite.BLL.SurveyQuestion qbll = new LearnSite.BLL.SurveyQuestion();
                    if (!qbll.ExistsByQvid(lxid))
                    {
                        vbll.Delete(lxid);//删除调查
                        lbll.Delete(Lid);//删除导航
                    }
                    else
                    {
                        string msg = "该调查卷存在试题，请先删除试题！";
                        LearnSite.Common.WordProcess.Alert(msg, this.Page);
                    }
                    break;
                case "3"://讨论
                    LearnSite.BLL.TopicDiscuss tbll = new LearnSite.BLL.TopicDiscuss();
                    tbll.Delete(lxid);//删除讨论
                    lbll.Delete(Lid);//删除导航
                    break;

                case "4"://表单
                    LearnSite.BLL.TxtForm tfmbll = new LearnSite.BLL.TxtForm();
                    tfmbll.Delete(lxid);//删除表单
                    lbll.Delete(Lid);//删除导航
                    break;
                case "9"://测评
                    LearnSite.BLL.Consoles conbll = new LearnSite.BLL.Consoles();
                    conbll.Delete(lxid);//删除测评
                    lbll.Delete(Lid);//删除导航
                    break;
            }
        }

        if (e.CommandName == "Top")
        {
            if (RowIndex == 0)
            {
                lbll.Lsortnew(Lcid);//如果首行，初始化序号
            }
            if (RowIndex > 0)
            {
                int toplid = Convert.ToInt32(((Label)GVlistmenu.Rows[RowIndex - 1].FindControl("LabelLid")).Text);//获取上个导航编号
                lbll.UpdateLsort(Lid, false);//当前导航减１向上
                lbll.UpdateLsort(toplid, true);//上个导航增１向下
            }
            System.Threading.Thread.Sleep(500);
            lbll.Lsortsncy(Lcid);//活动序号同步
        }
        if (e.CommandName == "Bottom")
        {
            int rowscount = GVlistmenu.Rows.Count;
            if (RowIndex < rowscount - 1)
            {
                int bottomlid = Convert.ToInt32(((Label)GVlistmenu.Rows[RowIndex + 1].FindControl("LabelLid")).Text);//获取下个导航编号
                lbll.UpdateLsort(bottomlid, false);//下个导航减１向上
                lbll.UpdateLsort(Lid, true);//当前导航增１向下
                System.Threading.Thread.Sleep(500);
                lbll.Lsortsncy(Lcid);//lbll.UpdateMissonListMene(Lcid, lxid);//活动序号同步
            }
        }

        System.Threading.Thread.Sleep(200);
        showmenu();
    }
    protected void GVlistmenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            HyperLink hl = (HyperLink)e.Row.FindControl("HlLtitle");
            string lxid = ((Label)e.Row.FindControl("LabelLxid")).Text;
            string ltype = ((Label)e.Row.FindControl("LabelLtype")).Text;
            string lid = ((Label)e.Row.FindControl("LabelLid")).Text;
            string Cid = Request.QueryString["cid"].ToString();
            string Cold = "";
            if (Request.QueryString["cold"] != null)
            {
                Cold = "&cold=T";
            }
            switch (ltype)
            {
                case "1":
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/mission.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "练习";
                    hl.NavigateUrl = "missionshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "6"://描述
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/description.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "阅读";
                    hl.NavigateUrl = "missionshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "2":
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/survey.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "调查";
                    hl.NavigateUrl = "~/survey/survey.aspx?cid=" + Cid + "&Vid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "3":
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/topic.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "讨论";
                    hl.NavigateUrl = "topicshow.aspx?tcid=" + Cid + "&tid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "4":
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/inquiry.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "填表";
                    hl.NavigateUrl = "txtformshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "5"://编程 
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/program.png";                   
                    ((Label)e.Row.FindControl("Label4")).Text = "积木";
                    hl.NavigateUrl = "programshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "8"://编程  
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/python.png";                  
                    ((Label)e.Row.FindControl("Label4")).Text = "代码";
                    hl.NavigateUrl = "pythonshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "9"://测评 
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/console.png";                   
                    ((Label)e.Row.FindControl("Label4")).Text = "测评";
                    hl.NavigateUrl = "consoleshow.aspx?ncid=" + Cid + "&nid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "10"://流程图  
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/mxgraph.png";                  
                    ((Label)e.Row.FindControl("Label4")).Text = "流程";
                    hl.NavigateUrl = "graphshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "11"://像素画  
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/pixel.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "像素";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "12"://网页
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/html.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "网页";
                    hl.NavigateUrl = "htmlshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "13"://编程  
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/pythonblock.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "拼图";
                    hl.NavigateUrl = "pythonshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "14"://python积木编程  
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/blockpy.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "积木";
                    hl.NavigateUrl = "pythonshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "15"://思维导图  
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/kitymind.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "脑图";
                    hl.NavigateUrl = "kitymindshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "16"://表格处理 
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/sheet.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "表格";
                    hl.NavigateUrl = "excelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "17"://二维码 
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/qrcode.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "二维码";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "18"://在线文档 
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/word.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "在线文档";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "19"://在线演示文稿
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/pptist.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "演示文稿";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "20"://在线海报设计
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/poster.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "海报设计";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "21"://风格迁移 图像分类
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/style.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "风格迁移";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "22"://图像分类
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/mlimg.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "图像分类";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "23"://人脸识别
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/face.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "人脸识别";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "24"://物联网mqtt
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/mqtt.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "物联网";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "25"://手绘画布
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/excalidraw.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "手绘画布";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "26"://推箱子地图
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/sokoban.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "推箱子地图";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "27"://人工智能对话
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/ai.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "人工智能对话";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "28"://语音合成
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/speek.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "语音合成";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "29"://文字识别
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/ocr.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "文字识别";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "30"://声音分析
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/sound.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "声音分析";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "31"://井字棋
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/tic-tac-toe.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "井字棋";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "32"://手写数字识别
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/handnum.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "手写数字识别";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "33"://markdown写作
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/markdown.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "markdown写作";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "34"://iframe嵌入网页
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/iframe.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "嵌入网页";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
                case "35"://文生图
                    ((Image)e.Row.FindControl("Image4")).ImageUrl = "~/images/text-to-image.png";
                    ((Label)e.Row.FindControl("Label4")).Text = "文生图";
                    hl.NavigateUrl = "pixelshow.aspx?mcid=" + Cid + "&mid=" + lxid + "&lid=" + lid + Cold;
                    break;
            }

            string strjs = "if(confirm('您确定要删除吗?'))return true;else return false; ";
            ((LinkButton)e.Row.FindControl("LinkBtnDel")).OnClientClick = strjs;
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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        showmenu();
    }
    protected void LinkBtnPython_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Vcid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/pythonadd.aspx?mcid=" + Vcid;
            Response.Redirect(url, true);
        }

    }
    protected void LinkBtnConsole_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string cid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/consoleadd.aspx?cid=" + cid;
            Response.Redirect(url, true);
        }

    }
    protected void LinkBtnGraph_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Vcid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/graphadd.aspx?mcid=" + Vcid;
            Response.Redirect(url, true);
        }

    }
    protected void LinkButtonPixel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Vcid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/pixeladd.aspx?mcid=" + Vcid;
            Response.Redirect(url, true);
        }

    }
    protected void LinkButtonHtml_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Vcid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/htmladd.aspx?mcid=" + Vcid;
            Response.Redirect(url, true);
        }

    }
    protected void LinkButtonKm_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Vcid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/kitymindadd.aspx?mcid=" + Vcid;
            Response.Redirect(url, true);
        }

    }
    protected void LinkButtonExcel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] != null)
        {
            string Vcid = Request.QueryString["cid"].ToString();
            string url = "~/teacher/exceladd.aspx?mcid=" + Vcid;
            Response.Redirect(url, true);
        }

    }
}
