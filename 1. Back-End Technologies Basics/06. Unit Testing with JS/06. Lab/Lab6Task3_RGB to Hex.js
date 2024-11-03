describe("Tests for rgb to hex function", function () {
  it("should return correct hex for valid rgb", function () {
    //arrange
    let result = rgbToHexColor(255, 142, 144);

    //act

    //assert
    expect(result).to.equal("#FF8E90");
  });

  it("should return correct hex for lower boundary", function () {
    //arrange
    let result = rgbToHexColor(0, 0, 0);

    //act

    //assert
    expect(result).to.equal("#000000");
  });

  it("should return correct hex for upper boundary", function () {
    //arrange
    let result = rgbToHexColor(255, 255, 255);

    //act

    //assert
    expect(result).to.equal("#FFFFFF");
  });

  it("should return undefinied for bigger than 255 number", function () {
    //arrange
    let result = rgbToHexColor(255, 255, 256);

    //act

    //assert
    expect(result).to.be.undefined;
  });

  it("should return undefinied for decimal red", function () {
    //arrange
    let result = rgbToHexColor(12.5, 255, 255);

    //act

    //assert
    expect(result).to.be.undefined;
  });

  it("should return undefinied for decimal green", function () {
    //arrange
    let result = rgbToHexColor(260, 15.6, 255);

    //act

    //assert
    expect(result).to.be.undefined;
  });

  it("should return undefinied for decimal blue", function () {
    //arrange
    let result = rgbToHexColor(255, 255, 34.5);

    //act

    //assert
    expect(result).to.be.undefined;
  });

  it("should return undefinied for strings", function () {
    //arrange
    let result = rgbToHexColor("a", "b", "c");

    //act

    //assert
    expect(result).to.be.undefined;
  });

  it("should return correct hex for negative number", function () {
    //arrange
    let result = rgbToHexColor(-1, 0, 0);

    //act

    //assert
    expect(result).to.be.undefined;
  });
});
