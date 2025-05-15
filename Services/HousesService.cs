using gregslist.Repositories;

namespace gregslist.Services;

public class HousesService
{
  public HousesService(HousesRepository housesRepository)
  {
    _housesRepository = housesRepository;
  }
  private readonly HousesRepository _housesRepository;

  internal List<House> GetHouses()
  {
    List<House> houses = _housesRepository.GetHouses();
    return houses;
  }

  internal House GetHouseById(int houseId)
  {
    House house = _housesRepository.GetHouseById(houseId);

    if (house == null)
    {
      throw new Exception($"No house found with the id of {houseId}");
    }

    return house;
  }
}