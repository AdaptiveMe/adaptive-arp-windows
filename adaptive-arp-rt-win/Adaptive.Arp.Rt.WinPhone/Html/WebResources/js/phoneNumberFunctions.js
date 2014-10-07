/**
 * This function returns 
 * -when the input parameter has an italian prefix, the output is 
 * 	the number in a standard format, null if it isn't a valid italian mobile phone number
 * -when the input parameter starts by '3' and has 9-11 digits , the output is the input 
 * parameter written in the standard italian format(+393..)
 * -in the other case the output is the same input parameter without character that could be added from numeric keypad
 * @param str
 * @returns
 */
function getDefaultNumber(str) {
	if(str){
		var newStr = str.replace(/ /g, '');
		newStr = newStr.replace(/\s/g,'');
		newStr = newStr.replace(/-/g, '');
		newStr = newStr.replace(/\./g,'');
		newStr = newStr.replace(/\*/g,'');	
		newStr = newStr.replace(/#/g,'');
		newStr = newStr.replace(/;/g,'');
		newStr = newStr.replace(/\//g,'');
		newStr = newStr.replace(/,/g,'');
		newStr = newStr.replace(/[()]/g,'');
		
		if (newStr) {

			if (newStr.length>3 && newStr.substring(0, 4) == '0039') {

				var pattern = /^(00393)\d{8,10}$/;
				if (pattern.test(newStr)) {
					retVal = '+' + newStr.substring(2);
					return (retVal);
				}

				return null;
			} else if (newStr.length>2 && newStr.substring(0, 3) == '+39') {
			
				var pattern = /^(\+393)\d{8,10}$/;
				if (pattern.test(newStr)) {
					return newStr;
				}

				return null;
			} else if (newStr.length>0 && newStr.substring(0, 1) == '3'){
				
				var pattern = /^\d{8,10}$/;
				if (pattern.test(newStr)) {
					retVal = '+39' + newStr;
					return (retVal);
				} 

				
			}
		}else {
			return null;
		}
		
		return newStr;
	} 
	return null;
}

/**
 * This function returns true if the parameter is a foreign phone number or, an italian valid phone number
 * so it avoid the italian landline numbers and the number with italian prefix that aren't well-formed (dashes are permitted)
 * @param number
 * @returns {Boolean}
 */
function isValid(number) {
	if (number) {
		var newStr = number.replace(/ /g, '');
		newStr = newStr.replace(/\s/g,'');
		newStr = newStr.replace(/-/g, '');
	
		if (newStr) {

			if (newStr.length>3 && newStr.substring(0, 4) == '0039') {
			
				var pattern = /^(00393)\d{8,10}$/;
				if (pattern.test(newStr)) {
					return true;
				}
			return false;

			} else if (newStr.length>2 && newStr.substring(0, 3) == '+39') {
				
				var pattern = /^(\+393)\d{8,10}$/;
				if (pattern.test(newStr)) {

					return true;
				}

				return false;
			} else if (newStr.length>0 && newStr.substring(0, 1) == '0') {
				var pattern = /^(0)\d{5,10}$/;
				if (pattern.test(newStr)) {
				
					return false;
				}
			}
		} else {

			return false;
		}
	} else {

		return false;
	}
	return true;
	
}