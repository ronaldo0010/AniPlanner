using Contracts;
using Entities.Dtos;
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
        var media = await _uow.MediaRepo.FindBatchWithTagsAsync();
        var result = new List<MediaDto>();

        foreach (var entity in media)
        {
            var mediaTags = entity.MediaTags
                .Select(x => x.Tag)
                .ToList();

            var tagsDto = mediaTags.SelectMany(x => new[]
            {
                new TagDto { Name = x.Name, TagId = x.TagId }
            }).ToList();
            
            result.Add(new MediaDto
            {
                MediaId = entity.MediaId,
                Type = entity.Type,
                Title = entity.Title,
                Status = entity.Status,
                PictureUrl = entity.PictureUrl,
                Episodes = entity.Episodes,
                MediaTags = tagsDto
            });
        }

        return Ok(result);
    }
}