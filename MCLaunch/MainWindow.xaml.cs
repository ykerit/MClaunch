using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using MCLaunch.Model;
using System.Collections.Generic;
using System.Windows.Navigation;
using System;

namespace MCLaunch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string names;
        Setting settings;
        //属性keyi 
        public string Names { get => names; set => names = value; }
        public Setting Settings { get => settings; set => settings = value; }

        public MainWindow()
        {
            InitializeComponent();
            Window_Loadedcombobox();
            Settings = new Setting(1);
        }
        /*窗口定制
         * 
         */
        private void Window_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private void close_window(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch { }
        }

        private void subs_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        
        //版本选择
        private void Window_Loadedcombobox()
        {
            Version version = new Version();
            Dictionary<int, string> list = new Dictionary<int, string>()
            {
                {1,version._Version},

            };
            comb.ItemsSource = list;
            comb.SelectedValuePath = "key";
            comb.DisplayMemberPath = "Value";
        }
        //开始游戏
        private void play_game(object sender, RoutedEventArgs e)
        {
            if (Names!=null)
            {
                Joint joint = new Joint(128, Settings.Max, Names);
                string cmd = joint.joint(joint.Path_jar).ToString();
                Process pro = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(@"C:\Program Files\Java\jre1.8.0_171\bin\javaw.exe", cmd);
                psi.UseShellExecute = false;
                pro.StartInfo = psi;
                pro.Start();
            }
            else
            {
                MessageBox.Show("名称不能为空");
            }
            
        }
        //跳转setting窗口
        private void link_setting(object sender, RoutedEventArgs e)
        {
            Setting wm = new Setting();
            wm.Show();
            wm.WindowState = WindowState.Normal;
        }
        //获取名称
        private void endchanged(object sender, RoutedEventArgs e)
        {
            Names = name.Text;
        }
    }
}
