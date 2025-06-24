using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using 仓储管理系统.DAL;
using 仓储管理系统.Entities;
using 仓储管理系统.ViewModels;
using 仓储管理系统.Views;

namespace 仓储管理系统
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // 注册DbContext
            services.AddScoped<WarehouseDBEntities>(provider => new WarehouseDBEntities());

            // 注册仓储
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IUserMenuRepository, UserMenuRepository>();


            // 注册 ViewModels
            services.AddTransient<RootViewModel>();
            services.AddTransient<MainViewModel>();

            // 注册 Views
            services.AddTransient<RootView>();
            services.AddTransient<IndexView>();

            ServiceProvider = services.BuildServiceProvider();

            base.OnStartup(e);
        }

    }
}