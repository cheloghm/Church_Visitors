﻿using Church_Visitors.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Church_Visitors
{
    public partial class App : Application
    {
        public static string BaseApiUrl = "https://yourapiurl.com";
        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

            var services = new ServiceCollection();
            services.AddDataServices(BaseApiUrl);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
