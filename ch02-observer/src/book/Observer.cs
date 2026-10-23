
public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

public interface IObserver
{
    void Update(float temperature, float humidity, float pressure);
}

public interface IDisplayElement
{
    void Display();
}

// Implementation of WeatherData

public class WeatherData : ISubject
{
    private readonly List<IObserver> observers = new();

    private float temperature;
    private float humidity;
    private float pressure;

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (IObserver observer in observers.ToArray())
        {
            observer.Update(temperature, humidity, pressure);
        }
    }

    // for WeatherStation (source from outside) to call this method
    public void SetMeasurements(
    float temperature,
    float humidity,
    float pressure)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        this.pressure = pressure;

        NotifyObservers();
    }

}