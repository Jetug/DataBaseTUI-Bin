using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace DataBaseLogic
{
    public struct Village
    {
        public string name;
        public string Name
        {
            get { return name; }
            set { name = Name; }
        }
        public string dev;
        public string Dev
        {
            get { return dev; }
            set { dev = Dev; }
        }
        public float area;
        public float Area
        {
            get { return area; }
            set { area = Area; }
        }
        public uint people;
        public uint People
        {
            get { return people; }
            set { people = People; }
        }

        public Village(string name = "", string dev = "", float area = 0, uint people = 0)
        {
            this.name = name;
            this.dev = dev;
            this.area = area;
            this.people = people;
        }
    }

    public struct House
    {
        public string name;
        public string Name { get { return name; } }
        public ushort num;
        public ushort Num { get { return num; } }
        public float area;
        public float Area { get { return area; } }
        public byte floor;
        public byte Floor { get { return floor; } }
        public string type;
        public string Type { get { return type; } }

        public House(string name = "", ushort num = 0, float area = 0, byte floor = 0, string type = "")
        {
            this.name = name;
            this.num = num;
            this.area = area;
            this.floor = floor;
            this.type = type;
        }
    }

    public struct Developer
    {
        public string name;
        public string Name { get { return name; } }
        public float inc;
        public float Inc { get { return inc; } }
        public string addr;
        public string Addr { get { return addr; } }

        public Developer(string name = "", float inc = 0, string addr = "")
        {
            this.name = name;
            this.inc = inc;
            this.addr = addr;
        }
    }

    public class BinFile
    {
        public BinFile()
        {
            if (!File.Exists(path + "Villages"))
            {
                CreateFile("Villages");
            }
            Load_DataBase();
            //Load_DataBase(ref villages);
            //Load_DataBase(ref houses);
            //Load_DataBase(ref developers);
        }

        public const string path = "C:/C#/DataBase_Bin/BinFiles/";
        private const byte village = 1;
        private const byte house = 2;
        private const byte developer = 3;

        public static string fileName = "Villages";
        public static List<Village> villages = new List<Village>();
        public static List<House> houses = new List<House>();
        public static List<Developer> developers = new List<Developer>();

        //public void Load_DataBase(ref List<Village> villages)
        //{
        //    try
        //    {       
                
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        villages = null;
        //    }

        //}

        //public void Load_DataBase(ref List<House> houses)
        //{
        //    try
        //    {

        //    }
        //    catch (FileNotFoundException)
        //    {
        //        houses = null;
        //    }

        //}

        //public void Load_DataBase(ref List<Developer> developers)
        //{
        //    try
        //    {
               
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        developers = null;
        //    }
        //}

        /// <summary>
        /// Загружает данные из файла в списки
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public void Load_DataBase()
        {
            BinaryReader reader = new BinaryReader(File.Open(path+fileName, FileMode.Open));
            if (villages != null)
                villages.Clear();
            if (houses != null)
                houses.Clear();
            if (developers != null)
                developers.Clear();
            while (reader.PeekChar() > -1)
            {
                switch (reader.ReadByte())
                {
                    case village:
                        villages.Add(new Village(reader.ReadString(), reader.ReadString(), reader.ReadSingle(), reader.ReadUInt32()));
                        break;
                    case house:
                        houses.Add(new House(reader.ReadString(), reader.ReadUInt16(), reader.ReadSingle(), reader.ReadByte(), reader.ReadString()));
                        break;
                    case developer:
                        developers.Add(new Developer(reader.ReadString(), reader.ReadSingle(), reader.ReadString()));
                        break;
                }

            }
            reader.Close();
        }

        /// <summary>
        /// Возвращает названия всех девелоперов в текущем файле
        /// </summary>
        /// <returns></returns>
        public List<string> GetDevNames()
        {
            Load_DataBase();
            List<string> names = new List<string>();
            try
            {
                foreach(Developer dev in developers)
                    names.Add(dev.name);
            }
            catch (FileNotFoundException)
            {
                names = null;
            }

            return names;
        }
        
        /// <summary>
        /// Создаёт новый бинарный файл
        /// </summary>
        public void CreateFile(string name)
        {
            BinaryWriter writer = new BinaryWriter(File.Open(path+name, FileMode.Create));
            writer.Close();
        }

        /// <summary>
        /// Возвращает список существующих файлов
        /// </summary>
        /// <returns></returns>
        public List<string> Load_FileList()
        {
            //List<string> fileNames = new List<string>();
            //fileNames = Directory.GetFiles(@"C:/C#/RunDll/XMLfiles/", "*.xml").ToList<string>();
            DirectoryInfo dinfo = new DirectoryInfo(path);
            FileInfo[] fileInfo = dinfo.GetFiles();
            List<string> files = new List<string>();

            foreach (FileInfo f in fileInfo)
                files.Add(f.ToString());
            return files;
        }

        /// <summary>
        /// Сохраняет списки в файл
        /// </summary>
        public void SaveAll()
        {
            CreateFile(fileName);
            SaveInFile(villages, FileMode.Append);
            SaveInFile(houses, FileMode.Append);
            SaveInFile(developers, FileMode.Append);
        }

        /// <summary>
        /// Сохраняет список посёлков в файл
        /// </summary>
        /// <param name="list"></param>
        /// <param name="mode"></param>
        public void SaveInFile(List<Village> list, FileMode mode)
        {
            BinaryWriter writer = new BinaryWriter(File.Open(path + fileName, mode));
            foreach (Village vill in list)
            {
                writer.Write(village);
                writer.Write(vill.name);
                writer.Write(vill.dev);
                writer.Write(vill.area);
                writer.Write(vill.people);
            }
            writer.Close();
        }

        /// <summary>
        /// Сохраняет список домов в файл
        /// </summary>
        /// <param name="list"></param>
        /// <param name="mode"></param>
        public void SaveInFile(List<House> list, FileMode mode)
        {
            BinaryWriter writer = new BinaryWriter(File.Open(path + fileName, mode));
            foreach (House hs in list)
            {
                writer.Write(house);
                writer.Write(hs.name);
                writer.Write(hs.num);
                writer.Write(hs.area);
                writer.Write(hs.floor);
                writer.Write(hs.type);
            }
            writer.Close();
        }

        /// <summary>
        /// Сохраняет список девелоперов в файл
        /// </summary>
        /// <param name="list"></param>
        /// <param name="mode"></param>
        
        public void SaveInFile(List<Developer> list, FileMode mode)
        {
            BinaryWriter writer = new BinaryWriter(File.Open(path + fileName, mode));
            foreach (Developer dev in list)
            {
                writer.Write(developer);
                writer.Write(dev.name);
                writer.Write(dev.inc);
                writer.Write(dev.addr);
            }
            writer.Close();
        }
    }
}