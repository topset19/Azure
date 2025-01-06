namespace PeikkoPrecastWallDesigner.Domain.Entities;

public class ComputingResult : Entity<Guid>
{
	public string Value { get; set; }
	public string Status { get; set; }
	public DateTime CreatedAt { get; set; }
}
