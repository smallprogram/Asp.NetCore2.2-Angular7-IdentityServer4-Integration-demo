// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;

namespace IdentityServer4.Quickstart.UI
{
    public class AccountOptions
    {
        // 是否允许本地登录
        public static bool AllowLocalLogin = true;
        // 是否允许记住用户登录信息
        public static bool AllowRememberLogin = true;

        // 记住登录状态的时间长度
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        // 是否显示注销提示
        public static bool ShowLogoutPrompt = true;

        // 是否注销之后自动重定向
        public static bool AutomaticRedirectAfterSignOut = false;

        // 指定正在使用的Windows身份验证方案
        public static readonly string WindowsAuthenticationSchemeName = Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;
        // 如果用户使用windows auth，我们是否应该从windows加载组
        public static bool IncludeWindowsGroups = false;
        // 设置无效凭据的错误信息
        public static string InvalidCredentialsErrorMessage = "Invalid username or password";
    }
}
