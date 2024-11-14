const Destination = require("../models/destinationModel");
const asyncHandler = require("express-async-handler");
const slugify = require("slugify");
const validateMongoDbId = require("../utils/validateMongodbId");

const createDestination = asyncHandler(async (req, res) => {
  try {
    if (req.body.title) {
      req.body.slug = slugify(req.body.title);
    }
    const newDestination = await Destination.create(req.body);
    res.json(newDestination);
  } catch (error) {
    throw new Error(error);
  }
});

const updateDestination = asyncHandler(async (req, res) => {
  const id = req.params;
  validateMongoDbId(id);
  try {
    if (req.body.title) {
      req.body.slug = slugify(req.body.title);
    }
    const updateDestination = await Destination.findOneAndUpdate({ id }, req.body, {
      new: true,
    });
    res.json(updateDestination);
  } catch (error) {
    throw new Error(error);
  }
});

const deleteDestination = asyncHandler(async (req, res) => {
  const {id} = req.params;
  validateMongoDbId(id);
  try {
    const deleteDestination = await Destination.findOneAndDelete({ _id: id });
    res.json(deleteDestination);
  } catch (error) {
    throw new Error(error);
  }
});

const getaDestination = asyncHandler(async (req, res) => {
  const { id } = req.params;
  validateMongoDbId(id);
  try {
    const findDestination = await Destination.findById(id);
    res.json(findDestination);
  } catch (error) {
    throw new Error(error);
  }
});

const getAllDestinations = asyncHandler(async (req, res) => {
  try {
    const queryObj = { ...req.query };
    const excludeFields = ["page", "sort", "limit", "fields"];
    excludeFields.forEach((el) => delete queryObj[el]);
    let queryStr = JSON.stringify(queryObj);
    queryStr = queryStr.replace(/\b(gte|gt|lte|lt)\b/g, (match) => `$${match}`);

    let query = Destination.find(JSON.parse(queryStr));

    if (req.query.sort) {
      const sortBy = req.query.sort.split(",").join(" ");
      query = query.sort(sortBy);
    } else {
      query = query.sort("-createdAt");
    }

    if (req.query.fields) {
      const fields = req.query.fields.split(",").join(" ");
      query = query.select(fields);
    } else {
      query = query.select("-__v");
    }

    const page = req.query.page;
    const limit = req.query.limit;
    const skip = (page - 1) * limit;
    query = query.skip(skip).limit(limit);
    if (req.query.page) {
      const DestinationsCount = await Destination.countDocuments();
      if (skip >= DestinationsCount) throw new Error("This Page does not exists");
    }
    const destination = await query;
    res.json(destination);
  } catch (error) {
    throw new Error(error);
  }
});

module.exports = {
  createDestination,
  getaDestination,
  getAllDestinations,
  updateDestination,
  deleteDestination
};
