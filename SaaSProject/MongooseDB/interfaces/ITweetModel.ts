import Mongoose = require("mongoose");
import internal = require("stream");

interface ITweetModel extends Mongoose.Document {
    authorID: string,
    tweetID: string,
    tweetDescription: string,
    tweetDate: Date
}
export {ITweetModel};