namespace Framework.Xamariner.Interfaces
{
    public interface IDomainManager
    {
        Framework.Xaml.DomainModel CreateDomainModel();
        void RegisterViewModels();
    }
}

