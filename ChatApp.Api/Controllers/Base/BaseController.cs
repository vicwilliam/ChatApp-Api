using ChatApp.Api.Extensions;
using ChatApp.Application.Service.Base;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers.Base;

public class BaseController<T> : Controller where T : class
{
    protected readonly IBaseService<T> _baseService;
    protected readonly IHttpContextAccessor _contextAccessor;
    protected Guid UserId;

    public BaseController(IBaseService<T> baseService, IHttpContextAccessor contextAccessor)
    {
        _baseService = baseService;
        _contextAccessor = contextAccessor;
        GetUserId();
    }

    protected Guid GetUserId()
    {
        UserId = _contextAccessor?
            .HttpContext?.User?.Identity?.GetUserId() ?? Guid.Empty;
        return UserId;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAll()
    {
        IQueryable<T> query = _baseService.GetAll();

        var pageResult = query.ToList();

        if (pageResult.Count == 0)
            return NoContent();

        return Ok(pageResult);
    }
}