function printObjectProperties(obj) {
    for (let key in obj) {
      console.log(`${key} -> ${obj[key]}`);
    }
  }