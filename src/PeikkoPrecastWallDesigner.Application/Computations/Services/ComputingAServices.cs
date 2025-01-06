using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Exceptions;
using PeikkoPrecastWallDesigner.Application.Common.DTOs;
using PeikkoPrecastWallDesigner.Domain.Infrastructure.External.MessageBrokers;
using PeikkoPrecastWallDesigner.Domain.Infrastructure.Persistence.Repositories;
using PeikkoPrecastWallDesigner.Application.Computations.Mappers;
using PeikkoPrecastWallDesigner.Application.Computations.DTOs;
using PeikkoPrecastWallDesigner.Domain.Services.Computations;
using System.Text.Json;

namespace PeikkoPrecastWallDesigner.Application.Computations.Services
{
	/// <summary>
	/// Computing Application Service
	/// </summary>
	public class ComputingAService : IComputingAService
	{
		private readonly IComputingDService _compService;
		private readonly IMessageSender<LayerLoadsComputingResultDto> _messageSender;
		private readonly IComputingResultRepository _compResRepository;

		public ComputingAService(
			IComputingDService computingService,
			IMessageSender<LayerLoadsComputingResultDto> messageSender,
			IComputingResultRepository resultRepository)
		{
			_compService = computingService;
			_messageSender = messageSender;
			_compResRepository = resultRepository;
		}

		public async Task<ComputingResultDto> ComputeLayerLoadsAsync(LayersDto data)
		{
			try
			{
				var layers = LayersMapper.ToEntity(data);
				_compService.GeometryValidation(layers);
				var compResult = new ComputingResult
				{
					Id = Guid.NewGuid(),
					Value = string.Empty,
					Status = "Processing",
					CreatedAt = DateTime.UtcNow
				};
				var compResultDto = new LayerLoadsComputingResultDto
				{
					Id = compResult.Id,
					Value = JsonSerializer.Serialize(layers),
					Status = compResult.Status
				};

				try
				{
					await _messageSender.SendMessageAsync(compResultDto);
				}
				catch (Exception ex)
				{
					compResult.Status = "Unavailable";
					await _compResRepository.UpdateAsync(compResult);
					throw new Exception("Computing service is unavailable", ex);
				}
				return (ComputingResultMapper.ToDto(compResult));
			}
			catch (GeometryValidationException ex)
			{
				throw new Exception("Validation failed: " + ex.Message);
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while computing layer loads.", ex);
			}
		}

		public async Task<ComputingResultDto> ComputeLayerLoadsBackgroundAsync(LayersDto data)
		{
			try
			{
				var layers = LayersMapper.ToEntity(data);
				_compService.GeometryValidation(layers);

				var compResult = new ComputingResult
				{
					Id = Guid.NewGuid(),
					Value = string.Empty,
					Status = "Processing",
					CreatedAt = DateTime.UtcNow
				};
				await _compResRepository.AddAsync(compResult);

				var compResultDto = new LayerLoadsComputingResultDto
				{
					Id = compResult.Id,
					Value = JsonSerializer.Serialize(layers),
					Status = compResult.Status
				};

				_ = Task.Run(async () =>
				{
					try
					{
						await Task.Yield();
						var computationResult = _compService.ComputeLoads(layers);

						compResult.Value = JsonSerializer.Serialize(computationResult);
						compResult.Status = "Completed";
						await _compResRepository.UpdateAsync(compResult);
					}
					catch (Exception ex)
					{
						compResult.Status = "Failed";
						compResult.Value = JsonSerializer.Serialize(new { Error = ex.Message });
						await _compResRepository.UpdateAsync(compResult);
					}
				});

				return ComputingResultMapper.ToDto(compResult);
			}
			catch (GeometryValidationException ex)
			{
				throw new Exception("Validation failed: " + ex.Message);
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while computing layer loads.", ex);
			}
		}


		public async Task<ComputingResultDto?> GetComputingResultAsync(Guid id)
		{
			try
			{
				var compRes = await _compResRepository.GetByIdAsync(id);
				if (compRes == null)
					return (null);
				else
					return (ComputingResultMapper.ToDto(compRes));
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while getting data.", ex);
			}
		}
		public async Task<bool> PatchComputingResultAsync(ComputingResultDto dto)
		{
			try
			{
				var existingEntity = await _compResRepository.GetByIdAsync(dto.Id);
				if (existingEntity == null)
					return (false);
				var patchOperationList = new Dictionary<string, object>
				{
					{"/value", dto.Value},
					{"/status", dto.Status}
				};
				
				await _compResRepository.PatchAsync(dto.Id, patchOperationList);
				return (true);
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while patching data.", ex);
			}
		}
	}
}
