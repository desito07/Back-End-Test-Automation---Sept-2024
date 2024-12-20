const products = [
  { id: 1, name: "Laptop", category: "Electronics", price: 999, stock: 50 },
  {
    id: 2,
    name: "Headphones",
    category: "Electronics",
    price: 199,
    stock: 200,
  },
  {
    id: 3,
    name: "Coffee Maker",
    category: "Home Appliances",
    price: 49,
    stock: 100,
  },
];

const store = solve(products);

function solve(products) {
  // Filter products by category
  function getProductsByCategory(category) {
    return products.filter((product) => product.category === category);
  }

  // Add a new product
  function addProduct(id, name, category, price, stock) {
    products.push({ id, name, category, price, stock });
    return products;
  }

  // Find product by ID
  function getProductById(id) {
    const product = products.find((product) => product.id === id);
    return product ? product : `Product with ID ${id} not found`;
  }

  // Remove product by ID
  function removeProductById(id) {
    const index = products.findIndex((product) => product.id === id);
    if (index !== -1) {
      products.splice(index, 1);
      return products;
    } else {
      return `Product with ID ${id} not found`;
    }
  }

  // Update product price by ID
  function updateProductPrice(id, newPrice) {
    const product = products.find((product) => product.id === id);
    if (product) {
      product.price = newPrice;
      return products;
    } else {
      return `Product with ID ${id} not found`;
    }
  }

  // Update product stock by ID
  function updateProductStock(id, newStock) {
    const product = products.find((product) => product.id === id);
    if (product) {
      product.stock = newStock;
      return products;
    } else {
      return `Product with ID ${id} not found`;
    }
  }

  // Return references to the methods
  return {
    getProductsByCategory,
    addProduct,
    getProductById,
    removeProductById,
    updateProductPrice,
    updateProductStock,
  };
}
console.log(JSON.stringify(store.removeProductById(2)));
