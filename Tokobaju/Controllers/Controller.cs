using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tokobaju.Controllers;

[ApiController]
[Authorize]
public class Controller : ControllerBase
{
}