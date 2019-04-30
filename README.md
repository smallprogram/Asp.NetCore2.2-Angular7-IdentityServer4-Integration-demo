<img src="https://img.shields.io/badge/Visual%20Studio-2019-green.svg">
<img src="https://img.shields.io/badge/Asp.Net%20Core-2.2-blue.svg">
<img src="https://img.shields.io/badge/IdentityServer4-2.4.0-red.svg">
<img src="https://img.shields.io/badge/Angular-7.2.0-blue.svg">
<img src="https://img.shields.io/badge/Node-10.15.0-lightgrey.svg">

# Asp.NetCore2.2-Angular7-IdentityServer4-Integration-demo
## 持续更新中......（Continuous update...）
**开发详情请参考SmallProgramDome项目下的Markdown文件**  
For development details, please refer to the Markdown file under the SmallProgramDome project.

## 这个Demo的介绍
- **AngularClient**: Angular客户端,位于
  - `SmallProgramDemo\AngularClient`
- **ApiResource**: 受IdentityServer4保护的REST Api资源，位于
  - `SmallProgramDemo\SmallProgramDemo.api`
  - `SmallProgramDemo\SmallProgramDemo.Core`
  - `SmallProgramDemo\SmallProgramDemo.Infrastructure`
- **IdentityServer4**: IdentityServer4，位于
  - `SmallProgramDemo\SmallProgramDemo.idp`
- **MvcClient**: Asp.NetCore客户端(用于测试，在本项目中没有什么意义)，位于
  - `SmallProgramDemo\SmallProgramDemo.MvcClient`
- **MarkdownImageConvertBase64**:一个用于生成markdown嵌入base64编码图片的工具，对于本项目没什么用，主要用来写Markdown文件，位于
  - `SmallProgramDemo\Tools.MarkDownImageConvertBase64`

## 运行方式
- 使用Visual Studio 2019打开SmallProgramDemo\SmallProgramDemo.sln,还原所有Nuget package.
- 更新`SmallProgramDemo\SmallProgramDemo.api`项目下，`appsettings.Development.json`文件中`HomeConnection`的数据库链接字符串，使其符合你本机的数据库环境。
- 设置`SmallProgramDemo.Api`为启动项目，使用VS自带的Package Manager Console，选定`SmallProgramDemo\SmallProgramDemo.Infrastructure`为默认项目，执行`update-database`，生成数据库(数据库为MSSQL数据库).
- 在vs解决方案中属性中选择启动多个项目，分别选择`SmallProgramDemo\SmallProgramDemo.api`和`SmallProgramDemo\SmallProgramDemo.Idp`，点击VS启动，这时会启动两个自托管的服务，其中api的https端口为6001,Idp的https端口为5001.
- 如果你选择使用Angular作为客户端，请使用CMD命令行窗口或PowerShell中，将当前路径设置到项目中AngularClient文件夹下，执行`npm install`命令，还原客户端相关包。还原成功之后，在AngularClient目录下执行`ng serve -o`命令，即可运行Angular客户端，并查看项目效果。

## 一些记录
前端还未实现当个POST查看、修改功能，未来有时间将会继续完善这个Demo.