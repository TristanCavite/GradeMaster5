using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using GradeMaster5.Services;


namespace GradeMaster5.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IDialogService _dialogService;
        private AppDbContext _dbContext;

        public string Username { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel(IDialogService dialogService, AppDbContext dbContext)
        {
            _dialogService = dialogService;
            _dbContext = dbContext; // Assuming dbContext is injected or initialized elsewhere if not injected

            LoginCommand = new Command(async () => await ExecuteLogin());
        }

        private async Task ExecuteLogin()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await _dialogService.ShowAlertAsync("Error", "Please enter both username and password.", "OK");
                return;
            }

            try
            {
                // Retrieve the user from the database
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == Username);

                // Check if user exists and the password matches
                if (user != null && user.Password == Password)
                {
                    await _dialogService.ShowAlertAsync("Success", "Login successful!", "OK");
                    // Navigate to another page or update the UI accordingly
                    // For example: await Shell.Current.GoToAsync("//MainPage");
                }
                else
                {
                    await _dialogService.ShowAlertAsync("Error", "Invalid username or password.", "OK");
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
