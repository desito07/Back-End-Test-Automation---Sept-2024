const mongoose = require("mongoose"); 

var destinationSchema = new mongoose.Schema(
  {
    name: {
      type: String,
      required: true,
      trim: true,
    },
    location: {
      type: String,
      required: true,
    },
    description: {
      type: String,
      required: true,
    },
    attractions: {
      type: [String],
      required: true,
    },
    bestTimeToVisit: {
      type: String,
      required: true,
    },
    category: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'Category',
      required: true,
    },
    ratings: [
      {
        star: Number,
        comment: String,
        postedby: { type: mongoose.Schema.Types.ObjectId, ref: "User" },
      },
    ],
    imageUrls: [String],
  },
  { timestamps: true }
);

// Export the model
module.exports = mongoose.model("Destination", destinationSchema);
