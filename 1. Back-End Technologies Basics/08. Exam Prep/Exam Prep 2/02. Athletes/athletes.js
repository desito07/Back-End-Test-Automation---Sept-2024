let athletes = [
  {
    id: 1,
    name: "Usain Bolt",
    sport: "Sprinting",
    medals: 8,
    country: "Jamaica",
  },
  {
    id: 2,
    name: "Michael Phelps",
    sport: "Swimming",
    medals: 23,
    country: "USA",
  },
  {
    id: 3,
    name: "Simone Biles",
    sport: "Gymnastics",
    medals: 7,
    country: "USA",
  },
];

const olympics = solve(athletes);

function solve(athletes) {
  function getAthletesBySport(sport) {
    return athletes.filter((athlete) => athlete.sport === sport);
  }

  function addAthlete(id, name, sport, medals, country) {
    athletes.push({ id, name, sport, medals, country });
    return athletes;
  }

  function getAthleteById(id) {
    const athlete = athletes.find((athlete) => athlete.id === id);
    return athlete ? athlete : `Athlete with ID ${id} not found`;
  }

  function removeAthleteById(id) {
    const index = athletes.findIndex((athlete) => athlete.id === id);
    if (index !== -1) {
      athletes.splice(index, 1);
      return athletes;
    } else {
      return `Athlete with ID ${id} not found`;
    }
  }

  function updateAthleteMedals(id, newMedals) {
    const athlete = athletes.find((athlete) => athlete.id === id);
    if (athlete) {
      athlete.medals = newMedals;
      return athletes;
    } else {
      return `Athlete with ID ${id} not found`;
    }
  }

  function updateAthleteCountry(id, newCountry) {
    const athlete = athletes.find((athlete) => athlete.id === id);
    if (athlete) {
      athlete.country = newCountry;
      return athletes;
    } else {
      return `Athlete with ID ${id} not found`;
    }
  }

  return {
    getAthletesBySport,
    addAthlete,
    getAthleteById,
    removeAthleteById,
    updateAthleteMedals,
    updateAthleteCountry,
  };
}
console.log(olympics.updateAthleteCountry(10, "Canada"));
