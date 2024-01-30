namespace API.Domain;

public class Location
{
    public Guid Id { get; set; }

    /// <summary>
    ///     The location code.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    ///     The location name.
    /// </summary>
    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}