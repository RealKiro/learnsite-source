using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpGrade : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            checkdatabase();
    }
    protected void Btnupgrade_Click(object sender, EventArgs e)
    {
        if (LearnSite.DBUtility.UpdateGrade.TableExistCheck())
        {
            if (!LearnSite.DBUtility.UpdateGrade.VersionCheck())
            {
                Oldupdate();//旧网站更新
                LearnSite.DBUtility.UpdateGrade.UpdateTableEnglish();
                LearnSite.DBUtility.UpdateGrade.UpdateTable1251();//给学生表增加标记字段
                LearnSite.DBUtility.UpdateGrade.UpdateTable1252();//给作品表增加修改天数字段
                LearnSite.DBUtility.UpdateGrade.UpdateTable1253();//给作品表增加修改天数字段

                Labelmsg.Text = "升级完毕，请删除本页面！以免数据库出错！";

                string ch = "数据库已经更新到最新版，点击跳回教师首页！";
                LearnSite.Common.CookieHelp.ClearTeacherCookies();
                LearnSite.Common.CookieHelp.ClearStudentCookies();//教师退出的话把本机模拟学生角色登录的学生平台也退出
                System.Threading.Thread.Sleep(500);
                LearnSite.Common.WordProcess.Alert(ch, this.Page);
                HttpRuntime.Close();//清除缓存，释放资源
                Response.Redirect("~/teacher/index.aspx", false);
            }
            else
            {                
                string ch = "已经更新过了！请点击跳回教师首页！\n若要求运行一次则补丁已经修正！";
                LearnSite.Common.WordProcess.Alert(ch, this.Page);
                Response.Redirect("~/teacher/index.aspx", false);
            }
        }
        else
        {
            string ch = "很抱歉您创建的数据库为空，不存在数据表，请仔细查看或请求帮助！";
            LearnSite.Common.WordProcess.Alert(ch, this.Page);
        }
    }

    private void checkdatabase()
    {
        if (!LearnSite.DBUtility.SqlHelper.DatabaseExist())//如果数据库不存在
        {
            Panel1.Visible = true;
            showPanel();
            Btnupgrade.Enabled = false;
            string msgstr = "";
            if (MasterDbExist(TextBoxSqlServer.Text, TextBoxDbUser.Text, TextBoxDbPwd.Text))
            {
                msgstr = "数据库服务名称、账号、密码正确！<br/><br/>如果数据库已创建，请在下面修改为正确数据库名称！<br/><br/>如果未创建新数据库，请填写你的新数据库名称！";
            }
            else
            {
                msgstr = "数据库服务名称、账号、密码不正确！请改修改正确！";
            }
            Labelmsg.Text = msgstr;
        }
        else
        {
            if (LearnSite.DBUtility.SqlHelper.CountTable() > 0)
            {
                BtnCreateTable.Visible = false;
                Panel1.Visible = false;
                Btnupgrade.Enabled = true;
                Btnupgrade.Visible = true;
                Labelmsg.Text = "数据库连接正常！数据表完整，请点击上面执行更新，进行初始化或版本升级！";
            }
            else {
                BtnCreateTable.Visible = true;
                Btnupgrade.Visible = false;
                Labelmsg.Text = "数据库连接正常！第一次安装请点击创建数据表按钮后，然后执行更新进行初始化！";
            }
        }
    }

    private void showPanel()
    {
        string myconnstr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;
        string[] constr = LearnSite.DBUtility.DbLinkEdit.ReadSqlConfig(myconnstr);
        TextBoxSqlServer.Text = constr[0];
        TextBoxDbName.Text = constr[1];
        TextBoxDbUser.Text = constr[2];
        TextBoxDbPwd.Text = constr[3];
    }
    private bool MasterDbExist(string dbserver, string dbuser, string dbpwd)
    {
        string masterdb = "master";
        string masterConnstring = String.Format("Data Source={0};Initial Catalog={1};uid={2};pwd={3};", dbserver, masterdb, dbuser, dbpwd);
        return LearnSite.DBUtility.DbLinkEdit.DatabaseExist(masterConnstring);
    }
    protected void Buttonedit_Click(object sender, EventArgs e)
    {
        string dbserver = TextBoxSqlServer.Text;
        string dbname = TextBoxDbName.Text;
        string dbuser = TextBoxDbUser.Text;
        string dbpwd = TextBoxDbPwd.Text;
        if (dbserver != "" && dbname != "" && dbuser != "" && dbpwd != "")
        {
            string teststr = dbserver + dbname + dbuser + dbpwd;
            if (teststr.IndexOf(';') < 0 && teststr.IndexOf('=') < 0)
            {
                string namevalue = "SqlServer";
               // string ftpnamevalue = "Ftp";
               // string ftpdbname = dbname + ftpnamevalue;
                if (MasterDbExist(dbserver, dbuser, dbpwd))
                {
                    Buttonedit.Enabled = false;
                    LearnSite.DBUtility.DbLinkEdit.WriteSqlConfig(namevalue, dbserver, dbname, dbuser, dbpwd);
                   // LearnSite.DBUtility.DbLinkEdit.WriteSqlConfig(ftpnamevalue, dbserver, ftpdbname, dbuser, dbpwd);
                    string url = "~/upgrade.aspx";
                    LearnSite.Common.WordProcess.Alert("Webconfig修改成功！", this.Page);
                    Response.Redirect(url, false);
                }
                else
                {
                    Labelmsg.Text = "数据库服务器名称、账号、密码填写可能错误！";
                }
            }
            else
            {
                Labelmsg.Text = "请不要填写非法字符，浪费时间！";
            }
        }
        else
        {
            Labelmsg.Text = "请填写正确的数据库服务器名称等！";
        }
    }

    private void Oldupdate()
    {
        LearnSite.DBUtility.UpdateGrade.updateDatabase();
        LearnSite.DBUtility.UpdateGrade.CreateNewTable();
        LearnSite.DBUtility.UpdateGrade.UpdateTable105();
        LearnSite.DBUtility.UpdateGrade.UpdateTable106();
        LearnSite.DBUtility.UpdateGrade.UpdateTable107();
        LearnSite.DBUtility.UpdateGrade.UpdateHtmlToHtm();
        LearnSite.DBUtility.UpdateGrade.UpdateTable108();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1081();//未更新Works表中的Wgrade,Wterm
        LearnSite.DBUtility.UpdateGrade.UpdateTable1082();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1092();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1093();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1094();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1095();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1096();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1098();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1100();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1101();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1102();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1103();//新增已删除学生表DelStudents
        LearnSite.DBUtility.UpdateGrade.UpdateTable1105();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1106();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1107();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1108();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1109();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1110();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1200();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1201();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1202();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1203();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1205();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1206();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1207();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1208();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1209();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1210();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1211();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1212();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1213();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1214();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1215();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1216();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1217();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1218();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1220();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1222();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1226();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1228();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1229();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1230();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1232();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1251();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1252();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1253();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1260();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1280();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1300();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1320();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1330();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1332();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1333();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1335();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1336();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1337();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1338();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1339();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1350();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1352();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1360();
        LearnSite.DBUtility.UpdateGrade.UpdateTable1365();
    }
    protected void BtnCreateTable_Click(object sender, EventArgs e)
    {
        int n = LearnSite.DBUtility.SqlHelper.CreateTable();
        Labelmsg.Text = "创建数据表成功！"+n.ToString();
        LearnSite.Common.WordProcess.Alert("创建数据表成功！可以执行更新，导入打字英文字典", this.Page);
        Response.Redirect("~/upgrade.aspx", false);
    }
}
