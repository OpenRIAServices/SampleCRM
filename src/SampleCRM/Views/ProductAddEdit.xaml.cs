﻿using OpenRiaServices.Client;
using OpenSilver.Controls;
using System;
using System.Windows;

namespace SampleCRM.Web.Views
{
    public partial class ProductAddEdit : BaseUserControl
    {
        public event EventHandler ProductDeleted;
        public event EventHandler ProductAdded;

        private Models.Products _prodcutViewModel = new Models.Products();
        public Models.Products ProductViewModel
        {
            get { return _prodcutViewModel; }
            set
            {
                if (_prodcutViewModel != value)
                {
                    _prodcutViewModel = value;
                    OnPropertyChanged();
#if DEBUG
                    Console.WriteLine($"ProductAddEdit, ProductViewModel: {value.ProductID} {value.Name} selected");
#endif
                }
            }
        }

        public ProductAddEdit()
        {
            InitializeComponent();
            DataContext = this;
        }

        public override void ArrangeLayout()
        {
            base.ArrangeLayout();
            grdNarrow.Visibility = BoolToVisibilityConverter.Convert(IsMobileUI);
            grdWide.Visibility = BoolToVisibilityConverter.Convert(!IsMobileUI);
        }

        private async void btnEditPicture_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
            "|PNG Portable Network Graphics (*.png)|*.png" +
            "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
            "|BMP Windows Bitmap (*.bmp)|*.bmp" +
            "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
            "|GIF Graphics Interchange Format (*.gif)|*.gif";

            var result = await fileDialog.ShowDialogAsync();
            if (!result.GetValueOrDefault())
                return;

            var imageFile = fileDialog.File;
            if (imageFile.Length < 1)
                return;

            using (var fileStream = imageFile.OpenRead())
            {
                byte[] buffer = new byte[fileStream.Length];
                await fileStream.ReadAsync(buffer, 0, buffer.Length);
                ProductViewModel.Picture = buffer;
            }
        }


        public void Save(ProductsContext context)
        {
            if (ProductViewModel.IsNew && context.Products.CanAdd || context.Products.CanEdit)
            {
                if (!formGeneral.CommitEdit())
                {
                    ErrorWindow.Show("Invalid General Info");
                    return;
                }

                if (!formPrice.CommitEdit())
                {
                    ErrorWindow.Show("Invalid Price Info");
                    return;
                }

                if (!formStock.CommitEdit())
                {
                    ErrorWindow.Show("Invalid Stock Info");
                    return;
                }

                if (!formDetails.CommitEdit())
                {
                    ErrorWindow.Show("Invalid Details Info");
                    return;
                }

                if (ProductViewModel.IsNew)
                {
                    context.Products.Add(ProductViewModel);
                }

                context.SubmitChanges(OnAddSubmitCompleted, null);
            }
            else
            {
                throw new AccessViolationException("RIA Service Add / Edit Entity for Product Context is denied");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var productContext = new ProductsContext();
            if (productContext.Products.CanRemove)
            {
                productContext.Products.Remove(ProductViewModel);
                productContext.SubmitChanges(OnDeleteSubmitCompleted, null);
            }
            else
            {
                throw new AccessViolationException("RIA Service Delete Entity for Customer Context is denied");
            }
        }

        private void OnDeleteSubmitCompleted(SubmitOperation so)
        {
            if (so.HasError)
            {
                MessageBox.Show(string.Format("Submit Failed: {0}", so.Error.Message));
                Console.WriteLine(string.Format("Submit Failed: {0}", so.Error.StackTrace));
                so.MarkErrorAsHandled();
            }
            else
            {
                if (ProductDeleted != null)
                    ProductDeleted(this, new EventArgs());
            }
        }

        private void OnAddSubmitCompleted(SubmitOperation so)
        {
            if (so.HasError)
            {
                if (so.Error.Message.StartsWith("Submit operation failed. Access to operation"))
                {
                    ErrorWindow.Show("Access Denied", "Insuficient User Role", so.Error.Message);
                }
                else
                {
                    ErrorWindow.Show("Access Denied", so.Error.Message, "");
                }
                so.MarkErrorAsHandled();
            }
            else
            {
                if (ProductAdded != null)
                    ProductAdded(this, new EventArgs());
            }
        }

        private void formGeneral_EditEnded(object sender, System.Windows.Controls.DataFormEditEndedEventArgs e)
        {

        }

        private void formPrice_EditEnded(object sender, System.Windows.Controls.DataFormEditEndedEventArgs e)
        {

        }

        private void formStock_EditEnded(object sender, System.Windows.Controls.DataFormEditEndedEventArgs e)
        {

        }

        private void formDetails_EditEnded(object sender, System.Windows.Controls.DataFormEditEndedEventArgs e)
        {

        }
    }
}
