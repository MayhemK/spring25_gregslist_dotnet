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

  internal House CreateHouse(House houseData)
  {
    House house = _housesRepository.CreateHouse(houseData);
    return house;
  }

  internal House UpdateHouse(int houseId, House houseUpdateData, Account userInfo)
  {
    House house = GetHouseById(houseId);
    if (house.CreatorId != userInfo.Id)
    {
      throw new Exception("You are not allowed to update someone else's house!");
    }

    house.Bedrooms = houseUpdateData.Bedrooms ?? house.Bedrooms;
    house.Bathrooms = houseUpdateData.Bathrooms ?? house.Bathrooms;
    house.Levels = houseUpdateData.Levels ?? house.Levels;
    house.Price = houseUpdateData.Price ?? house.Price;
    house.Description = houseUpdateData.Description ?? house.Description;
    house.ImgUrl = houseUpdateData.ImgUrl ?? house.ImgUrl;

    _housesRepository.UpdateHouse(house);
    return house;
  }

  internal string DeleteHouse(int houseId, Account userInfo)
  {
    House house = GetHouseById(houseId);
    if (house.CreatorId != userInfo.Id)
    {
      throw new Exception("You are not allowed to delete this House!");
    }
    _housesRepository.DeleteHouse(houseId);
    return "Your House has been deleted!";
  }
}