using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace MCLaunch.View
{
    /// <summary>
    /// usericon.xaml 的交互逻辑
    /// </summary>
    public partial class usericon : UserControl
    {
        private object evriment;

        public usericon()
        {
            InitializeComponent();
            /*using (BinaryReader loader = new BinaryReader(File.Open(@".\image\icon1.gif", FileMode.Open)))
            {
                FileInfo fd = new FileInfo(@"/image/icon1.gif");
                int Length = (int)fd.Length;
                byte[] buf = new byte[Length];
                buf = loader.ReadBytes((int)fd.Length);
                loader.Dispose();
                loader.Close();
                //开始加载图像
                BitmapImage bim = new BitmapImage();
                bim.BeginInit();
                bim.StreamSource = new MemoryStream(buf);
                bim.EndInit();
                icon.Source = bim;
                GC.Collect(); //强制回收资源
            }*/

        }
        private void openimage()
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "JAVA主程序|*.exe";
            fdlg.RestoreDirectory = false;
            if ((bool)fdlg.ShowDialog())
            {
                 System.IO.Path.GetFullPath(fdlg.FileName);
            }
        }

    }
}
