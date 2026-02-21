using EquipmentServiceDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using EquipmentServiceDesk.Views;
using EquipmentServiceDesk.Models;
using Microsoft.Extensions.DependencyInjection;


namespace EquipmentServiceDesk.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly CurrentUserService _currentUserService;
        private readonly IServiceProvider _serviceProvider;
        private readonly AuthService _authService;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string errorMessage;

       public LoginViewModel(IServiceProvider serviceProvider, AuthService authService, CurrentUserService currentUserService)
        {
            _authService = authService;
            _serviceProvider = serviceProvider;
            _currentUserService = currentUserService;
        }

        [RelayCommand]
        private async Task LoginAsync(string password)
        {
            var user = await _authService.LoginAsync(Username, password);

            if (user == null)
            {
                ErrorMessage = "Неверный логин или пароль";
                return;
            }
            _currentUserService.CurrentUser = user;

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w is LoginView)
                ?.Close();
        }

        [RelayCommand]
        private async Task RegisterAsync(string password)
        {
            bool result = await _authService.RegisterAsync(Username, password);

            if (!result)
            {
                ErrorMessage = "Пользователь уже существует";
                return;
            }
            MessageBox.Show("Регbстрация успешна!");
        }
        private void OpenMainWindow(User user)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            var viewModel = (MainViewModel)mainWindow.DataContext;
            viewModel.Initialize(user);
            mainWindow.Show();

            Application.Current.Windows[0]?.Close();
        }
    }
}
