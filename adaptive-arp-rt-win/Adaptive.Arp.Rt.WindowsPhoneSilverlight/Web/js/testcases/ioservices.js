
var testServiceName = "iWorkspaceSrv";
var testServiceType = 4;
var testJSONServiceRequestContent = '{method:contactService.getContactDetail,id:1,params:[MAPS]}';
var testJSONServiceRequest = new Object();
testJSONServiceRequest.Session = new Object();
testJSONServiceRequest.Content = testJSONServiceRequestContent;
/*
// request contenttype attribute will override service type
testJSONServiceRequest.ContentType = "x-gwt-rpc";

// example to add headers to the request
var requestHeaders = new Array();
var rHeader1 = new Object();
rHeader1.Name = "X-GWT-Permutation";
rHeader1.Value = "permutation-value";
var rHeader2 = new Object();
rHeader2.Name = "X-GWT-Module_Base";
rHeader2.Value = "module-base-value";
requestHeaders[0] = rHeader1;
requestHeaders[1] = rHeader2;
testJSONServiceRequest.Headers = requestHeaders;


// example to add cookies to the request
var requestCookies = new Array();
var cookie1 = new Object();
cookie1.Name = "mycookie1";
cookie1.Value = "maps";
var cookie2 = new Object();
cookie2.Name = "mycookie2";
cookie2.Value = "12345";
requestCookies[0] = cookie1;
requestCookies[1] = cookie2;
testJSONServiceRequest.Session.Cookies = requestCookies;
*/

var testService = new Object();
testService.Name = "iWorkspaceSrv";
testService.Type = 4;
var testServiceEndPoint = new Object();
testServiceEndPoint.Scheme = "/iworkspacesrv/scheme";
testServiceEndPoint.Host = "https://workspace.gft.com";
testServiceEndPoint.Path = "/iworkspacesrv/jsonrpc";
testService.Endpoint = testServiceEndPoint;
testService.RequestMethod = 0;  // POST method


var testServiceOctect = {};
testServiceOctect.Name = "getBinary";
testServiceOctect.Endpoint = {};
testServiceOctect.Endpoint.Host = "http://builder.gft.com/appstore/apploader-test/test-1.1.0.zip";
testServiceOctect.Endpoint.Port = 0;
testServiceOctect.Endpoint.Path = "";
testServiceOctect.RequestMethod = 0;
testServiceOctect.Type = Appverse.IO.SERVICETYPE_OCTET_BINARY;

var testRequestContentOctet = '';
var testRequestOctet = {};
testRequestOctet.Session = {};
testRequestOctet.Content = testRequestContentOctet;

var testStorePath = "tmp-showcase.zip";

//var testServiceWithObject = '{"Endpoint": {"ProxyUrl": null, "Port": 0, "Scheme": "/iworkspacesrv/scheme", "Host": "https://workspace.gft.com", "Path": "/iworkspacesrv/jsonrpc" }, "Name": "iWorkspaceSrv", "RequestMethod": 0,  "Type": 4}';

// I/O SERVICES TEST CASE

var TestCase_IOServices = [Appverse.IO,
	[['Async.InvokeService','{"param1":' + JSON.stringify(testJSONServiceRequest) +',"param2":' + JSON.stringify(testServiceName) +',"param3":' + JSON.stringify(testServiceType) +'}'],
	['GetService','{"param1":' + JSON.stringify(testServiceName) +',"param2":' + JSON.stringify(testServiceType) + '}'],
	['GetServices',''],
	['InvokeService','{"param1":' + JSON.stringify(testJSONServiceRequest) +',"param2":' + JSON.stringify(testServiceName) +',"param3":' + JSON.stringify(testServiceType) +'}'],
	['InvokeServiceForBinary','{"param1":' + JSON.stringify(testRequestOctet) +',"param2":' + JSON.stringify(testServiceOctect) +',"param3":' + JSON.stringify(testStorePath) +'}']]
];  
//"param2":' + JSON.stringify(testService)