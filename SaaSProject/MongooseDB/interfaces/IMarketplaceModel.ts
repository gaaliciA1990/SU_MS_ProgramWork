import Mongoose = require("mongoose");
import internal = require("stream");

interface IMarketplaceModel extends Mongoose.Document {
    _id: string,
    metaverse: string,
    salesLastDay: number,
    salesLastWeek: number    
}
export {IMarketplaceModel};