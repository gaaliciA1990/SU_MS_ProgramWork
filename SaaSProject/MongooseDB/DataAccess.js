"use strict";
exports.__esModule = true;
exports.DataAccess = void 0;
var Mongoose = require("mongoose");
var DataAccess = /** @class */ (function () {
    // static DB_CONNECTION_STRING: string = "mongodb://dbAdmin:test@localhost:3000/metaverseData?authSource=admin";
    function DataAccess() {
        DataAccess.connect();
    }
    DataAccess.connect = function () {
        if (this.mongooseInstance)
            return this.mongooseInstance;
        this.mongooseConnection = Mongoose.connection;
        this.mongooseConnection.on("open", function () {
            console.log("Connected to mongodb.");
        });
        this.mongooseInstance = Mongoose.connect(this.DB_CONNECTION_STRING);
        return this.mongooseInstance;
    };
    DataAccess.DB_CONNECTION_STRING = "mongodb+srv://adminUser:SaasMetaDetector2022@metadetector-cluster.ks3os.mongodb.net/metaDetector?retryWrites=true&w=majority";
    return DataAccess;
}());
exports.DataAccess = DataAccess;
DataAccess.connect();
