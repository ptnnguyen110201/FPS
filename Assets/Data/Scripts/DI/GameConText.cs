public class GameContext
{
    public static GameContext Instance { get; } = new GameContext();
    public DIContainer Container { get; private set; } = new DIContainer();

    public void SetDIContainer(DIContainer container) =>  this.Container = container;
    
    public T Resolve<T>() => this.Container.Resolve<T>();
    public void InjectInto(object obj) => this.Container.InjectInto(obj);
}

