mappingServicesMock = function(action) {
    var path;
    switch (action) {
        case 'verifyActivationCode':
            path = '/mock/verify_activation_code';
            //{"activationCode":"","context":{"guid":"","activeUser":{"deviceUniqueID":"","institutionCode":"","userCode":""},"language":0,"version":"","deviceModel":""}}
            break;
        case 'verifyFirstAccess':
            path = '/mock/verify_first_access';
            //{"password":"","activationCode":"","context":{"guid":"","activeUser":{"deviceUniqueID":"","institutionCode":"","userCode":""},"language":0,"version":"","deviceModel":""}}
            break;
        case 'verifyActivate':
            path = '/mock/activate';
            //{"authorizationInfoList":[],"context":{"guid":"","activeUser":{"deviceUniqueID":"","institutionCode":"","userCode":""},"language":0,"version":"","deviceModel":""}}
            break;

        case 'verifyAuth':
            path = '/mock/verify_authentication';
            //{"password":"","visualizationFlags":{"balanceFlag":false,"lastAccessFlag":false,"lastPaymentsFlag":false,"operatingServicesFlag":false,"settingsFlag":false},"context":{"guid":"","activeUser":{"deviceUniqueID":"","institutionCode":"","userCode":""},"language":0,"version":"","deviceModel":""}}
            break;

        case 'userStatus':
            path = '/mock/secure/user_status';
            checkSessionRequired = true;
            //{"flags":{"balanceFlag":false,"lastAccessFlag":false,"lastPaymentsFlag":false,"operatingServicesFlag":false,"settingsFlag":false},"context":{"guid":"","activeUser":{"deviceUniqueID":"","institutionCode":"","userCode":""},"language":0,"version":"","deviceModel":""}}
            break;

        case 'contacts':
            path = '/mock/secure/get_contacts';
            checkSessionRequired = true;
            //{"userContacts":[],"context":{"guid":"","activeUser":{"deviceUniqueID":"","institutionCode":"","userCode":""},"language":0,"version":"","deviceModel":""}}
            break;
        case 'executePay':
            path = '/mock/secure/execute_payment';
            checkSessionRequired = true;
            escapedRequestData = true;  //escapedRequestData true because user can enter special characters
            //{"payment":{"amount":0.01,"reason":"","contactAddressee":{"phoneContact":{},"mail":""},"operationDate":1390222265035,"instrumentID":0,"paymentStatus":0},"authorizationInfoList":[{ "authorizationCode": "42", "authorizationValue": "31"}],"context":{"guid":"","activeUser":{"deviceUniqueID":"","institutionCode":"","userCode":""},"language":0,"version":"","deviceModel":""}}
            break;
        case 'authCode':
            path = '/mock/secure/auth_code';
            //{"context":{"guid":"","activeUser":{"deviceUniqueID":"","institutionCode":"","userCode":""},"language":0,"version":"","deviceModel":""}}
            break;

        case 'thresholds':
            path = '/mock/secure/modify_thresholds';
            checkSessionRequired = true;
            escapedRequestData = true;
            //{"operatingService":{"service":{"type":0,"status":0},"instrument":{"id":0,"type":0,"cardNumber":"","iban":""},"thresholds":{}},"authorizationInfoList":[{ "authorizationCode": "42", "authorizationValue": "31"}],"context":{"guid":"","activeUser":{"deviceUniqueID":"","institutionCode":"","userCode":""},"language":0,"version":"","deviceModel":""}}
            break;

    }
    return path;
};

mappingServicesReal = function(action) {
    var path;
    switch (action) {
        case 'verifyFirstAccess':
            path = '/rest/sec/verifyFirstAccess';
            break;
        case 'verifyActivate':
            path = '/rest/sec/activate';
            break;
        case 'verifyAuth':
            path = '/rest/sec/login';
            break;

        case 'userStatus':
            path = '/rest/jsonservices/userServiceFacade/retrieveUserStatus';
            checkSessionRequired = true;
            break;

        case 'contacts':
            path = '/rest/jsonservices/paymentServiceFacade/getContacts';
            checkSessionRequired = true;
            break;
        case 'inviteContact':
            path = '/rest/jsonservices/userServiceFacade/inviteNewContact';
            checkSessionRequired = true;
            break;
        case 'executePay':
            path = '/rest/jsonservices/paymentServiceFacade/executePayment';
            checkSessionRequired = true;
            escapedRequestData = true;  //escapedRequestData true because user can enter special characters
            break;
        case 'authCode':
            path = '/rest/jsonservices/paymentServiceFacade/getAuthorizationInfo';
            break;
        case 'cancelPay':
            path = '/rest/jsonservices/paymentServiceFacade/cancelPendingPayment';
            checkSessionRequired = true;
            break;

        case 'thresholds':
            path = '/rest/jsonservices/settingsServiceFacade/modifyThresholds';
            checkSessionRequired = true;
            break;
        case 'saveWallet':
            path = '/rest/jsonservices/settingsServiceFacade/modifyPreferences';
            checkSessionRequired = true;
            break;

        case 'logout':
            path = '/rest/sec/logout';
            break;
            
        //PRELOGIN
        case 'retrieveSettings':
            path = '/rest/jsonservices/preloginServiceFacade/retrieveSettings';
            break;
        case 'discoverServices':
            path = '/rest/jsonservices/preloginServiceFacade/discoverServices';
            break;
            
        case 'verifyPendingPayment':
            path = '/rest/jsonservices/offlinePaymentServiceFacade/verifyPendingPayment';
            break;
        case 'verifyCollectAuthorizationCode':
            path = '/rest/jsonservices/offlinePaymentServiceFacade/verifyCollectAuthorizationCode';
            break;
        case 'confirmCollectPendingPayment':
            path = '/rest/jsonservices/offlinePaymentServiceFacade/confirmCollectPendingPayment';
            break;
    }
    return path;
};

//mappingServices = mappingServicesMock;
mappingServices = mappingServicesReal;


osInfo = Appverse.System.GetOSInfo().Version;
deviceModel = Appverse.System.GetOSHardwareInfo().Name + "/" + osInfo;

generateContext = function() {
    if (!institutionCode)
        institutionCode = "";

    //TEST
    registrationNumber = "9999999999";
    return '"context":{"guid":"' + guid() + '","activeUser":{"deviceUniqueID":"' + deviceUniqueId + '","institutionCode":"' + institutionCode + '","userCode":"' + username + '","groupCode":"' + config.GROUP_CODE + '"},"language":' + languageEnum + ',"version":"' + config.VERSION_APP + '","deviceModel":"' + deviceModel + '","registrationNumber":"' + registrationNumber + '"}';
};

goBackLogin = function() {
    console.log("APP EXIT: User clicks Logout button or Session Expired");
    if (Appverse.isBackground()) {
        console.log("App is currently in background... app reload will be delayed");
        reloadDelayed = true;
        //window.setInterval(goBackLogin, 500); // check background status after 500 mseconds
    } else {
        reloadDelayed = false;
        location.reload();
    }
};

androidKeyboardFn = function() {
    if (Appverse.is.Android) {
        timeToHideAndroidKeyboard = 800;
    }
};

getAID = function() {
    try {
        if (config.NFC_APP_ID[institutionCode][1]) {
            if (username.substring(0, 2) == config.NFC_APP_ID[institutionCode][1]) {
                AIDNFC = config.NFC_APP_ID[institutionCode][2];
            }
            else {
                AIDNFC = config.NFC_APP_ID[institutionCode][0];
            }
        }
        else {
            AIDNFC = config.NFC_APP_ID[institutionCode][0];
        }
    }
    catch (e) {
        console.log('ERROR GETTING AID ---- INSTITUTIONCODE:' + institutionCode + ' ERROR:' + e);
    }
};

checkP2PStatus = function(services) {
    p2pSOSP = false;
    favP2PPayment = null;
    $('.statusp2p').html('<div class="iconNonSusc"></div><div class="statusText">' + i18Str.SERVICE_DISABLED + '</div>');
    Ext.ComponentQuery.query('button[name="p2pMenu"]')[0].disable();
    $('.imgp2p').attr("src", './resources/images/ubipay_inv_den_non_attivo.png');
    Ext.ComponentQuery.query('button[name="p2pPaySubmenu"]')[0].enable();
    Ext.ComponentQuery.query('button[name="p2pTransfersSubmenu"]')[0].enable();
    Ext.ComponentQuery.query('button[name="p2pSettingsSubmenu"]')[0].enable();
    $('.descriptionp2p').show();
    if (textObject["K_SER_P2P_DIS"]) {
        $('.descriptionp2p').html(textObject["K_SER_P2P_DIS"]);
    }
    if (services) {
        services.forEach(function(item) {
            var serviceName, imgName;
            switch (item.service.type) {
                case config.P2P_SERVICE:
                    serviceName = 'p2p';
                    imgName = './resources/images/ubipay_inv_den_attivo.png';
                    if (item.favourites) {
                        if (item.favourites.flgFavPayment) {
                            favP2PPayment = item;
                        }
                        else {
                            if (!favP2PPayment) {
                                favP2PPayment = item;
                            }
                        }
                    }
                    switch (item.service.status) {
                        case config.SERVICE_ACTIVE:
                            $('.img' + serviceName).attr("src", imgName);
                            $('.status' + serviceName).html('<div class="statusText">' + i18Str.SERVICE_AVAILABLE + '</div>');
                            Ext.ComponentQuery.query('button[name="p2pMenu"]')[0].enable();
                            $('.descriptionp2p').hide();
                            break;
                        case config.SERVICE_DISABLED:
                            favP2PPayment = null;
                            break;
                        case config.SERVICE_SAT_GG:
                            if (textObject["K_SER_SAT_GG"]) {
                                $('.descriptionp2p').html(textObject["K_SER_SAT_GG"]);
                            }
                            Ext.ComponentQuery.query('button[name="p2pMenu"]')[0].enable();
                            Ext.ComponentQuery.query('button[name="p2pPaySubmenu"]')[0].disable();
                            break;
                        case config.SERVICE_SAT_MM:
                            if (textObject["K_SER_SAT_MM"]) {
                                $('.descriptionp2p').html(textObject["K_SER_SAT_MM"]);
                            }
                            Ext.ComponentQuery.query('button[name="p2pMenu"]')[0].enable();
                            Ext.ComponentQuery.query('button[name="p2pPaySubmenu"]')[0].disable();
                            break;
                        case config.SERVICE_SAT_NO:
                            if (textObject["K_SER_SAT_NO"]) {
                                $('.descriptionp2p').html(textObject["K_SER_SAT_NO"]);
                            }
                            Ext.ComponentQuery.query('button[name="p2pMenu"]')[0].enable();
                            Ext.ComponentQuery.query('button[name="p2pPaySubmenu"]')[0].disable();
                            break;
                        case config.SERVICE_SUSPENDED:
                            if (textObject["K_SER_SOSP"]) {
                                $('.descriptionp2p').html(textObject["K_SER_SOSP"]);
                            }
                            p2pSOSP = true;
                            Ext.ComponentQuery.query('button[name="p2pMenu"]')[0].enable();
                            Ext.ComponentQuery.query('button[name="p2pPaySubmenu"]')[0].disable();
                            Ext.ComponentQuery.query('button[name="p2pSettingsSubmenu"]')[0].disable();
                            break;
                    }
                    break;
                    /*case config.NFC_SERVICE:
                     serviceName = 'nfc';
                     imgName = './resources/images/HomePlaceholder.png';
                     break;
                     case config.MP_SERVICE:
                     serviceName = 'wallet';
                     imgName = './resources/images/Masterpass_Foto.png';
                     break;*/
            }

        });
    }
    checkHomeHeight();
};

checkNFCStatus = function() {
    $('.descriptionnfc').show();
    if (Appverse.is.iOS || (Appverse.is.Android && (parseFloat(osInfo) < 4))) {
        NFCAvailable = false;
        var storeHome = Ext.ComponentQuery.query('list[name="homeList"]')[0].getStore();
        var index = storeHome.find('service', "nfc");
        if (index != -1) {
            storeHome.removeAt(index);
        }
        Ext.ComponentQuery.query('button[name="nfcMenu"]')[0].hide();
        /*$('.statusnfc').html('<div class="iconNonSusc"></div><div class="statusText">' + i18Str.SERVICE_DISABLED + '</div>');
         if (textObject["K_NFCNOTSUP"]) {
         $('.descriptionnfc').html(textObject["K_NFCNOTSUP"]);
         }
         Ext.ComponentQuery.query('button[name="nfcMenu"]')[0].disable();
         $('.imgnfc').attr("src", './resources/images/ubipay_pago_cl_non_attivo.png');*/
    }
    else {
        NFCAvailable = true;
        if (Appverse.NFC.IsWalletAppInstalled(config.WALLET_PACKAGE)) {
            $('.statusnfc').html('<div class="statusText">' + i18Str.SERVICE_AVAILABLE + '</div>');
            Ext.ComponentQuery.query('button[name="nfcMenu"]')[0].enable();
            $('.imgnfc').attr("src", './resources/images/ubipay_pago_cl_attivo.png');
            $('.descriptionnfc').hide();
            getAID();
        }
        else {
            $('.statusnfc').html('<div class="iconNonSusc"></div><div class="statusText">' + i18Str.SERVICE_DISABLED + '</div>');
            //$('.descriptionnfc').html(i18Str.NFC_TELCO_ERROR);
            if (textObject["K_NFCWALTELCO"]) {
                $('.descriptionnfc').html(textObject["K_NFCWALTELCO"]);
            }
            Ext.ComponentQuery.query('button[name="nfcMenu"]')[0].disable();
            $('.imgnfc').attr("src", './resources/images/ubipay_pago_cl_non_attivo.png');
        }
    }
    checkHomeHeight();
};
checkHomeHeight = function() {
    if ($('.homeList .x-scroll-scroller-vertical').height() > $('.homeList').height()) {
        Ext.ComponentQuery.query('list[name="homeList"]')[0].setScrollable(true);
    } else {
        Ext.ComponentQuery.query('list[name="homeList"]')[0].setScrollable(false);
    }
}

checkWalletHeight = function() {
    if ($('.walletListPanel .x-scroll-scroller-vertical').height() > $('.walletListPanel').height()) {
        Ext.ComponentQuery.query('container[name="walletListPanel"]')[0].setScrollable(true);
    } else {
        Ext.ComponentQuery.query('container[name="walletListPanel"]')[0].setScrollable(false);
    }
}

handleSecurityExceptionNFC = function(exception) {
    switch (exception.Type) {
        case  Appverse.NFC.SECURITY_ERROR_USB_DEBUG_ENABLED:
            Ext.Msg.alert(textObject["K_NFC_TIT_ERR"] ? textObject["K_NFC_TIT_ERR"] : "K_NFC_TIT_ERR", textObject["K_NFCDEBUGON"] ? textObject["K_NFCDEBUGON"] : "K_NFCDEBUGON");
            AppLoaded = true;
            unMask();
            break;
        case  Appverse.NFC.SECURITY_ERROR_DEVICE_ROOTED:
            Ext.Msg.alert(textObject["K_TIT_NFCROOT"] ? textObject["K_TIT_NFCROOT"] : "K_TIT_NFCROOT", textObject["K_NFCROOTPRIV"] ? textObject["K_NFCROOTPRIV"] : "K_NFCROOTPRIV");
            AppLoaded = true;
            unMask();
        case  Appverse.NFC.SECURITY_ERROR_LOCK_DISABLED:
        case  Appverse.NFC.SECURITY_ERROR_UNKNOWN:
        default:
            Ext.Msg.alert(textObject["K_NFC_TIT_ERR"] ? textObject["K_NFC_TIT_ERR"] : "K_NFC_TIT_ERR", textObject["K_NFC_SECERR"] ? textObject["K_NFC_SECERR"] : "K_NFC_SECERR");
            AppLoaded = true;
            unMask();
            break;
    }
};

fillSettingsForm = function() {
    if (NFCAvailable) {
        Ext.ComponentQuery.query('container[name="settingsNFC"]')[0].show();
    }
    else {
        Ext.ComponentQuery.query('container[name="settingsNFC"]')[0].hide();
    }
    //Ext.ComponentQuery.query('container[name="resultMessageSettings"]')[0].hide();
    if (skipTutorial) {
        Ext.ComponentQuery.query('checkboxfield[name="tutorialCheck"]')[0].check();
    }
    else {
        Ext.ComponentQuery.query('checkboxfield[name="tutorialCheck"]')[0].uncheck();
    }
    if (defaultNFCTime) {
        Ext.ComponentQuery.query('selectfield[name="selectTimeNFC"]')[0].setValue(defaultNFCTime);
    }
    else {
        Ext.ComponentQuery.query('selectfield[name="selectTimeNFC"]')[0].getSelectTimeNFC().setValue(30);
    }
    if (enableWidget) {
        Ext.ComponentQuery.query('checkboxfield[name="widgetCheck"]')[0].check();
    }
    else {
        Ext.ComponentQuery.query('checkboxfield[name="widgetCheck"]')[0].uncheck();
    }
	Ext.ComponentQuery.query('selectfield[name="selectTimeNFC"]')[0].removeCls('changed');

};

//Language selection
(function() {
    try {
        var localeCurrent = Appverse.System.GetLocaleCurrent();
        var language = config.DEFAULT_LANGUAGE;
        if (localeCurrent) {
            language = localeCurrent.Language;
        }
        var languageFound = Appverse.I18N.GetLocaleSupportedDescriptors().indexOf(language);

        if (languageFound != -1) {
            i18Str = Appverse.I18N.GetResourceLiterals(language);
            console.log('Loading resources for language: ' + language);
        } else {
            i18Str = Appverse.I18N.GetResourceLiterals();
            language = config.DEFAULT_LANGUAGE;
            console.log('Loading resources for default language (check i18n configuration)');
        }
        if (!i18Str) {
            console.log('WARNING:: resource literals are not correctly loaded. Please check your i18n configuration.');
            i18Str = {}; // avoiding null pointer
            language = config.DEFAULT_LANGUAGE;
            location.reload();
        }

    } catch (e) {
        console.log('WARNING:: there was an unhandled error during the language selection. Exception message: ' + e);
        i18Str = {}; // avoiding null pointer
        language = config.DEFAULT_LANGUAGE;
        location.reload();
    }
    switch (language) {
        case 'it':
            languageEnum = config.ITALIAN_ENUM;
            break;
        case 'ita':
            languageEnum = config.ITALIAN_ENUM;
            break;
        case 'en':
            languageEnum = config.ENGLISH_ENUM;
            break;
        case 'eng':
            languageEnum = config.ENGLISH_ENUM;
            break;
    }
}());

//Server Connection
getDataFromServer = function(action, content, callback) {
    checkSessionRequired = false;
    escapedRequestData = false;
    var path = mappingServices(action);
    if (!skipLoadingMask) {
        loading();
    }
    setTimeout(function() {
        if (Appverse.Net.IsNetworkReachable(config.CONNECTIVITY_HOST)) {
            if (checkSessionRequired) {
                if (SecurityUtils.isSessionExpired()) {
                    Ext.Viewport.hideMenu('left'); //SIAPTPE-179 - App in foreground - Close menu when session expired
                    Ext.Msg.alert(i18Str.ATENTION, i18Str.SESSION_EXPIRED, function() {
                        inactivityTimeout = true;
                        console.log("SESSION EXPIRED (by app)");
                        getDataFromServer('logout', '{ ' + generateContext() + '}', 'logoutCallback');
                    });
                    AppLoaded = true;
                    unMask();
                    return;
                }
                else {
                    SecurityUtils.resetSessionTime();
                }
            }
            try {
                serverES.Endpoint.Path = originalServerPath + path;

                if (content)
                    serviceRequest.Content = content;
                else
                    serviceRequest.Content = "";

                if (escapedRequestData)
                    Appverse.unescapeNextRequestData = false; //Appverse.unescapeNextRequestData changed to false because user can enter special characters

                // prepare callback interception
                var callbackid = callback + "_" + Date.now();
                AppverseLog.interceptFunction(callback, callbackid);
                Appverse.IO.Async.InvokeService(serviceRequest, serverES, null, callback, callbackid);

            } catch (e) {
                console.log(e);

                // sending error to Appverse remote logging
                AppverseLog.logTrace(AppverseLog.status[3], 'getDataFromServer', 'Error invoking service [' + path + ']. Exception message: ' + e, 0);

                return null;
            }
        }
        else {
            Ext.Msg.alert(i18Str.ATENTION, i18Str.NO_CONNECTIVITY);
            AppLoaded = true;
            unMask();
        }
    }, 80);
};

AppverseLog.buildNum = "build_" + config.VERSION_APP;
if ((typeof (Appverse.is.Emulator) != "undefined" && Appverse.is.Emulator == true) || !config.ALLOW_LOG)
    AppverseLog.enabled = false;
else
    AppverseLog.enabled = true;