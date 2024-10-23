using KoiPondConstruct.Common;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Service;
using KoiPondConstruction.Service.Base;
using System;
using System.Windows;
using System.Windows.Controls;

namespace KoiPondConstruct.WPFApplication
{
    public partial class EditWindow : Window
    {
        private readonly ICustomerRequestService _customerRequestService;
        private TblCustomerRequestDetail _detail;

        public EditWindow(long Id, ICustomerRequestService customerRequestService)
        {
            InitializeComponent();
            _customerRequestService = customerRequestService;
            LoadData(Id);
        }

        private async void LoadData(long Id)
        {
            ServiceResult serviceResult = await _customerRequestService.GetRequestDetailDetailByIdAsync(Id);
            if (serviceResult != null && serviceResult.Status == Const.SUCCESS_READ_CODE)
            {
                _detail = serviceResult.Data as TblCustomerRequestDetail;

                // Populate fields
                IdTextBlock.Text = _detail.Id.ToString(); // Non-editable
                HomeownerFirstNameTextBox.Text = _detail.HomeownerFirstName;
                HomeownerLastNameTextBox.Text = _detail.HomeownerLastName;
                HomeownerPhoneTextBox.Text = _detail.HomeownerPhone;
                // Convert DateOnly to DateTime for DatePicker
                HomeownerDateOfBirthPicker.SelectedDate = _detail.HomeownerDateOfBirth.ToDateTime(new TimeOnly(0, 0)); // midnight
                HeightTextBox.Text = _detail.Height.ToString();
                WidthTextBox.Text = _detail.Width.ToString();
                LengthTextBox.Text = _detail.Length.ToString();
                ShapeTextBox.Text = _detail.Shape;
                BudgetTextBox.Text = _detail.Budget.ToString();
                AddressTextBox.Text = _detail.Address;
                NoteTextBox.Text = _detail.Note;
            }
        }

        private async void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Update the detail object with the new values
            _detail.HomeownerFirstName = HomeownerFirstNameTextBox.Text;
            _detail.HomeownerLastName = HomeownerLastNameTextBox.Text;
            _detail.HomeownerPhone = HomeownerPhoneTextBox.Text;

            // Convert the DatePicker value back to DateOnly
            if (HomeownerDateOfBirthPicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = HomeownerDateOfBirthPicker.SelectedDate.Value;
                _detail.HomeownerDateOfBirth = DateOnly.FromDateTime(selectedDate);
            }

            _detail.Height = long.TryParse(HeightTextBox.Text, out var height) ? height : _detail.Height;
            _detail.Width = long.TryParse(WidthTextBox.Text, out var width) ? width : _detail.Width;
            _detail.Length = long.TryParse(LengthTextBox.Text, out var length) ? length : _detail.Length;
            _detail.Shape = ShapeTextBox.Text;
            _detail.Budget = long.TryParse(BudgetTextBox.Text, out var budget) ? budget : _detail.Budget;
            _detail.Address = AddressTextBox.Text;
            _detail.Note = NoteTextBox.Text;

            // Call the service to save changes
            ServiceResult serviceResult = await _customerRequestService.UpdateCustomerDetailAsync(_detail);
            if (serviceResult != null && serviceResult.Status == Const.SUCCESS_UPDATE_CODE)
            {
                MessageBox.Show("Changes saved successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error saving changes.");
            }
        }



        private void ResetFields_Click(object sender, RoutedEventArgs e)
        {
            // Reload the data to reset the fields
            LoadData(_detail.Id);
        }

        private void CancelWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
