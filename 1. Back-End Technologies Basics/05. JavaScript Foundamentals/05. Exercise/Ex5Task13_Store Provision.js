function updateStoreStock(currentStock, orderedProducts) {
  let store = {};

  // Process the current stock array
  for (let i = 0; i < currentStock.length; i += 2) {
    let product = currentStock[i];
    let quantity = Number(currentStock[i + 1]);
    store[product] = quantity;
  }

  // Process the ordered products array
  for (let i = 0; i < orderedProducts.length; i += 2) {
    let product = orderedProducts[i];
    let quantity = Number(orderedProducts[i + 1]);

    // If the product is already in stock, add the quantity
    if (store.hasOwnProperty(product)) {
      store[product] += quantity;
    } else {
      // If it's a new product, add it to the store
      store[product] = quantity;
    }
  }

  // Print the store inventory in the required format
  for (let product in store) {
    console.log(`${product} -> ${store[product]}`);
  }
}
