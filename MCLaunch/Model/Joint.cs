using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCLaunch.Model
{
    /*该类用于处理json数据
     * 解析
     * 拼接
     */
    class Joint
    {

        private RootObject root;
        private int xmin;
        private int xmax;
        private string name;
        private string id;
        private string assetIndex;
        //构造
        public Joint()
        {
            root = JsonConvert.DeserializeObject<RootObject>(get_json());
            ID = root.id;
        }
        public Joint(int min, int max, string name1)
        {
            root = JsonConvert.DeserializeObject<RootObject>(get_json());
            Path_jar = Libraries_path(root);
            Xmin = min;
            Xmax = max;
            Name = name1;
            ID = root.id;
            AssetIndex = root.assetIndex.id;
        }

        //属性
        public ArrayList Path_jar { get; set; }
        public int Xmax { get => xmax; set => xmax = value; }
        public int Xmin { get => xmin; set => xmin = value; }
        public string Name { get => name; set => name = value; }
        public string ID { get => id; set => id = value; }
        public string AssetIndex { get => assetIndex; set => assetIndex = value; }
        //json读取
        private string get_json()
        {
            string dirpath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string folder = dirpath + "\\.minecraft\\versions\\1.12.2\\1.12.2.json";
            StreamReader s = System.IO.File.OpenText(folder);
            string str = s.ReadToEnd();
            return str;
        }
        //json数据获取
        private ArrayList Libraries_path(RootObject root)
        {
            ArrayList pathlist = new ArrayList();
            foreach (Libraries lb in root.libraries)
            {
                if (lb.downloads.artifact != null)
                {
                    pathlist.Add(lb.downloads.artifact.path);
                }

            }
            return pathlist;
        }
        private string get_mainClass(RootObject root)
        {
            return root.mainClass;
        }
        private string get_minecraftArguments(RootObject root)
        {
            return root.minecraftArguments;
        }
        //字符串拼接
        public StringBuilder joint(ArrayList list)
        {
            StringBuilder str = new StringBuilder();
            string dirpath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string folder = dirpath + @"\.minecraft\libraries\";
            str.Append("-XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow ");
            string setting = "-Xms" + this.Xmin.ToString() + "M" + " " + "-Xmx" + this.Xmax.ToString() + "M" + " ";
            str.Append(setting);
            str.Append("-Djava.library.path=");
            str.Append("\"" + dirpath);
            str.Append(@"\.minecraft\versions\" + this.ID + "\\" + this.ID + "-natives" + "\"");
            str.Append(" -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true -cp ");
            str.Append("\"");
            foreach (string item in list)
            {
                string new_str = item.Replace("/", "\\");
                str.Append(folder);
                str.Append(new_str);
                str.Append(";");
            }
            str.Append(dirpath + @"\.minecraft\versions\" + this.ID + "\\" + this.ID + ".jar");
            str.Append("\"");
            str.Append(" net.minecraft.client.main.Main ");
            string minecraftArguments = "--username " + "\"" + this.Name + "\" "
                + "--version " + "\"" + "yker.0.1" + "\" " + "--gameDir " + "\"" +
                dirpath + "\\.minecraft" + "\" " + "--assetsDir " + "\"" + dirpath + "\\.minecraft\\assets" + "\"";
            str.Append(minecraftArguments);
            str.Append(" --assetIndex " + "\"" + this.AssetIndex + "\"");
            str.Append(" --accessToken ");
            str.Append(@"""d9729feb74992cc3482b350163a1a010""");
            return str;
        }

    }
    //json解析
    public class AssetIndex
    {
        public string id { get; set; }
        public string sha1 { get; set; }
        public string size { get; set; }
        public string totalSize { get; set; }
        public string url { get; set; }
    }

    public class Client
    {
        public string sha1 { get; set; }
        public string size { get; set; }
        public string url { get; set; }
    }

    public class Server
    {
        public string sha1 { get; set; }
        public string size { get; set; }
        public string url { get; set; }
    }


    public class Artifact
    {
        public string path { get; set; }
        public string sha1 { get; set; }
        public string size { get; set; }
        public string url { get; set; }
    }

    public class Downloads
    {
        public Artifact artifact { get; set; }
    }

    public class Libraries
    {
        public Downloads downloads { get; set; }
        public string name { get; set; }
    }

    public class File
    {
        public string id { get; set; }
        public string sha1 { get; set; }
        public string size { get; set; }
        public string url { get; set; }
    }

    public class RootObject
    {
        public AssetIndex assetIndex { get; set; }
        public string assets { get; set; }
        public Downloads downloads { get; set; }
        public string hidden { get; set; }
        public string id { get; set; }
        public List<Libraries> libraries { get; set; }
        public string mainClass { get; set; }
        public string minecraftArguments { get; set; }
        public string minimumLauncherVersion { get; set; }
        public string releaseTime { get; set; }
        public string time { get; set; }
        public string type { get; set; }
    }
}
