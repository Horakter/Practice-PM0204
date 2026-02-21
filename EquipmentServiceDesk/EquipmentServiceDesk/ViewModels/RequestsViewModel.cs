using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquipmentServiceDesk.Data;
using EquipmentServiceDesk.Models;
using EquipmentServiceDesk.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EquipmentServiceDesk.ViewModels
{
    public partial class RequestsViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        private User _currentUser;

        private readonly RequestService _requestService;

        [ObservableProperty]
        private ObservableCollection<Request> requests;

        [ObservableProperty]
        private Request selectedRequest;

        [ObservableProperty]
        private string newTitle;

        [ObservableProperty]
        private string newDescription;

        [ObservableProperty]
        private string selectedStatusFilter;

        public List<string> StatusFilter { get; } = new() { "Все", "Новая", "В работе", "Завершена" };

        public RequestsViewModel(RequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Initialize(User user)
        {
            _currentUser = user;
            await LoadRequests();
        }

        private async Task LoadRequests()
        {
            var list = await _requestService.GetAllAsync();
            Requests = new ObservableCollection<Request>(list);
        }

        [RelayCommand]
        private void ApplyFilter()
        {
            LoadRequests();
        }

        [RelayCommand]
        private void ChangeStatus(string newStatus)
        {
            if (SelectedRequest == null)
                return;

            SelectedRequest.Status = newStatus;

            _context.Requests.Update(SelectedRequest);
            _context.SaveChanges();

            LoadRequests();
        }


        [RelayCommand]
        private void AddRequest()
        {
            if (string.IsNullOrWhiteSpace(NewTitle))
                return;

            var request = new Request
            {
                Title = NewTitle,
                Description = NewDescription,
                Status = "Новая",
                CreatedAt = DateTime.Now,
                UserId = _currentUser.Id,
                EquipmentId = 1
            };

            _context.Requests.Add(request);
            _context.SaveChanges();

            NewTitle = "";
            NewDescription = "";

            LoadRequests();
        }

        [RelayCommand]
        private void UpdateRequest()
        {
            if (SelectedRequest == null)
                return;

            _context.Requests.Update(SelectedRequest);
            _context.SaveChanges();

            LoadRequests();
        }

        [RelayCommand]
        private void DeleteRequest()
        {
            if (SelectedRequest == null)
                return;
            if (_currentUser.Role != "Admin" && SelectedRequest.UserId != _currentUser.Id)
            {
                MessageBox.Show("Вы не можете удалить чужую заявку.");
                return;
            }
            _context.Requests.Remove(SelectedRequest);
            _context.SaveChanges();

            LoadRequests();
        }

        [RelayCommand]
        private void Close()
        {
            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w is Views.RequestsView)
                ?.Close();
        }
    }
}