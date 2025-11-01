<%@ Page Language="C#" AutoEventWireup="true" CodeFile="htmleditor.aspx.cs" Inherits="student_htmleditor" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >  
  <meta charset="UTF-8">
  <style>
		body {
			margin: 0;
			overflow: hidden;
		}
		.html_banner {
			height: 30px;
			line-height:30px;
			vertical-align: middle;
			padding-left: 20px;
			font-weight: bold;
			background-color: #eee;
			color: #666;
			user-select: none;
			box-shadow: 2px 2px 2px Gainsboro;
			padding:4px;
		}
		#main {
			display: flex;
		}
		#left {
			width: calc(55% - 6px);
		}
		#resize {
			width: 4px;
			height: 100vh;
			cursor: ew-resize;
			background-color:WhiteSmoke;
			box-shadow: 2px 2px 2px #999;
		}
		#resize:hover {
			background-color: Gainsboro;			
		}
		#right {
			width: 45%;
			margin:4px;
			background-repeat: no-repeat;
			background-size: cover;
			background-position: center;
		}
		#tooltip{
			position: absolute;
			bottom: 0px;
			height:24px;
			left:58px;
			user-select: none;
			background-color: #eee;
			padding-top:2px;
			padding-left:8px;
			z-index:666;
			font-size:14px;
			#opacity:0.9;
			width: 100%;
		}
		.keyword{
			display:inline-block;		
			margin:auto;
			padding:2px;
			color:#666;	
			cursor: hand;
			min-width:30px;
			text-align:center;
		}
		.keyword:hover{
			cursor: hand;
			color:#fff;
			background-color: #333;
		}
		
		#tool i{
			color:#999;
		}
		#tool i:hover{
			cursor: hand;
			color:#000;
		}
		
		#sidebyleft{
	        position:absolute;
	        left:400px;
	        top:6px;	
            z-index: 999;
            color: #333;
			line-height:30px;
        }
		#sideby{
	        position:absolute;
	        right:50px;
	        top:6px;	
            z-index: 999;
            color: #333;
			line-height:30px;
			background-color: #eee;	
        }
		.sp{
		  width: 30px;
		  display:inline-block;
		}
		.spl{
		  width: 5px;
		  display:inline-block;
		}		
		.buttonshow{
			border: 1px #999 solid;			
		}
		.buttonshow:hover{		
			background-color: #b7c79a;
			color:#000;
		}		
		button{
			border: 1px #999 solid;			
		}
		button:hover{		
			background-color: #FCC796;//#b7c79a;
			color:#000;
		}
		.icon{	
			background-color: #555;
			color:#eee;
			border-radius:6px;
			padding:3px;
		}
		#tool{
			position:absolute;
			float:left;
			left:4px;
			bottom:50px;
			z-index:1000;
		}
		#html_page{	
			border: 0px ;		
			outline: none;
			font-size:18px;
			background-color:transparent;
			padding:4px;
		}
		#html_page:hover{
			border: 1px solid #ddd ;
			background-color:#fff;
		}
		#preview-frame{
			width: 100%;
			height: 100%;
			border: 0px;
		}
		
	</style>
<link href="../code/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
<script src="../code/jquery.min.js" type="text/javascript"></script>
<script src="../code/build/src/ace.js" type="text/javascript"></script>
<script src="../code/build/src/ext-language_tools.js" type="text/javascript"></script>
<link href="../js/tinybox.css" rel="stylesheet" type="text/css" />
<script src="../js/tinybox.js" type="text/javascript"></script>
    <script src="../code/html2canvas.min.js" type="text/javascript"></script>
</head>
<body>
    <div>
    <div class="html_banner">
	    <span class="icon">网页</span>	 
        <span class="spl"></span>
		<input type="text" id="html_page" name="pagename" title="网页文件名称">
    </div>
<div id="tooltip">
	<span class="keyword" title="超文本语言">html</span>
	<span class="keyword" title="页面头部">head</span>
	<span class="keyword" title="网页标题">title</span>
	<span class="keyword" title="样式">style</span>
	<span class="keyword" title="网页主体">body</span>
	<span class="keyword" title="居中对齐">center</span>
	<span class="keyword" title="一级标题">h1</span>
	<span class="keyword" title="超链接">a</span>
	<span class="keyword" title="字体">font</span>	
	<span class="keyword" title="段落">p</span>
	<span class="keyword" title="层">div</span>
	<span class="keyword" title="图片">img</span>
	<span class="keyword" title="视频">video</span>
	<span class="keyword" title="音频">audio</span>
	<span class="keyword" title="表单">form</span>
	<span class="keyword" title="换行">br</span>
	<span class="keyword" title="文本框">input</span>
	<span class="keyword" title="用户名 文本框">username</span>
	<span class="keyword" title="内容 文本框">content</span>
	<span class="keyword" title="提交按钮">submit</span>
</div>	
<div id="tool">
	<i class="fa fa-plus" aria-hidden="true"  onclick="fontbig()" title="放大代码" /></i><br><br>
	<i class="fa fa-minus" aria-hidden="true" onclick="fontsmall()" title="缩小代码" /></i><br><br><br>
	<i class="fa fa-undo" aria-hidden="true"  onclick="backward()" title="撤销" /></i><br><br>
	<i class="fa fa-rotate-right" aria-hidden="true" onclick="forward()" title="重做" /></i>
</div>
	
        <div id="main">
        <div id="left"></div>
        <div id="resize"></div>
        <div id="right">
			<iframe id="preview-frame" ></iframe>
		</div>
        </div>
<div  id="sidebyleft">
</div>

<div  id="sideby">
<button  onclick="example()" class="buttonshow"  title="网页模板">
<i class="fa fa-file-code-o" aria-hidden="true"></i> 网页模板</button>
<span class="sp"></span>
<button  onclick="showMission()" class="buttonshow"  title="查看学案">
<i class="fa fa-book" aria-hidden="true"></i> 学案</button>
<span class="sp"></span>
<button  onclick="showShare()" class="buttonshow"  title="网页空间">
<i class="fa fa-hdd-o" aria-hidden="true"></i> 网页空间</button>
<span class="sp"></span>
<button  type = "button" onclick="savehtml()" class="buttonsave"  title="立即保存到服务器上" >
<i class="fa fa-save" aria-hidden="true"></i> 保存</button>
<span class="sp"></span>
<button  onclick="returnurl()" class="button" title="返回到学案页面">
<i class="fa fa-reply" aria-hidden="true"></i> 返回</button>
</div>

   <div id="mcontext" style="display: none; background: #D1D1D1; overflow-y: auto; overflow-x: hidden;
        position: absolute;   height: 420px; z-index: 999;  bottom: 0px; right:0px;opacity:99%; ">
        <div style="margin:6px; ">
        <%=Mcontents %><br />
        </div>
    </div>

	<script type="text/javascript" >
	    var editor = ace.edit("left", {
	        theme: "ace/theme/chrome",
	        mode: "ace/mode/html"
	    });
        var fontsize=15;
		editor.setShowPrintMargin(false);
	    editor.setFontSize(fontsize);
	    editor.focus();
		//editor.setOption("dragEnabled", false);
		editor.getSession().setUseWorker(false);
	    editor.setOptions({
	        wrap: true,
			enableBasicAutocompletion: true,
			enableSnippets: true,
	        enableLiveAutocompletion: true
	    });

		function backward(){
			editor.undo();
		}
		
		function forward(){
			editor.redo();
		}
		
        function fontbig(){
	        fontsize+=1;
	        editor.setFontSize(fontsize);	
        }

        function fontsmall(){
	        fontsize-=1;
	        if (fontsize<16){
		        fontsize=16;
	        }
	        editor.setFontSize(fontsize);	
        }

	    var previewFrame = document.getElementById("preview-frame");
		const frameDoc = previewFrame.contentDocument || previewFrame.contentWindow.document;
		var htmlpage = document.getElementById("html_page");
	    var snum = "<%=Snum %>";
	    var id = "<%=Id %>";
	    var cf = "<%=codefile %>";
	    var fpage = "<%=Fpage %>";
        var mypage= "<%=Mypage %>";
		htmlpage.value = mypage;
        var myweb="../website/"+snum+"/"+mypage;
	    var codefile = decodeURIComponent(window.atob(cf)); //定义字典

	    var sessionkey = "htmlcode" + snum + "-" + id + "-" + mypage;

		// 更新预览函数
		function updatePreview(htmlCode) {
                frameDoc.open();
                frameDoc.write(htmlCode);//同步预览
                frameDoc.close();
				document.title = frameDoc.title;//同步标题
        }

		
		function autosaving(){
			var codestr = editor.getValue();
	        if (editor.session.getLength()>8) {
	            localStorage .setItem(sessionkey, codestr);//如果行数大于模板数量8行就自动保存
				console.log("自动保存");
	        }
		}
		
	    function autopreview() {
	        var codestr = editor.getValue();
	        if (codestr != null && codestr != "") {
				updatePreview(codestr);
	        }
	    }
	    function getsession() {
	        var codestr = localStorage.getItem(sessionkey);
	        if (codestr != null && codestr != "") {
	            editor.setValue(codestr, 1);
	            //localStorage .clear();//读取后清除，防止污染
	            updatePreview(codestr);
				console.log("读取临时缓存");
				console.log(codestr);
	        }
			else{
				console.log("读取临时缓存无");
			}
	    }
		
		editor.getSession().on('change', function() {
			autopreview();
			autosaving();
            $(".buttonsave").css("background-color","#FF8080" );
		  }
		);

	    if (codefile != '' && codefile != null) {
	        setcode(codefile);
	        updatePreview(codefile);       
	        console.log("读取数据库储存程序");
			//console.log(codefile);			
			$(".buttonsave").css("background-color","" );
	    }
		else{
			getsession();	       
		}
		
	    function setcode() {
	        editor.setValue(codefile, 1);
	    }
        function blob(dataURI) {
            var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0]; 
            var byteString = atob(dataURI.split(',')[1]); 
            var arrayBuffer = new ArrayBuffer(byteString.length); 
            var intArray = new Uint8Array(arrayBuffer); 

            for (var i = 0; i < byteString.length; i++) {
                intArray[i] = byteString.charCodeAt(i);
            }
            return new Blob([intArray], {type: mimeString});
        }

	    function savehtml() { 
        	var htmlcode = editor.getValue();
        	if (htmlcode != null && htmlcode != "") {
                html2canvas(frameDoc.body, {
                    useCORS: true
                }).then(pic => {
                    var dataURL=pic.toDataURL("image/jpg",0.5);
                    var cover=blob(dataURL);
					
        	        localStorage .setItem(sessionkey, htmlcode); //保存时更新临时数据
        	        var urls = 'uploadhtml.ashx?id=' + id;
        	        var formData = new FormData();
        	        var encodehtml = window.btoa(encodeURIComponent(htmlcode));
        	        formData.append('codefile', encodehtml);
        	        formData.append('mypage', mypage);
                    formData.append('cover', cover);

        	        $.ajax({
        	            url: urls,
        	            type: 'POST',
        	            cache: false,
        	            data: formData,
        	            processData: false,
        	            contentType: false
        	        }).done(function (res) {
        	            alert("保存成功！");
						$(".buttonsave").css("background-color","" );
        	            //console.log(res);
        	        }).fail(function (res) {
        	            console.log(res)
        	        }); 	
            
                });		
        	}
        }

        function showhtml(){
            window.open(myweb,"blank");  
           // window.location.href=mypage;
        }

		function downhtml(){
			var filename="mypage";
			var type="text/html";
			var content= editor.getValue();
			var ele = document.createElement('a');// 创建下载链接
			ele.download = filename;//设置下载的名称
			ele.style.display = 'none';// 隐藏的可下载链接
			// 字符内容转变成blob地址
			var blober = new Blob([content],{type});
			ele.href = URL.createObjectURL(blober);
			// 绑定点击时间
			document.body.appendChild(ele);
			ele.click();
			// 然后移除
			document.body.removeChild(ele);
		}

        function returnurl() {
			localStorage .clear(); //返回后清除，防止污染
            if (confirm('是否要离开此页面？') == true) {
                window.location.href = fpage;
            }
        }
        
        function example() {
			if( editor.session.getLength()<8)
			{
				var examplecode = "JTNDaHRtbCUzRSUwRCUwQSUyMCUyMCUyMCUyMCUzQ2hlYWQlM0UlMEQlMEElMjAlMjAlMjAlMjAlMjAlMjAlMjAlMjAlM0N0aXRsZSUzRSUyMCUzQyUyRnRpdGxlJTNFJTBEJTBBJTIwJTIwJTIwJTIwJTNDJTJGaGVhZCUzRSUwRCUwQSUyMCUyMCUyMCUyMCUzQ2JvZHklM0UlMEQlMEElMjAlMjAlMjAlMjAlMjAlMjAlMjAlMjAlMEQlMEElMjAlMjAlMjAlMjAlM0MlMkZib2R5JTNFJTBEJTBBJTNDJTJGaHRtbCUzRQ==";
				var exampledecode = decodeURIComponent(window.atob(examplecode)); //网页模板
				updatePreview(exampledecode);
				editor.setValue(exampledecode, 1);
			}
        }

	</script>

    <script type="text/javascript" >
        window.onload = function () {
            var resize = document.getElementById('resize');
            var left = document.getElementById('left');
            var right = document.getElementById('right');
            var main = document.getElementById('main');
            var minwidth = 400;
            resize.onmousedown = function (e) {
                // 记录鼠标按下时的x轴坐标
                var preX = e.clientX;
                resize.left = resize.offsetLeft;
			
                document.onmousemove = function (e) {
                    var curX = e.clientX;
                    var deltaX = curX - preX;
					console.log("增量", deltaX);
                    var leftWidth = resize.left + deltaX;
                    // 左边区域的最小宽度限制为64px
                    if (leftWidth < minwidth) leftWidth = minwidth;
                    // 右边区域最小宽度限制为64px
                    if (leftWidth > main.clientWidth - minwidth) leftWidth = main.clientWidth - minwidth;
                    // 设置左边区域的宽度
                    left.style.width = leftWidth + 'px';
                    // 设备分栏竖条的left位置
                    resize.style.left = leftWidth;
                    // 设置右边区域的宽度
                    right.style.width = (main.clientWidth - leftWidth - 6) + 'px';
                    //console.log("左侧", leftWidth);
                }
                document.onmouseup = function (e) {
                    document.onmousemove = null;
                }
        };
	};
        $(".keyword").click(function () {
            var keytxts = $(this).html();
            var keystr = keytxts.split('<br>');
            var keytxt = keystr[0];
            console.log(keytxt);
            var cmdstr = "";
            switch (keytxt) {
                case "html":
                    cmdstr = "<html>\r\n\n\n</html>";
                    break;
                case "head":
                    cmdstr = "<head>\r\n\t</head>";
                    break;
                case "title":
                    cmdstr = "<title></title>";
                    break;
                case "style":
                    cmdstr = "<style>\r\n\n\t\t</style>";
                    break;
                case "body":
                    cmdstr = '<body bgcolor=" " background=" ">\r\n\t</body>';
                    break;
                case "center":
                    cmdstr = "<center>\r\n\n\t\t</center>";
                    break;
                case "h1":
                    cmdstr = "<h1> </h1>";
                    break;
                case "a":
                    cmdstr = '<a href=" #链接网址 ">链接文字</a>';
                    break;
                case "font":
                    cmdstr = '<font face="宋体" size=16  color="red">文字</font>';
                    break;
                case "p":
                    cmdstr = "<p>\r\n\n\t\t\t</p>";
                    break;
                case "div":
                    cmdstr = "<div>\r\n\n\t\t\t</div>";
                    break;
                case "img":
                    cmdstr = '<img src=" " ></img>';
                    break;
                case "video":
                    cmdstr = '<video src=" " width="320" height="240" controls></video>';
                    break;
                case "audio":
                    cmdstr = '<audio src=" " controls></audio>';
                    break;
                case "form":
                    cmdstr = '<form action="../../website/post.html" target="_blank">\r\n\n\t\t</form>';
                    break;
                case "br":
                    cmdstr = '<br>';
                    break;
                case "input":
                    cmdstr = '<input type="text" name="username" >';
                    break;
                case "username":
                    cmdstr = '<input type="text" name="username" >';
                    break;
                case "content":
                    cmdstr = '<input type="text" name="content" >';
                    break;
                case "submit":
                    cmdstr = '<input type="submit" value="提交" >';
                    break;
                default:
                    break;
            }

            var cursorPosition = editor.getCursorPosition();
            editor.session.insert(cursorPosition, cmdstr);
            editor.focus();

        });
		
	</script>
    <script type="text/javascript">
        function showShare() {
            var urlat = "webspace.aspx";
            TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 550, height: 400, fixed: false, maskopacity: 100, close: true })
        }
        function showMission() {
            $("#mcontext").slideToggle();
        }
    </script>
	
    </div>
</body>
</html>