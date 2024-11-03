function solve(arr) {
  let obj = {};
  for (let i = 0; i < arr.length; i++) {
    let input = arr[i].split(" ");
    let name = input[0];
    let number = input[1];
    obj[name] = number;
    // console.log(input);
  }
  for (let name in obj) {
    console.log(`${name} -> ${obj[name]}`);
  }
}
