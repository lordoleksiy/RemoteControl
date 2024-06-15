using RemoteControlDesktop.ViewModel;
using System;
using System.Windows;


namespace RemoteControlDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIconViewModel notifyIconVM;
        public MainWindow(MainViewModel viewModel, NotifyIconViewModel notifyIconViewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            notifyIconVM = notifyIconViewModel;
            viewModel.HideEvent += ToggleMainWindowVisibility;
            notifyIconViewModel.AddToQueryMethod(ToggleMainWindowVisibility);
        }

        private void ToggleMainWindowVisibility(object? sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
                Show();
                Activate();
            }
            else
            {
                WindowState = WindowState.Minimized;
                Hide();
            }
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            notifyIconVM.Dispose();
        }
    }
}
