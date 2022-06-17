/*********************************************
* Автор: Мазанова Ю.Н.                       *
* Задание 3 - Перегрузка операций            *
* 3.1 - Рандом, перегрузка операций, методы  *
*********************************************/

using System;

namespace OperationOverload
{
  public class SquareMatrix : IComparable
  {
    public int Size { get; set; }
    public string Name { get; set; }
    public double[,] Matrix { get; set; }
                       //конструкторы для случайной генерации матриц
    public SquareMatrix()
    {
    }
    
    public SquareMatrix(string name)
    {
      var rand = new Random();
      Name = name;
      Size = rand.Next(2, 5);
      Matrix = new double[Size, Size];
        for (var rowIndex = 0; rowIndex < Size; ++rowIndex) 
        {
          for (var columnIndex = 0; columnIndex < Size; ++columnIndex)
          {
            Matrix[rowIndex, columnIndex] = rand.Next(-250, 250);
          }
        }
    }

    public SquareMatrix(int size, string name)
    {
      Name = name;
      Size = size;
      Matrix = new double[Size, Size];
      var rand = new Random();
        for (var rowIndex = 0; rowIndex < Size; ++rowIndex) 
        {
          for (var columnIndex = 0; columnIndex < Size; ++columnIndex) 
          {
            Matrix[rowIndex, columnIndex] = rand.Next(-250, 250);
          }
        }
    }

    public SquareMatrix(int size, string name, double[ , ] elements) 
    {
      Name = name;
      Size = size;
      Matrix = new double[Size, Size];
      var rand = new Random();
        for (var rowIndex = 0; rowIndex < Size; ++rowIndex)
        {
          for (var columnIndex = 0; columnIndex < Size; ++columnIndex) 
          {
            Matrix[rowIndex, columnIndex] = elements[rowIndex, columnIndex];
          }
        }
    }
                                       //перегрузка операций
    public static SquareMatrix operator + (SquareMatrix left, SquareMatrix right) 
    {
      if (left.Size != right.Size)
      {
        throw new SquareMatrixSizeException("Матрицы должны быть одного размера.");
      }
        var elementsAmount = left.Size * left.Size;
        double[ , ] elements = new double[left.Size, left.Size];
        var elementsCount = 0;
          for (var rowIndex = 0; rowIndex < left.Size; ++rowIndex)
          {
            for (var columnIndex = 0; columnIndex < left.Size; ++columnIndex)
            {
              elements[rowIndex, columnIndex] = left.Matrix[rowIndex, columnIndex] + right.Matrix[rowIndex, columnIndex];
              ++elementsCount;
            }
          }
            var name = "Результат";
            return new SquareMatrix(left.Size, name, elements);
    }
    
    public static SquareMatrix operator * (SquareMatrix left, SquareMatrix right)
    {
      if (left.Size != right.Size)
      {
        throw new SquareMatrixSizeException("Матрицы должны быть одного размера.");
      }
        var elementsAmount = left.Size * left.Size;
        double[,] elements = new double[left.Size, left.Size];
        var elementsCount = 0;
          for (var rowIndex = 0; rowIndex < left.Size; ++rowIndex)
          {
            for (var columnIndex = 0; columnIndex < left.Size; ++columnIndex)
            {
              elements[rowIndex, columnIndex] = left.Matrix[rowIndex, columnIndex] * right.Matrix[rowIndex, columnIndex];
              ++elementsCount;
            }
          }
            var name = "Результат";
            return new SquareMatrix(left.Size, name, elements);
    }

    public static bool operator > (SquareMatrix left, SquareMatrix right)
    {
      if (left.SumOfElements() > right.SumOfElements())
      {
        return true;
      }
        return false;
    }

    public static bool operator < (SquareMatrix left, SquareMatrix right) 
    {
      if (left.SumOfElements() < right.SumOfElements())
      {
        return true;
      }
        return false;
    }

    public static bool operator >= (SquareMatrix left, SquareMatrix right)
    {
      if (left.SumOfElements() >= right.SumOfElements())
      {
        return true;
      }
        return false;
      }

    public static bool operator <= (SquareMatrix left, SquareMatrix right) 
    {
      if (left.SumOfElements() <= right.SumOfElements()) 
      {
        return true;
      }
        return false;
    }

    public static bool operator == (SquareMatrix left, SquareMatrix right) 
    {
      if (left.Size != right.Size)
      {
        return false;
      }
        for (var rowIndex = 0; rowIndex < left.Size; ++rowIndex) 
        {
          for (var columnIndex = 0; columnIndex < left.Size; ++columnIndex)
          {
            if (left.Matrix[rowIndex, columnIndex] != right.Matrix[rowIndex, columnIndex])
            {
              return false;
            }
          }
        }
          return true;
    }

    public static bool operator != (SquareMatrix left, SquareMatrix right)
    {
      if (left.Size != right.Size)
      {
        return true;
      }
        for (var rowIndex = 0; rowIndex < left.Size; ++rowIndex)
        {
          for (var columnIndex = 0; columnIndex < left.Size; ++columnIndex)
          {
            if (left.Matrix[rowIndex, columnIndex] != right.Matrix[rowIndex, columnIndex])
            {
              return true;
            }
          }
        }
          return false;
    }

    public static bool operator true (SquareMatrix matrix)
    {
      return (matrix.Determinant() != 0);
    }

    public static bool operator false (SquareMatrix matrix)
    {
      return (matrix.Determinant() == 0);
    }

    public double SumOfElements() 
    {
      double sum = 0;
        for (var rowIndex = 0; rowIndex < Size; ++rowIndex) 
        {
          for (var columnIndex = 0; columnIndex < Size; ++columnIndex)
          {
            sum += Matrix[rowIndex, columnIndex];
          }
        }
          return sum;
    }

    public double Determinant() 
    {
      if (this.Size == 2) 
      {
        return (this.Matrix[0, 0] * this.Matrix[1, 1] - this.Matrix[0, 1] * this.Matrix[1, 0]);
      }
        var matrix = new SquareMatrix(this.Size - 1, "Результат");
        return matrix.SumOfElements();
    }
    
    public static implicit operator string (SquareMatrix matrix) 
    {
      var elementCount = 1;
      var result = "";
        for (var rowIndex = 0; rowIndex < matrix.Size; ++rowIndex)
        {
          for (var columnIndex = 0; columnIndex < matrix.Size; ++columnIndex)
          {
            result += ($"Элемент {elementCount}: {matrix.Matrix[rowIndex, columnIndex]}  ");
            ++elementCount;
          }
        }
          return result;
    }

    public SquareMatrix Transpose() 
    {
      var tMatrix = new SquareMatrix(this.Size, $"{this.Name} транспонирована");
        for (var rowIndex = 0; rowIndex < this.Size; ++rowIndex) 
        {
          for (var columnIndex = 0; columnIndex < this.Size; ++columnIndex) 
          {
            tMatrix.Matrix[columnIndex, rowIndex] = this.Matrix[rowIndex, columnIndex];
          }
        }
          return tMatrix;
    }

    public static implicit operator SquareMatrix (double[ , ] elements)
    {
      var elementsAmount = elements.Length;
        if (elementsAmount % 2 == 0)
        {
          var size = elementsAmount / 2;
          return new SquareMatrix(size, "Результат", elements);
        } 
        else 
        {
          throw new SquareMatrixSizeException("Размеры массива должны иметь одинаковую длину.");
        }
    }
                                              //методы
    public override string ToString()
    {
      var elementCount = 1;
      var result = "";
        for (var rowIndex = 0; rowIndex < this.Size; ++rowIndex) 
        {
          for (var columnIndex = 0; columnIndex < this.Size; ++columnIndex) 
          {
            result += ($"Элемент {elementCount}: {this.Matrix[rowIndex, columnIndex]}  ");
            ++elementCount;
          }
        }
          return result;
    }

    public int CompareTo(object obj) 
    {
      if (obj is SquareMatrix) 
      {
        var param = obj as SquareMatrix;
          if (param.SumOfElements() > this.SumOfElements())
          {
            return -1;
          }
            if (param.SumOfElements() < this.SumOfElements())
            {
              return 1;
            }
              if (param.SumOfElements() == this.SumOfElements()) 
              {
                return 0;
              }
      }
        return -1;
    }

    public override bool Equals(object obj)
    {
      if (obj is SquareMatrix)
      {
        var param = obj as SquareMatrix;
          if (param.Size != this.Size)
          {
            return false;
          }
            for (var rowIndex = 0; rowIndex < param.Size; ++rowIndex) 
            {
              for (var columnIndex = 0; columnIndex < param.Size; ++columnIndex)
              {
                if (param.Matrix[rowIndex, columnIndex] != this.Matrix[rowIndex, columnIndex])
                {
                  return false;
                }
              }
            }
              return true;
      }
        return false;
    }

    public override int GetHashCode()
    {
      return (Int32)this.SumOfElements();
    }

    public void PrintMatrix()
    {
      var row = "";
      Console.WriteLine($"Матрица {this.Name}:\n");
        for (var rowIndex = 0; rowIndex < this.Size; ++rowIndex)
        {
          for (var columnIndex = 0; columnIndex < this.Size; ++columnIndex)
          {
            row += $"{this.Matrix[rowIndex, columnIndex]}\t";
          }
            Console.WriteLine(row);
            Console.WriteLine();
            row = "";
        }
    }
  }
}
