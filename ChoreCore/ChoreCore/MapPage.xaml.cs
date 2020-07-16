﻿using ChoreCore.ViewModels;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreCore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private IMapViewModel viewModel;

        public MapPage()
        {
            InitializeComponent();

            viewModel = (IMapViewModel)Locator.Current.GetService(typeof(IMapViewModel));
            BindingContext = viewModel;
        }
    }
}