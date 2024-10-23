using System.Windows;
using KoiPondConstruct.Common;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Service;
using KoiPondConstruct.Service.DTOs; // Make sure this namespace is correct

namespace KoiPondConstruct.WPFApplication
{
    public partial class CreateCustomerRequestDetailWindow : Window
    {
        private readonly ICustomerRequestService _customerRequestService;

        public CreateCustomerRequestDetailWindow(ICustomerRequestService customerRequestService)
        {
            InitializeComponent();
            _customerRequestService = customerRequestService;
            LoadRequestIds();
        }

        private async void LoadRequestIds()
        {
            var serviceRes = await _customerRequestService.GetCustomerRequestsAsync();
            List<CustomerRequestListDTO> requestList = (List<CustomerRequestListDTO>)serviceRes.Data;

            // Set the ItemsSource and SelectedValuePath for the ComboBox
            RequestIdComboBox.ItemsSource = requestList;
            RequestIdComboBox.SelectedValuePath = "Id"; // Use this to bind the selected value
        }


        private void ResetFields_Click(object sender, RoutedEventArgs e)
        {
            HomeownerFirstNameTextBox.Clear();
            HomeownerLastNameTextBox.Clear();
            HomeownerPhoneTextBox.Clear();
            HomeownerDateOfBirthPicker.SelectedDate = null;
            HeightTextBox.Clear();
            WidthTextBox.Clear();
            LengthTextBox.Clear();
            ShapeTextBox.Clear();
            BudgetTextBox.Clear();
            TypeTextBox.Clear();
            AddressTextBox.Clear();
            NoteTextBox.Clear();
            RequestIdComboBox.SelectedIndex = -1;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create the customer request detail object from the fields
                var requestDetail = new TblCustomerRequestDetail
                {
                    // Ensure RequestId is cast to long
                    RequestId = (long)RequestIdComboBox.SelectedValue,

                    HomeownerFirstName = HomeownerFirstNameTextBox.Text,
                    HomeownerLastName = HomeownerLastNameTextBox.Text,
                    HomeownerPhone = HomeownerPhoneTextBox.Text,

                    // Convert the DateTime to DateOnly, throw an exception if no date is selected
                    HomeownerDateOfBirth = HomeownerDateOfBirthPicker.SelectedDate.HasValue
                        ? DateOnly.FromDateTime(HomeownerDateOfBirthPicker.SelectedDate.Value)
                        : throw new InvalidOperationException("Date of birth must be selected"),

                    // Parse height, width, and length to long; default to 0 if parsing fails
                    Height = long.TryParse(HeightTextBox.Text, out var height) ? height : 0,
                    Width = long.TryParse(WidthTextBox.Text, out var width) ? width : 0,
                    Length = long.TryParse(LengthTextBox.Text, out var length) ? length : 0,
                    Shape = ShapeTextBox.Text,

                    // Parse budget to long; default to 0 if parsing fails
                    Budget = long.TryParse(BudgetTextBox.Text, out var budget) ? budget : 0,

                    Type = TypeTextBox.Text,
                    Address = AddressTextBox.Text,
                    Note = NoteTextBox.Text
                };

                // Call the service to create the customer request detail
                var result = await _customerRequestService.CreateCustomRequestDetailAsync(requestDetail);

                // Check the result status and provide feedback to the user
                if (result.Status == Const.SUCCESS_CREATE_CODE)
                {
                    MessageBox.Show("Customer request detail created successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("An error occurred while creating the request.");
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
