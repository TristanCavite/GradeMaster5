using Microsoft.Maui.Controls;
using GradeMaster5.ViewModels;
using GradeMaster5.Services;
namespace GradeMaster5.Views
{
    public partial class MainWindow : ContentPage
    {
        public MainWindow(IDialogService dialogService, AppDbContext dbContext)
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(dialogService, dbContext);
        }
    }
}