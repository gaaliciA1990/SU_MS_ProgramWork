var chai = require('chai');
var chaiHttp = require('chai-http');

var expect = chai.expect;

var http = require('http');
chai.use(chaiHttp);

describe('Test Tiles result', function () {
	//	this.timeout(15000);

	let hostURL = "https://metadetector.azurewebsites.net";
	let path = "/app/tweets";
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

	it('The first entry in the array has known properties', function () {
		expect(requestResult[0]).to.have.property('authorID')
		expect(requestResult[0]).to.have.property('tweetID')
		expect(requestResult[0]).to.have.property('tweetDescription')
		expect(requestResult[0]).to.have.property('tweetDate');
		expect(requestResult[0]).to.have.property('_id');
		expect(response.body).to.not.be.a.string;
	});

	it('The elements in the array have the expected properties with defined types', function () {
		expect(response.body).to.satisfy(
			function (body) {
				for (var i = 0; i < body.length; i++) {
					expect(body[i]).to.have.property('_id').that.is.a('string');
					expect(body[i]).to.have.property('authorID').that.is.a('string');
					expect(body[i]).to.have.property('tweetID').that.is.a('string');
					expect(body[i]).to.have.property('tweetDescription').that.is.a('string');
				}
				return true;
			});
	});

});