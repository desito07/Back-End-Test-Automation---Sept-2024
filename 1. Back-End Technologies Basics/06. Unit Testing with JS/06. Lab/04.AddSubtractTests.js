const { expect } = require('chai');

const { createCalculator } = require('./04.AddSubtract.js');

describe('createCalculator', () => {
    let calculator;

    // Run before each test to ensure each test gets a fresh calculator instance
    beforeEach(() => {
        calculator = createCalculator();
    });

    it('should return an object containing add, subtract, and get methods', () => {
        // Arrange & Act
        const methods = ['add', 'subtract', 'get'];
        
        // Assert
        methods.forEach(method => {
            expect(typeof calculator[method]).equal('function');
        });
    });

    it('should keep an internal sum that cannot be accessed or modified directly', () => {
        // Arrange & Act
        const internalValue = calculator.value;

        // Assert
        expect(internalValue).to.be.undefined;
    });

    it('add() should correctly add a number to the internal sum', () => {
        // Arrange
        const input = 5;

        // Act
        calculator.add(input);
        const result = calculator.get();

        // Assert
        expect(result).equal(5);
    });

    it('subtract() should correctly subtract a number from the internal sum', () => {
        // Arrange
        const input = 3;

        // Act
        calculator.subtract(input);
        const result = calculator.get();

        // Assert
        expect(result).equal(-3);
    });

    it('add() should correctly add a numeric string to the internal sum', () => {
        // Arrange
        const input = "10";

        // Act
        calculator.add(input);
        const result = calculator.get();

        // Assert
        expect(result).equal(10);
    });

    it('subtract() should correctly subtract a numeric string from the internal sum', () => {
        // Arrange
        const input = "4";

        // Act
        calculator.subtract(input);
        const result = calculator.get();

        // Assert
        expect(result).equal(-4);
    });

    it('should handle a combination of add and subtract operations correctly', () => {
        // Arrange
        const operations = [
            { method: 'add', value: 10 },
            { method: 'subtract', value: 4 },
            { method: 'add', value: "5" },
            { method: 'subtract', value: "3" }
        ];

        // Act
        operations.forEach(op => calculator[op.method](op.value));
        const result = calculator.get();

        // Assert
        expect(result).equal(8); // 10 - 4 + 5 - 3 = 8
    });

    it('get() should return the current internal sum without modifying it', () => {
        // Arrange
        calculator.add(15);
        calculator.subtract(5);

        // Act
        const firstResult = calculator.get();
        const secondResult = calculator.get();

        // Assert
        expect(firstResult).equal(10);
        expect(secondResult).equal(10); // get() should not change the sum
    });

    it('should initialize with a sum of 0', () => {
        // Arrange & Act
        const result = calculator.get();

        // Assert
        expect(result).equal(0);
    });
});