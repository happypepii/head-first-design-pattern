// ===== 行為介面 =====
interface IFlyBehavior { void Fly(); }
interface IQuackBehavior { void Quack(); }

// ===== 飛行行為 =====
class FlyWithWings : IFlyBehavior
{
    public void Fly() => Console.WriteLine("I'm flying!!");
}

class FlyNoWay : IFlyBehavior
{
    public void Fly() => Console.WriteLine("I can't fly");
}

class FlyRocketPowered : IFlyBehavior
{
    public void Fly() => Console.WriteLine("I'm flying with a rocket!");
}

// ===== 叫聲行為 =====
class QuackSound : IQuackBehavior
{
    public void Quack() => Console.WriteLine("Quack");
}

class Squeak : IQuackBehavior
{
    public void Quack() => Console.WriteLine("Squeak");
}

class MuteQuack : IQuackBehavior
{
    public void Quack() => Console.WriteLine("<< Silence >>");
}

// ===== Duck 基底類別 =====
abstract class Duck
{
    protected IFlyBehavior flyBehavior;
    protected IQuackBehavior quackBehavior;
    // 子類別忘了賦值，PerformFly()/PerformQuack() 就會 NullReferenceException，編譯器完全不會提醒。可以在建構子注入
    public void SetFlyBehavior(IFlyBehavior fb) => flyBehavior = fb;
    public void SetQuackBehavior(IQuackBehavior qb) => quackBehavior = qb;

    public void PerformFly() => flyBehavior.Fly();
    public void PerformQuack() => quackBehavior.Quack();

    public void Swim() => Console.WriteLine("All ducks float, even decoys!");
    public abstract void Display();
}

// ===== 各種鴨子 =====
class MallardDuck : Duck
{
    public MallardDuck()
    {
        flyBehavior = new FlyWithWings();
        quackBehavior = new QuackSound();
    }
    public override void Display() => Console.WriteLine("I'm a real Mallard duck");
}

class DecoyDuck : Duck
{
    public DecoyDuck()
    {
        flyBehavior = new FlyNoWay();
        quackBehavior = new MuteQuack();
    }
    public override void Display() => Console.WriteLine("I'm a decoy duck");
}

class RubberDuck : Duck
{
    public RubberDuck()
    {
        flyBehavior = new FlyNoWay();
        quackBehavior = new Squeak();
    }
    public override void Display() => Console.WriteLine("I'm a rubber duck");
}

class ModelDuck : Duck
{
    public ModelDuck()
    {
        flyBehavior = new FlyNoWay();
        quackBehavior = new QuackSound();
    }
    public override void Display() => Console.WriteLine("I'm a model duck");
}

// ===== 測試 =====
class Program
{
    static void Main()
    {
        Duck mallard = new MallardDuck();
        mallard.Display();
        mallard.PerformFly();
        mallard.PerformQuack();

        Duck rubber = new RubberDuck();
        rubber.Display();
        rubber.PerformFly();
        rubber.PerformQuack();

        Duck decoy = new DecoyDuck();
        decoy.Display();
        decoy.PerformFly();
        decoy.PerformQuack();

        // 執行期動態換行為!!!!
        Duck model = new ModelDuck();
        model.Display();
        model.PerformFly();                          // I can't fly
        model.SetFlyBehavior(new FlyRocketPowered());
        model.PerformFly();                          // I'm flying with a rocket!
    }
}