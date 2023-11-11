﻿using System;
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
using WordsCup.Pages.Basic;


namespace WordsCup
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void TextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegPage rg = new RegPage();
            rg.Left = this.Left;
            rg.Top = this.Top;
            rg.Width = this.ActualWidth;
            rg.Height = this.ActualHeight;
            rg.WindowState = this.WindowState;
            rg.Show();
            this.Close();
        }
    }
}
