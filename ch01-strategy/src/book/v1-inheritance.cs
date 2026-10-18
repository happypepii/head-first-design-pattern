// v1-inheritance.cs
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
    duck.Quack();
    duck.Fly();
    Console.WriteLine();
}

abstract class Duck
{
    // 問題 4:如果在這裡新增 Dive(),所有鴨子都會多一個 Dive(),
    //         包含不會潛水的橡皮鴨,每個子類別都得回頭檢查
    public virtual void Quack() { Console.WriteLine("quack"); }
    public virtual void Fly()   { Console.WriteLine("fly"); }
    public void Swim()          { Console.WriteLine("swim"); }
    public abstract void Display();
}

class MallardDuck : Duck
{
    public override void Display() { Console.WriteLine("mallard"); }
}

class RedheadDuck : Duck
{
    public override void Display() { Console.WriteLine("redhead"); }
}

class RubberDuck : Duck
{
    public override void Display() { Console.WriteLine("rubber"); }

    // 問題 1:為了「改變行為」,必須覆寫 ,一個可以 ,但如果有千千萬萬個呢?
    public override void Quack() { Console.WriteLine("squeak"); }
    public override void Fly() { }   // 問題 3:空覆寫,呼叫端看不出是「不會飛」還是「忘了寫」
}

class DecoyDuck : Duck
{
    public override void Display() { Console.WriteLine("decoy"); }

    // 問題 1:跟 RubberDuck.Fly() 一模一樣,重複的程式碼無法共用
    public override void Quack() { }
    public override void Fly() { }
}

class WoodenDuck : Duck
{
    public override void Display() { Console.WriteLine("wooden"); }

    public override void Quack() { }
    // 問題 3:忘了覆寫 Fly(),木鴨飛起來了,編譯器不會警告
}

// 問題 2:行為在 class 定義時就固定了。
//         想讓 RubberDuck 在某個條件下變成會飛,這裡做不到,
//         只能新增另一個子類別,但已經建立的物件無法換