Utility = function() {
};

Utility.prototype.createDateTimeObjectFromDate = function (jsDate) {
	
	if(jsDate) {
		return this.createDateTimeObjectFromValues(
			jsDate.getDate(), 
			jsDate.getMonth() +1, 
			jsDate.getFullYear(), 
			jsDate.getHours(), 
			jsDate.getMinutes(), 
			jsDate.getSeconds());
	}
	
	return null;
}

Utility.prototype.createDateTimeObjectFromValues = function (day,month,year,hour,minute,seconds) {
	var dateTimeObject = new Object();
	if(day && month && year) { // day, month and year are REQUIRED
		dateTimeObject.Year = year;
		dateTimeObject.Month = month;
		dateTimeObject.Day = day;
		dateTimeObject.Hour = (hour?hour:0);
		dateTimeObject.Minute = (minute?minute:0);
		dateTimeObject.Second = (seconds?seconds:0);
	}
	return dateTimeObject;
}

Utility.prototype.printDateTimeObject = function (dateTimeObject) {
	if(dateTimeObject)
		return dateTimeObject.Day + "-" + dateTimeObject.Month + "-" + dateTimeObject.Year + " " + dateTimeObject.Hour + ":" + dateTimeObject.Minute + ":" + dateTimeObject.Second;
	return "undefined";
}

ShowcaseUtils = new Utility();