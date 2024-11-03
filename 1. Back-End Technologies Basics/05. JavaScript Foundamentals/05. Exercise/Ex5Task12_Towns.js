function parseTowns(townsArray) {
  townsArray.forEach((townInfo) => {
    // Split the town info by the delimiter " | "
    let [town, latitude, longitude] = townInfo.split(" | ");

    // Parse latitude and longitude to numbers and format to two decimal places
    latitude = Number(latitude).toFixed(2);
    longitude = Number(longitude).toFixed(2);

    // Create an object and print it in the desired format
    let townObject = {
      town: town,
      latitude: latitude,
      longitude: longitude,
    };

    console.log(townObject);
  });
}
