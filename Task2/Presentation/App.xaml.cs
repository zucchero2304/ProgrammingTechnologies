using Presentation.ViewModels;
using Presentation.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IDialogService dialogService = new DialogService(MainWindow);

            dialogService.Register<ProductInfoViewModel, ProductInfoView>();

            var viewModel = new ProductViewModel(dialogService);//dependency injection
            var view = new MainWindow { DataContext = viewModel };

            view.ShowDialog();
        }
    }  
}
