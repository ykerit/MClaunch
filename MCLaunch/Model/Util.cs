using MCLaunch.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCLaunch
{
    class Util
    {
        
    }
    //获取版本
    class Version
    {
        private string _version;
        public Version()
        {
            Joint joint = new Joint();
            _Version = joint.ID;
        }

        public string _Version { get => _version; set => _version = value; }
    }
    //设置工具
    class Options
    {
        private string folder;
        private string dirpath;
        private string width;
        private string height;
        public Options(string wid="0",string hei="0")
        {
            Width = wid;
            Height = hei;
            Folder = get_folder();
        }

        public string Folder { get => folder; set => folder = value; }
        public string Dirpath { get => dirpath; set => dirpath = value; }
        public string Width { get => width; set => width = value; }
        public string Height { get => height; set => height = value; }

        public string get_folder()
        {
            string dirpath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string folder = dirpath + "\\.minecraft\\options.txt";
            return folder;
        }
        public void set_windows_size()
        {
            try
            {
               
                // using 语句关闭 StreamWriter
                using (StreamWriter ws = new StreamWriter(Folder))
                {
                    StreamReader rs = new StreamReader(@".\Model\options.txt");
                    ws.WriteLine(rs.ReadToEnd());
                    ws.WriteLine("overrideWidth:" + Width);
                    ws.WriteLine("overrideHeight:" + Height);
                    rs.Close();
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
