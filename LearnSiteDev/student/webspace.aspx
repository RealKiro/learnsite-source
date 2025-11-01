<%@ Page Language="C#" AutoEventWireup="true" CodeFile="webspace.aspx.cs" Inherits="student_webspace" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
    .by {margin: 0px;background-color: #E6F0E7}
    .disk{margin: auto; text-align: center; width: 550px; font-size: 11pt; font-family: 宋体, Arial, Helvetica, sans-serif;}
    .dhead{border-width: 1px; border-color: #CCCCCC; padding: 2px; background-color: #CFE4D0; border-bottom-style: solid;}
    .dcontext{margin: auto; padding: 2px;background-color: #FFFFDD; height: 340px; overflow-x: hidden;}
    .dfile{border-width: 1px; border-color: #E6E8E6; border-top-style: dashed; text-align: left; padding:4px; font-family: Arial; }
    .txt{ line-height:16px; }
    .leftcss{ float:left; left:10px; width:12px;margin:auto;}
    .rightcss{ float:right; right:2px;width:88%;}
	.leftlist{float:left;width:85%;font-size: 12pt;}
	.rightlist{float:right;width:10%;font-size: 9pt; text-align:right;}
	.nomenu{user-select: none;padding-right:10px;}
	.btnup{right:50%; background-color:#E6F0E7;border-width:1px;}	
	.btnup:hover{background-color:#DAF1DC;}
	.dfile img{height:32px;}
	.del{opacity:0.02;}
	.del:hover{opacity:0.9;}
    </style>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/dropzone/dropzone-min.js" type="text/javascript"></script>
    <link href="../code/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body  class="by">
    <form id="form1" class="dropzone" runat="server">
    <div id="doc_area" class="disk" > 
        <div class="dhead">
             <asp:Label ID="Labeltitle" runat="server" Font-Bold="True" Font-Size="11pt"></asp:Label>
        </div>   
        <div id="file_area" class="dcontext"  > 
                <asp:DataList ID="Dlfilelist" runat="server" 
                    RepeatColumns="2" RepeatDirection="Horizontal" CellPadding="3" 
                    CellSpacing="3" Width="99%" 
                    HorizontalAlign="Center" onitemcommand="Dlfilelist_ItemCommand" 
                    onitemdatabound="Dlfilelist_ItemDataBound" >
                    <ItemTemplate>
                        <div class="dfile"> 
                           <div class="leftlist" >                            
                            <asp:HyperLink ID="HLtype" runat="server"  ImageUrl='<%# Eval("Kftpe") %>' CssClass="nomenu"></asp:HyperLink>
                            <asp:HyperLink ID="HLfname" runat="server" NavigateUrl='<%# Eval("Kfurl") %>' Target="_blank" Text='<%# Eval("KfnameShort") %>' Font-Underline="False" ></asp:HyperLink>                             
                           </div>
						   <div class="rightlist">				
                            <asp:Label ID="Labelnum" runat="server" Text='<%# Eval("Kfnum") %>'  Visible="false"></asp:Label> 
                            <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandArgument='<%# Eval("Kfurl") %>' 
                                CommandName="D" ImageUrl="~/images/delete.gif" ToolTip="删除"  CssClass="del" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <SeparatorStyle BorderColor="Silver" BorderStyle="Dotted" BorderWidth="1px" />
                </asp:DataList>        
        </div>
		<div id="up_area" style="padding-top:10px;">
		  <button type="button" id="btnupload" class="btnup"> <i class="fa fa-upload" aria-hidden="true"></i> 文件上传</button>
		<div>
     </div>    
    </div>
    </div>

    </form>
    <script type="text/javascript" >
        var urlstr = "share.ashx?isweb=true";

        $("#btnupload").dropzone({
            url: urlstr,
            method: "POST",
            addRemoveLinks: true,
            maxFiles: 1, //一次性上传的文件数量上限
            maxFilesize: 30, //MB
            uploadMultiple: false,
            parallelUploads: 100,
            acceptedFiles: ".png,.jpg,.jpeg,.gif,.mp4,.mp3",
            previewsContainer: false,
            success: function (file, response, e) {
                location.reload();
            }
        });


    </script>
</body>
</html>
