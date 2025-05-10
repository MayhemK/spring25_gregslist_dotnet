namespace gregslist_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HousesController : ControllerBase
{
  public HousesController(HousesService housesService, Auth0Provider auth0Provider)
  {
    _housesService = housesService;
    _auth0Provider = auth0Provider;
  }

  private readonly HousesService _housesService;
  private readonly Auth0Provider _auth0Provider;

  [HttpGet]
  public ActionResult<List<House>> GetAllHouses()
  {
    try
    {
      List<House> houses = _housesService.GetHouses();
      return Ok(houses);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
codeworks.code-snippets
{
  "Class Template": {
    "prefix": "cs:class",
    "body": [
      "namespace $WORKSPACE_NAME.${RELATIVE_FILEPATH/([a-zA-Z0-9]+)[\\\\\\/]..+/$1/};",
      "",
      "public class $TM_FILENAME_BASE",
      "{",
      "${0}",
      "}"
    ],
    "description": "A simple starter for a C# class",
    "scope": "csharp",
    "isFileTemplate": true
  },
  "Controller Template": {
  "prefix": "cs:api_controller",
    "body": [
      "namespace $WORKSPACE_NAME.Controllers;",
      "",
      "[ApiController]",
      "[Route(\"api/[controller]\")]",
      "public class $TM_FILENAME_BASE : ControllerBase",
      "{",
      "${0}",
      "}"
    ],
    "description": "A simple starter for a C# API Controller class",
    "scope": "csharp",
    "isFileTemplate": true
  },
}
