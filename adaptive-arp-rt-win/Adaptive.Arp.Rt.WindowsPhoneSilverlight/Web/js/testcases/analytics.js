
// The web property ID with the format UA-99999999-9
var testWebPropertyID = "UA-29725590-2";
var testAnalyticsEventGroup = "showcase";
var testAnalyticsEventAction = "initialization";
var testAnalyticsEventLabel = "initialized";
var testAnalyticsEventValue = 1;
var testAnalyticsRelativeUrl = "/appverse-showcase-analytics";


// Webtrekk vars
var testWebtrekkUrl = "http://wtk.appverse.com";
var testWebtrekkTrackId = "999999999999999";
// sampling rate will supress requests every x users.
//var testWebtrekkSamplingRate = "10";
var testWebtrekkClickId = "My Button";
var testWebtrekkContentId = "ES_Prod_Screen_PoC_";
if(Appverse.is.iOS) {
	testWebtrekkContentId = testWebtrekkContentId + "iOS";
} else if(Appverse.is.Android) {
	testWebtrekkContentId = testWebtrekkContentId + "Android";
}
var testWebtrekkRequestInterval = "600";

var testWebtrekkParametersCollection = new Object();
testWebtrekkParametersCollection.AdditionalParameters = new Array();
var testWebtrekkParameter = new Object();
testWebtrekkParameter.Name = "la";
testWebtrekkParameter.Value = "es";
testWebtrekkParametersCollection.AdditionalParameters[0] = testWebtrekkParameter;

//********** UI COMPONENTS

//********** ANALYTICS TESTCASES
var TestCase_Analytics = [Appverse.Analytics,
	[['StartTracking','{"param1":' + JSON.stringify(testWebPropertyID) + '}'],
	['StopTracking',''],
	['TrackEvent','{"param1":' + JSON.stringify(testAnalyticsEventGroup) +',"param2":' + JSON.stringify(testAnalyticsEventAction) + ',"param3":' + JSON.stringify(testAnalyticsEventLabel) + ',"param4":' + JSON.stringify(testAnalyticsEventValue) + '}'],
	['TrackPageView','{"param1":' + JSON.stringify(testAnalyticsRelativeUrl) +'}']]];

var TestCase_Webtrekk = [Appverse.Webtrekk,
	//[['StartTracking','{"param1":' + JSON.stringify(testWebtrekkUrl) + ',"param2":' + JSON.stringify(testWebtrekkTrackId) + ',"param3":' + JSON.stringify(testWebtrekkSamplingRate) + '}'],
	[['StartTracking','{"param1":' + JSON.stringify(testWebtrekkUrl) + ',"param2":' + JSON.stringify(testWebtrekkTrackId) + '}'],
	['StopTracking',''],
	['SetRequestInterval','{"param1":' + JSON.stringify(testWebtrekkRequestInterval) + '}'],
	['TrackClick','{"param1":' + JSON.stringify(testWebtrekkClickId) + ',"param2":' + JSON.stringify(testWebtrekkContentId) + ',"param3":' + JSON.stringify(testWebtrekkParametersCollection) + '}'],
	['TrackContent','{"param1":' + JSON.stringify(testWebtrekkContentId) + ',"param2":' + JSON.stringify(testWebtrekkParametersCollection) + '}']]];
	
//********** HANDLING CALLBACKS
