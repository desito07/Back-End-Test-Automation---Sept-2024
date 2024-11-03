function stringSubstring(word, text) {
  // Convert both word and text to lowercase for case-insensitive comparison
  let lowerWord = word.toLowerCase();
  let lowerText = text.toLowerCase();

  // Split the text into an array of words
  let wordsArray = lowerText.split(" ");

  // Check if the word exists in the array of words
  if (wordsArray.includes(lowerWord)) {
    console.log(word); // Print the original word if found
  } else {
    console.log(`${word} not found!`); // Print 'not found' if the word isn't in the text
  }
}