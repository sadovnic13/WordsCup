﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WordsCup
{
    /// <summary>
    /// Логика взаимодействия для WordSelectPage.xaml
    /// </summary>
    public partial class WordSelectPage : Window
    {
        public WordSelectPage()
        {
            InitializeComponent();
        }

        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            BlurEffect bE = new BlurEffect();
            bE.Radius = 5;
            Effect = bE;

            DownloadAnimation dialog = new DownloadAnimation();

            dialog.Owner = this;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            this.IsEnabled = false;
            dialog.Show();

            SearchPage sP = await SearchPage.CreateAsync();

            sP.Show();
            this.Close();
        }
    }
}
