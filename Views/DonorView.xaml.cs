﻿using charity_management_system.DataStores;
using charity_management_system.Models;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace charity_management_system.Views
{
    /// <summary>
    /// Interaction logic for DonorView.xaml
    /// </summary>
    public partial class DonorView : UserControl
    {
        public DonorView()
        {
            InitializeComponent();
        }
        private void addDonor(object sender, RoutedEventArgs e)
        {

            DonorDataStore.instance.data.Add(new Donor() { name = nameTextBox.Text });
        }
    }
}
