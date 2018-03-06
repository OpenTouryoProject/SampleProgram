//**********************************************************************************
//* テンプレート
//**********************************************************************************

// サンプル中のテンプレートなので、必要に応じて使用して下さい。

//**********************************************************************************
//* クラス名        ：Startup
//* クラス日本語名  ：Startup
//*
//* 作成日時        ：－
//* 作成者          ：－
//* 更新履歴        ：－
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         ＸＸＸＸ
//**********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CordovaBackend
{
    /// <summary>Startup</summary>
    public class Startup
    {
        #region members & constructor

        /// <summary>IConfiguration</summary>
        public IConfiguration Configuration { get; }

        /// <summary>constructor</summary>
        /// <param name="configuration">IConfiguration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        #endregion

        #region method

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to add services to the container.
        /// このメソッドは、ランタイムによって呼び出されます。
        /// このメソッドを使用して、コンテナにサービスを追加します。
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Adds MVC services to the specified IServiceCollection.
            // 指定されたIServiceCollectionにMVCサービスを追加します。
            services.AddMvc();
        }

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to configure the HTTP request pipeline.
        /// このメソッドは、ランタイムによって呼び出されます。
        /// このメソッドを使用して、HTTP要求パイプラインを構成します。
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // 開発時
                // 開発用エラー画面
                app.UseDeveloperExceptionPage();
                // Browser Linkのセットアップ 
                app.UseBrowserLink();
            }
            else
            {
                // 本番時
                // エラー画面
                app.UseExceptionHandler("/Home/Error");
            }

            // wwwroot以下の静的ファイルをリンクする場合。
            app.UseStaticFiles();

            // MVCを有効にしてルーティングを設定
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #endregion
    }
}
