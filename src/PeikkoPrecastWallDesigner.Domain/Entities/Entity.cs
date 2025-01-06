namespace PeikkoPrecastWallDesigner.Domain.Entities;

public abstract class Entity<Tid>
{
	public Tid Id { get; set; }
}
