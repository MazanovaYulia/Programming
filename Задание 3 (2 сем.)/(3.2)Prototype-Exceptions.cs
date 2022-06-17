/*********************************************
* Автор: Мазанова Ю.Н.                       *
* Задание 3 - Перегрузка операций            *
* 3.2 - “Прототип”, исключения               *
*********************************************/

using System;

namespace OperationOverload 
{                                       //паттерн "прототип"
  public class SquareMatrixClone : SquareMatrix, ICloneable
  {
    public SquareMatrixClone()
    {
    }
    
    public SquareMatrixClone(string name) : base(name)
    {
    }
    
    public SquareMatrixClone(int size, string name) : base(size, name)
    {
    }
          
    public SquareMatrixClone(int size, string name, double[,] elements) : base(size, name, elements)
    {
    }
        
    public object Clone() 
    {
      var clone = new SquareMatrixClone();
      clone.Size = this.Size;
      clone.Name = string.Copy(this.Name);
      clone.Matrix = this.Matrix;
      return clone;
    }
  }
                                         //исключения
  public class SquareMatrixSizeException : Exception
  {
    public SquareMatrixSizeException (string message) : base(message)
    {
    }
  }
}
