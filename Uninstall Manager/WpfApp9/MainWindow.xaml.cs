using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Interop;
using System.Diagnostics;
using System.IO;
using System.ComponentModel;

namespace WpfApp9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    class App_add
    {
        public ImageSource ImageData { get; set; }
        public string Title { get; set; }
        public string Provider { get; set; }
    }
    class temp_item_add
    {
        public string Path { get; set; }
        public string Type { get; set; }
    }
    public partial class MainWindow : Window
    {
        [DllImport("Kernel32.dll")]
        public extern static IntPtr LoadLibrary(string libName);
        [DllImport("User32.dll")]
        public extern static IntPtr LoadIcon(IntPtr libHandle, int lpIconName);
        IntPtr lib = LoadLibrary("shell32.dll");
        public MainWindow()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);
        }
        private readonly BackgroundWorker get_elements = new BackgroundWorker();
        List<string> Uninstall_paths = new List<string>();//Collection with Uninstallers paths
        int apps_count = 0;        
        private void get_elements_DoWork(object sender, DoWorkEventArgs e)
        {            
            void get_apps(RegistryKey OpenBaseKey)
            {
                string[] programs = OpenBaseKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall").GetSubKeyNames();
                foreach (string app in programs)
                {
                    string unistallstring = (string)OpenBaseKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + app).GetValue("UninstallString");
                    if (unistallstring != null || unistallstring != "")
                    {
                        try
                        {
                            RegistryKey path = OpenBaseKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + app);
                            string displayname = (string)path.GetValue("DisplayName");
                            string InstallLocation = (string)path.GetValue("InstallLocation");
                            if (displayname == null || displayname == "")
                                displayname = "Unknown";
                            string publisher = (string)path.GetValue("Publisher");
                            if (publisher == null || publisher == "")
                                publisher = "Unknown";
                            string displayicon = (string)path.GetValue("DisplayIcon");
                            if (displayicon == null)
                            displayicon = @"C:\Windows\system32\ComputerDefaults.exe";
                            string icon_path = displayicon.Split(',')[0];
                            displayicon = displayicon.ToLower().Replace('"', ' ').Trim();
                            IntPtr data;
                            if (!unistallstring.ToLower().Contains("msiexec"))
                            {
                            data = System.Drawing.Icon.ExtractAssociatedIcon(icon_path).ToBitmap().GetHbitmap(); 
                            }
                            else
                            {
                                data = System.Drawing.Icon.ExtractAssociatedIcon(@"C:\Windows\system32\ComputerDefaults.exe").ToBitmap().GetHbitmap();
                            }                                              
                            App_add item = new App_add() { Title = displayname, Provider = publisher};                 
                            object[] item_info = new object[2];
                            item_info[0] = data;
                            item_info[1] = item;
                            get_elements.ReportProgress(0, item_info);
                            Uninstall_paths.Add(unistallstring);
                            apps_count++;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            get_apps(RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32));//Getting applications for x32 from HKLM
            get_apps(RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry32));//Getting applications for x32 from HKCU
            if (Environment.Is64BitOperatingSystem)
                get_apps(RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64));//Getting applications for x64 from HKLM
        }
        private void get_elements_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Add item to listView
            object[] item_info = e.UserState as object[];
            IntPtr data = (IntPtr)item_info[0];
            App_add item = (App_add)item_info[1];
            ImageSource image = Imaging.CreateBitmapSourceFromHBitmap(data, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            item.ImageData = image;
            listView.Items.Add(item);
            programs_count.Content = apps_count+" apps were found";
        }
    void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
       
        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            exit.Foreground = System.Windows.Media.Brushes.Gray;
            hide.Foreground = System.Windows.Media.Brushes.White;
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            exit.Foreground = System.Windows.Media.Brushes.White;
        }

        private void hide_MouseEnter(object sender, MouseEventArgs e)
        {
            exit.Foreground = System.Windows.Media.Brushes.White;
            hide.Foreground = System.Windows.Media.Brushes.Gray;
        }

        private void hide_MouseLeave(object sender, MouseEventArgs e)
        {
            hide.Foreground = System.Windows.Media.Brushes.White;
        }

        private void hide_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }        
        private void Form1_Loaded(object sender, RoutedEventArgs e)
        {
            get_elements.WorkerReportsProgress = true;
            get_elements.WorkerSupportsCancellation = true;
            get_elements.DoWork += get_elements_DoWork;
            get_elements.ProgressChanged += get_elements_ProgressChanged;
            get_elements.RunWorkerAsync();           
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {         
            listView_Copy.Visibility = Visibility.Visible;
            listView_Copy.Items.Clear();
            if (textBox.Text == "")
                listView_Copy.Visibility = Visibility.Hidden;
            else
            {
                for (int i = 0; i < listView.Items.Count; i++)
                {
                    App_add app = listView.Items[i] as App_add;
                    if (app.Title.ToLower().Contains(textBox.Text.ToLower()))
                    {
                        listView_Copy.Items.Add(app);
                    }
                }
            }
        }

        private void unistall_Click(object sender, RoutedEventArgs e)
        {            
            uninstall.IsEnabled = false;
            App_add selected_item = (App_add)listView.SelectedItem;
            MessageBoxResult res = MessageBox.Show("You are sure that you want to uninstall the program?", "Uninstall Manager", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                var process = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    WorkingDirectory = @"C:\Windows\System32",
                    FileName = @"C:\Windows\System32\cmd.exe",
                    Arguments = "/c " + '"' + Uninstall_paths[listView.SelectedIndex] + '"',
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(process).WaitForExit();                                           
                Uninstall_paths.RemoveAt(listView.SelectedIndex);
                listView.Items.RemoveAt(listView.SelectedIndex);
            }
            listView_Copy.Visibility = Visibility.Hidden;
            textBox.Text = "";
        }

        private void listView_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                listView.SelectedItem=listView_Copy.SelectedItem;
                if (listView_Copy.SelectedItem == null)
                uninstall.IsEnabled = false;
                else
                uninstall.IsEnabled = true;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedItem == null)
                uninstall.IsEnabled = false;
            else
                uninstall.IsEnabled = true;
        }

    }
    public class IconExtractor
    {

        public static Icon Extract(string file, int number, bool largeIcon)
        {
            IntPtr large;
            IntPtr small;
            ExtractIconEx(file, number, out large, out small, 1);
            try
            {
                return System.Drawing.Icon.FromHandle(largeIcon ? large : small);
            }
            catch
            {
                return null;
            }

        }
        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

    }
}
