function printGradeDescription(grade) {
    // Ensure the input is a number and is within the valid range
    if (typeof grade !== "number" || grade < 2.00 || grade > 6.00) {
        console.log("Please enter a valid grade between 2.00 and 6.00.");
        return;
    }

    let description;
    
    if (grade < 3.00) {
        description = "Fail";
        console.log(`${description} (${Math.floor(grade)})`);
        return; // Exit after handling "Fail" case
    } else if (grade < 3.50) {
        description = "Poor";
    } else if (grade < 4.50) {
        description = "Good";
    } else if (grade < 5.50) {
        description = "Very good";
    } else {
        description = "Excellent";
    }
    
    console.log(`${description} (${grade.toFixed(2)})`);
}