var testKeyPairName1 = "mykey1";
var testKeyPairName2 = "mykey2";
var testKeyPairName3 = "mykey3";

var testKeyPairNames = [testKeyPairName1, testKeyPairName2, testKeyPairName3];

var testKeyPair1 = {};
testKeyPair1.Key = testKeyPairName1;
testKeyPair1.Value = "myvalue1";

var testKeyPair2 = {};
testKeyPair2.Key = testKeyPairName2;
testKeyPair2.Value = "myvalue2";

var testKeyPair3 = {};
testKeyPair3.Key = testKeyPairName3;
testKeyPair3.Value = "myvalue3";

var testKeyPairs = [testKeyPair1, testKeyPair2, testKeyPair3];


//********** UI COMPONENTS

//********** SECURITY TESTCASES
var TestCase_Security = [Appverse.Security,
	[['IsDeviceModified',''],
	 ['GetStoredKeyValuePair','{"param1":' +  JSON.stringify(testKeyPairName1) + '}'],
	 ['Async.GetStoredKeyValuePair','{"param1":' +  JSON.stringify(testKeyPairName1) + '}'],
	 ['GetStoredKeyValuePairs','{"param1":' +  JSON.stringify(testKeyPairNames) + '}'],
	 ['Async.GetStoredKeyValuePairs','{"param1":' +  JSON.stringify(testKeyPairNames) + '}'],
	 ['StoreKeyValuePair','{"param1":' +  JSON.stringify(testKeyPair1) + '}'],
	 ['Async.StoreKeyValuePair','{"param1":' +  JSON.stringify(testKeyPair1) + '}'],
	 ['StoreKeyValuePairs','{"param1":' +  JSON.stringify(testKeyPairs) + '}'],
	 ['Async.StoreKeyValuePairs','{"param1":' +  JSON.stringify(testKeyPairs) + '}'],
	 ['RemoveStoredKeyValuePair','{"param1":' +  JSON.stringify(testKeyPairName1) + '}'],
	 ['Async.RemoveStoredKeyValuePair','{"param1":' +  JSON.stringify(testKeyPairName1) + '}'],
	 ['RemoveStoredKeyValuePairs','{"param1":' +  JSON.stringify(testKeyPairNames) + '}'],
	 ['Async.RemoveStoredKeyValuePairs','{"param1":' +  JSON.stringify(testKeyPairNames) + '}']
	]];
	
//********** HANDLING CALLBACKS

/**
 * Applications should override/implement this method to be aware of storing of KeyPairs object into the local secure storage, and should perform the desired javascript code on this case.
 * <br> For further information see, {@link Appverse.Security.KeyPair KeyPair}.
 * <br> @version 4.2
 * @method
 * @param {Appverse.Security.KeyPair[]} storedKeyPairs An array of KeyPair objects successfully stored in the device local secure storage.
 * @param {Appverse.Security.KeyPair[]} failedKeyPairs An array of KeyPair objects that could not be successfully stored in the device local secure storage.
 * <pre> Available in: <br> iOS <img src="resources/images/check.png"/> | android <img src="resources/images/check.png"/> | windows <img src="resources/images/error.png"/> | emulator <img src="resources/images/check.png"/></pre>
 */
Appverse.OnKeyValuePairsStoreCompleted = function(storedKeyPairs, failedKeyPairs){
	Showcase.app.getController('Main').toast("Stored " + (storedKeyPairs?storedKeyPairs.length:0) + " keys; failed: " + (failedKeyPairs?failedKeyPairs.length:0));
};

/**
 * Applications should override/implement this method to be aware of KeyPair objects found in the device local secure storage, and should perform the desired javascript code on this case.
 * <br> For further information see, {@link Appverse.Security.KeyPair KeyPair}.
 * <br> @version 4.2
 * @method
 * @param {Appverse.Security.KeyPair[]} foundKeyPairs An array of KeyPair objects found in the device local secure storage.
 * <pre> Available in: <br> iOS <img src="resources/images/check.png"/> | android <img src="resources/images/check.png"/> | windows <img src="resources/images/error.png"/> | emulator <img src="resources/images/check.png"/></pre>
 */
Appverse.OnKeyValuePairsFound = function(foundKeyPairs){
	console.dir(foundKeyPairs);
	
	Showcase.app.getController('Main').toast("Found " + (foundKeyPairs?foundKeyPairs.length:0) + " stored keys");
	
	console.dir((foundKeyPairs!=null && foundKeyPairs.length>0)?foundKeyPairs[0].Value: "null");
};

/**
 * Applications should override/implement this method to be aware of deletion of KeyPairs objects from the local secure storage, and should perform the desired javascript code on this case.
 * <br> For further information see, {@link Appverse.Security.KeyPair KeyPair}.
 * <br> @version 4.2
 * @method
 * @param {Appverse.Security.KeyPair[]} removedKeyPairs An array of KeyPair objects successfully removed from the device local secure storage.
 * @param {Appverse.Security.KeyPair[]} failedKeyPairs An array of KeyPair objects that could not be removed from the device local secure storage.
 * <pre> Available in: <br> iOS <img src="resources/images/check.png"/> | android <img src="resources/images/check.png"/> | windows <img src="resources/images/error.png"/> | emulator <img src="resources/images/check.png"/></pre>
 */
Appverse.OnKeyValuePairsRemoveCompleted = function (removedKeyPairs, failedKeyPairs){
	
	Showcase.app.getController('Main').toast("Removed " + (removedKeyPairs?removedKeyPairs.length:0) + " stored keys; failed: " + (failedKeyPairs?failedKeyPairs.length:0));
};
