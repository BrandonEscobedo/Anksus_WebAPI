namespace TestAnskus.Client.Utility.Interfaces.Observer
{
    public interface IPublisher
    {
        void Notify();
        void AddObserver();
        void RemoveObserver();

    }
}
