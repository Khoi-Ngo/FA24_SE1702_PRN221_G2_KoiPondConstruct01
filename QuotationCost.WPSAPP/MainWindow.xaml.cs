using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KoiPondConstruction.Common;
using KoiPondConstruction.Data.DBContext;
using KoiPondConstruction.Data.Models;
using KoipondContruction.Service;
using Microsoft.EntityFrameworkCore;

namespace QuotationCost.WPSAPP
{

    public partial class MainWindow : Window
    {
        private readonly TblQuotationCostSevice _quotationCostService;
        private TblQuotationCost _selectedQuotationCost;

        

        public MainWindow()
        {
            InitializeComponent();
            _quotationCostService = new TblQuotationCostSevice();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData(); 
        }

        // Load dữ liệu vào ListView
        private async Task LoadData()
        {
            var result = await _quotationCostService.GetAll();

            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                lvQuotationCost.ItemsSource = result.Data as List<TblQuotationCost>;
            }
            else
            {
                MessageBox.Show(result.Message); // Hiển thị thông báo nếu không có dữ liệu
            }
        }

        // Lưu hoặc cập nhật thông tin
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var quotation = new TblQuotationCost
            {
                Id = _selectedQuotationCost?.Id ?? 0,
                Total = long.Parse(txtTotal.Text),
                Currency = txtCurrency.Text,
                ContentText = txtContentText.Text,
                CreatedBy = txtCreatedBy.Text,
                ApprovedBy = txtApprovedBy.Text,
                File = txtFile.Text,
                Note = txtNote.Text,
                IsDeleted = cbIsDeleted.IsChecked ?? false
            };

            //if (_selectedQuotationCost != null)
            //{
            //    // Cập nhật nếu đã chọn bản ghi
            //    quotation.Id = _selectedQuotationCost.Id;
            //    _quotationCostService.Save(quotation);
            //}
            var result = _quotationCostService.Save(quotation);

            MessageBox.Show("Data saved successfully.");
            LoadData();
            ResetFields();
        }

        // Xóa bản ghi đã chọn
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedQuotationCost != null)
            {
                _quotationCostService.DeleteById(_selectedQuotationCost.Id);
                MessageBox.Show("Data deleted successfully.");
                LoadData();
                ResetFields();
            }
            else
            {
                MessageBox.Show("Please select a quotation to delete.");
            }
        }

        // Reset các trường nhập liệu
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }

        // Reset tất cả các field
        private void ResetFields()
        {
            txtTotal.Clear();
            txtCurrency.Clear();
            txtContentText.Clear();
            txtCreatedBy.Clear();
            txtApprovedBy.Clear();
            txtFile.Clear();
            txtNote.Clear();
            cbIsDeleted.IsChecked = false;
            _selectedQuotationCost = null; // Bỏ chọn bản ghi đang chọn
        }

        // Tìm kiếm bản ghi dựa trên các tiêu chí
        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string createdBy = txtSearchCreatedBy.Text.Trim();
            string approvedBy = txtSearchApprovedBy.Text.Trim();
            string contentText = txtSearchContentText.Text.Trim();

            var results = await _quotationCostService.SearchQuotations(createdBy, approvedBy, contentText);
            if (results.Status == Const.SUCCESS_READ_CODE)
            {
                lvQuotationCost.ItemsSource = results.Data as List<TblQuotationCost>;
            }
            else
            {
                MessageBox.Show(results.Message);
            }
        }

        // Lấy thông tin chi tiết khi nhấp đúp vào ListView để chỉnh sửa
        private void lvQuotationCost_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lvQuotationCost.SelectedItem is TblQuotationCost quotation)
            {
                // Chọn item và hiển thị lên UI
                _selectedQuotationCost = quotation;
                txtTotal.Text = quotation.Total.ToString();
                txtCurrency.Text = quotation.Currency;
                txtContentText.Text = quotation.ContentText;
                txtCreatedBy.Text = quotation.CreatedBy;
                txtApprovedBy.Text = quotation.ApprovedBy;
                txtFile.Text = quotation.File;
                txtNote.Text = quotation.Note;
                cbIsDeleted.IsChecked = quotation.IsDeleted;
            }
        }
    }

}