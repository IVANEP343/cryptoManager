namespace CryptoManager.DTOs.Client
{
    /// <summary>
    /// DTO de salida para devolver información de cliente.
    /// </summary>
    public class ClientDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;

        public ClientDto(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
