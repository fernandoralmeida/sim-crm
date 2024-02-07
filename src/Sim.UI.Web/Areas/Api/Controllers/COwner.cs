using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class COwners : ControllerBase
{
    private readonly IAppServiceSecretaria _owner;
    public COwners(IAppServiceSecretaria owner)
    {
        _owner = owner;
    }

    [HttpGet("owner")]
    public async Task<IActionResult> GetOwners()
    {
        return Ok(await _owner.DoListHierarquia1Async(await _owner.DoListAsync()));
    }

    [HttpGet("owner/{d}")]
    public async Task<IActionResult> GetSubOwners([FromRoute] string d)
    {
        return Ok(await _owner.DoListHierarquia2from1Async(await _owner.DoListAsync(), Guid.Parse(d)));
    }

    [HttpGet("owner/{d}/{s}")]
    public async Task<IActionResult> GetSubOwners([FromRoute] string d, [FromRoute] string s)
    {
        return Ok(await _owner.DoListAsync(i => i.Id == Guid.Parse(s)));
    }



}