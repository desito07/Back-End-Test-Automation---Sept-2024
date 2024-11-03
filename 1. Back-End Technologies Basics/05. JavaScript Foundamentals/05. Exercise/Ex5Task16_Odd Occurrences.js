function oddOccurrences(input) {
  let words = input.toLowerCase().split(" "); // Convert input to lowercase and split into words
  let wordCounts = {};

  // Count occurrences of each word
  words.forEach((word) => {
    if (wordCounts[word]) {
      wordCounts[word] += 1;
    } else {
      wordCounts[word] = 1;
    }
  });

  // Filter words that appear an odd number of times and keep their original case from the input
  let result = [];
  input.split(" ").forEach((word) => {
    if (
      wordCounts[word.toLowerCase()] % 2 !== 0 &&
      !result.includes(word.toLowerCase())
    ) {
      result.push(word.toLowerCase());
    }
  });

  // Print the result
  console.log(result.join(" "));
}
