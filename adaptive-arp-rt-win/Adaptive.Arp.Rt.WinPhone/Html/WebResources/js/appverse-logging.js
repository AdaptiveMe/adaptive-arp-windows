AppverseLog = {
	enabled : true,
	googleDocsPath : 'https://docs.google.com/forms/d/14yBJFiEQ28NIpjE69-tpqaCG1ySsd9jmKcy_-XSCefY/formResponse',
	buildNum: 'not_defined',
	status: ['INITIALIZED', 'INTERCEPTED', 'PROCESSED', 'ERROR'],
	activeIds: {},
	interceptedFunctions: {},
	environment: 'DEV',
	requestTimeout: 9000  // setting a timeout in milliseconds, to prevent hanging the code forever while waiting for a read to occur
};

AppverseLog.logTrace = function(status, funcName, funcArguments, elapsedTime) {

	if(AppverseLog.enabled) {
		try {
			var xhr = new XMLHttpRequest(); 
			var today = new Date();
			xhr.open( "POST", AppverseLog.googleDocsPath, true);  // true, to handle requests asynchronously
			xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
			
			xhr.onload = function (e) {
			  if (xhr.readyState === 4) {
				if (xhr.status === 200) {
				  //console.log("onLoad success: " + xhr.responseText);
				} else {
				  //console.error("onLoad status different from 200: " + xhr.statusText);
				}
			  }
			};
			
			xhr.onerror = function (e) {
			  //console.error("onError: " + xhr.statusText);
			};
			
			xhr.ontimeout = function () {
				console.error("The request for " + AppverseLog.googleDocsPath + " timed out.");
			};
			xhr.timeout = AppverseLog.requestTimeout;
			
			var entry_0_single = {name:'entry.1545688079',value: navigator.userAgent};
			var entry_1_single = {name:'entry.1993669219',value: AppverseLog.buildNum};
			var entry_2_single = {name:'entry.1103422338',value: status};
			var entry_3_single = {name:'entry.307015137',value: funcName};
			var entry_4_single = {name:'entry.706550193',value: funcArguments};
			var entry_5_single = {name:'entry.1038203751',value: elapsedTime};
			var entry_6_single = {name:'entry.312909810',value: AppverseLog.environment};

			var reqData = AppverseLog.parsePOSTData(entry_0_single) + "&" + AppverseLog.parsePOSTData(entry_1_single) + "&" 
				+ AppverseLog.parsePOSTData(entry_2_single) + "&" + AppverseLog.parsePOSTData(entry_3_single) 
				+ "&"+ AppverseLog.parsePOSTData(entry_4_single) + "&"+ AppverseLog.parsePOSTData(entry_5_single)
				+ "&"+ AppverseLog.parsePOSTData(entry_6_single);
			//console.log(reqData);
		
			xhr.send(reqData);
		} catch (e) {
			//console.log("Error logging trace for function [" + funcName +"]: " + e);
			return null;
		}
	}
}

AppverseLog.parsePOSTData = function(param) {
	return param.name + "=" + param.value;
}

AppverseLog.interceptFunction = function(functionName, functionId) {
	if(AppverseLog.enabled) {
		var func = window[functionName];
		if(func)  {
			//console.log("applying AppverseLog FuncInterceptor");
			var start = Date.now();
			AppverseLog.activeIds[functionId] = start;
			AppverseLog.logTrace(AppverseLog.status[0], functionName + "["+ functionId +"]", null, 0);
			if(!AppverseLog.interceptedFunctions[functionName]) {
				window[functionName] = AppverseLog.FuncInterceptor(func, functionName);
				//console.log("AppverseLog :: '" + functionName + "' function included to interceptedFunctions");
				AppverseLog.interceptedFunctions[functionName] = true;
			}
		}
	}
};

AppverseLog.FuncInterceptor = function(func, functionName) {
	
	return function() {
		
		//console.log("function intercepted");
		
		var elapsed = "undefined";
		var functionId = "undefined";
		if(arguments != null && arguments.length>1) {
			functionId =  arguments[1]; // callback id is being received as second argument
			var end = Date.now();
			elapsed = end - AppverseLog.activeIds[functionId];
		}
		
		AppverseLog.logTrace(AppverseLog.status[1], functionName + "["+ functionId +"]", JSON.stringify(arguments), elapsed);
		
		var result = func.apply(this, arguments);
		
		//console.log("Function processed with arguments: " + JSON.stringify(arguments));
		
		var end2 = Date.now();
		var previous = elapsed;
		elapsed = end2 - AppverseLog.activeIds[functionId];
		var partial = elapsed - previous;
		AppverseLog.logTrace(AppverseLog.status[2], functionName + "["+ functionId +"]", JSON.stringify(arguments), partial +" ; " + elapsed);
		
		delete AppverseLog.activeIds[functionId];
		
		return result;
	}
};