using EquipmentServiceDesk.Models;
using EquipmentServiceDesk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EquipmentServiceDesk.Views
{
    /// <summary>
    /// Interaction logic for RequestsView.xaml
    /// </summary>
    public partial class RequestsView : Window
    {
        private readonly RequestsViewModel _viewModel;

        public RequestsView(RequestsViewModel vm)
        {
            InitializeComponent();
            _viewModel = vm;
            DataContext = vm;
        }

        public void Initialize(User user)
        {
            _viewModel.Initialize(user);
        }
    }
}
