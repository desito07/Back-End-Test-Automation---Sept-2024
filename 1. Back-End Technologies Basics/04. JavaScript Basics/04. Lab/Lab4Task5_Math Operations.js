function mathOperations(num1, num2, operator) {
  if (operator == "+") {
    console.log(num1 + num2);
  } else if (operator == "-") {
    console.log(num1 - num2);
  } else if (operator == "*") {
    console.log(num1 * num2);
  } else if (operator == "/") {
    console.log(num1 / num2);
  } else if (operator == "%") {
    console.log(num1 % num2);
  } else if (operator == "**") {
    console.log(num1 ** num2);
  }
}
mathOperations(3, 5.5, "*");
