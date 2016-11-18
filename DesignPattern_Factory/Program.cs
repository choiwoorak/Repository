using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
    class Program
    {
        public static void Main()
        {
            Pizza orderPizza = Pizza.PizzaFactory(Pizza.PizzaType.Seafood);
            Console.WriteLine(orderPizza.GetPrice().ToString("C2")); // $11.50
            Console.ReadKey();
        }
    }

}

public abstract class Pizza
{
    public abstract decimal GetPrice(); // 추상 Method - 반듯이 구현해야함

    public enum PizzaType
    {
        HamMushroom, Deluxe, Seafood
    }

    // PizzaFactory Method
    public static Pizza PizzaFactory(PizzaType pizzaType)
    {
        // Parameter로 PizzaType을 넣으면 Switch에 해당하는 Class객체가 Return된다.
        // * Pizza는 Based Class(기반 클래스)이고, return값은 Inheritance Class(파생 클래스)이다. *
        // 
        switch (pizzaType)
        {
            case PizzaType.HamMushroom:
                return new HamAndMushroomPizza();

            case PizzaType.Deluxe:
                return new DeluxePizza();

            case PizzaType.Seafood:
                return new SeafoodPizza();
        }

        throw new System.NotSupportedException("The pizza type " + pizzaType.ToString() + " is not recognized.");
    }
}
/* TEST CODE
public class InheritanceClass : Pizza
{    
    // InheritanceClass는 상속된 추상 맴버 'Pizza.GetPrice()'을 구현하지 않습니다.
    // 컴파일에서 부터 에러가 발생 왜냐면 Pizza Class의 추상 Method를 구현지 않았으니깐!
} 
 */

public class HamAndMushroomPizza : Pizza
{
    private decimal price = 8.5M;
    public override decimal GetPrice() { return price; }
}

public class DeluxePizza : Pizza
{
    private decimal price = 10.5M;
    public override decimal GetPrice() { return price; }
}

public class SeafoodPizza : Pizza
{
    private decimal price = 11.5M;
    public override decimal GetPrice() { return price; }
}

// Somewhere in the code

  