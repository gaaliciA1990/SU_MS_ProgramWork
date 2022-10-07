var chai = require('chai');
var chaiHttp = require('chai-http');
var async = require('async');

var assert = chai.assert;
var expect = chai.expect;
var should = chai.should();

var http = require('http');
chai.use(chaiHttp);

describe('Test Tile by coordinate', function () {
	//	this.timeout(15000);

	let hostURL = "https://metadetector.azurewebsites.net";
	let path = "/app/tile/?x=-150&y=150";
	var requestResult;
	var response;

	before(function (done) {
		chai.request(hostURL)
			.get(path)
			.end(function (err, res) {
				requestResult = res.body;
				response = res;
				expect(err).to.be.null;
				expect(res).to.have.status(200);
				done();
			});
	});

	it('Should return 1 Tile object', function () {
		expect(response).to.have.status(200);
		//Expect the response body to be json
		expect(response).to.be.json;
		expect(response.body).to.be.a('Object');
		expect(response).to.have.headers;

	});

	it('Should have  known properties', function () {
		expect(requestResult).to.include.all.keys('tileId', 'type', 'updatedAt');
		expect(requestResult).to.have.property('_id');
		expect(requestResult).to.have.property('estateId', 1186);
		expect(response.body).to.have.deep.property('tokenId');
		expect(response.body).to.not.be.a.string;
	});

	it('Should have all expected properties with expected types', function () {
		var theTile = response.body;
		expect(theTile).to.have.property('_id').that.is.a('string');
		expect(theTile).to.have.property('tileId').that.is.a('string');
		expect(theTile).to.have.property('updatedAt').that.is.a('number');
		expect(theTile).to.have.property('type').that.is.a('string');
		expect(theTile).to.have.property('estateId', 1186).that.is.a('number');
		expect(theTile).to.have.property('owner').that.is.a('string');
		expect(theTile).to.have.property('tokenId').that.is.a('string');
	});
});