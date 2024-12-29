using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Amqp.Framing;
using PeikkoPrecastWallDesigner.Application.Common.DTOs;
using PeikkoPrecastWallDesigner.Application.Computations.Mappers;
using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Enums;
using PeikkoPrecastWallDesigner.Domain.Exceptions;
using PeikkoPrecastWallDesigner.Domain.Service.Computations;


namespace PeikkoPrecastWallDesigner.UnitTests.Application.Computations
{
	public class ComputingAServiceTests
	{
		private readonly ComputingDService _compService;

		public ComputingAServiceTests()
		{
			_compService = new ComputingDService();
		}

		[Fact]
		public void GeometryValidation_Valid_Data()
		{

			var input = new LayersDto
			{
				InternalLayer = new LayerDto
				{
					Name = "InternalLayer",
					X = 0,
					Y = 0,
					Width = 3400,
					Height = 2600,
					Thickness = 120,
				},
				ExternalLayer = new LayerDto
				{
					Name = "ExternalLayer",
					X = -100,
					Y = 0,
					Width = 3600,
					Height = 2800,
					Thickness = 80,
				},
				InsulatedLayerThickness = 90,
				Hole = new HoleDto
				{
					Name = "Hole",
					X = 1000,
					Y = 800,
					Width = 1200,
					Height = 1200,
					Position = "Both"
				}
			};

			var layers = LayersMapper.ToEntity(input);

			var exception = Record.Exception(() => _compService.GeometryValidation(layers));

			Assert.Null(exception);
		}


		[Theory]
		[InlineData("Internal")]
		[InlineData("External")]
		[InlineData("Both")]
		public void GeometryValidation_Valid_HolePosition(string position)
		{
			var input = new LayersDto
			{
				InternalLayer = new LayerDto
				{
					Name = "InternalLayer",
					X = 0,
					Y = 0,
					Width = 3400,
					Height = 2600,
					Thickness = 120,
				},
				ExternalLayer = new LayerDto
				{
					Name = "ExternalLayer",
					X = -100,
					Y = 0,
					Width = 3600,
					Height = 2800,
					Thickness = 80,
				},
				InsulatedLayerThickness = 90,
				Hole = new HoleDto
				{
					Name = "Hole",
					X = 1000,
					Y = 800,
					Width = 1200,
					Height = 1200,
					Position = position
				}
			};
			var layers = LayersMapper.ToEntity(input);

			var exception = Record.Exception(() => _compService.GeometryValidation(layers));

			Assert.Null(exception);
		}

		[Theory]
		[InlineData(39)]
		[InlineData(391)]
		[InlineData(-10)]
		public void GeometryValidation_InsulationThickness_Invalid(int insulatedLayerThickness)
		{
			var input = new LayersDto
			{
				InternalLayer = new LayerDto
				{
					Name = "InternalLayer",
					X = 0,
					Y = 0,
					Width = 3400,
					Height = 2600,
					Thickness = 120,
				},
				ExternalLayer = new LayerDto
				{
					Name = "ExternalLayer",
					X = -100,
					Y = 0,
					Width = 3600,
					Height = 2800,
					Thickness = 80,
				},
				InsulatedLayerThickness = insulatedLayerThickness,
				Hole = new HoleDto
				{
					Name = "Hole",
					X = 1000,
					Y = 800,
					Width = 1200,
					Height = 1200,
					Position = "Both"
				}
			};
			

			var exception = Assert.Throws<GeometryValidationException>(() =>
			{
				var layers = LayersMapper.ToEntity(input);
				_compService.GeometryValidation(layers);
			});

			Assert.Equal(EGeometryValidationExceptionType.InsulationThicknessInvalid, exception.ExceptionType);
		}

		[Theory]
		[InlineData(1000, -1000)]
		[InlineData(1600, -1000)]
		public void GeometryValidation_CenterGravity_OutOfBoundsd(double X1, double X2)
		{
			var input = new LayersDto
			{
				InternalLayer = new LayerDto
				{
					Name = "InternalLayer",
					X = X1,
					Y = 0,
					Width = 3400,
					Height = 2600,
					Thickness = 120,
				},
				ExternalLayer = new LayerDto
				{
					Name = "ExternalLayer",
					X = X2,
					Y = 0,
					Width = 3600,
					Height = 2800,
					Thickness = 80,
				},
				InsulatedLayerThickness = 90,
				Hole = new HoleDto
				{
					Name = "Hole",
					X = 1000,
					Y = 800,
					Width = 1200,
					Height = 1200,
					Position = "Both"
				}
			};

			var exception = Assert.Throws<GeometryValidationException>(() =>
			{
				var layers = LayersMapper.ToEntity(input);
				_compService.GeometryValidation(layers);
			});

			Assert.Equal(EGeometryValidationExceptionType.CenterGravityOutOfBounds, exception.ExceptionType);
		}

		[Theory]
		[InlineData("")]
		[InlineData("InvalidString")]
		public void GeometryValidation_HolePosition_Invalid(string position)
		{
			var input = new LayersDto
			{
				InternalLayer = new LayerDto
				{
					Name = "InternalLayer",
					X = 0,
					Y = 0,
					Width = 3400,
					Height = 2600,
					Thickness = 120,
				},
				ExternalLayer = new LayerDto
				{
					Name = "ExternalLayer",
					X = -100,
					Y = 0,
					Width = 3600,
					Height = 2800,
					Thickness = 80,
				},
				InsulatedLayerThickness = 90,
				Hole = new HoleDto
				{
					Name = "Hole",
					X = 1000,
					Y = 800,
					Width = 1200,
					Height = 1200,
					Position = position
				}
			};

			var exception = Assert.Throws<GeometryValidationException>(() =>
			{
				var layers = LayersMapper.ToEntity(input);
				_compService.GeometryValidation(layers);
			});

			Assert.Equal(EGeometryValidationExceptionType.HolePositionInvalid, exception.ExceptionType);
		}
	}
}