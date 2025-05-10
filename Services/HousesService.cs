
namespace gregslist_dotnet.Services;

public class HousesService
{
  public HousesService(HousesRepository housesRepository)
  {
    _housesRepository = housesRepository;
  }
  private readonly HousesRepository _housesRepository;

  internal List<House> GetHouses()
  {
    throw new NotImplementedException();
  }
}