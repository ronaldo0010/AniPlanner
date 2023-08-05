using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AniPlannerApi.Controllers;

public class MediaController : ControllerBase
{
    private IUnitOfWork _uow;
    
    public MediaController(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    [HttpGet("random-one")]
    public IActionResult GetSingleMedia()
    {
        var media = _uow.MediaRepo.FindAll();
        var skip = new Random().Next(0, media.Count());
        var result = media.Skip(skip).First();
        
        return Ok(result);
    }
    
    
    [HttpGet("all")]
    public IActionResult GetAllMedia()
    {
        var media = _uow.MediaRepo.FindAll();
        
        return Ok(media.ToList());
    }
    
    
    [HttpGet("batch")]
    public IActionResult GetBatchMedia()
    {
        var media = _uow.MediaRepo.FindAll();
        var skip = new Random().Next(0, media.Count());

        var result = media
            .Skip(skip)
            .Take(10)
            .ToList();
        
        return Ok(result);
    }
}