using Microsoft.AspNetCore.Mvc;


namespace ContendoAdmin.Api;

// [Authorize(AuthenticationSchemes = "Bearer")]
// [EnableCors("APIPolicy")]
[ApiController]
[Route(@"api/[controller]")]
public class BaseController : ControllerBase
{

}