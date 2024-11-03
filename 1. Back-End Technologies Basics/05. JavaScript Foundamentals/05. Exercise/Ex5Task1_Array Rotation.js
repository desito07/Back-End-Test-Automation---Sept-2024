function rotateArray(arr, rotations) {
  // Ensure that the number of rotations is within the bounds of the array's length
  let effectiveRotations = rotations % arr.length;

  // Use slice to take the part of the array to rotate and the remaining part
  let rotatedPart = arr.slice(effectiveRotations);
  let rotatedTail = arr.slice(0, effectiveRotations);

  // Concatenate the rotated parts
  return rotatedPart.concat(rotatedTail).join(" ");
}
