namespace Domain.Commons;
public abstract class DataFile
{
    public Guid Id { get; set; } = new Guid();
    public required string Name { get; set; }
    public required string Url { get; set; }
    public long Size { get; set; }
    public required string Extension { get; set; }
}