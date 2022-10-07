import Mongoose = require("mongoose");
import {DataAccess} from '../DataAccess';
import {ITweetModel} from '../interfaces/ITweetModel';

let mongooseConnection = DataAccess.mongooseConnection;
let mongooseObj = DataAccess.mongooseInstance;

class TweetModel {
    public schema:any;
    public model:any;

    public constructor() {
        this.createSchema();
        this.createModel();
    }

    public createSchema(): void {
        this.schema = new Mongoose.Schema(
            {
                authorID: String,
                tweetID: String,
                tweetDescription: String,
                tweetDate: Date
            }, {collection: 'tweets'}
        );
    }

    public createModel(): void {
        this.model = mongooseConnection.model<ITweetModel>("Tweet", this.schema);
    }

    public retrieveTweetById(response:any, filter:Object): any {
        var query = this.model.findOne(filter);
        query.exec( (err, tweet) => {
            console.log(tweet);
            response.json(tweet);
        });
    }

    public retrieveAllTweets(response:any): any {
        var query = this.model.find({});
        query.exec( (err, tweetList) => {
            console.log(tweetList);
            response.json(tweetList);
        });
    }
}
export {TweetModel};