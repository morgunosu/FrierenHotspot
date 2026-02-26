using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfButton = System.Windows.Controls.Button;
using WinForms = System.Windows.Forms;

namespace FrierenHotspot;

public partial class MainWindow : Window
{
    private WinForms.NotifyIcon? _trayIcon;

    public MainWindow()
    {
        InitializeComponent();
        if (string.IsNullOrEmpty(Thread.CurrentThread.CurrentUICulture.Name))
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
        
        SetupTrayIcon(); 
        UpdateUiTexts(); 
        LoadNetworkAdapters();
        LogToTerminal(Properties.Resources.StatusReady);
    }

    private void SetupTrayIcon()
    {
        _trayIcon = new WinForms.NotifyIcon();
        try { _trayIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty); }
        catch { _trayIcon.Icon = System.Drawing.SystemIcons.Application; }
        _trayIcon.Text = Properties.Resources.AppTitle;
        _trayIcon.Visible = true;
        _trayIcon.DoubleClick += (s, e) => { Show(); WindowState = WindowState.Normal; };

        var contextMenu = new WinForms.ContextMenuStrip();
        contextMenu.Items.Add(Properties.Resources.IsRu ? "Развернуть" : "Restore", null, (s, e) => { Show(); WindowState = WindowState.Normal; });
        contextMenu.Items.Add(Properties.Resources.IsRu ? "Выход" : "Exit", null, (s, e) => System.Windows.Application.Current.Shutdown());
        _trayIcon.ContextMenuStrip = contextMenu;
    }

    private void BtnMinimizeTray_Click(object sender, RoutedEventArgs e) => Hide();
    protected override void OnClosed(EventArgs e) { _trayIcon?.Dispose(); base.OnClosed(e); }

    private void UpdateUiTexts()
    {
        LblMainTitle.Text = Properties.Resources.AppTitle;
        LblSsid.Text = Properties.Resources.LabelSsid;
        LblPassword.Text = Properties.Resources.LabelPassword;
        LblAdapter.Text = Properties.Resources.LabelAdapter;
        BtnStart.Content = Properties.Resources.BtnStart;
        BtnStop.Content = Properties.Resources.BtnStop;
        BtnShare.Content = Properties.Resources.BtnShare;
        LblTerminal.Text = Properties.Resources.TerminalTitle;
        TipSsid.ToolTip = Properties.Resources.TipSsid;
        TipPassword.ToolTip = Properties.Resources.TipPassword;
        TipAdapter.ToolTip = Properties.Resources.TipAdapter;

        if (_trayIcon?.ContextMenuStrip != null)
        {
            _trayIcon.ContextMenuStrip.Items[0].Text = Properties.Resources.IsRu ? "Развернуть" : "Restore";
            _trayIcon.ContextMenuStrip.Items[1].Text = Properties.Resources.IsRu ? "Выход" : "Exit";
        }
    }

    private void BtnLang_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not WpfButton btn || btn.Tag is not string langCode || Thread.CurrentThread.CurrentUICulture.Name == langCode) return;
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(langCode);
        UpdateUiTexts();
        LogToTerminal($"> Language changed to: {langCode.ToUpper()}");
    }

    private void LoadNetworkAdapters()
    {
        var adapters = NetworkInterface.GetAllNetworkInterfaces()
            .Where(ni => (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet) 
                         && ni.OperationalStatus == OperationalStatus.Up 
                         && !ni.Description.Contains("Virtual") && !ni.Description.Contains("Loopback"))
            .Select(ni => new { Name = $"{ni.Name} ({ni.Description})", Id = ni.Id })
            .ToList();
        
        CmbAdapters.ItemsSource = adapters;
        if (adapters.Any()) CmbAdapters.SelectedIndex = 0;
    }

    private void BtnShare_Click(object sender, RoutedEventArgs e) => new InstructionWindow { Owner = this }.ShowDialog();

    private async void BtnStart_Click(object sender, RoutedEventArgs e)
    {
        if (PboxPassword.Password.Length < 8)
        {
            LogToTerminal("ERROR: Password must be at least 8 chars.");
            return;
        }

        LogToTerminal("> Configuring network...");
        await RunCommandAsync($"netsh wlan set hostednetwork mode=allow ssid=\"{TxtSsid.Text}\" key=\"{PboxPassword.Password}\"");
        LogToTerminal("> Starting broadcast...");
        await RunCommandAsync("netsh wlan start hostednetwork");

        LogToTerminal("------------------------------------------------");
        LogToTerminal(Properties.Resources.IsRu ? "> ВАЖНО: Сейчас откроется окно настроек." : "> IMPORTANT: Network settings will open.");
        LogToTerminal(Properties.Resources.IsRu ? "> Дайте доступ интернета на новую сеть!" : "> Share internet to the new network!");
        LogToTerminal("------------------------------------------------");

        try
        {
            await Task.Delay(1000); 
            Process.Start(new ProcessStartInfo("ncpa.cpl") { UseShellExecute = true });
        }
        catch (Exception ex) { LogToTerminal($"ERR: {ex.Message}"); }
    }

    private async void BtnStop_Click(object sender, RoutedEventArgs e)
    {
        LogToTerminal("> Stopping network...");
        await RunCommandAsync("netsh wlan stop hostednetwork");
        LogToTerminal("> Network stopped.");
    }

    private async Task RunCommandAsync(string command)
    {
        try
        {
            var processInfo = new ProcessStartInfo("cmd.exe", $"/c chcp 65001 >nul & {command}")
            {
                CreateNoWindow = true, UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8, StandardErrorEncoding = Encoding.UTF8
            };

            using var process = new Process { StartInfo = processInfo };
            process.OutputDataReceived += (s, args) => { if (!string.IsNullOrWhiteSpace(args.Data)) LogToTerminal(args.Data); };
            process.ErrorDataReceived += (s, args) => { if (!string.IsNullOrWhiteSpace(args.Data)) LogToTerminal("ERR: " + args.Data); };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            await process.WaitForExitAsync();
        }
        catch (Exception ex) { LogToTerminal($"FATAL ERROR: {ex.Message}"); }
    }

    private void LogToTerminal(string? message)
    {
        if (string.IsNullOrEmpty(message)) return;
        Dispatcher.Invoke(() => { TerminalOutput.Text += $"{message}\n"; TerminalScroll.ScrollToBottom(); });
    }

    private void TglShowPassword_Click(object sender, RoutedEventArgs e)
    {
        if (TglShowPassword.IsChecked == true)
        {
            TxtPasswordVisible.Text = PboxPassword.Password;
            TxtPasswordVisible.Visibility = Visibility.Visible;
            PboxPassword.Visibility = Visibility.Collapsed;
            TglShowPassword.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4081"));
        }
        else
        {
            PboxPassword.Password = TxtPasswordVisible.Text;
            PboxPassword.Visibility = Visibility.Visible;
            TxtPasswordVisible.Visibility = Visibility.Collapsed;
            TglShowPassword.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#4A4A55"));
        }
    }

    private void PboxPassword_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (PboxPassword.Visibility == Visibility.Visible) TxtPasswordVisible.Text = PboxPassword.Password;
    }

    private void TxtPasswordVisible_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
         if (TxtPasswordVisible.Visibility == Visibility.Visible) PboxPassword.Password = TxtPasswordVisible.Text;
    }

    private void DragWindow_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left) DragMove();
    }

    private void BtnClose_Click(object sender, RoutedEventArgs e) => System.Windows.Application.Current.Shutdown();
}