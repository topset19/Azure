namespace PeikkoPrecastWallDesigner.Domain.Entities;

public class Layer
{
	public string Name { get; set; }
	public double X { get; set; }
	public double Y { get; set; }
	public double Width { get; set; }
	public double Height { get; set; }
	public double Thickness { get; set; }

	public Layer(string name, double x, double y, double width, double height, double thickness)
	{
		Name = name;
		X = x;
		Y = y;
		Width = width;
		Height = height;
		Thickness = thickness;
	}
}
