# 2019-04-15
### �����������Ŀ</br>
api: asp.net core 2.2 webapi��Ŀ����Ҫ���ڹ���Rest API</br>
core�� �������</br>
Infrastructure :�������</br>

### Asp.netCore Environment��������
Asp.net coreĬ�Ϸ�Ϊ������������</br>
<b>Production(����������Ĭ��ֵ)</br>
Development(����������</br>
Staging����ʱ������</b></br>
����������������Ŀ�ļ���LaunchSettings.json�ļ�������</br>

ͨ����startup�ļ���ʹ��env.IsDevelopment,env.IsProduction,env.IsStaging�����жϵ�ǰ��������������
</br>
��������������Properties\LaunchSettings.json�������ã�Ҳ������ϵͳ�����û�������
</br>
<b>���Ե������岻ͬ�Ļ���������Startup��</b><br>
���磺<br>
<b>Startup{��������}<br>
startupDevelopment<br>
StartupProduction<br>
StartupStaging<br>
</b>
��Program������IWebHostBuilderʱʹ��<br>
`UseStartup(IWebHostBuilder,String)`
������<br>
`UseStartup<Startup>(IWebHostBuilder)`.<br>
����String������StartupXXX���ڵ�Assembly�����֡�<br>

### ֧��HTTPS
΢��Ҫ������asp.net core����ʹ��https�ض����м������http�����ض���https<br>
��Startup������<br>
ConfigureServices����ע�Ტ���ö˿ں�״̬��ȣ�<br>
      `services.AddHttpsRedirection(...)`<br>
Configure����ʹ�ø��м��<br>
`app.UseHttpsRedirection()`

### ֧��HSTS
HSTS��Http Strict Transport Security Protocol<br>
��startup������<br>
ConfigureServices����ע�������HSTS��<br>
`services.AddHsts(...)`<br>
Configure����ʹ�ø��м��<br>
`app.UseHsts()`<br>

### ����Entity Framework Core
1. ��װEF��
* Microsoft.EntityFrameworkCore.Design for Infrastructure
* Microsoft.EntityFrameworkCore.SqlServer for Core
2. ����Context
* ����Entities
* ����Context���̳���DbContext
3. ��Startup����ע��Context
`service.AddDbContext<xxxContext>(...)`
### EFCore����
1. ��Entities����Core��Ŀ��
2. Context����Infrastructure��Ŀ��
3. ��Api��Ŀ�н���ע������
### ���EFCore���ݿ�Ǩ��
�ο��ĵ���<br>
https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/cli/powershell
1. ʹ��Package Manager Console��������
2. Add-Migration [Name] ���Ǩ��
3. Update-Database [-Verbose]ִ������Ǩ�Ƹ��µ����ݿ�,-Verbose����ָ��ʱ����ʾִ����ϸ��
4. ���Ҫɾ��Ǩ�ƣ���ִ��Remove-Migration [Name]ɾ��Ǩ�����ݣ�֮��ִ��Update-Database��ɾ����Ǩ�Ʋ������µ����ݿ�<br>
<b>Ҳ����ʹ��dotnet cliִ������</b>

### ����DbSeed����ʼ�����ݿ�����
�ο�Api��Ŀ��Program��ĵ��ã��ο�Infrastructure��Ŀ��Database\MyContextSeed.cs

### ʹ��Unit Of Work + Repositoryģʽ
1. ʹ��Repositoryģʽ�����ݿ�Context���������������ã�����δ���ڻ����ݿ��ṩ�̣��ó־û�������̬��
2. ʹ��Repositoryģʽ���ڲ���
3. ʹ��Repositoryģʽ���ڴ��븴��
4. ʹ�ýӿ�IRepository��ͨ������ע��ʵ������ϣ�������ѭDIPԭ��(��νDIPԭ�����SOLID���Dԭ��Ҫ��߼���ģ�鲻Ӧ�������ڵͼ���ģ�飬���Ƕ�Ӧ�������ڳ���)
5. ʹ�ýӿ�IRepository�����㵥Ԫ����

### ΪʲôҪʹ��Unit Of Work
1. DbContext�Ѿ�ʵ����Unit Of Work��Repositoryģʽ��
2. Controller�� ��Ӧ��ֱ��ʹ��DbContext

### ��ʹ��Unit Of Work + Repositoryģʽʱ
 `postRepository.AddPost(post);`<br>
`await unitOfWork.SaveAsync();`<br>
����ʹ�õ�DbContextΪͬһ��DbContext��EFCore�Ѿ��������ǿ���������ʹ�õ�DbContext���������ڡ�


### Asp.net Core����ע����������
1. Transient:ÿ������������(����ָHttp Request)���ᴴ��һ���µ�ʵ�����Ƚ��ʺ�����������״̬��service.
2. Scope:ÿ��HTTP���󶼻ᴴ��һ��ʵ��
3. Singleton���ڵ�һ�����󴴽�һ��ʵ�����Ժ�Ҳֻ�Ƿ��ص�һ�δ�����ʵ����������ConfigureService��������ʱ����һ��Ψһ��ʵ��

