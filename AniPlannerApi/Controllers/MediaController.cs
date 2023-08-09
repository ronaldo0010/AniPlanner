using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AniPlannerApi.Controllers;

public class MediaController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    
    public MediaController(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    [HttpGet("random-one")]
    public async Task<IActionResult> GetSingleMedia()
    {
        var media = _uow.MediaRepo.FindAll();
        var skip = new Random().Next(0, media.Count());
        var result = await media.Skip(skip).FirstAsync();
        
        return Ok(result);
    }
    
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllMedia()
    {
        var media = _uow.MediaRepo.FindAll();
        var result = await media.ToListAsync();
        
        return Ok(result);
    }
    
    
    [HttpGet("batch")]
    public async Task<IActionResult> GetBatchMedia()
    {
        var media = _uow.MediaRepo.FindAll();
        var skip = new Random().Next(0, media.Count());

        var result = await media
            .Skip(skip)
            .Take(10)
            .ToListAsync();
        
        return Ok(result);
    }
}