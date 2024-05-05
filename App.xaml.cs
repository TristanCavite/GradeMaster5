using GradeMaster5.Views;

namespace GradeMaster5
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Resolve MainWindow using the service provider
            MainPage = serviceProvider.GetService<MainWindow>();
        }
    }
}