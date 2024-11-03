function printNthElement(arr, step) {
    let result = [];
    // Start the loop at the first element and increment by the step value
    for (let i = 0; i < arr.length; i += step) {
        result.push(arr[i]);
    }
    return result;
}
