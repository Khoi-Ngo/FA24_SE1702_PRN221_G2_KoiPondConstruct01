using KoiPondConstruct.Service;
using KoiPondConstruct.Service.DTOs;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KoiPondConstruct.WPFApplication
{
    public partial class MainWindow : Window
    {

        #region init + load data
        private readonly ICustomerRequestService _customerRequestService;
        private IList<GetCustRequestDetailListDTOResponse> _customerRequestData;

        public MainWindow(ICustomerRequestService customerRequestService)
        {
            InitializeComponent();
            _customerRequestService = customerRequestService;
            LoadData();
        }

        private async void LoadData()
        {
            _customerRequestData = (await _customerRequestService.GetAllCustomerRequestDetailsAsync()).Data as IList<GetCustRequestDetailListDTOResponse>;
            CustomerRequestDataGrid.ItemsSource = _customerRequestData;
        }
        #endregion

        #region Sorting
        private void CustomerRequestDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            var column = e.Column;
            var direction = (column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
            var sortedData = _customerRequestData.OrderBy(c => c.GetType().GetProperty(column.SortMemberPath).GetValue(c)).ToList();
            if (direction == ListSortDirection.Descending)
            {
                sortedData.Reverse();
            }
            CustomerRequestDataGrid.ItemsSource = sortedData;
            column.SortDirection = direction;
        }
        #endregion

        #region Search Methods
        private void FirstNameSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }

        private void LastNameSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }

        private void PhoneSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }

        private void FilterData()
        {
            var filteredData = _customerRequestData.Where(c =>
                (string.IsNullOrEmpty(FirstNameSearchBox.Text) || c.HomeownerFirstName.Contains(FirstNameSearchBox.Text, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(LastNameSearchBox.Text) || c.HomeownerLastName.Contains(LastNameSearchBox.Text, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(PhoneSearchBox.Text) || c.HomeownerPhone.Contains(PhoneSearchBox.Text, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            CustomerRequestDataGrid.ItemsSource = filteredData;
        }

        #endregion


        #region handle update
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = (GetCustRequestDetailListDTOResponse)((Button)sender).DataContext;
            EditWindow editWindow = new EditWindow(selectedRequest.Id, _customerRequestService);
            editWindow.Closed += (s, args) => LoadData(); // Reload data when EditWindow is closed
            editWindow.ShowDialog();
        }
        #endregion

        #region handle delete
        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = (GetCustRequestDetailListDTOResponse)((Button)sender).DataContext;

            // Show confirmation dialog
            var result = MessageBox.Show($"Are you sure you want to delete Request ID: {selectedRequest.Id}?",
                                          "Confirm Delete",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Warning);

            // Check the user's response
            if (result == MessageBoxResult.Yes)
            {
                // Call the delete method
                await _customerRequestService.DeleteCustomerRequestDetailByIdAsync(selectedRequest.Id);

                // Reload the data
                LoadData();
            }
            // If 'No' is clicked, do nothing and simply return
        }

        #endregion

        #region handle create
        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateCustomerRequestDetailWindow(_customerRequestService);
            createWindow.Closed += (s, args) => LoadData(); // Reload data when the create window is closed
            createWindow.ShowDialog(); // This will open the window as a modal dialog
        }

        #endregion

        #region handle view detail
        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = (GetCustRequestDetailListDTOResponse)((Button)sender).DataContext;
            var detailWindow = new DetailWindow(_customerRequestService, selectedRequest.Id);
            detailWindow.ShowDialog();
        }

        #endregion



    }
}
