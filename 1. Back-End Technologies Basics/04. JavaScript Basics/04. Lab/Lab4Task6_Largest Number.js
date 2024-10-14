function largestNumber(num1, num2, num3){
    let result;
    if(num1 < num3 && num2 < num3){
        result = num3;
    }
    else if(num2 < num1 && num3 < num1){
        result = num1;
    }
    else if(num1 < num2 && num3 < num2){
        result = num2;
    }
    console.log(`The largest number is ${result}.`)
}
largestNumber(-3, -5, -22.5);