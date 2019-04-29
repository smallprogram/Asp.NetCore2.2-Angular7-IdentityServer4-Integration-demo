// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const idpBase = 'https://localhost:5001';
export const apiResourceUrl = 'https://localhost:6001'
export const angularBase = 'http://localhost:4200';


export const environment = {
  production: false,
  apiUrlBase:'/api',
  openIdConnectSettings:{
    authority: `${idpBase}`,        //授权服务器地址
    client_id: 'AngularClient',     //clientId
    redirect_uri: `${angularBase}/signin-oidc`,  //登录后跳转地址
    post_logout_redirec_uri: `${angularBase}/`,   //登出后跳转地址
    scope: 'openid profile email RESTApi',   //授权的Scope
    response_type: 'id_token token',    //返回的东西
    automaticSilentRenew: true,    //是否自动刷新AccessToken
    silent_redirect_uri: `${angularBase}/redirect-silentrenew`   //刷新AccessToken后的地址
  }
};


/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
