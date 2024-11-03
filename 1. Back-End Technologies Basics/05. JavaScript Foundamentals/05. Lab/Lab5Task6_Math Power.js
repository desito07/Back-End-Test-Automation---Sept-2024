function mathPower(number, power) {
    let result = 1;
  
    for (let i = 0; i < power; i++) {
      result = result * number;
    }
  
    return result;
  }