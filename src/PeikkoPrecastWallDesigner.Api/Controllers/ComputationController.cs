using Microsoft.AspNetCore.Mvc;
using PeikkoPrecastWallDesigner.Application.Common.DTOs;
using PeikkoPrecastWallDesigner.Application.Computations.DTOs;
using PeikkoPrecastWallDesigner.Application.Computations.Services;

namespace PeikkoPrecastWallDesigner.Controllers
{
	[Route("api/computations")]
    [ApiController]
    public class ComputationController : ControllerBase
    {
        private readonly ComputingAService _compAppService;

        public ComputationController(ComputingAService computeAppService)
        {
			_compAppService = computeAppService;
        }

		[HttpGet("test")]
		public async Task<IActionResult> TestEndpoint()
		{
			return Ok("Success");
		}

		[HttpPost("loads")]
		public async Task<IActionResult> ComputeLayerLoads([FromBody] LayersDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				var res = await _compAppService.ComputeLayerLoadsAsync(model);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Error = "An unexpected error occurred.", Message = ex.Message });
			}
		}

		[HttpGet("loads/{id}")]
		public async Task<IActionResult> GetLoadResult(Guid id)
		{
			try
			{
				var res = await _compAppService.GetComputingResultAsync(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Error = "An unexpected error occurred.", Message = ex.Message });
			}
		}
		[HttpPatch("loads")]
		public async Task<IActionResult> PatchLoadResult([FromBody] ComputingResultDto dto)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);
				if (dto.Id == Guid.Empty)
					return BadRequest(new { Error = "The ID field is invalid." });
				await _compAppService.PatchComputingResultAsync(dto);
				return Ok("Success");
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Error = "An unexpected error occurred.", Message = ex.Message });
			}
		}
	}
}
