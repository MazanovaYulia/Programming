/*********************************************
* Автор: Мазанова Ю.Н.                       *
* Задание 3 - Перегрузка операций            *
* 3.3 - Матричный калькулятор                *
*********************************************/

using System;

namespace OperationOverload 
{
  public class MatrixCalculator 
  {
    private static MatrixCalculator instance;
    
    private MatrixCalculator() 
    {
    }
      
    public static MatrixCalculator GetInstance 
    {
      get
      {
        if (instance == null)
        {
          instance = new MatrixCalculator();
        }
          return instance;
      }
    }

    private SquareMatrixClone CreateSquareMatrix()
    {
      var notSet = true;
      Console.WriteLine("Введите имя матрицы: ");
      var name = Console.ReadLine();
      Console.WriteLine("\nСгенерировать случайную матрицу?\n 0 - нет\n 1 - да");
        while (notSet) 
        {
          switch (Console.ReadLine()) 
          {
            case "0":
              notSet = false;
              break;
            case "1":
              return new SquareMatrixClone(name);
            default:
              Console.WriteLine("Попробуйте заново.");
              break;
          }
        }

      notSet = true;
      var size = 0;
        while (notSet)
        {
          Console.WriteLine("\nВведите размер матрицы: ");
            if (!int.TryParse(Console.ReadLine(), out size) || size <= 1)
            {
              Console.WriteLine("Попробуйте заново.");
            } 
            else 
            {
              notSet = false;
            }
        }

      notSet = true;
      Console.WriteLine("\nСгенерировать случайные элементы?\n 0 - нет\n 1 - да");
        while (notSet) 
        {
          switch (Console.ReadLine())
          {
            case "0":
              notSet = false;
              break;
            case "1":
              return new SquareMatrixClone(size, name);
            default:
              Console.WriteLine("Попробуйте заново.");
              break;
          }
        }

      notSet = true;
      var elements = new double[size, size];
      double currentElement;
        for (var rowIndex = 0; rowIndex < size; ++rowIndex)
        {
          for (var columnIndex = 0; columnIndex < size; ++columnIndex)
          {
            while (notSet)
            {
              Console.WriteLine($"Введите элемент {rowIndex}{columnIndex}: ");
                if (!double.TryParse(Console.ReadLine(), out currentElement))
                {
                  Console.WriteLine("Попробуйте заново.");
                } 
                else
                {
                  elements[rowIndex, columnIndex] = currentElement;
                  notSet = false;
                }
            }
              notSet = true;
          }
        }
          return new SquareMatrixClone(size, name, elements);
    }

    private string Comparison(SquareMatrixClone left, SquareMatrixClone right)
    {
      if (left > right)
      {
        return $"{left.Name} > {right.Name}";
      }
      else if (left < right) 
      {
        return $"{left.Name} < {right.Name}";
      }
      else
      {
        return $"{left.Name} = {right.Name}";
      }
    }

    public void Calculator()
    {
      Console.WriteLine("Первая матрицы:\n");
      var left = CreateSquareMatrix();
      Console.Clear();
      Console.WriteLine("Вторая матрица:\n");
      var right = CreateSquareMatrix();
      Console.Clear();
      left.PrintMatrix();
      Console.WriteLine("\n");
      right.PrintMatrix();
      Console.WriteLine("\n0 - Вычитание");
      Console.WriteLine("1 - Умножение");
      Console.WriteLine("2 - Сравнение");
      Console.WriteLine("3 - Транспонирование");
      Console.WriteLine("4 - Выход");
      
      var option = true;
        while (true)
        {
          while (option)
          {
            Console.WriteLine("\nВыберите функцию: ");
              switch (Console.ReadLine())
              {
                case "0":
                  try
                  {
                    var result = (SquareMatrix)left.Clone();
                    result -= right;
                    result.PrintMatrix();
                  }
                  catch (SquareMatrixSizeException exception)
                  {
                    Console.WriteLine(exception.Message);
                    break;
                  }
                    option = false;
                    break;
                case "1":
                  try 
                  {
                    var result = (SquareMatrix)left.Clone();
                    result *= right;
                    result.PrintMatrix();
                  }
                  catch (SquareMatrixSizeException exception) 
                  {
                    Console.WriteLine(exception.Message);
                    break;
                  }
                    option = false;
                    break;
                case "2":
                  Console.WriteLine(Comparison(left, right));
                  option = false;
                  break;
                case "3":
                  var tMatrix = (SquareMatrix)left.Clone();
                  tMatrix = tMatrix.Transpose();
                  tMatrix.PrintMatrix();
                  tMatrix = (SquareMatrix)right.Clone();
                  tMatrix = tMatrix.Transpose();
                  tMatrix.PrintMatrix();
                  option = false;
                  break;
                case "4":
                  return;
                default:
                  Console.WriteLine("Попробуйте заново.");
                  break;
              }
                option = true;
          }
        }
    }
  }

  class Program
  {
		static void Main(string[] args)
    {
			MatrixCalculator.GetInstance.Calculator();
		}
	}
}
