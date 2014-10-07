// Appverse Utilities
AppLoaded = true,
        Timer = null,
        myMask = null;

store2option = function(store, field, all) {

    options = [];
    if (all)
        options[0] = {
            text: 'All',
            value: 0
        };

    values = store.collect(field);

    for (var i in values)
        if (!isNaN(i)) {
            options[options.length] = {
                text: values[i],
                value: values[i]
            };
        }

    return options;
};


striptag = function(value) {
    return value.replace(/(<([^>]+)>)/ig, "");
};


setActiveStyleSheet = function(title) {
    var href;

    switch (title) {
        case "android":
            href = "style/styleA.css";
            break;
        default:
            href = "style/style.css";
    }

    for (var i = 0; (a = document.getElementsByTagName("link")[i]); i++) {
        if (a.getAttribute("title") == "unityCSS")
            a.href = href;
    }
};

guid = function() {
    function _p8(s) {
        var p = (Math.random().toString(16) + "000000000").substr(2, 8);
        return s ? "-" + p.substr(0, 4) + "-" + p.substr(4, 4) : p;
    }
    return _p8() + _p8(true) + _p8(true) + _p8();
};

getAboutPanel = function(appName, version) {
    if (!aboutPanel)
        aboutPanel = new Ext.Panel({
            id: 'aboutPanel',
            cls: 'aboutPanel',
            floating: true,
            centered: true,
            modal: true,
            html: '<div class="aboutText">' +
                    '<div class="aboutLogo"></div>' +
                    '<div class="aboutName">' + appName + '</div>' +
                    '<div class="aboutVersion">Version ' + version + '</div>' +
                    '<div class="aboutGreetings">-By GFT Mobility Team</div>' +
                    '</div>'
        });

    return	aboutPanel;
};


preventRotateAndroidPhone = function() {
    if (Appverse.is.Android) {
        // PREVENT APPLICATION TO ROTATE, JUST FOR SMARTPHONES
        if (Appverse.is.Phone)
            if (Ext.getOrientation() == "landscape") {
                maskPanel = new Ext.Panel({
                    id: 'maskPanel',
                    cls: 'maskPanel',
                    modal: true,
                    autoDestroy: true,
                    hideOnMaskTap: false,
                    fullscreen: true,
                    floating: true,
                    centered: true,
                    html: '<div class="noRotate"></div>',
                    listeners: {
                        hide: function() {
                            maskPanel.destroy();
                        },
                        destroy: function() {
                            maskPanel = null;
                        }
                    }
                });
                maskPanel.show();
                return maskPanel;
            } else {
                try {
                    Appverse.System.LockOrientation(true, Appverse.System.ORIENTATION_PORTRAIT);
                } catch (e) {

                }
            }
    }
};
dbObject = null;
getDB = function(name) {
    if (name.indexOf('.db') == -1)
        name = name + ".db";
    if (dbObject) {
        if (dbObject.Name == name)
            return dbObject;
    }
    if (Appverse.Database.ExistsDatabase(name)) {
        // get database reference object
        dbObject = Appverse.Database.GetDatabase(name);
    }
    else {
        // database does not exists, create it.
        dbObject = Appverse.Database.CreateDatabase(name);
    }
    return dbObject;
};

createTableDB = function(dbName, tableName, properties) {

    var existsTable = Appverse.Database.Exists(getDB(dbName), tableName);

    if (!existsTable) {
        // 1.1. Create settings table is it does not exists
        //var createTableSql = "CREATE table "+tableName + " (" + properties + ")";
        existsTable = Appverse.Database.CreateTable(getDB(dbName), tableName, properties);
    }
    return existsTable;

};

deleteTableDB = function(dbName, tableName) {

    var existsTable = Appverse.Database.Exists(getDB(dbName), tableName);
    var deletedTable = false;

    if (existsTable) {
        deletedTable = Appverse.Database.DeleteTable(getDB(dbName), tableName);
    }
    return deletedTable;
};

dmlDB = function(dbName, statement) {

    res = Appverse.Database.ExecuteSQLStatement(getDB(dbName), statement);

    return res;
};

queryDB = function(dbName, query) {
    rs = Appverse.Database.ExecuteSQLQuery(getDB(dbName), query);
    return rs;
};

function stripTags(value) {
    return value.replace(/(<([^>]+)>)/ig, "");
}
;

// param dateAttribute must be coded as DD/MM/YYYY

function filterByDate(store, startDate, endDate, dateAttribute) {

    startinMili = startDate.getTime();
    endinMili = endDate.getTime();
    if (startinMili > endinMili) {
        Ext.Msg.alert('ERROR');
        return false;
    }

    store.filterBy(function(record, id) {
        dateOp = record.get(dateAttribute).split('/');
        dateOp = new Date(dateOp[1] + '/' + dateOp[0] + '/' + dateOp[2]).getTime();
        return ((dateOp >= startinMili) && (dateOp <= endinMili));
    });


}
;

Number.prototype.formatMoney = function(c, d, t) {
    var n = this,
            c = isNaN(c = Math.abs(c)) ? 2 : c,
            d = d == undefined ? "," : d,
            t = t == undefined ? "." : t,
            s = n < 0 ? "-" : "",
            i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
            j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};


coords = null;
heading = null;
isValidCoor = false;
populateCoordInterval = null;

populateCoord = function(contPopulate) {
    if (contPopulate == null) {
        googleCoords = backupCoords;
        contPopulate = 0;
        Mbank.myPosition = null;
        isValidCoor = false;
        if (!initializingBranches)
            contPopulate = 10;
    }


    loading();
    //centerMap adaptation

    console.log('populate')
    coords = Appverse.Geo.GetCoordinates();

    // heading= Unity.Geo.GetHeading(Unity.Geo.NORTHTYPE_TRUE);
    if (validCoordinates(coords, contPopulate)) {
        //Ext.getBody().unmask();

        isValidCoor = true;
        Mbank.loadCoordinates = true;
        if (contPopulate < 11) {
            googleCoords = {
                latitude: coords.XCoordinate,
                longitude: coords.YCoordinate
            };
            backupCoords = googleCoords;

        } else {
            googleCoords = Mbank.realCoords;
            if (!Appverse.Geo.IsGPSEnabled()) {
                floatingCreator(Mbank.L10nController.localizedUIString("WARNING"), Mbank.L10nController.localizedUIString("NOLOCATIONENABLE"), 'OK', null, 1);
            } else {
                floatingCreator(Mbank.L10nController.localizedUIString("WARNING"), Mbank.L10nController.localizedUIString("POSITIONNOTFOUND"), 'OK', function() {
                    Mbank.myPosition = googleCoords;
                    backupCoords = googleCoords;
                }, 1);
            }
        }

        Mbank.myPosition = googleCoords;
        // backupCoords= googleCoords;

    } else {

        window.setTimeout(function() {
            populateCoord(++contPopulate)
        }, 1000);

    }

};

validCoordinates = function(value, contPopulate) {
    if (contPopulate > 10) {
        return true;
    }

    if (value == null) {
        return false;
    } else if (value.YCoordinate == 0 && value.XCoordinate == 0) {
        return false;
    } else if (value.YDoP < 0 || value.YDoP > 5000) {
        return false;
    } else if (value.XDoP < 0 || value.XDoP > 5000) {
        return false;
    }

    return true;
};



auSendEmailF = function() { //TODO Issues with data at send email

    testContent = 'PROTOCOL\n';


    function stringToBytes(str) { //Function taken from iWorkspace util.js
        var ch, st, re = [];
        for (var i = 0; i < str.length; i++) {
            ch = str.charCodeAt(i);  // get char
            st = [];                 // set up "stack"
            do {
                st.push(ch & 0xFF);  // push byte to stack
                ch = ch >> 8;          // shift value down by 1 byte
            }
            while (ch);
            // add stack contents to result
            // done because chars have "wrong" endianness
            re = re.concat(st.reverse());
        }
        // return an array of bytes
        return re;
    }

    var byteVCard = stringToBytes(testContent);

    var vCardAttachment = {
        MimeType: 'text/csv',
        FileName: 'vCard.csv',
        Data: byteVCard,
        DataSize: byteVCard.length
    };

    var emailData = {
        Attachment: [vCardAttachment],
        Subject: 'Protocol',
        MessageBodyMimeType: 'text/html',
        MessageBody: 'Order Book content'
    }

    Appverse.Messaging.SendEmail(emailData);
};


keyUpFn = function(fn) {
    return checkKeyUpGoButton = function(field, e) {
        if (e.browserEvent.keyCode == 13) {// Happens when enter is pressed
            fn;
        }
    };
};

failureAjaxFn = function(response, request) {

    return errMessage = '<b>Error en la petici䮼/b> ' + request.url + ' '
            + ' <b>status</b> ' + response.status + ''
            + ' <b>statusText</b> ' + response.statusText + ''
            + ' <b>responseText</b> ' + response.responseText + '';

};

AjaxLocal = function(name, store, append) {
    Ext.Ajax.request({
        url: name,
        method: 'GET',
        success: function(response, request) {
            data = Ext.decode(response.responseText);
            store.loadData(data.exhibitors, append);
        },
        failure: failureAjaxFn,
        timeout: 10000
    });
};

disable_iFrame = function() {
    $('#aptFrame').contents().find('a').click(function(event) {
        //add Methods to do after click
        event.preventDefault();
    });
};
function X2JS() {

    var DOMNodeTypes = {
        ELEMENT_NODE: 1,
        TEXT_NODE: 3,
        DOCUMENT_NODE: 9
    };

    function parseDOMChildren(node) {


        if (node.nodeType == DOMNodeTypes.DOCUMENT_NODE) {
            var result = new Object;
            var child = node.firstChild;
            var childName = child.localName;
            if (childName == null)
                childName = child.nodeName;

            result[childName] = parseDOMChildren(child);
            return result;
        }
        else
        if (node.nodeType == DOMNodeTypes.ELEMENT_NODE) {
            var result = new Object;
            result._cnt = 0;

            var nodeChildren = node.childNodes;

            // Children nodes
            for (var cidx = 0; cidx < nodeChildren.length; cidx++) {
                var child = nodeChildren[cidx];
                var childName = child.localName;
                if (childName == null)
                    childName = child.nodeName;

                result._cnt++;
                if (result[childName] == null) {
                    result[childName] = parseDOMChildren(child);
                    result[childName + "_asArray"] = new Array();
                    result[childName + "_asArray"][0] = result[childName];
                }
                else {
                    if (result[childName] != null) {
                        if (!(result[childName] instanceof Array)) {
                            var tmpObj = result[childName];
                            result[childName] = new Array(nodeChildren.length);
                            result[childName][0] = tmpObj;

                            result[childName + "_asArray"] = result[childName];
                        }
                    }
                    var aridx = 0;
                    while (result[childName][aridx] != null)
                        aridx++;
                    (result[childName])[aridx] = parseDOMChildren(child);
                }
            }

            // Attributes
            for (var aidx = 0; aidx < node.attributes.length; aidx++) {
                var attr = node.attributes[aidx];
                result._cnt++;
                result["_" + attr.name] = attr.value;
            }

            if (result._cnt == 1 && result["#text"] != null) {
                result = result["#text"];
            }
            return result;
        }
        else
        if (node.nodeType == DOMNodeTypes.TEXT_NODE) {
            return node.nodeValue;
        }
    }
    ;

    function startTag(jsonObj, element, attrList, closed) {
        var resultStr = "<" + element;
        if (attrList != null) {
            for (var aidx = 0; aidx < attrList.length; aidx++) {
                var attrName = attrList[aidx];
                var attrVal = jsonObj[attrName];
                resultStr += " " + attrName.substr(1) + "='" + attrVal + "'";
            }
        }
        if (!closed)
            resultStr += ">";
        else
            resultStr += "/>";
        return resultStr;
    }

    function endTag(elementName) {
        return "</" + elementName + ">"
    }

    function endsWith(str, suffix) {
        return str.indexOf(suffix, str.length - suffix.length) !== -1;
    }

    function parseJSONTextObject(jsonTxtObj) {
        var result = "";
        if (jsonTxtObj["#text"] != null) {
            result += jsonTxtObj["#text"];
        }
        else {
            result += jsonTxtObj;
        }
        return result;
    }

    function parseJSONObject(jsonObj) {
        var result = "";

        for (var it in jsonObj) {

            if (endsWith(it.toString(), ("_asArray")) || it.toString()[0] == "_")
                continue;

            var subObj = jsonObj[it];

            var attrList = [];
            for (var ait in subObj) {
                if (ait.toString()[0] == "_") {
                    attrList.push(ait)
                }
            }

            if (subObj != null && subObj instanceof Object && subObj["#text"] == null) {

                if (subObj instanceof Array) {
                    var arrayOfObjects = true;
                    if (subObj.length > 0) {
                        arrayOfObjects = subObj[0] instanceof Object;
                    }
                    else {
                        result += startTag(subObj, it, attrList, true);
                    }

                    for (var arIdx = 0; arIdx < subObj.length; arIdx++) {
                        if (arrayOfObjects)
                            result += parseJSONObject(subObj[arIdx]);
                        else {
                            result += startTag(subObj, it, attrList, false);
                            result += parseJSONTextObject(subObj[arIdx]);
                            result += endTag(it);
                        }
                    }
                }
                else {
                    result += startTag(subObj, it, attrList, false);
                    result += parseJSONObject(subObj);
                    result += endTag(it);
                }
            }
            else {
                result += startTag(subObj, it, attrList, false);
                result += parseJSONTextObject(subObj);
                result += endTag(it);
            }

        }

        return result;
    }

    this.xml2json = function(xmlDoc) {
        return parseDOMChildren(xmlDoc);
    }

    this.xml_str2json = function(xmlDocStr) {
        var parser = new DOMParser();
        var xmlDoc = parser.parseFromString(xmlDocStr, "text/xml");
        return this.xml2json(xmlDoc);
    }

    this.json2xml_str = function(jsonObj) {
        return parseJSONObject(jsonObj, true);
    }

    this.json2xml = function(jsonObj) {
        var parser = new DOMParser();
        var xmlStr = this.json2xml_str(jsonObj);
        return parser.parseFromString(xmlStr, "text/xml");
    }
}
//var x2js = new X2JS();

//ArrayIndexOf(mainScreenItems,function(obj){return obj.mainOrder == 9})
ArrayIndexOf = function(a, fnc) {
    if (!fnc || typeof (fnc) != 'function') {
        return -1;
    }
    if (!a || !a.length || a.length < 1)
        return -1;
    for (var i = 0; i < a.length; i++) {
        if (fnc(a[i]))
            return i;
    }
    return -1;
};


appverseCollect = function(store, field) {
    var myArray = new Array();
    var prev = null;
    sorters = store.getSorters();
    store.sort(field, 'ASC');
    store.each(function(record) {
        value = record.get(field);
        if (value != prev)
            myArray.push(value);
        prev = value;
    });
    store.sort(sorters);
    return myArray;
};

Array.prototype.exists = function(o) {
    for (var i = 0; i < this.length; i++)
        if (this[i] === o)
            return true;
    return false;
};



String.prototype.replaceAll = function(str1, str2, ignore)
{
    return this.replace(new RegExp(str1.replace(/([\,\!\\\^\$\{\}\[\]\(\)\.\*\+\?\|\<\>\-\&])/g, function(c) {
        return "\\" + c;
    }), "g" + (ignore ? "i" : "")), str2);
};

maskBody = function() {
    setTimeout(function() {
        Ext.getBody().mask();
    }, 100);
}
CheckLoadingDone = function() {
    //console.log(Ext.Viewport.getMasked());
    if (AppLoaded && Ext.ComponentQuery.query('loadmask').length) {//Only unmask if it's a load mask
        Ext.ComponentQuery.query('loadmask').forEach(function(item) {
            if (!item.isHidden() && item.getParent().getId() == Ext.Viewport.getId()) {
                Ext.Viewport.setMasked(false);
                clearInterval(TimerMask);
                clearTimeout(TimeoutMask);             
            }
        });
    }
};

loading = function(loadText, maskTimeout) {
    if (!Ext)
        return;
    AppLoaded = false;
    if (!loadText)
        loadText = i18Str.LOADING;
    if(!maskTimeout){
        maskTimeout= 60000;
    }
    /*TimerMask = window.setInterval(CheckLoadingDone, 500);*/
    Ext.Viewport.setMasked({
        xtype: 'loadmask',
        cls: 'loadingMask',
        message: loadText,
        indicator: true,
        zIndex: 2200
    });
    TimeoutMask= setTimeout(function(){Ext.Viewport.setMasked(false);
    /*clearInterval(TimerMask);*/},maskTimeout);

};

unMask=function(){
    if (Ext.ComponentQuery.query('loadmask').length) {//Only unmask if it's a load mask
        Ext.ComponentQuery.query('loadmask').forEach(function(item) {
            if (!item.isHidden() && item.getParent().getId() == Ext.Viewport.getId()) {
                Ext.Viewport.setMasked(false);
                clearTimeout(TimeoutMask);             
            }
        });
    }
    
}

$import = function(c, async) {
    var b = document.createElement("script");
    if (async)
        b.setAttribute("async", true);
    b.setAttribute("src", c);
    b.setAttribute("type", "text/javascript");
    document.getElementsByTagName("head")[0].appendChild(b)
};

wait = function(sec) {
    if (isNaN(sec))
        sec = 10;
    t = new Date().getTime();
    waitToggle = true;
    while (waitToggle) {
        p = new Date().getTime();
        if ((p - t) > sec * 1000)
            waitToggle = false;
    }
}


var dateFormat = function() {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
            timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
            timezoneClip = /[^-+\dA-Z]/g,
            pad = function(val, len) {
                val = String(val);
                len = len || 2;
                while (val.length < len)
                    val = "0" + val;
                return val;
            };

    // Regexes and supporting functions are cached through closure
    return function(date, mask, utc) {
        var dF = dateFormat;

        // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
        if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
            mask = date;
            date = undefined;
        }

        // Passing date through Date applies Date.parse, if necessary
        date = date ? new Date(date) : new Date;
        if (isNaN(date))
            throw SyntaxError("invalid date");

        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        // Allow setting the utc argument via the mask
        if (mask.slice(0, 4) == "UTC:") {
            mask = mask.slice(4);
            utc = true;
        }

        var _ = utc ? "getUTC" : "get",
                d = date[_ + "Date"](),
                D = date[_ + "Day"](),
                m = date[_ + "Month"](),
                y = date[_ + "FullYear"](),
                H = date[_ + "Hours"](),
                M = date[_ + "Minutes"](),
                s = date[_ + "Seconds"](),
                L = date[_ + "Milliseconds"](),
                o = utc ? 0 : date.getTimezoneOffset(),
                flags = {
                    d: d,
                    dd: pad(d),
                    ddd: dF.i18n.dayNames[D],
                    dddd: dF.i18n.dayNames[D + 7],
                    m: m + 1,
                    mm: pad(m + 1),
                    mmm: dF.i18n.monthNames[m],
                    mmmm: dF.i18n.monthNames[m + 12],
                    yy: String(y).slice(2),
                    yyyy: y,
                    h: H % 12 || 12,
                    hh: pad(H % 12 || 12),
                    H: H,
                    HH: pad(H),
                    M: M,
                    MM: pad(M),
                    s: s,
                    ss: pad(s),
                    l: pad(L, 3),
                    L: pad(L > 99 ? Math.round(L / 10) : L),
                    t: H < 12 ? "a" : "p",
                    tt: H < 12 ? "am" : "pm",
                    T: H < 12 ? "A" : "P",
                    TT: H < 12 ? "AM" : "PM",
                    Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
                    o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
                    S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
                };

        return mask.replace(token, function($0) {
            return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
}();

// Some common format strings
dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
    isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
    dayNames: [
        "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
        "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
    ],
    monthNames: [
        "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
        "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
    ]
};

// For convenience...
Date.prototype.format = function(mask, utc) {
    return dateFormat(this, mask, utc);
};


SecurityUtils = {
    sessionTime: new Date().getTime()
};

SecurityUtils.isSessionExpired = function() {
    var milisecondsTimeOut = config.INACTIVITY_TIMEOUT * 60 * 1000;
    var actualTime = new Date().getTime();
    if (actualTime - SecurityUtils.sessionTime > milisecondsTimeOut) {
        return true;
    } else {
        return false;
    }
};

SecurityUtils.resetSessionTime = function() {
    SecurityUtils.sessionTime = new Date().getTime();
};