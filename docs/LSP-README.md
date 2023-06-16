# LSP - Liskov Substitution Principle

> A subclass must be substitutable for their base types.

## Inheritence 'is a' must be inheritance 'is-a substitute'

Inheritance is not enougth when extend object hierarchy. You need to do it in a smart way- using LSP.

The *Liskov Substitution Principle* says that the object of a derived class should be able to replace an object of the base class without bringing any errors in the system or modifying the behavior of the base class.

## How to implement LSP

> **'Is A' relationship **is insuficiand from a OOP perspective ; check that subtypes are **substitutable** for their base types.

> Take care not to break base type invariants when creating subtypes or implement interfaces

> search for typologies like:

    - type checking, typically using the **is** and **as** keywords
    - null checking, particularly when special behavior is done in the case of null rather than just throwing an exception
    - and NotImplementedExceptions

## The sample from Robert "Uncle Bob" Martin

We have a parcel with 2 rectangles and 1 squares and we want to create an method to calculate the Area of them.

We have noticed that a square is a particular type of rectangle.

```c#
public class Rectangle
  {
      public virtual int Height { get; set; }
      public virtual int Width { get; set; }
  }

  public class Square : Rectangle
  {
      private int _height;
      private int _width;
      public override int Height
      {
          get
          {
              return _height;
          }
          set
          {
              _height = value;
              _width = value;
          }
      }
      public override int Width
      {
          get
          {
              return _width;
          }
          set
          {
             _width = value;
             _height = value;
          }
      }
 
  }
```

Let's use SRP and calculate area with another class:

```c#
  public class Calculator
        {
            public static int Area(Rectangle r)
            {
                return r.Height * r.Width;
            }
 
            public static int Area(Square s)
            {
                return s.Height * s.Height;
            }
        }
```

In our module we need to calculate the total area for  2 rectangles and 1 squares:

```c#
  Calculator.Parcels.Add(new Rectangle{Height = 5, Width = 2});
  Calculator.Parcels.Add(new Rectangle{Height = 4, Width = 5});
  Calculator.Parcels.Add(new Square{Height = 2});
  
  var total = Calculator.Compute();

  Console.WriteLine($"We expect to be 34(10 + 20 + 4) but is {total}");
```

we expect 34 but it is 30! Why? Because the child class does not substitute
the parent class.

Try in fiddle:

```C#
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {  
        Calculator.Parcels.Add(new Rectangle{Height = 5, Width = 2});
        Calculator.Parcels.Add(new Rectangle{Height = 4, Width = 5});
        Calculator.Parcels.Add(new Square{Height = 2});
        
        var total = Calculator.Compute();
        
        Console.WriteLine($"We expect to be 34(10 + 20 + 4) but is {total}");
    }
}

public class Rectangle
  {
      public virtual int Height { get; set; }
      public virtual int Width { get; set; }
  }

  public class Square : Rectangle
  {
      private int _height;
      private int _width;
      public override int Height
      {
          get
          {
              return _height;
          }
          set
          {
              _height = value;

          }
      }
      public override int Width
      {
          get
          {
              return _width;
          }
          set
          {
             _width = value;

          }
      }
 
  }
  
  public class Calculator
  {
   public static List<Rectangle> Parcels = new List<Rectangle>();
   
   public static int Area(Rectangle r)
   {
    var area = r.Height * r.Width;
    Console.WriteLine($"Compute Rectangle Area: {area}");
    return area;
   }

   public static int Area(Square s)
   {
    var area = s.Height * s.Height;
    Console.WriteLine($"Compute Square Area: {area}");
    return area;
   }
   
   public static int Compute()
   {
  int total = 0;
  foreach(Rectangle parcel in Calculator.Parcels)
  {
   total += Calculator.Area(parcel); // 10 + 20 + 4 
  } 
    return total;
   }
  }
```

## How to fix it

```c#

public  abstract class Shape
        {
            public abstract int Area();
        }
```

For Rectanle we have:

```C#
public class Rectangle :Shape
  {
      public  int Height { get; set; }
      public  int Width { get; set; }

      public override int Area()
      {
          return Height * Width;
      }
  }

```

For Square:

```c#
public class Square : Shape
  {
      public int Sides;
      public override int Area()
      {
          return Sides * Sides;
      } 
  }

```

and calculator:

```c#
  public class Calculator
  {
   public static List<Shape> Parcels = new List<Shape>();
   
  
   public static int Compute()
   {
  int total = 0;
  foreach(Shape parcel in Calculator.Parcels)
  {
   total += parcel.Area(); // 10 + 20 + 4 
  } 
    return total;
   }
  }
```

We expect 34 and it is 34! Why? Because the child class( parcel of type Rectangle or Square) substitute the parent class(Shape).

Try in fiddle:

```c#
using System;
using System.Collections.Generic;
     
public class Program
{
 public static void Main()
 {
  
  Calculator.Parcels.Add(new Rectangle{Height = 5, Width = 2});
  Calculator.Parcels.Add(new Rectangle{Height = 4, Width = 5});
  Calculator.Parcels.Add(new Square{Sides = 2});
  
  var total = Calculator.Compute();
  
  Console.WriteLine($"We expect to be 34(10 + 20 + 4) ;It is {total}");
 }
}

public  abstract class Shape
        {
        public abstract int Area();
        }

public class Square : Shape
  {
      public int Sides;
      public override int Area()
      {
          return Sides * Sides;
      } 
  }
public class Rectangle :Shape
  {
      public  int Height { get; set; }
      public  int Width { get; set; }
      public override int Area()
      {
          return Height * Width;
      }
  }  
  public class Calculator
  {
   public static List<Shape> Parcels = new List<Shape>();
     
   public static int Compute()
   {
  int total = 0;
  foreach(Shape parcel in Parcels)
  {
   total += parcel.Area(); // 10 + 20 + 4 
  } 
    return total;
   }
  }
```

## Resources

- [Check with fiddle](https://dotnetfiddle.net/)
- [Simplifying the Liskov Substitution Principle of SOLID in C#](https://www.infragistics.com/community/blogs/b/dhananjay_kumar/posts/simplifying-the-liskov-substitution-principle-of-solid-in-c)
- [The Liskov Substitution Principle](https://cleancoders.com/episode/clean-code-episode-11-p2)
