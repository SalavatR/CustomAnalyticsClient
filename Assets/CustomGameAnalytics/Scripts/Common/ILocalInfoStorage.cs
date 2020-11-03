namespace CustomGameAnalytics.Scripts.Common
{
    public interface ILocalInfoStorage<T>
    {
        void Save(T obj);
        T Load();

    }
}