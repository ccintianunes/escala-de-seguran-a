namespace EscalaSeguranca.Repositories;

public interface IUnitOfWork : IDisposable
{
    IPolicialRepository PolicialRepository { get; }
    IEscalaRepository EscalaRepository { get; }
    ILocalRepository LocalRepository { get; }
    IMarcacaoEscalaRepository MarcacaoEscalaRepository { get; }

    int Complete();
    Task<int> CompleteAsync();
}
