using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MCLaunch
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        private int max;
        int flags;
        public int Max { get => max; set => max = value; }
        public Setting(int flag,int maxsize = 2048)
        {
            flags = flag;
            Max = maxsize;
        }
        public Setting()
        {
            InitializeComponent();
        }
        //窗口模板
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
        //自动获取java路径
        public string Javapath()
        {
            string javapath = "";
            try//判断异常
            {
                String[] js = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment").GetSubKeyNames();//读取jre路径。
                                                                                                                               //如果探测无异常才会执行下面的代码
                javapath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment\" + js[0]).GetValue("JavaHome") + "\\bin\\javaw.exe";
            }
            catch (Exception jreex)//如果出现异常(如未安装jre而安装了jdk)
            {
                if (jreex.Message.Contains("未将对象引用设置到对象的实例"))//读取异常
                {
                    try//继续判断异常
                    {
                        String[] js = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Development Kit").GetSubKeyNames();//读取jdk路径。
                        javapath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Development Kit\" + js[0]).GetValue("JavaHome") + "\\jre\\bin\\javaw.exe";
                    }
                    catch (Exception jdkex)//如果出现异常
                    {
                        MessageBox.Show("未安装Java而导致的:\n\r" + jdkex.Message, "异常");
                    }
                }
            }
            return javapath;
        }
        //手动获取java路径
        private void openfile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "JAVA主程序|*.exe";
            fdlg.RestoreDirectory = false;
            if ((bool)fdlg.ShowDialog())
            {
                evriment.Text = System.IO.Path.GetFullPath(fdlg.FileName);
            }
        }
        //保存
        private void savedata(object sender, RoutedEventArgs e)
        {
            if (!maxsize.Text.Equals(""))
            {
                Max = Convert.ToInt32(maxsize.Text);
            }
            
            //窗口设置
            setwindows(box_x.Text, box_y.Text);
            if (allscreen.IsChecked == true)
            {
                
            }
            if (allos.IsChecked == true)
            {

            }
            if (natives.IsChecked == true)
            {

            }
            if (savelaunch.IsChecked == true)
            {

            }
        }
        private void setwindows(string width,string height)
        {
            Options options = new Options(width, height);
            options.set_windows_size();
        }
        //取消
        private void close_setting(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch { }
        }

     
    }

}
