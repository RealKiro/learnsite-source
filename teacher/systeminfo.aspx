<%@ Page Title="" Language="C#" MasterPageFile="~/teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" enableViewStateMac="false" CodeFile="systeminfo.aspx.cs" Inherits="Teacher_systeminfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold">
    <center>
        <br />
        <table style="border: 1px solid #D8D8D8; width: 300px; " 
            cellpadding="5" cellspacing="0">
            <tr>
                <td style="background-color: #E6E6E6; font-weight: bold;" colspan="2" 
                    align="center">
                    网站分析统计</td>
            </tr>
            <tr>
                <td style="width: 120px">
                    学案总数：</td>
                <td>
                    <asp:Label ID="Label15" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    作品总数：</td>
                <td>
                    <asp:Label ID="Label16" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    学生总数：</td>
                <td>
                    <asp:Label ID="Label17" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    签到次数：</td>
                <td>
                    <asp:Label ID="Label18" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    打字次数：</td>
                <td>
                    <asp:Label ID="Label19" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    资源总数：</td>
                <td>
                    <asp:Label ID="Label20" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
       &nbsp;<asp:HyperLink ID="HLcomputer" runat="server" 
            NavigateUrl="~/teacher/computers.aspx" CssClass="HyperlinkLong" 
            EnableTheming="False" EnableViewState="False">机器名IP对应表</asp:HyperLink>
        &nbsp;&nbsp;
        <asp:HyperLink ID="HLmythware" runat="server" 
            NavigateUrl="~/teacher/mythware.aspx" CssClass="HyperlinkLong" 
            EnableTheming="False" EnableViewState="False">极域班级模型</asp:HyperLink>
        <br />
        <br />
<table style="border: 1px solid #D8D8D8;  width: 800px;  text-align: left;  " 
            cellpadding="4" cellspacing="0">
            <tr>
                <td style="font-weight: bold; color: black; height: 16px; background-color: #E6E6E6; text-align: center;" 
                    colspan="4">
                    <asp:Label ID="Labelcomputer" runat="server" ></asp:Label>
                    服务器状态</td>
            </tr>
            <tr>
                <td style="width: 115px" >
                    服务器IP：</td>
                <td style="width: 262px" >
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:Image ID="ImageLogin" runat="server" ImageUrl="~/images/green.gif" />
                </td>
                <td style="width: 158px" >
                    .NET引擎版本：</td>
                <td >
                    <asp:Label ID="Label8" runat="server" ></asp:Label>
                                </td>
            </tr>
            <tr>
                <td style="width: 115px" >
                    服务器名称：</td>
                <td style="width: 262px" >
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
                <td style="width: 158px" >
                    脚本超时时间：</td>
                <td >
                    <asp:Label ID="Label9" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 115px" >
                    操作系统：</td>
                <td style="width: 262px" >
                    <asp:Label ID="Label3" runat="server" ></asp:Label>
                </td>
                <td style="width: 158px" >
                    开机运行时长：</td>
                <td >
                    <asp:Label ID="Label10" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 115px" >
                    CPU数：</td>
                <td style="width: 262px" >
                    <asp:Label ID="Label4" runat="server" ></asp:Label>
                </td>
                <td style="width: 158px" >
                    进程开始时间：</td>
                <td >
                    <asp:Label ID="Label11" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 115px" >
                    CPU类型</td>
                <td style="width: 262px" >
                    <asp:Label ID="Label5" runat="server" ></asp:Label>
                </td>
                <td style="width: 158px" >
                    AspNet内存占用：</td>
                <td >
                    <asp:Label ID="Label12" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 115px" >
                    信息服务软件：</td>
                <td style="width: 262px" >
                    <asp:Label ID="Label7" runat="server" ></asp:Label>
                </td>
                <td style="width: 158px" >
                    AspNet CPU时间：</td>
                <td >
                    <asp:Label ID="Label13" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 115px" >
                    服务器区域语言</td>
                <td style="width: 262px" >
                    <asp:Label ID="Label21" runat="server" ></asp:Label>
                </td>
                <td style="width: 158px" >
                    AspNet当前线程数：</td>
                <td >
                    <asp:Label ID="Label14" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 115px" >
                    网站平台版本：</td>
                <td style="width: 262px" >
                    <asp:Label ID="Label6" runat="server" ></asp:Label>
                </td>
                <td style="width: 158px" >
                    Session总数：</td>
                <td >
                    <asp:Label ID="Label22" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 115px" >
                    全局变量数：</td>
                <td style="width: 262px" >
                    <asp:Label ID="Label23" runat="server" ></asp:Label>
                </td>
                <td style="width: 158px" >
                    网站异常记录：</td>
                <td >
        <asp:HyperLink ID="HLsitelog" runat="server" 
            NavigateUrl="~/teacher/sitelog.aspx" BorderStyle="None" EnableTheming="False" 
                        EnableViewState="False"  Font-Underline="False" 
                        ForeColor="Black" Target="_blank" ToolTip="请及时向温州水乡回复修正！">查询</asp:HyperLink>
                </td>
            </tr>
            </table>
    <br />
    </center>
        </div>
</asp:Content>

