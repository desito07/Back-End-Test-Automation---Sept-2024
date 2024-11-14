const mongoose = require('mongoose');

const recipeSchema = new mongoose.Schema(
  {
    title: {
      type: String,
      required: true,
      trim: true
    },
    description: {
      type: String,
      trim: true
    },
    ingredients: [
      {
        name: {
          type: String,
          required: true
        },
        quantity: {
          type: String,
          required: true
        }
      }
    ],
    instructions: [
      {
        step: {
          type: String,
          required: true
        }
      }
    ],
    cookingTime: {
      type: Number, // time in minutes
      required: true
    },
    servings: {
      type: Number,
      required: true
    },
    category: {
      type: mongoose.Schema.Types.ObjectId, 
      ref: "Category"
    },
    author: {
      type: mongoose.Schema.Types.ObjectId, 
      ref: "User"
    }
  },
  {
    timestamps: true,
  }
);

module.exports = mongoose.model('Recipe', recipeSchema);
