let cars = [
  {
    id: 1,
    brand: "Toyota",
    model: "Corolla",
    year: 2020,
    price: 20000,
    inStock: true,
  },
  {
    id: 2,
    brand: "Honda",
    model: "Civic",
    year: 2019,
    price: 22000,
    inStock: true,
  },
  {
    id: 3,
    brand: "Ford",
    model: "Mustang",
    year: 2021,
    price: 35000,
    inStock: false,
  },
];

const dealership = solve(cars);

function solve(cars) {
  // Filter cars by brand
  function getCarsByBrand(brand) {
    return cars.filter((car) => car.brand === brand);
  }

  // Add a new car
  function addCar(id, brand, model, year, price, inStock) {
    cars.push({ id, brand, model, year, price, inStock });
    return cars;
  }

  // Find a car ID
  function getCarById(id) {
    const car = cars.find((car) => car.id === id);
    if (car) {
      return car;
    } else {
      return `Car with ID ${id} not found`;
    }
  }

  // Remove a car by ID
  function removeCarById(id) {
    const i = cars.findIndex((car) => car.id === id);
    if (i !== -1) {
      cars.splice(i, 1);
      return cars;
    } else {
      return `Car with ID ${id} not found`;
    }
  }

  // Update car price
  function updateCarPrice(id, newPrice) {
    const car = cars.find((car) => car.id === id);
    if (car) {
      car.price = newPrice;
      return cars;
    } else {
      return `Car with ID ${id} not found`;
    }
  }

  // Update car stock
  function updateCarStock(id, inStock) {
    const car = cars.find((car) => car.id === id);
    if (car) {
      car.inStock = inStock;
      return cars;
    } else {
      return `Car with ID ${id} not found`;
    }
  }

  return {
    getCarsByBrand,
    addCar,
    getCarById,
    removeCarById,
    updateCarPrice,
    updateCarStock,
  };
}

console.log(JSON.stringify(dealership.removeCarById(3)));
