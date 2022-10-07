import Mongoose = require("mongoose");
import internal = require("stream");

interface ITileModel extends Mongoose.Document {
    tileId: string,
    x: number,
    y: number,
    updatedAt: number,
    type: string,
    top: boolean,
    left: boolean,
    topLeft: boolean,
    estateId: number,
    owner: string, // hexadecimal string type?
    tokenId: string // this number is way too big for an integer
}
export {ITileModel};