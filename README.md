
# 一。LearnSite介绍
## 关于 LearnSite 信息技术学习平台

[LearnSite](https://www.openlearnsite.com) 是一个专为中小学信息技术课堂设计的**开源教学辅助平台**。它通过整合新课教学、作业提交、作品互评、课堂测验及资源管理等功能，将教师从繁重的批改与统计中解放出来，让教学更聚焦于课堂本身。

平台支持**高度定制**：教师可按需开关功能模块、调整界面，甚至绑定学生IP实现精细化管理。同时，活跃的社区持续推动其进化——2026年初的更新已加入**网页课件活动、物联网/人工智能体验（如人脸识别、AI绘画）及在线协作工具**，紧跟技术前沿。

## 信息技术教学平台功能结构与特色
- 平台结构： 
    - 学生平台：网站首页、我的课程、我的作品、常识测验、打字练习、资源下载。 
    - 教师平台：上课、备课、作品、签到、学生、测验、打字、资源、信息、状态。 
    - 系统后台：系统设置、班级设置、教师管理、新生导入、空间生成、学年升班、系统退出。
    

# 二。其它
- LearnSite学习平台讨论QQ群： 5847120
- [OpenLearnSite](https://www.openlearnsite.com/)
- [原作者：温州水乡 Github](https://github.com/WaterCountry/Learnsite)
* [Gitee：LearnSite主仓库源码](https://gitee.com/jnschool/learnsite-wz)
* [Gitee：LearnSite成都版分支源码](https://gitee.com/jnschool/game/tree/master/LearnSite_ChengDu)
* [Github：learnsite-docker](https://github.com/RealKiro/learnsite-docker)
* [Gitee：learnsite-docker(同步Github镜像)](https://gitee.com/realiy/learnsite-docker)
* [Docker仓库：learnsite-web](https://hub.docker.com/r/orzg/learnsite-web)
* [Docker仓库：mssql-learnsite](https://hub.docker.com/r/orzg/mssql-learnsite)
* [Github：LearnSite 编译版](https://github.com/RealKiro/learnsite)
  * 提示：主仓库源码需编译，故从openlearnsite下载编译版后重新上传作为临时仓库以供Docker克隆使用
* [Gitee：LearnSite 编译版(同步Github镜像)](https://gitee.com/realiy/learnsite)

- [x] LearnSite信息技术学习平台当前最新版本：
![Gitee 最新发行版](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fgitee.com%2Fapi%2Fv5%2Frepos%2Fjnschool%2Flearnsite-wz%2Ftags%3F%26per_page%3D1%26direction%3Ddesc%26sort%3Dupdated&query=%24%5B0%5D.name&logo=gitee&label=%E6%9C%80%E6%96%B0%E7%89%88%E6%9C%AC&color=c71d23)
- [x] learnsite-web Docker仓库最新更新时间：
![Docker latest 更新](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fhub.docker.com%2Fv2%2Frepositories%2Forzg%2Flearnsite-web%2Ftags%2Flatest&query=%24.last_updated&logo=docker&label=latest%20更新&color=2496ED)

- TODO：
  - [ ] learnsite3.0 + postgresql 敬请期待

# 三。项目结构
```
learnsite/
|-- .git/                           # Git版本控制目录
|-- .gitee/                         # Gitee平台配置文件
|-- .gitignore                      # Git忽略配置
|-- LICENSE                         # 开源许可证
|-- README.md                       # 项目说明文档
|
|-- App_Code/                       # 核心业务逻辑代码（ASP.NET三层架构）
|   |-- Bll/                        # 业务逻辑层(Business Logic Layer)
|   |-- Dal/                       # 数据访问层(Data Access Layer)
|   |-- Model/                     # 数据模型(Entity/Model)
|   |-- Common/                    # 通用组件
|   |-- Utility/                   # 工具类库
|   |-- Store/                     # 存储相关处理
|
|-- Bin/                            # 编译后的依赖文件(DLL)
|-- App_Themes/                     # 主题样式文件
|   |-- Student/                    # 学生主题
|   |-- Teacher/                    # 教师主题
|
|-- teacher/                        # 教师后台管理
|   |-- Teach.master                # 教师页面母版
|   |-- index.aspx                  # 教师工作台首页
|   |-- start.aspx                  # 开始上课
|   |-- course.aspx                 # 课程列表
|   |-- coursecreate.aspx           # 创建课程
|   |-- courseedit.aspx             # 编辑课程
|   |-- courseshow.aspx             # 查看课程
|   |-- courseimport.aspx           # 导入课程
|   |-- courseold.aspx              # 过往课程
|   |-- coursedel.aspx              # 删除课程
|   |-- courseanalyse.aspx          # 课程分析
|   |-- coursetotal.aspx            # 课程统计
|   |-- missionadd.aspx             # 添加课堂活动
|   |-- missionedit.aspx            # 编辑课堂活动
|   |-- missionshow.aspx            # 查看课堂活动
|   |-- showtask.aspx               # 显示任务
|   |-- showmission.aspx            # 显示活动
|   |-- student.aspx                # 学生管理
|   |-- studentadd.aspx             # 添加学生
|   |-- studentedit.aspx            # 编辑学生
|   |-- studentshow.aspx            # 查看学生
|   |-- studentdel.aspx             # 删除学生
|   |-- delstudents.aspx            # 批量删除学生
|   |-- works.aspx                  # 作品管理
|   |-- workshow.aspx               # 查看作品
|   |-- workcheck.aspx              # 作品评分
|   |-- worknoscore.aspx            # 未评分作品
|   |-- workpackage.aspx            # 打包下载
|   |-- studentwork.aspx            # 学生作品详情
|   |-- studentworks.aspx           # 学生作品列表
|   |-- stuworkcircle.aspx          # 作品圈展示
|   |-- signin.aspx                 # 课堂签到
|   |-- signshow.aspx               # 签到展示
|   |-- notsign.aspx                # 未签到学生
|   |-- typeadd.aspx                # 添加打字文章
|   |-- typeedit.aspx               # 编辑打字文章
|   |-- typeshow.aspx               # 查看打字文章
|   |-- typedel.aspx                # 删除打字文章
|   |-- typer.aspx                  # 打字管理
|   |-- typerset.aspx               # 打字设置
|   |-- printtyper.aspx             # 打印成绩单
|   |-- typechineseadd.aspx         # 添加中文打字
|   |-- typechineseedit.aspx        # 编辑中文打字
|   |-- typechineseshow.aspx        # 查看中文打字
|   |-- typechinesedel.aspx         # 删除中文打字
|   |-- typechinese.aspx            # 中文打字管理
|   |-- typechineseset.aspx         # 中文打字设置
|   |-- soft.aspx                   # 软件列表
|   |-- softadd.aspx               # 添加软件
|   |-- softedit.aspx              # 编辑软件
|   |-- softdel.aspx               # 删除软件
|   |-- softcategory.aspx          # 软件分类
|   |-- softview.aspx              # 查看软件
|   |-- softnomic.aspx             # 软件排行
|   |-- wareadd.aspx               # 添加资源
|   |-- wareedit.aspx              # 编辑资源
|   |-- wareshow.aspx              # 查看资源
|   |-- htmladd.aspx               # 添加网页课件
|   |-- htmledit.aspx              # 编辑网页课件
|   |-- htmlshow.aspx              # 查看网页课件
|   |-- programadd.aspx            # 添加编程作业
|   |-- programedit.aspx           # 编辑编程作业
|   |-- programshow.aspx           # 查看编程作业
|   |-- pythonadd.aspx             # 添加Python作业
|   |-- pythonedit.aspx            # 编辑Python作业
|   |-- pythonshow.aspx            # 查看Python作业
|   |-- pixeladd.aspx              # 添加像素画
|   |-- pixeledit.aspx             # 编辑像素画
|   |-- pixelshow.aspx             # 查看像素画
|   |-- graphadd.aspx              # 添加图表
|   |-- grapgedit.aspx             # 编辑图表
|   |-- graphshow.aspx             # 查看图表
|   |-- exceladd.aspx              # 添加表格
|   |-- exceledit.aspx             # 编辑表格
|   |-- excelshow.aspx             # 查看表格
|   |-- kitymindadd.aspx           # 添加思维导图
|   |-- kitymindedit.aspx          # 编辑思维导图
|   |-- kitymindshow.aspx          # 查看思维导图
|   |-- txtformadd.aspx            # 添加表单
|   |-- txtformedit.aspx           # 编辑表单
|   |-- txtformshow.aspx           # 查看表单
|   |-- topicadd.aspx              # 添加话题
|   |-- topicedit.aspx             # 编辑话题
|   |-- topicshow.aspx             # 查看话题
|   |-- attitude.aspx              # 课堂表现
|   |-- attitudegroup.aspx         # 表现分组
|   |-- circlegroup.aspx           # 小组圈分组
|   |-- circlegroups.aspx          # 小组圈列表
|   |-- circleshow.aspx            # 查看小组圈
|   |-- grouping.aspx              # 分组管理
|   |-- termview.aspx              # 学期视图
|   |-- termscores.aspx            # 学期成绩
|   |-- termscores.aspx            # 学期评分
|   |-- gauge.aspx                 # 仪表盘
|   |-- gaugeitem.aspx             # 仪表盘项目
|   |-- myseat.aspx               # 我的座位
|   |-- computers.aspx            # 机房电脑状态
|   |-- ipstudent.aspx            # IP学生绑定
|   |-- infomation.aspx           # 信息查看
|   |-- systeminfo.aspx            # 系统状态
|   |-- sitelog.aspx              # 网站日志
|   |-- package.aspx              # 打包管理
|   |-- shareview.aspx            # 分享查看
|   |-- sharegview.aspx           # 分组查看
|   |-- reorder.aspx              # 排序
|   |-- helper.aspx               # 辅助工具
|   |-- qrcodeadd.aspx            # 二维码管理
|   |-- mythware.aspx             # 极域电子教室
|   |-- learnrate.aspx            # 学习率统计
|   |-- problem.aspx              # 问题管理
|   |-- consoleadd.aspx           # 添加控制台
|   |-- consolepreview.aspx        # 预览控制台
|   |-- consoleshow.aspx          # 显示控制台
|   |-- judgeedit.aspx            # 编辑判断题
|   |-- judgesave.ashx            # 保存判断题
|   |-- ware.ashx                  # 资源处理器
|   |-- getproject.ashx           # 获取项目
|   |-- Runpy.ashx                # 运行Python
|
|-- student/                        # 学生前台功能
|   |-- Stud.master                # 学生页面母版
|   |-- index.aspx                  # 学生首页
|   |-- mycourse.aspx              # 我的课程
|   |-- mywork.aspx                # 我的作品
|   |-- myquiz.aspx                # 常识测验
|   |-- mytype.aspx                # 打字练习
|   |-- myfinger.aspx              # 指纹练习
|   |-- mychinese.aspx             # 中文打字
|   |-- myinfo.aspx                # 我的信息
|   |-- mynum.aspx                 # 学号信息
|   |-- myfile.aspx               # 我的文件
|   |-- myportfolio.aspx           # 我的作品集
|   |-- coding.aspx               # 编程学习
|   |-- program.aspx               # 编程作业
|   |-- programing.aspx           # 编程中
|   |-- python.aspx                # Python编程
|   |-- pythonIdle.aspx            # Python海龟绘图
|   |-- pythonblock.aspx           # Python积木
|   |-- pythonblockly.aspx         # Python Blockly
|   |-- pysolve.aspx               # Python解答
|   |-- console.aspx               # 控制台
|   |-- excel.aspx                 # 表格处理
|   |-- word.aspx                  # 文档处理
|   |-- draw.aspx                  # 绘图
|   |-- pptist.aspx                # PPT制作
|   |-- pptupload.ashx             # PPT上传
|   |-- mqtt.aspx                  # 物联网
|   |-- kitymind.aspx              # 思维导图
|   |-- markdown.aspx              # Markdown写作
|   |-- mxgraph.aspx               # 流程图
|   |-- pixel.aspx                 # 像素画
|   |-- sokoban.aspx               # 推箱子游戏
|   |-- qrcode.aspx                # 二维码
|   |-- quizstart.aspx             # 开始测验
|   |-- quizview.aspx              # 查看测验
|   |-- quizrank.aspx              # 测验排行
|   |-- myexam.aspx                # 我的考试
|   |-- typerank.aspx              # 打字排行
|   |-- typerclass.aspx            # 班级打字
|   |-- alltyper.aspx              # 全部打字成绩
|   |-- allchinese.aspx            # 全部中文打字
|   |-- allfinger.aspx             # 全部指纹
|   |-- mysurvey.aspx              # 我的问卷
|   |-- mysurveyclass.aspx         # 班级问卷
|   |-- mysurveymate.aspx          # 问卷伙伴
|   |-- mysurveyrank.aspx          # 问卷排行
|   |-- mysurveyresult.aspx        # 问卷结果
|   |-- autonomic.aspx             # 自主学习
|   |-- autonomiccategory.aspx     # 自主分类
|   |-- attituderank.aspx          # 表现排行
|   |-- handnum.aspx               # 手势识别
|   |-- chat.aspx                  # 聊天室
|   |-- showcourse.aspx            # 课程展示
|   |-- summary.aspx               # 课程总结
|   |-- summaryedit.aspx           # 编辑总结
|   |-- description.aspx           # 课程描述
|   |-- register.aspx              # 注册
|   |-- download.aspx              # 资源下载
|   |-- downfile.aspx              # 下载文件
|   |-- downwork.aspx              # 下载作品
|   |-- website.aspx               # 网站空间
|   |-- webspace.aspx              # 网站空间
|   |-- webstore.aspx              # 作品仓库
|   |-- webstoresql.aspx           # SQL作品
|   |-- codeproject.aspx           # 代码项目
|   |-- codeshare.aspx             # 代码分享
|   |-- groupshare.aspx            # 小组分享
|   |-- masterwork.aspx            # 优秀作品
|   |-- myevaluate.aspx            # 互评
|   |-- myrule.aspx                # 规则
|   |-- topicdiscuss.aspx          # 话题讨论
|   |-- htmledit.aspx              # 网页编辑
|   |-- htmleditor.aspx            # 网页编辑器
|   |-- iframe.aspx                # 内嵌框架
|   |-- style.aspx                 # 样式
|   |-- autoupload.aspx            # 自动上传
|   |-- turtleidle.aspx            # 海龟绘图idle
|   |-- txtform.aspx               # 表单
|   |-- txtformresult.aspx         # 表单结果
|   |-- savechat.ashx              # 保存聊天
|   |-- saveform.ashx              # 保存表单
|   |-- share.ashx                 # 分享
|   |-- getpo.ashx                 # 获取位置
|   |-- getproject.ashx            # 获取项目
|   |-- website.ashx               # 网站
|   |-- upload*.ashx               # 各类上传(作品/图片/媒体/项目等)
|   |-- uploadgroup.aspx           # 小组上传
|   |-- uploadgroupm.aspx          # 小组移动端上传
|   |-- uploadwork.aspx            # 作品上传
|   |-- uploadworkm.aspx           # 作品移动端上传
|
|-- manager/                        # 系统管理员后台
|   |-- Manage.master               # 管理员母版
|   |-- index.aspx                 # 管理首页
|   |-- setting.aspx               # 系统设置
|   |-- roomselect.aspx            # 机房选择
|   |-- createroom.aspx            # 创建机房
|   |-- studentimport.aspx         # 新生导入
|   |-- divide.aspx                # 班级分班
|   |-- backup.aspx                # 数据库备份
|   |-- clearold.aspx              # 清理旧数据
|   |-- copygood.aspx              # 复制优质
|   |-- showstudent.aspx           # 查看学生
|   |-- teacher.aspx               # 教师管理
|   |-- teacheradd.aspx            # 添加教师
|   |-- teacheredit.aspx           # 编辑教师
|   |-- upgrade.aspx               # 系统升级
|
|-- plugins/                        # 插件库（需单独部署）
|   |-- pptx/                      # PPT幻灯片插件
|   |   |-- README.md
|   |-- km/                        # 思维导图(kityminder)
|   |   |-- kityminder.editor.js  # 编辑器核心
|   |   |-- demo.km               # 示例文件
|   |-- luckysheet/                # 在线表格(协作编辑)
|   |   |-- docs/                 # 文档
|   |-- docx/                      # 在线文档
|   |-- canvas-editor/             # 画板编辑器
|   |-- canvas-ppt/                # 画板PPT
|   |-- flash/                     # Flash播放器
|   |   |-- Scratch.swf           # Scratch虚拟机
|   |-- flexpaper/                 # 文档翻页插件
|   |-- flv/                       # 视频播放器
|   |-- pdf/                       # PDF查看器
|   |   |-- pdf.html
|   |   |-- pdfobject.js
|   |   |-- build/                # 编译文件
|   |-- psd/                       # PSD图片预览
|   |-- office.htm                 # Office在线预览
|   |-- flash.htm                  # Flash在线播放
|   |-- excalidraw/                # 绘图工具
|   |-- freemind/                  # 思维导图
|   |-- qrcode/                    # 二维码生成
|   |-- photo/                     # 图片处理
|   |-- sketchup/                  # 3D模型查看
|   |-- scratch/                    # Scratch运行时
|   |   |-- Scratch.swf            # Scratch虚拟机
|   |   |-- Friend.sb              # 示例项目
|   |   |-- swfobject.js
|   |-- python/                    # Python编辑器插件
|   |-- mqtt.html                  # 物联网插件入口
|   |-- txt/                       # 文本插件
|   |-- mp3/                       # 音频插件
|   |-- 1.swf                     # 通用Flash
|
|-- code/                           # 编程学习工具集
|   |-- index.html                 # Python开发与集成环境(PyDevilder)
|   |-- mqtt.html                  # 物联网实验室(MQTT)
|   |-- htmleditor.html            # 在线网页设计编辑器
|   |-- blockpy.html               # Python Blockly积木编程
|   |-- block.html                 # Python拼图编程
|   |-- mqtt/                      # MQTT物联网资源
|       |-- mqtt.min.js            # MQTT客户端库
|
|-- ai/                             # 人工智能初步
|   |-- ai.html                    # AI体验入口
|   |-- ai.png                     # 图标
|   |-- bundle.js                  # 打包库
|   |-- crypto-js.js               # 加密库
|   |-- T-Rex/                     # 恐龙游戏AI
|   |-- Tic_Tac_Toe/               # 井字棋AI
|   |-- handnum/                   # 手势数字识别
|   |-- speak/                     # 语音合成
|   |-- stylization/               # 图像风格化
|   |-- styleml5/                  # ML5.js机器学习
|   |-- translate/                 # 翻译
|   |-- webcanvas/                 # Web画布
|
|-- deepseek/                       # DeepSeek AI助手（Docker 化部署）
|   |-- deepseek.py                # Python后端（支持 Docker）
|   |-- index.html                 # 入口页面（导航页）
|   |-- deepseek.html              # AI会话助手
|   |-- deepseek.aspx              # AI会话后端
|   |-- deepseek.css               # 样式
|   |-- aidraw.html                # AI绘画生成(DALL-E)
|   |-- aidraw.aspx                # AI绘画后端
|   |-- ocr.html                   # OCR文字识别
|   |-- ocr.aspx                   # OCR后端
|   |-- speek.html                 # 语音合成(TTS)
|   |-- speek.aspx                 # TTS后端
|   |-- soundlab.html              # 语音识别(STT)实验
|   |-- soundlab.aspx              # STT后端
|   |-- robot.html                 # 翻译机器人
|   |-- robot.css                  # 机器人样式
|   |-- tic-tac-toe.html           # 井字棋AI游戏
|   |-- tic-tac-toe.aspx           # 井字棋后端
|   |-- tic-tac-toe.js             # 游戏逻辑
|   |-- deepseek使用说明.txt       # 使用说明
|   |-- webfonts/                  # 字体资源
|   |-- downloads/                  # AI生成图片目录
|   |-- uploads/                    # 上传文件目录
|   |-- downmp3/                    # 语音合成目录
|
|-- machine/                        # 机器学习/图像分类
|   |-- index.html                 # 图像分类实验入口
|   |-- imageclass.aspx            # 图像分类API
|   |-- imageclass.aspx.cs
|   |-- main.js                    # 主脚本
|   |-- hand.js                    # 手势识别
|   |-- hand.html                  # 手势实验
|   |-- test.html                  # 测试页
|   |-- preview.html               # 预览页
|   |-- upmodel.ashx              # 上传模型
|   |-- tf.min.js                 # TensorFlow.js
|   |-- fabric.min.js             # Canvas库
|   |-- teachablemachine-image.min.js  # TeachableMachine
|   |-- style.css                 # 样式
|   |-- mobilenetv3/               # MobileNet模型
|   |   |-- model.json
|   |   |-- group1-shard1of2.bin
|   |   |-- group1-shard2of2.bin
|   |-- fonts/                     # 字体
|   |-- 神经网络结构图.jpg          # 结构图
|
|-- faceai/                         # 人脸识别体验
|   |-- index.html                 # 人脸识别探秘
|   |-- index.js                  # 主脚本
|   |-- index.css                 # 样式
|   |-- meme.html                 # 魔法表情包
|   |-- meme.js                   # 表情包脚本
|   |-- meme.aspx                 # 表情包后端
|   |-- video.html                # 视频捕捉人脸
|   |-- video.js                  # 视频脚本
|   |-- webcam.html               # 扫脸应用
|   |-- webcam.js                 # 扫脸脚本
|   |-- webcam.aspx               # 扫脸后端
|   |-- face.aspx                 # 人脸API
|   |-- face-api.js              # face-api.js库
|   |-- face-api.min.js          # 压缩版
|   |-- upface.ashx              # 上传人脸
|   |-- models/                   # 人脸模型
|   |-- images/                   # 示例图片
|   |-- videos/                   # 示例视频
|
|-- scratch/                        # Scratch积木编程
|   |-- index.html                 # Scratch访问入口
|   |-- player.html                # 作品播放器
|   |-- blocks-only.html           # 只含积木版本
|   |-- project.sb3               # 示例项目
|   |-- lib.min.js                # 核心库
|   |-- analytics.js              # 分析
|   |-- extension-worker.js        # 扩展工作线程
|   |-- static/                   # 静态资源
|   |-- asset/                    # 资源
|   |-- chunks/                   # 块
|   |-- 改动的地方备忘.txt         # 改动记录
|
|-- blockly/                       # Blockly图形化小游戏
|   |-- index.html                 # 访问入口
|   |-- zh-hans/                   # 中文语言包
|
|-- Piano/                         # 自由钢琴
|   |-- index.html                 # 访问入口
|   |-- static/                    # 静态资源
|   |-- favicon.png               # 图标
|   |-- README.md
|   |-- LICENSE
|
|-- pixelartmaker/                 # 像素画创作
|   |-- index.html                 # 访问入口
|   |-- PixelArt.html             # 画布
|   |-- pixelartmaker.js          # 核心脚本
|   |-- PixelPlay.js              # 播放
|   |-- gif.js                    # GIF生成
|   |-- gif.worker.js             # GIF工作线程
|   |-- html2canvas.min.js        # 截图
|   |-- lz-string-1.4.4.js        # 压缩
|   |-- style.css                 # 样式
|   |-- draw.ogg                  # 音效
|   |-- music.mp3                  # 背景音乐
|   |-- README.md
|
|-- sokoban/                       # 推箱子小游戏
|   |-- index.html                 # 访问入口
|   |-- index.aspx                 # 后端
|   |-- index.aspx.cs
|   |-- mapview.html              # 地图查看
|   |-- test.html                 # 测试页
|   |-- push.ashx                 # 推送
|   |-- js/                       # 脚本
|   |-- images/                    # 图片
|   |-- sound/                    # 音效
|   |-- README.md
|   |-- LICENSE.html
|
|-- ztype/                         # 太空打字游戏
|   |-- index.html                 # 访问入口
|   |-- ztype.js                  # 游戏核心
|   |-- ztype-trainer.js          # 训练模式
|   |-- backend.ashx              # 后端
|   |-- media/                    # 媒体资源
|   |-- README.md
|
|-- wuziqi/                        # AI五子棋
|   |-- index.html                 # 访问入口
|   |-- index.aspx                 # 后端
|   |-- index.aspx.cs
|   |-- wuziqi.ashx               # AI处理
|   |-- js/                       # 脚本
|   |-- images/                    # 图片
|   |-- sound/                    # 音效
|   |-- style/                    # 样式
|
|-- markdown/                      # MarkDown写作
|   |-- index.html                 # 访问入口
|   |-- editormd.js                # 编辑器核心
|   |-- editormd.min.js            # 压缩版
|   |-- preview.html               # 预览页
|   |-- css/                       # 样式
|   |-- lib/                      # 库
|   |-- plugins/                   # 插件
|   |-- fonts/                     # 字体
|   |-- images/                    # 图片
|
|-- fabriceditor/                  # 在线图片设计(Canvas)
|   |-- index.html                 # 访问入口
|   |-- poster.aspx                # 海报
|   |-- poster.aspx.cs
|   |-- uploadposter.ashx          # 上传
|   |-- js/                       # 脚本
|   |-- assets/                    # 资源
|   |-- template/                  # 模板
|   |-- shape/                     # 形状
|   |-- svg/                       # SVG图形
|   |-- photo/                     # 照片
|   |-- font-tmpl/                 # 字体模板
|
|-- mxgraph/                       # 流程图绘制工具
|   |-- index.html                 # 访问入口
|   |-- open.html                  # 打开文件
|   |-- test.html                  # 测试页
|   |-- viewer.html                # 查看器
|   |-- mxClient.js                # 客户端核心
|   |-- mxClient.min.js            # 压缩版
|   |-- alljs.py                   # 打包脚本
|   |-- js/                       # 脚本
|   |-- images/                    # 图片
|   |-- styles/                    # 样式
|   |-- stencils/                  # 模板
|   |-- resources/                 # 资源
|   |-- src/                       # 源码
|   |-- deflate/                   # 压缩
|   |-- sanitizer/                 # 过滤
|   |-- jscolor/                   # 颜色选择
|
|-- quiz/                          # 试题测验系统
|   |-- quiz.aspx                  # 测验主页
|   |-- quizadd.aspx              # 添加测验
|   |-- quizedit.aspx             # 编辑测验
|   |-- quizselect.aspx           # 选择测验
|
|-- survey/                        # 问卷调查
|   |-- survey.aspx               # 问卷主页
|   |-- surveyadd.aspx            # 添加问卷
|   |-- surveyitem.aspx           # 问卷项目
|   |-- surveyquestion.aspx       # 问卷问题
|
|-- weboffice/                     # 在线文档编辑器(WPS/Office)
|   |-- getoffice.aspx             # 文档获取接口
|   |-- getoffice.aspx.cs
|   |-- LoadWebOffice.js           # 加载脚本
|   |-- main.js                   # 主脚本
|   |-- WebOffice.cab              # ActiveX控件
|   |-- weboffice.htm             # 文档页面
|   |-- webofficetone.htm         # 文档页面2
|
|-- seat/                          # 座位相关
|   |-- seatshow.aspx              # 座位表展示
|   |-- seatshow.aspx.cs
|   |-- computer.aspx              # 机房座位管理
|   |-- computer.aspx.cs
|   |-- house.aspx                 # 教室座位
|   |-- house.aspx.cs
|   |-- ip.aspx                    # IP分配
|   |-- ip.aspx.cs
|   |-- getip.aspx                 # 获取IP
|   |-- saveseat.ashx              # 保存座位
|
|-- profile/                       # 学生个人界面
|   |-- Pf.master                 # 母版页
|   |-- mychange.aspx             # 修改信息
|   |-- myclass.aspx              # 我的班级
|   |-- mygroup.aspx              # 我的小组
|   |-- myname.aspx               # 修改姓名
|   |-- myphoto.aspx              # 我的照片
|   |-- mypwd.aspx                # 修改密码
|   |-- mysex.aspx                # 修改性别
|   |-- mysign.aspx              # 修改签名
|   |-- myterm.aspx               # 学期信息
|
|-- python/                        # Python绘画编程(turtle海龟绘图)
|   |-- index.aspx                 # Python首页
|   |-- idle.aspx                  # IDLE编程环境
|   |-- turtle.aspx                # 海龟绘图
|   |-- sketch.aspx                # 素描
|   |-- code.aspx                  # 代码
|   |-- visit.aspx                 # 访问
|   |-- manage.aspx                # 管理
|   |-- match.aspx                 # 比赛
|   |-- matchnew.aspx              # 新比赛
|   |-- matchrank.aspx             # 比赛排行
|   |-- matchshow.aspx             # 比赛展示
|   |-- question.aspx              # 题目
|   |-- questionedit.aspx          # 编辑题目
|   |-- uploadcode.ashx            # 上传代码
|   |-- uploadturtle.ashx          # 上传绘图
|   |-- thumbnail/                 # 缩略图
|
|-- lessons/                       # 课堂活动相关设置
|   |-- precourse.aspx            # 课前预习
|   |-- premission.aspx           # 课前任务
|   |-- prescm.master             # 课前母版
|   |-- presurvey.aspx            # 课前调查
|   |-- pretopicdiscuss.aspx     # 课前话题
|   |-- thinkadd.aspx             # 添加思考
|   |-- thinkedit.aspx            # 编辑思考
|   |-- thinkshow.aspx            # 显示思考
|
|-- kindeditor/                    # 学案设计编辑器(富文本)
|   |-- kindeditor-min.js         # 核心库
|   |-- aspnet/                   # ASP.NET集成
|   |-- lang/                      # 语言包
|   |-- plugins/                   # 插件
|   |-- themes/                    # 主题
|   |-- license.txt                # 许可证
|
|-- plupload/                      # 文件上传组件
|   |-- plupload.min.js           # 核心库
|   |-- plupload.full.min.js      # 完整版
|   |-- plupload.dev.js           # 开发版
|   |-- moxie.js                  # 运行时
|   |-- moxie.min.js              # 压缩版
|   |-- Moxie.swf                 # Flash上传
|   |-- Moxie.xap                 # Silverlight
|   |-- jquery.plupload.queue     # jQuery队列
|   |-- jquery.ui.plupload        # jQuery UI
|   |-- i18n/                     # 国际化
|
|-- luckysheetbottle/             # 在线协作表格后端（需单独部署）
|   |-- web.py                    # Python后端
|   |-- index.html                # 入口
|   |-- index.tpl                 # 模板
|   |-- static/                   # 静态文件
|   |-- expendPlugins/            # 扩展插件
|   |-- README读我.txt             # 说明
|
|-- js/                            # 前端运行框架
|   |-- jquery.min.js              # jQuery库
|   |-- bootstrap.min.css          # Bootstrap样式
|   |-- skulpt.min.js              # Python浏览器运行
|   |-- skulpt-stdlib.js           # Python标准库
|   |-- brython/                   # Brython(Python in JS)
|   |-- flash.js/ruffle.js         # Flash兼容
|   |-- pdfobject.js               # PDF查看
|   |-- nicEdit.js                 # 富文本编辑器
|   |-- dropzone/                  # 拖拽上传
|   |-- flot/                      # 图表库
|   |-- css/                       # 样式文件
|   |-- fonts/                     # 字体文件
|   |-- images/                    # UI图片
|   |-- webfonts/                  # Web字体
|
|-- images/                        # 缓存图库/网站素材
|
|-- sql/                           # 初始数据库脚本
|   |-- learnsite.sql              # 数据库结构
|
|-- LearnSiteServer/               # Windows桌面服务端程序
|   |-- LearnSiteServer.exe        # 主程序
|
|-- backupdb/                      # 数据库备份目录
|-- log/                           # 运行日志目录
|-- spec/                          # 课题研究/规格说明
|   |-- research.aspx             # 课题研究
|
|-- 说明必读/                      # 项目文档
|   |-- 平台搭建说明.txt           # 部署指南
|   |-- 网页课件与平台信息交互.txt # 交互说明
|   |-- 历史更新及更新方法.txt    # 更新日志
|   |-- 学生导入模板.xls         # 导入模板
|   |-- 学案里添加填表演示.gif   # 演示动画
|   |-- IIS里添加的mime类型.txt  # MIME配置
|   |-- 使用openssl生成pfx数字证书.txt # 证书配置
|   |-- 免责声明.txt
|   |-- 乡村教师版学案模版说明.txt
|   |-- 学生平台导航调整方法.txt
|   |-- LearnSite信息科技学习平台配置核对.pdf
|   |-- Windows Server2022下搭建Learnsite网站.pdf
|   |-- learnsite应用服务说明图解.png
|   |-- Scratch编程使用说明/
|   |-- 功能页面介绍/
|   |-- 图解问题/
|   |-- 数据库脚本/
|   |   |-- learnsite.sql
|   |   |-- 说明.txt
|   |   |-- 特别注意说明.txt
|   |-- 常见问题/
|
# 根目录核心文件
|-- index.aspx                     # 学生平台首页
|-- index.aspx.cs                  # 首页后端代码
|-- indexno.html                   # 无Javascript首页
|-- en.aspx                        # English版首页
|-- en.aspx.cs                     # English版后端
|-- en.xls                         # 英文词库
|-- error.aspx                     # 错误页面
|-- error.aspx.cs
|-- test.aspx                      # 系统测试页
|-- test.aspx.cs
|-- test.htm                       # 测试页(HTML)
|-- upgrade.aspx                   # 系统升级页
|-- upgrade.aspx.cs
|-- web.config                    # 站点配置文件( IIS )
|-- web.config.docker              # Docker配置
|-- website.xml                    # 网站配置信息
|-- favicon.ico                    # 网站图标
|-- sound.html                     # 在线音频播放
|-- noaccess.htm                   # 无权访问页面
|-- filenotfound.htm               # 404页面
```