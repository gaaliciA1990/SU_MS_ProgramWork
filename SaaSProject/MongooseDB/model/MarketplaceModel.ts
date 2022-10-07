import Mongoose = require("mongoose");
import { DataAccess } from '../DataAccess';
import { IMarketplaceModel } from '../interfaces/IMarketplaceModel';

let mongooseConnection = DataAccess.mongooseConnection;
let mongooseObj = DataAccess.mongooseInstance;

class MarketplaceModel {
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
                metaverse: String,
                salesLastDay: Number,
                salesLastWeek: Number
            }, { collection: 'marketplaces' }
        );
    }

    public createModel(): void {
        this.model = mongooseConnection.model<IMarketplaceModel>("Marketplace", this.schema);
    }

    // Pull all marketplace data
    public async retrieveAllSales(): Promise<MarketplaceModel[]> {
        //Find all tiles with a distinct estateId to avoid duplicates
        var result = await this.model.find();
        console.log(result);
        
        return result;
    }
}
export { MarketplaceModel };