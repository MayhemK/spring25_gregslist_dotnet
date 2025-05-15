


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

  internal void UpdateHouse(House house)
  {
    string sql = @"
    UPDATE houses
    SET
    bedrooms = @Bedrooms,
    bathrooms = @Bathrooms,
    levels = @Levels,
    price = @Price,
    description = @Description,
    img_url = @ImgUrl
    WHERE id = @Id
    LIMIT 1;";

    int rowsAffected = _db.Execute(sql, house);
    if (rowsAffected > 1)
    {
      throw new Exception(rowsAffected + " rows were updated, ERROR!");
    }
  }
}