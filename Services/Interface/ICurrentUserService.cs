namespace ExakisNeliteTSP.Services.Interface
{
    public interface ICurrentUserService
    {
        string Oid { get; }
        string Email { get; }
        string FullName { get; }
    }
}
