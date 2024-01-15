namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMessageRepository MessageRepository { get; }
        ILikesRepository LikesRepository { get; }
        IAssetRepository AssetRepository { get; }
        Task<bool> Complete(); //to complete that 2 or 3 transactions are done, if not then no
        bool HasChanges(); //if context has changes which is tracking
    }
}
