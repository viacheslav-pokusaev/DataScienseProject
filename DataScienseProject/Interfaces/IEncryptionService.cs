namespace DataScienseProject.Interfaces
{
    public interface IEncryptionService
    {
        string EncryptPassword(string pass);
        string DescryptPassword(string pass);
    }
}
