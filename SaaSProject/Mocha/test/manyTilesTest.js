var chai = require('chai');
var chaiHttp = require('chai-http');

var expect = chai.expect;

var http = require('http');
chai.use(chaiHttp);

describe('Test Many Tiles get ', function () {
	//	this.timeout(15000);

	let hostURL = "https://metadetector.azurewebsites.net";
	let path = "/app/tile/estate/4893";
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

	it('Should return an array object with more than 1 object', function () {
		expect(response).to.have.status(200);
		//Expect the response body to be json
		expect(response).to.be.json;
		expect(response.body).to.have.length.above(2);
		expect(response).to.have.headers;
		expect(response.body).to.be.an('array');
	});

	it('The first entry in the array has known properties', function () {
		expect(requestResult[0]).to.include.all.keys('tileId', 'type', 'updatedAt');
		expect(requestResult[0]).to.have.property('_id');
		expect(requestResult[0]).to.have.property('estateId', 4893);
		expect(response.body[0]).to.have.deep.property('tokenId');
		expect(response.body).to.not.be.a.string;
	});

	it('The elements in the array have the expected properties with defined types', function () {
		expect(response.body).to.satisfy(
			function (body) {
				for (var i = 0; i < body.length; i++) {
					expect(body[i]).to.have.property('_id').that.is.a('string');
					expect(body[i]).to.have.property('tileId').that.is.a('string');
					expect(body[i]).to.have.property('updatedAt').that.is.a('number');
					expect(body[i]).to.have.property('type').that.is.a('string');
					expect(body[i]).to.have.property('estateId', 4893).that.is.a('number');
					expect(body[i]).to.have.property('owner').that.is.a('string');
					expect(body[i]).to.have.property('tokenId').that.is.a('string');
				}
				return true;
			});
	});

});