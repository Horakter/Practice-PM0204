using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquipmentServiceDesk.Models;
using EquipmentServiceDesk.Services;
using EquipmentServiceDesk.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace EquipmentServiceDesk.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private User _currentUser;
        private readonly CurrentUserService _currentUserService;
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private string welcomeText;

        public string Username => _currentUserService.CurrentUser?.Username ?? "";

        public MainViewModel(CurrentUserService currentUserService, IServiceProvider serviceProvider)
        {
            _currentUserService = currentUserService;
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public void Initialize(User user)
        {
            _currentUser = user;
            WelcomeText = $"Добро пожаловать, {user.Username} ({user.Role})";
        }

        [RelayCommand]
        private void OpenRequests()
        {
            var requestsView = _serviceProvider.GetService<RequestsView>();
            requestsView.Show();
            var window = _serviceProvider.GetRequiredService<RequestsView>();
            window.Initialize(_currentUser);
            window.Show();
        }

        [RelayCommand]
        private void Logout()
        {
            var login = _serviceProvider.GetRequiredService<Views.LoginView>();
            login.Show();

            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w is MainWindow)
                ?.Close();
        }
    }
}
