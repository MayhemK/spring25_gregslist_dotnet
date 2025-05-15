

namespace gregslist.Repositories;

public class HousesRepository
{
  public HousesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal List<House> GetHouses()
  {
    string sql = @"
    SELECT 
    houses.*,
    accounts.*
    FROM houses
    INNER JOIN accounts ON accounts.id = houses.creator_id;";

    List<House> houses = _db.Query(sql, (House house, Account account) =>
    {
      house.Creator = account;
      return house;
    }).ToList();
    return houses;
  }

  internal House GetHouseById(int houseId)
  {
    string sql = @"
    SELECT 
    houses.*,
    accounts.*
    FROM houses
    INNER JOIN accounts ON accounts.id = houses.creator_id
    WHERE houses.id = @houseId;";

    House foundHouse = _db.Query(sql, (House house, Account account) =>
    {
      house.Creator = account;
      return house;
    }, new { houseId }).SingleOrDefault();
    return foundHouse;
  }

  internal House CreateHouse(House houseData)
  {
    string sql = @"
    INSERT INTO
    houses (bedrooms, bathrooms, levels, year, price, description, img_url, creator_id)
    VALUES (@Bedrooms, @Bathrooms, @Levels, @Year, @Price, @Description, @ImgUrl, @CreatorId)
    
    SELECT
    houses.*,
    accounts.*
    FROM houses
    INNER JOIN accounts on accounts.id - houses.creator_id
    WHERE houses.id = LAST_INSERT_ID();";

    House createdHouse = _db.Query(sql, (House house, Account account) =>
    {
      house.Creator = account;
      return house;
    }, houseData).SingleOrDefault();
    return createdHouse;
  }
}