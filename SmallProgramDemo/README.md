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

### 使用Unit Of Work + Repository模式
1. 使用Repository模式将数据库Context与控制器松耦合配置，方便未来在换数据库提供商，让持久化技术动态化
2. 使用Repository模式易于测试
3. 使用Repository模式易于代码复用
4. 使用接口IRepository，通过依赖注入实现松耦合，并且遵循DIP原则(所谓DIP原则就是SOLID里的D原则，要求高级别模块不应该依赖于低级别模块，他们都应该依赖于抽象)
5. 使用接口IRepository，方便单元测试

### 为什么要使用Unit Of Work
1. DbContext已经实现了Unit Of Work和Repository模式。
2. Controller等 不应该直接使用DbContext

### 在使用Unit Of Work + Repository模式时
 `postRepository.AddPost(post);`<br>
`await unitOfWork.SaveAsync();`<br>
他们使用的DbContext为同一个DbContext，EFCore已经帮助我们控制了他们使用的DbContext的生命周期。


### Asp.net Core服务注册生命周期
1. Transient:每次其他类请求(不是指Http Request)都会创建一个新的实例，比较适合轻量级的无状态的service.
2. Scope:每次HTTP请求都会创建一个实例
3. Singleton：在第一次请求创建一个实例，以后也只是返回第一次创建的实例；或者在ConfigureService代码运行时创建一个唯一的实例

