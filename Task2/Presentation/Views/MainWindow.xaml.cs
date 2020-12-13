﻿using Presentation.ViewModels;
using System.Windows;

namespace Presentation.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //only time we write something in "code behind" is data binding
            DataContext = new ProductViewModel();
        }
    }
}
