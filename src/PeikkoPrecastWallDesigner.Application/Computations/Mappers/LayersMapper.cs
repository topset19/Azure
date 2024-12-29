using System;
using PeikkoPrecastWallDesigner.Application.Common.DTOs;
using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Enums;
using PeikkoPrecastWallDesigner.Domain.Exceptions;

namespace PeikkoPrecastWallDesigner.Application.Computations.Mappers
{
	public static class LayersMapper
	{
		public static Layers ToEntity(LayersDto dto)
		{
			var internalLayer = new Layer(
				dto.InternalLayer.Name,
				dto.InternalLayer.X,
				dto.InternalLayer.Y,
				dto.InternalLayer.Width,
				dto.InternalLayer.Height,
				dto.InternalLayer.Thickness);

			var externalLayer = new Layer(
				dto.ExternalLayer.Name,
				dto.ExternalLayer.X,
				dto.ExternalLayer.Y,
				dto.ExternalLayer.Width,
				dto.ExternalLayer.Height,
				dto.ExternalLayer.Thickness);

			var insulatedLayer = new InsulatedLayer(
				name: "InsulatedLayer",
				thickness: dto.InsulatedLayerThickness,
				iLayer: internalLayer,
				eLayer: externalLayer);
			if (!Enum.TryParse<EHolePosition>(dto.Hole.Position, out var holePosition))
			{
				throw new GeometryValidationException(
					"Invalid hole position",
					EGeometryValidationExceptionType.HolePositionInvalid);
			}
			var hole = new Hole(
				dto.Hole.Name,
				dto.Hole.X,
				dto.Hole.Y,
				dto.Hole.Width,
				dto.Hole.Height,
				holePosition,
				internalLayer,
				externalLayer,
				insulatedLayer);

			return new Layers(internalLayer, externalLayer, insulatedLayer, hole);
		}

		public static LayersDto ToDto(Layers domain)
		{
			return new LayersDto
			{
				InternalLayer = new LayerDto
				{
					Name = domain.InternalLayer.Name,
					X = domain.InternalLayer.X,
					Y = domain.InternalLayer.Y,
					Width = domain.InternalLayer.Width,
					Height = domain.InternalLayer.Height,
					Thickness = domain.InternalLayer.Thickness
				},
				ExternalLayer = new LayerDto
				{
					Name = domain.ExternalLayer.Name,
					X = domain.ExternalLayer.X,
					Y = domain.ExternalLayer.Y,
					Width = domain.ExternalLayer.Width,
					Height = domain.ExternalLayer.Height,
					Thickness = domain.ExternalLayer.Thickness
				},
				InsulatedLayerThickness = domain.InsulatedLayer.Thickness,
				Hole = new HoleDto
				{
					Name = domain.Hole.Name,
					X = domain.Hole.X,
					Y = domain.Hole.Y,
					Width = domain.Hole.Width,
					Height = domain.Hole.Height,
					Position = domain.Hole.Position.ToString()
				}
			};
		}
	}
}
