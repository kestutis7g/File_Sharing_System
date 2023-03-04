using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers;

public abstract class BaseController : ControllerBase
{
    protected IMapper Mapper =>
        HttpContext.RequestServices.GetRequiredService<IMapper>();

    protected IConfiguration Configuration =>
        HttpContext.RequestServices.GetRequiredService<IConfiguration>();
}
