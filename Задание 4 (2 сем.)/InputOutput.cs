/*****************************************
* Автор: Мазанова Ю.Н.                   *
* Задание 4 - Стандартный ввод-вывод     *
*****************************************/

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace InputOutput
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Start();
    }
      static void Start()
      {
        while (true)
        {
          Menu();
          int OptionId = Convert.ToInt32(Console.ReadLine());
            switch (OptionId)
            {
              case 1:
                IndexFiles();
                break;
              case 2:
                IndexTxtFiles();
                break;
              case 3:
                Console.WriteLine("\nВведите ключевое слово:");
                string Keyword = Console.ReadLine();
                IndexhByKeywords(Keyword);
                break;
              default:
                Console.WriteLine("Такая функция не существует, выберите номер от 1 до 3.");
                break;
            }
        }
      }

  static void Menu()
  {
    Console.WriteLine("Выберите функцию:");
    Console.WriteLine("1 - Индексировать все файлы");
    Console.WriteLine("2 - Индексировать все текстовые файлы");
    Console.WriteLine("3 - Поиск по ключевым словам");
  }

  static void IndexFiles()
  {
    String CurrentDirectory = Directory.GetCurrentDirectory();
    string[] Files = Directory.GetFiles(CurrentDirectory);
    PrintFiles(Files, "Найден(о) " + Files.Length + " файл(ов) в каталоге: '" + CurrentDirectory + "':");
  }

  static void IndexTxtFiles()
  {
    String CurrentDirectory = Directory.GetCurrentDirectory();
    string[] Files = GetAllTxtFiles(CurrentDirectory);
    PrintFiles(Files, "Найден(о) " + Files.Length + " текстовый(х) файл(ов) в каталоге: '" + CurrentDirectory + "':");
  }

  static void IndexhByKeywords(string Keyword)
  {
    String CurrentDirectory = Directory.GetCurrentDirectory();
    string[] Files = SearchTxtFilesByKeyword(CurrentDirectory, Keyword);
    PrintFiles(Files, "Найден(о) " + Files.Length + " текстовый(х) файл(ов) в каталоге: '" + CurrentDirectory + "' с ключевыми словами '" + Keyword + "':");
  }

  static string[] SearchTxtFilesByKeyword(string CurrentDirectory, string Keyword)
  {
    string[] Files = GetAllTxtFiles(CurrentDirectory);
    List<string> NeedFiles = new List<string> { };

    foreach(string FileName in Files)
    {
      TextFile File = new TextFile(FileName);
      string FileContent = File.Content;

      if (FileContent.Contains(Keyword))
      {
        NeedFiles.Add(FileName);
      }
    }
    return NeedFiles.ToArray();
  }

  static void PrintFiles(string[] Files, string Message)
  {
    int fileIndex = 0;
    Console.WriteLine();
    Console.WriteLine(Message);
    
    foreach (string File in Files)
    {
      ++fileIndex;
      Console.WriteLine(fileIndex + ") " + File);
    }
    Console.WriteLine();
  }

  static string[] GetAllTxtFiles(string NeedDirectory)
  {
    List<string> NeedFiles = new List<string> { };
    string[] Files = Directory.GetFiles(NeedDirectory);
   
    foreach (string File in Files)
    {
      if (File.Contains(".txt")) 
      {
        NeedFiles.Add(File);
      }     
    }
      return NeedFiles.ToArray();
  }
 }
    
//сериализация
  public class TextFile
  {
    public string Name { get; set; } = "Не найдено";
    public string Content { get; set; } = "";
    FileHistory History = new FileHistory();
    
    public TextFile() 
    {
      Content = Read();
    }
    
    public TextFile(string name)
    {
      Name = name;
      Content = Read();
    }

    public void BinarySerialize()
    {
      Console.WriteLine("* Содержимое сериализуется в бинарный *");
      FileStream File = Open();
      BinaryFormatter Formatter = new BinaryFormatter();
      Formatter.Serialize(File, this);
      File.Close();
    }

    public void BinaryDeserialize()
    {
      Console.WriteLine("* Содержимое десериализуется из бинарного *");
      FileStream File = Open();
      var Formatter = new BinaryFormatter();
      var DeserializedFile = (TextFile) Formatter.Deserialize(File);
      File.Close();
    }

    public void XMLSerialize()
    {
      Console.WriteLine("* Содержимое сериализуется в XML *");
      FileStream File = Open();
      XmlSerializer Formatter = new XmlSerializer(typeof(TextFile));
      Formatter.Serialize(File, this);
      File.Close();
    }

    public void XMLDeserialize()
    {
      Console.WriteLine("* Содержимое десериализуется из XML *");
      FileStream File = Open();
      var Formatter = new XmlSerializer(typeof(TextFile));
      var DeserializedFile = (TextFile) Formatter.Deserialize(File);
      File.Close();
    }

    public FileStream Open()
    {
      FileStream File = new FileStream(Name, FileMode.OpenOrCreate);
      return File;
    }

    public String Read()
    {
      using (FileStream fstream = File.OpenRead(Name))
      {
        byte[] buffer = new byte[fstream.Length];
        fstream.Read(buffer, 0, buffer.Length);
        string textFromFile = Encoding.Default.GetString(buffer);
        Content = textFromFile;
        return textFromFile;
      }
    }

    public void Write()
    {
      StreamWriter File = new StreamWriter(Name);
      File.WriteLine(Content);
      File.Close();
    }

    public void SaveToMemento()
    {
      Console.WriteLine("* Содержимое записывается в память Memento *");
      History.Memento.Push(GetFileMemento());
    }

    public FileMemento GetFileMemento()
    {
      return new FileMemento(Content);
    }

    public void GetFromMemento()
    {
      Console.WriteLine("* Содержимое восстановлено из памяти Memento *");
      FileMemento Memento = History.Memento.Pop();
      this.Content = Memento.Content;
    }

    public void Print()
    {
      Console.WriteLine("Файл '" + Name + "': " + Content);
    }
  }

  public class FileMemento
  {
    public string Content { get; private set; }
    public FileMemento(string Content)
    {
      this.Content = Content;
    }
  }

  public class FileHistory
  {
    public Stack<FileMemento> Memento { get; private set; }
    public FileHistory()
    {
      Memento = new Stack<FileMemento>();
    }
  }
}
