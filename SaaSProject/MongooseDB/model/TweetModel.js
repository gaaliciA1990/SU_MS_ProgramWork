"use strict";
exports.__esModule = true;
exports.TweetModel = void 0;
var Mongoose = require("mongoose");
var DataAccess_1 = require("../DataAccess");
var mongooseConnection = DataAccess_1.DataAccess.mongooseConnection;
var mongooseObj = DataAccess_1.DataAccess.mongooseInstance;
var TweetModel = /** @class */ (function () {
    function TweetModel() {
        this.createSchema();
        this.createModel();
    }
    TweetModel.prototype.createSchema = function () {
        this.schema = new Mongoose.Schema({
            authorID: String,
            tweetID: String,
            tweetDescription: String,
            tweetDate: Date
        }, { collection: 'tweets' });
    };
    TweetModel.prototype.createModel = function () {
        this.model = mongooseConnection.model("Tweet", this.schema);
    };
    TweetModel.prototype.retrieveTweetById = function (response, filter) {
        var query = this.model.findOne(filter);
        query.exec(function (err, tweet) {
            console.log(tweet);
            response.json(tweet);
        });
    };
    TweetModel.prototype.retrieveAllTweets = function (response) {
        var query = this.model.find({});
        query.exec(function (err, tweetList) {
            console.log(tweetList);
            response.json(tweetList);
        });
    };
    return TweetModel;
}());
exports.TweetModel = TweetModel;
