import Mongoose = require("mongoose");
import { DataAccess } from '../DataAccess';
import { ITileModel } from '../interfaces/ITileModel';

let mongooseConnection = DataAccess.mongooseConnection;
let mongooseObj = DataAccess.mongooseInstance;

class TileModel {
    public schema: any;
    public model: any;

    public constructor() {
        this.createSchema();
        this.createModel();
    }

    public createSchema(): void {
        this.schema = new Mongoose.Schema(
            {
                _id: String,
                tileId: String,
                type: String,
                updatedAt: Number,
                name: String,
                owner: String,
                estateId: Number,
                tokenId: String,
                price: Number
            }, { collection: 'tiles' }
        );
    }

    public createModel(): void {
        this.model = mongooseConnection.model<ITileModel>("Tile", this.schema);
    }

    public retrieveTileById(response: any, filter: Object): any {
        var query = this.model.findOne(filter);
        query.exec((err, tile) => {
            console.log(tile);
            response.json(tile);
        });
    }

    // Pulls all Tiles 
    public async retrieveAllTiles(): Promise<TileModel[]> {
        //Find all tiles 
        var result = await this.model.find();
        console.log(result);
        return result;
    }

    public retrieveAllTilesInEstate(response: any, filter: Object): any {
        var query = this.model.find(filter);
        query.exec((err, tileList) => {
            console.log(tileList);
            response.json(tileList);
        });
    }

    public async retrieveTilesOfSpecificType(filter: Object): Promise<TileModel[]> {
        var tileList = await this.model.find(filter);
        console.log(tileList);
        return tileList;
    }

    public retrieveEstateIdsOfSpecificType(response: any, filter: Object): any {
        var query = this.model.find(filter).distinct("estateId");
        query.exec((err, estateIdList) => {
            console.log(estateIdList);
            response.json(estateIdList);
        });
    }

}
export { TileModel };