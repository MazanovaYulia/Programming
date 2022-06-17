/***********************************
* Автор: Мазанова Ю.Н.             *
* Задание 5 - Строки и коллекции   *
***********************************/

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringsAndCollections
{
  internal class StringsAndCollections
  {
    static void ReplaceKeywords(List<List<string>> DictionaryList, string TrueNumber)
    {
      StreamReader FileError = new StreamReader("TextWithErrors.txt");
      StreamWriter FileEdit = new StreamWriter("CorrectedText.txt");

      while (!FileError.EndOfStream)
      {
        string Line = FileError.ReadLine();
          foreach (List<string> Word in DictionaryList)
          {
            string WrongWords = Word[0];
            string CorrectWords = Word[1];
            Line = Line.Replace(WrongWords, CorrectWords);
          }

        var RegexExperssion = new Regex(@"([(]\d{3}[)]\s{1}\d{3})-(\d{2})-(\d{2})");
        var Matches = RegexExperssion.Matches(Line);
          foreach (Match Match in Matches)
          {
            Line = Line.Replace(Match.Value, TrueNumber);
          }
            FileEdit.WriteLine(Line);
      }
        FileError.Close();
        FileEdit.Close();
    }
        
    static List<List<string>> GetDictionaryList()
    {
      var DictionaryList = new List<List<string>>();
      StreamReader DictionaryFile = new StreamReader("DictionaryOfMisspelledWords.txt");
        while (!DictionaryFile.EndOfStream)
        {
          string Line = DictionaryFile.ReadLine();
          List<string> WordList = Line.Split('-').ToList();
          DictionaryList.Add(WordList);
        }
          DictionaryFile.Close();
          return DictionaryList;
    }
    
    static void Main(string[] args)
    {
      var DictionaryList = GetDictionaryList();
      string TrueNumber = "+380 12 345 67 89";

      ReplaceKeywords(DictionaryList, TrueNumber);
      Console.WriteLine("Для исправления ошибок в словах, введите неправильное слово в словарь (DictionaryOfMisspelledWords) с верным написанием через тире (например, пирвет-привет). В текстовом файле (TextWithErrors) введите предложение с ошибками. При запуске программы исправленный текст появится в текстовом файле CorrectedText. Попробуйте!");
    }
  }
}
