using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace labor_1
{
    class Program
    {

        static public string Command;
        static void Main(string[] args)
        {
            Console.Clear();
            while (true)
            {
                Command = Console.ReadLine();
                if (Command == "tasks")
                {
                    Console.WriteLine("СПИСОК ЗАДАНИЙ");
                    Console.WriteLine("[1] Задание 1");
                    Console.WriteLine("[2] Задание 2");
                    Console.WriteLine("[3] Задание 3");
                    Console.WriteLine("[4] Задание 4");
                    Console.WriteLine("[5] Задание 5");
                    Command = Console.ReadLine();
                    switch (Command)
                    {
                        case "1":
                            {
                                Task1();
                            }
                            break;
                        case "2":
                            {
                                Task2();
                            }
                            break;
                        case "3":
                            {
                                Task3();
                            }
                            break;
                        case "4":
                            {
                                Task4();
                            }
                            break;
                        case "5":
                            {
                                Task5();
                            }
                            break;
                    }
                }
            }
        }

        class Object
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        private static void Task5()
        {
            {
                string sourceFile = "D://test.txt"; // исходный файл
                string compressedFile = "D://test.zip"; // сжатый файл
                string targetFile = "D://test_new.txt"; // восстановленный файл

                // создание сжатого файла
                Compress(sourceFile, compressedFile);
                // чтение из сжатого файла
                Decompress(compressedFile, targetFile);

                Console.ReadLine();
            }

            static void Compress(string sourceFile, string compressedFile)
            {
                // поток для чтения исходного файла
                using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
                {
                    // поток для записи сжатого файла
                    using (FileStream targetStream = File.Create(compressedFile))
                    {
                        // поток архивации
                        using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                        {
                            sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                            Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                                sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                        }
                    }
                }
            }

            static void Decompress(string compressedFile, string targetFile)
            {
                // поток для чтения из сжатого файла
                using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
                {
                    // поток для записи восстановленного файла
                    using (FileStream targetStream = File.Create(targetFile))
                    {
                        // поток разархивации
                        using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(targetStream);
                            Console.WriteLine("Восстановлен файл: {0}", targetFile);
                        }
                    }
                }
            }
        }

        private static void Task4()
        {
            Console.Write("Введите свое имя: ");
            string name = Console.ReadLine();
            Console.Write("Название компании: ");
            string company = Console.ReadLine();
            Console.WriteLine("Введите зп: ");
            string price = Console.ReadLine();

            Console.Write("Введите имя друга: ");
            string name2 = Console.ReadLine();
            Console.Write("Введите зп друга: ");
            string price2 = Console.ReadLine();

            XDocument xdoc = new XDocument();
            // создаем первый элемент
            XElement object1 = new XElement("office");
            // создаематрибут
            XAttribute objectNameAttr = new XAttribute("name", name);
            XElement objectCompanyElem = new XElement("company", company);
            XElement objectPriceElem = new XElement("price", price);
            // добавляем атрибут и элементы в первый элемент
            object1.Add(objectNameAttr);
            object1.Add(objectCompanyElem);
            object1.Add(objectPriceElem);

            // создаемвторойэлемент
            XElement object2 = new XElement("office");
            XAttribute object2NameAttr = new XAttribute("name", name2);
            XElement object2CompanyElem = new XElement("company", company);
            XElement object2PriceElem = new XElement("price", price2);
            object2.Add(object2NameAttr);
            object2.Add(object2CompanyElem);
            object2.Add(object2PriceElem);
            // создаемкорневойэлемент
            XElement offices = new XElement("offices");
            // добавляем в корневой элемент
            offices.Add(object1);
            offices.Add(object2);
            // добавляем корневой элемент в документ
            xdoc.Add(offices);
            //сохраняем документ
            xdoc.Save("office.xml");


            //Чтение файла xml
            XDocument xdoc1 = XDocument.Load("office.xml");
            foreach (XElement phoneElement in xdoc.Element("offices").Elements("office"))
            {
                XAttribute nameAttribute = phoneElement.Attribute("name");
                XElement companyElement = phoneElement.Element("company");
                XElement priceElement = phoneElement.Element("price");

                if (nameAttribute != null && companyElement != null && priceElement != null)
                {
                    Console.WriteLine($"Имя: {nameAttribute.Value}");
                    Console.WriteLine($"Компания: {companyElement.Value}");
                    Console.WriteLine($"Зарплата: {priceElement.Value}");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
            File.Delete("office.xml");
        }

        private static async Task Task3()
        {
                using (FileStream fs = new FileStream("object.json", FileMode.OpenOrCreate))
                {
                    Object test = new Object() { Name = "Test", Value = 20 };
                    await JsonSerializer.SerializeAsync<Object>(fs, test);
                    Console.WriteLine("Data has been saved to file");
                }

                using (FileStream fs = new FileStream("object.json", FileMode.OpenOrCreate))
                {
                    Object restoredObject = await JsonSerializer.DeserializeAsync<Object>(fs);
                    Console.WriteLine($"Name: {restoredObject.Name}  Value: {restoredObject.Value}");
                }

            File.Delete("object.json");
            Console.WriteLine("Data has been deleted");
            
        }
        
        private static void Task2()
        {
            string dirNamex = "C:\\Program Files";

            DirectoryInfo dirInfod = new DirectoryInfo(dirNamex);

            Console.WriteLine($"Название каталога: {dirInfod.Name}");
            Console.WriteLine($"Полное название каталога: {dirInfod.FullName}");
            Console.WriteLine($"Время создания каталога: {dirInfod.CreationTime}");
            Console.WriteLine($"Корневой каталог: {dirInfod.Root}");

            string dirName = "C:\\";

            if (Directory.Exists(dirName))
            {
                Console.WriteLine("Подкаталоги:");
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
                Console.WriteLine("Файлы:");
                string[] files = Directory.GetFiles(dirName);
                foreach (string s in files)
                {
                    Console.WriteLine(s);
                }


                string path = @"C:\SomeDir";
                string subpath = @"program\avalon";
                DirectoryInfo dirInfoz = new DirectoryInfo(path);
                if (!dirInfoz.Exists)
                {
                    dirInfoz.Create();
                }
                dirInfoz.CreateSubdirectory(subpath);


                string oldPath = @"C:\SomeFolder";
                string newPath = @"C:\SomeDir";
                DirectoryInfo dirInfo = new DirectoryInfo(oldPath);
                if (dirInfo.Exists && Directory.Exists(newPath) == false)
                {
                    dirInfo.MoveTo(newPath);
                }

                Console.WriteLine("Удалить файл (0 - Да, 1 - Нет)?");
                int caseSwitch = Convert.ToInt32(Console.ReadLine());

                switch (caseSwitch)
                {
                    case 0:
                        string dirNamea = @"C:\SomeFolder";

                        try
                        {
                            DirectoryInfo dirInfox = new DirectoryInfo(dirNamea);
                            dirInfoz.Delete(true);
                            Console.WriteLine("Файл удален");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    default:
                        Console.WriteLine("Конец!");
                        break;
                }
            }
        }

        private static void Task1()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Name: {drive.Name}");
                Console.WriteLine($"Type: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Value : {drive.TotalSize}");
                    Console.WriteLine($"Free Space: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Label: {drive.VolumeLabel}");
                }
                Console.WriteLine();
            }
            
        }      
    }
}
