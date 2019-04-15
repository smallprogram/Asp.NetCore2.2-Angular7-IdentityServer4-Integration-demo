# 2019-04-15
### 添加了三个项目</br>
api: asp.net core 2.2 webapi项目，主要用于构建Rest API</br>
core： 核心类库</br>
Infrastructure :基础类库</br>

### Asp.netCore Environment环境变量
Asp.net core默认分为三个环境变量</br>
<b>Production(生产环境，默认值)</br>
Development(开发环境）</br>
Staging（临时环境）</b></br>
环境变量配置在项目文件中LaunchSettings.json文件中配置</br>

通过在startup文件中使用env.IsDevelopment,env.IsProduction,env.IsStaging进行判断当前环境变量的内容
</br>
环境变量可以在Properties\LaunchSettings.json进行配置，也可以在系统中设置环境变量
</br>
<b>可以单独定义不同的环境变量的Startup类</b><br>
例如：<br>
<b>Startup{环境变量}<br>
startupDevelopment<br>
StartupProduction<br>
StartupStaging<br>
</b>
在Program里配置IWebHostBuilder时使用<br>
`UseStartup(IWebHostBuilder,String)`
而不是<br>
`UseStartup<Startup>(IWebHostBuilder)`.<br>
其中String参数是StartupXXX所在的Assembly的名字。<br>

### 支持HTTPS
微软要求所有asp.net core程序使用https重定向中间件，将http请求重定向到https<br>
在Startup中配置<br>
ConfigureServices方法注册并配置端口和状态码等：<br>
      `services.AddHttpsRedirection(...)`<br>
Configure方法使用该中间件<br>
`app.UseHttpsRedirection()`

### 支持HSTS
HSTS：Http Strict Transport Security Protocol<br>
在startup里配置<br>
ConfigureServices方法注册和配置HSTS：<br>
`services.AddHsts(...)`<br>
Configure方法使用该中间件<br>
`app.UseHsts()`<br>

### 集成Entity Framework Core
1. 安装EF包
* Microsoft.EntityFrameworkCore.Design for Infrastructure
* Microsoft.EntityFrameworkCore.SqlServer for Core
2. 建立Context
* 建立Entities
* 建立Context，继承自DbContext
3. 在Startup类中注册Context
`service.AddDbContext<xxxContext>(...)`
### EFCore配置
1. 将Entities放在Core项目中
2. Context放在Infrastructure项目中
3. 在Api项目中进行注册配置
### 添加EFCore数据库迁移
参考文档：<br>
https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/cli/powershell
1. 使用Package Manager Console输入命令
2. Add-Migration [Name] 添加迁移
3. Update-Database [-Verbose]执行数据迁移更新到数据库,-Verbose参数指定时将显示执行明细。
4. 如果要删除迁移，请执行Remove-Migration [Name]删除迁移内容，之后执行Update-Database将删除的迁移操作更新到数据库<br>
<b>也可以使用dotnet cli执行命令</b>

### 创建DbSeed，初始化数据库数据
参考Api项目中Program类的调用，参考Infrastructure项目中Database\MyContextSeed.cs

### 使用

