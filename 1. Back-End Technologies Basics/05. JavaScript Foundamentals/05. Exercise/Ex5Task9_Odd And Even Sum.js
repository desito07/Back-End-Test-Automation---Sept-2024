function oddAndEvenSum(number) {
  let oddSum = 0;
  let evenSum = 0;

  // Convert number to a string to iterate through each digit
  let numStr = number.toString();

  // Iterate through each digit
  for (let digit of numStr) {
    let currentDigit = Number(digit);

    // Check if the digit is even or odd and add it to the respective sum
    if (currentDigit % 2 === 0) {
      evenSum += currentDigit;
    } else {
      oddSum += currentDigit;
    }
  }

  // Output the results
  console.log(`Odd sum = ${oddSum}, Even sum = ${evenSum}`);
}