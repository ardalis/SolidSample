# OCP - Open- Close Principle

> Software entities (classes, modules, functions, etc.) should be __Open__ for extension, but __Closed__ for modification

This [means](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle):

- A software entities (classes, modules, functions, etc.) will be said to be __Open__  if it is still available for extension. For example, it should be possible to add fields to the data structures it contains, or new elements to the set of functions it performs.
- A software entities (classes, modules, functions, etc.) will be said to be __Closed__ if [it] is available for use by other modules. This assumes that the module has been given a well-defined, stable description (the interface in the sense of information hiding).

In particular for a *class*: it is __Closed__, since it may be compiled, stored in a library, baselined, and used by client classes. But it is also o__Opened__, since any new class may use it as parent, adding new features. When a descendant class is defined, there is no need to change the original or to disturb its clients.

## How to implement OCP

> Using Composition (with DI)
> Call Inheritance and create new classes
> Adding parameters - we keep the firs method, and we call it from an overload method with more parameters
> Extension methods -group them like an extension nuget package

## The sample from Robert "Uncle Bob" Martin

We have a parcel with 2 rectangles and 1 square and we want to create an algorithm to run some operation on them. After a time the parcel of land increase to a bigger one, but with circles, elipses or other geometrical shapes.

Let's fix our problem with *Strategy Design Pattern* .

### Raw implementation

```c#
using System;
using System.Collections;
using System.Collections.Generic;

namespace Solid.OpenClosePrinciple
{
    public class Program
        {
            static void Main(string[] args)
            {
                // We need to compute the area of a parcel of land with 2 square and 1 rectangle
    
                var parcelFromRight = new Square{Side = 3};
                var parcelFromLeft = new Square{Side = 5};
                var parcelCenter = new Rectangle{Height = 2, Width = 3};
                
                var areaParcelFromRight = ComputeArea(parcelFromRight.GetType().ToString(), parcelFromRight.Side);
                var areaFromLeft = ComputeArea(parcelFromLeft.GetType().ToString(), parcelFromLeft.Side);
                var areaParcelCenter = ComputeArea(parcelCenter.GetType().ToString(), parcelCenter.Width);
                
                var total = areaParcelFromRight + areaFromLeft + areaParcelCenter;
                
                Console.WriteLine(total); 
            }

        static int ComputeArea(string typeOfParcel, int width, int height = 0)
        {
            if(typeOfParcel.Contains("Square"))  
                return width * width;
            else if(typeOfParcel.Contains( "Rectangle"))  
                return width * height;
            else
                throw new NotImplementedException();
        }
    
        public class Rectangle
        {
            public int Height {get; set;}
            public int Width {get; set;}  
        }
    
        public class Square 
        {
            public int Side {get; set;}
    
        }
    }
}
```

Try it in [fiddle](https://dotnetfiddle.net/).

Therefore if we need to compute area for other shapes we need to add new "if" in function:

```c#
    static int ComputeArea(string typeOfParcel, int width, int height = 0)
    {
        if(typeOfParcel.Contains("Square"))  
            return width * width;
        else if(typeOfParcel.Contains( "Rectangle"))  
            return width * height;
        else if(typeOfParcel.Contains( "Circle"))  
            return 2 * height * PI;
        else if(typeOfParcel.Contains( "Elipse"))  
            return width * height;
        else if(typeOfParcel.Contains( "Curve"))  
            return width * height;                                    
        else
            throw new NotImplementedException();
    }
```

Thus, we have modified the algorithm but also all unit tests. A lot of work to do here.
The code is not readable, it is hard to maintain and hard to test. No way to have here a self explanatory code strategy!

## How to fix it - we will use *The Strategy Design Pattern*

We will have a hierachy of clases started from this interface

```c#
    public interface IShapeStrategy
    {
        void  Operation();
    }

```

For Rectangle we have to compute a perimeter:

```C#
    public class RectanglePerimeter: IShapeStrategy
    {
        public int Height {get; set;}
        public int Width {get; set;}
        
        public void Operation()
        {
            var perim = (Height * 2) + (Width * 2);
            
            Console.WriteLine($"The perimeter is {perim}.");
        }
    }

```

For Square we need an area computation operation:

```c#
    public class RectanglePerimeter: IShapeStrategy
    {
        public int Height {get; set;}
        public int Width {get; set;}
        
        public void Operation()
        {
            var perim = (Height * 2) + (Width * 2);
            
            Console.WriteLine($"The perimeter is {perim}.");
        }
    }

```

We also have a Context class which run the strategies for each concrete class:

```c#
 public class ShapeStrategyContext
 {
  public IShapeStrategy _strategy;
  
  public ShapeStrategyContext(IShapeStrategy strategy)
  {
   _strategy = strategy;
  }
  
  public void Execute()
  {
   _strategy.Operation();
  }
 }

```

Let do all operations on our parcel and for other shapes(circle, elipse, etc.) as well.

Try in fiddle:

```c#
using System;
using System.Collections;
using System.Collections.Generic;

namespace Solid.OpenClosePrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
            // We need to compute the area of a parcel of land with 2 square and 1 rectangle
   
   var parcelFromRightAria = new SquareAria{Side = 3};
   var parcelFromLeftAria = new SquareAria{Side = 5};
   var parcelCenterPerimeter = new RectanglePerimeter{Height = 2, Width = 3};
   
   List<IShapeStrategy> parcelOperations = new List<IShapeStrategy>();
   parcelOperations.Add(parcelFromRightAria);
   parcelOperations.Add(parcelFromLeftAria);
   parcelOperations.Add(parcelCenterPerimeter);
   
   ShapeStrategyContext context;
   foreach(var shape in parcelOperations)
   {
    context = new ShapeStrategyContext(shape);
    context.Execute();
   }
   
   Console.WriteLine($"All operations are set.");
        }
    }
 
    public interface IShapeStrategy
    {
        void  Operation();
    }
 
    public class RectanglePerimeter: IShapeStrategy
    {
        public int Height {get; set;}
        public int Width {get; set;}
        
        public void Operation()
        {
            var perim = (Height * 2) + (Width * 2);
            
            Console.WriteLine($"The perimeter is {perim}.");
        }
    }
 
    public class SquareAria : IShapeStrategy
    {
        public int Side {get; set;}
        
        public void Operation()
        {
            var aria = (Side * Side);
            
            Console.WriteLine($"The aria is {aria}.");
        }
    }
 
 public class ShapeStrategyContext
 {
  public IShapeStrategy _strategy;
  
  public ShapeStrategyContext(IShapeStrategy strategy)
  {
   _strategy = strategy;
  }
  
  public void Execute()
  {
   _strategy.Operation();
  }
 }
}

```

We can easely extend out module with a new shape like Circle for instance:

```c#
    public class CircleLength: IShapeStrategy
    {
        public int Radius {get; set;}
        
        public void Operation()
        {
            var length = 2* PI * Radius;
            
            Console.WriteLine($"The perimeter is {perim}.");
        }
    }

```

All our classes and functions are __closed to modification__: no need to modify the strategy class, no need to modify the existing shapes.
Therefore no need to modify the existing unit tests, only to add new ones.

## Resources

- [Check with fiddle](https://dotnetfiddle.net/)
- [Learning OCP](https://medium.com/@pablodarde/learning-the-open-closed-principle-with-the-strategy-design-pattern-933dfa04d1e8)
- [The Open-Close Principle](https://cleancoders.com/episode/clean-code-episode-10)
- [Strategy Design Pattern](https://www.tutorialspoint.com/design_pattern/strategy_pattern.htm)

### Clean Architecture References

- [Design Patterns: Elements of Reusable Object-Oriented Software](https://www.amazon.com.br/dp/0201633612/?coliid=I3BZ6YLWVQOODS&colid=33HSVS6YEB9GQ&psc=1&ref_=lv_ov_lig_dp_it_im)
- [Clean Architecture](https://www.amazon.com.br/Clean-Architecture-Craftsmans-Software-Structure/dp/0134494164/ref=pd_bxgy_img_1/146-6852552-2489063?pd_rd_w=zjy9c&pf_rd_p=4a943320-02ab-4775-ad7a-eaf57d00a244&pf_rd_r=ZKKP8CPB3JEAT1YGKPZE&pd_rd_r=6bf3a408-31a9-4080-9645-7b48a056ffa4&pd_rd_wg=NbIDx&pd_rd_i=0134494164&psc=1)
- [The Clean Architecture (The Clean Code Blog)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)