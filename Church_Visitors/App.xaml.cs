using Church_Visitors.Extensions;

namespace Church_Visitors
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            var services = new ServiceCollection();
            services.AddDataServices("connect ECONNREFUSED 127.0.0.1:27017", "ChurchDB");

        }
    }
}