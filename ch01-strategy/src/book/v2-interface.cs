// v2-interface.cs
var ducks = new List<Duck>
{
    new MallardDuck(),
    new RedheadDuck(),
    new RubberDuck(),
    new DecoyDuck(),
    new WoodenDuck(),
};

foreach (var duck in ducks)
{
    Console.WriteLine($"[{duck.GetType().Name}]");
    duck.Display();
    if (duck is IQuackable q) q.Quack();   // 新問題:呼叫端得自己判斷有沒有這個能力
    if (duck is IFlyable f) f.Fly();
    Console.WriteLine();
}

interface IFlyable  { void Fly(); }
interface IQuackable { void Quack(); }

abstract class Duck
{
    public void Swim() { Console.WriteLine("swim"); }
    public abstract void Display();
}

class MallardDuck : Duck, IFlyable, IQuackable
{
    public override void Display() { Console.WriteLine("mallard"); }

    // 問題 5:跟 RedheadDuck 的 Fly()/Quack() 完全一樣,只能複製貼上
    public void Fly()   { Console.WriteLine("fly with wings"); }
    public void Quack() { Console.WriteLine("quack"); }
}

class RedheadDuck : Duck, IFlyable, IQuackable
{
    public override void Display() { Console.WriteLine("redhead"); }

    // 問題 5:同上。若要改飛行方式,這裡和 MallardDuck 都得各改一次
    public void Fly()   { Console.WriteLine("fly with wings"); }
    public void Quack() { Console.WriteLine("quack"); }
}

class RubberDuck : Duck, IQuackable
{
    public override void Display() { Console.WriteLine("rubber"); }

    // 改善:不需要再寫空的 Fly()
    public void Quack() { Console.WriteLine("squeak"); }
}

class DecoyDuck : Duck
{
    public override void Display() { Console.WriteLine("decoy"); }
    // 改善:不實作任何介面,就是不會飛也不會叫
}

class WoodenDuck : Duck
{
    public override void Display() { Console.WriteLine("wooden"); }
    // 改善:v1 忘了覆寫 Fly() 會飛起來;
    //       這裡「忘了」只會變成不會飛,不會靜悄悄出錯
}

// 問題 2:仍然存在。實作哪些介面在 class 定義時就固定了,
//         執行期無法讓一隻鴨子換飛行方式