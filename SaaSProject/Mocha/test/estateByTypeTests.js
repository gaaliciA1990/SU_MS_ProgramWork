var chai = require('chai');
var chaiHttp = require('chai-http');

var expect = chai.expect;

var http = require('http');
const exp = require('constants');
chai.use(chaiHttp);

describe('Test Estates by Type', function () {
	//	this.timeout(15000);

	let hostURL = "https://metadetector.azurewebsites.net";
	let path = "/app/estates/type/owned";
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
		expect(response.body).to.have.length.above(1);
		expect(response).to.have.headers;
		expect(response.body).to.be.an('array');
	});

	it('The first and last value are as expected', function () {
		expect(requestResult[0]).to.equal(15);
		expect(requestResult[194]).to.equal(4882);
		expect(response.body).to.not.be.a.string;
	});
});