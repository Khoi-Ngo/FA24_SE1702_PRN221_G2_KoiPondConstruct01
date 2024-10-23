using KoiPondConstruct.Common;
using KoiPondConstruct.Data.Entities;
using KoiPondConstruct.Service;
using KoiPondConstruction.Service.Base;
using System.Threading.Tasks;
using System.Windows;

namespace KoiPondConstruct.WPFApplication
{
    public partial class DetailWindow : Window
    {
        private readonly ICustomerRequestService _customerRequestService;

        public DetailWindow(ICustomerRequestService customerRequestService, long id)
        {
            InitializeComponent();
            _customerRequestService = customerRequestService;
            loadData(id);
        }

        private async void loadData(long id)
        {
            ServiceResult serviceRes = await _customerRequestService.GetRequestDetailDetailByIdAsync(id);
            if (serviceRes != null && serviceRes.Status == Const.SUCCESS_READ_CODE)
            {
                TblCustomerRequestDetail detail = serviceRes.Data as TblCustomerRequestDetail;

                if (detail != null)
                {
                    TxtId.Text = detail.Id.ToString();
                    TxtRequestId.Text = detail.RequestId.ToString();
                    TxtSampleDesignId.Text = detail.SampleDesignId.ToString();
                    TxtHomeownerName.Text = $"{detail.HomeownerFirstName} {detail.HomeownerLastName}";
                    TxtHomeownerPhone.Text = detail.HomeownerPhone;
                    TxtHomeownerDateOfBirth.Text = detail.HomeownerDateOfBirth.ToString("MM/dd/yyyy");
                    TxtHeight.Text = detail.Height.ToString();
                    TxtWidth.Text = detail.Width.ToString();
                    TxtLength.Text = detail.Length.ToString();
                    TxtShape.Text = detail.Shape;
                    TxtBudget.Text = detail.Budget.ToString();
                    TxtType.Text = detail.Type;
                    TxtAddress.Text = detail.Address;
                    TxtNote.Text = detail.Note;
                    TxtIsDeleted.Text = detail.IsDeleted ? "Yes" : "No";
                }
                else
                {
                    MessageBox.Show("Details could not be loaded.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
