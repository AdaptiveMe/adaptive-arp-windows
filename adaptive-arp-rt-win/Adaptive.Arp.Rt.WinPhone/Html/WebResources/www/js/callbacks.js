/**
 * Applications should override/implement this method to be aware of being lanched by a third-party application, and should perform the desired javascript code on this case.
 * <br> For further information see, {@link Appverse.System.LaunchData LaunchData}.
 * <br> @version 4.2
 * @method
 * @param {Appverse.System.LaunchData[]} launchData The launch data received.
 * <pre> Available in: <br> iOS <img src="resources/images/check.png"/> | android <img src="resources/images/check.png"/> | windows <img src="resources/images/error.png"/> | emulator <img src="resources/images/error.png"/></pre>
 * 
 */
Appverse.OnExternallyLaunched = function(launchData) {
    if (launchData != null) {
        console.log("launch data array length: " + launchData.length);
        var stringLog = "size:" + launchData.length;
        for (var i = 0; i < launchData.length; i++) {
            console.log(launchData[i].Name + "=" + launchData[i].Value);
            stringLog = stringLog + "<br/>" + launchData[i].Name + "=" + launchData[i].Value;
        }
        Ext.Msg.alert("Externally Launched", stringLog);
    } else {

        Ext.Msg.alert("Externally Launched", "No launch data received");
    }
};


var submitCallBack = function(result, id) {
    Showcase.app.getController('Main').getMsg().hide();
    console.log("async result: " + id);

    // SHOWING RESULT AS A JSON STRING IN the result form field
    var resultJsonString = null;
    if (result != null) {
        resultJsonString = JSON.stringify(result);
    }

    testCase.setResult(resultJsonString);
    Ext.ComponentQuery.query('test')[0].setRecord(testCase);
    Showcase.app.getController('Main').showResult();
};