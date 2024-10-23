using KoiPondConstruct.Service;
using KoiPondConstruct.Service.DTOs;
using System.Collections.Generic;
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

        public MainWindow(ICustomerRequestService customerRequestService)
        {
            InitializeComponent();
            _customerRequestService = customerRequestService;
            LoadData();
        }

        private async void LoadData()
        {
            var allRequestDetails = (await _customerRequestService.GetAllCustomerRequestDetailsAsync()).Data as IList<GetCustRequestDetailListDTOResponse>;

            CustomerRequestDataGrid.ItemsSource = allRequestDetails;
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

        #region handle view create
        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected request
            var selectedRequest = (GetCustRequestDetailListDTOResponse)((Button)sender).DataContext;

            // Create the detail window and pass the request ID
            var detailWindow = new DetailWindow(_customerRequestService, selectedRequest.Id);

            // Show the detail window as a modal dialog
            detailWindow.ShowDialog();
        }

        #endregion

    }
}
