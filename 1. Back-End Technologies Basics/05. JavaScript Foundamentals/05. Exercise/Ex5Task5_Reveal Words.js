function revealWords(words, sentence) {
  // Split the words by comma to get an array of words
  let wordList = words.split(", ");

  // Iterate through each word and replace the corresponding template in the sentence
  wordList.forEach((word) => {
    // Create a pattern for the template that has the same length as the word
    let template = "*".repeat(word.length);
    sentence = sentence.replace(template, word);
  });

  return sentence;
}
