const { expect } = require('chai');

const { isSymmetric } = require('./02.checkForSymmetry.js');

describe('Tests for check symmetry function', function () {

    it('should return true for symmetryc array', function() {
    
        //arrange
    let result = isSymmetric([1,2,3,2,1]);
    
        //act
    
        //assert
    expect(result).to.true;
    
    });

    it('should return false for non-symmetryc array', function() {
    
        //arrange
    let result = isSymmetric([1,2,3,5,1]);
    
        //act
    
        //assert
    expect(result).to.false;
    
    });

    it('should return true for empty array', function() {
    
        //arrange
    let result = isSymmetric([]);
    
        //act
    
        //assert
    expect(result).to.true;
    
    });

    it('should return false for non-array', function() {
    
        //arrange
    let result = isSymmetric('This is not an array');
    
        //act
    
        //assert
    expect(result).to.false;
    
    });

    it('should return false for non number element', function() {
    
        //arrange
    let result = isSymmetric([1, '1']);
    
        //act
    
        //assert
    expect(result).to.false;
    
    });

    it('should return true for single element array', function() {
    
        //arrange
    let result = isSymmetric([1]);
    
        //act
    
        //assert
    expect(result).to.true;
    
    });

});