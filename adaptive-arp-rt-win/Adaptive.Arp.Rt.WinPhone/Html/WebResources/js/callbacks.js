
window['callbackTraceUnhandledError'] = function(callbackFnName, callbackFnArguments, exceptionMessage, skipAlert) {
    console.log(exceptionMessage);
    // sending error to Appverse remote logging
    AppverseLog.logTrace(AppverseLog.status[3], callbackFnName, 'Exception message: ' + exceptionMessage + ' --------- Arguments: ' + JSON.stringify(callbackFnArguments), 0);
    if (!skipAlert) {
        Ext.Msg.alert(i18Str.ATENTION, i18Str.UNHANDLED_SERVER_ERROR);
    }
};
window['checkSpecificErrors'] = function(result) {
    switch (result.code) {
        case config.CODE_SESSION_EXPIRATION:
            if (inactivityTimeout) {//if session already expired in the app and in the server we avoid to show another alert to user
                console.log("SESSION EXPIRED (by server)");
                goBackLogin();

            }
            else {
                Ext.Viewport.hideMenu('left'); //SIAPTPE-179 - App in foreground - Close menu when session expired
                Ext.Msg.alert(i18Str.ATENTION, i18Str.SESSION_EXPIRED, function() {
                    UBI.app.getController('MainCtrl').cancelCurrentPayments();
                    console.log("SESSION EXPIRED (by server)");
                    goBackLogin();
                });
            }
            break;
        case config.CODE_UNMATCHING_VERSION:
            Ext.Msg.alert(i18Str.ATENTION, i18Str.WRONG_VERSION, function() {
                console.log("NO MATCHING VERSION (goBackLogin)");
                goBackLogin();
            });
            break;
        default:
            var escapedError = escape(result.message);
            escapedError = unescape(escapedError);
            Ext.Msg.alert(i18Str.ATENTION, escapedError);
            return false;
    }
    return true;
};

window['refreshSessionCookies'] = function(result) {
    serviceRequest.Session.Cookies = [];
    for (i = 0; i < result.Session.Cookies.length; i++) {
        if (result.Session.Cookies[i]) {
            var cookiesL = serviceRequest.Session.Cookies.length;
            serviceRequest.Session.Cookies[cookiesL] = {};
            serviceRequest.Session.Cookies[cookiesL].Name = result.Session.Cookies[i].Name;
            serviceRequest.Session.Cookies[cookiesL].Value = result.Session.Cookies[i].Value;
        }

    }
};

window['loginFn'] = function(result) {
    refreshSessionCookies(result);

    SecurityUtils.resetSessionTime();


    Ext.Viewport.setActiveItem('main');
    if (skipTutorial) {
        Ext.ComponentQuery.query('main')[0].setActiveItem('home');
    }
    else {
        Ext.ComponentQuery.query('main')[0].setActiveItem('tutorial');
        Ext.ComponentQuery.query('button[name="menuButton"]')[0].hide();
    }

    if (userData.textApp) {
        textObject = {};
        userData.textApp.forEach(function(item) {
            textObject[item["key"]] = item["text"];
        });
    }

    Ext.ComponentQuery.query('button[name="walletMenu"]')[0].disable();

    //NFC
    checkNFCStatus();

    checkP2PStatus(userData.userStatus.operatingServices);

    //Call MP Service

    skipLoadingMask = true;
    getDataFromServer('userStatus', '{"flagSafety":false,"flags":{"balanceFlag":false,"lastAccessFlag":false,"lastPaymentsFlag":false,"operatingServicesFlag":true,"settingsFlag":false,"searchPaymentsFlag":false,"visualizationType":' + config.VIEW_MP + '},' + generateContext() + '}', 'getMPLoginCallback');
    //getMPLoginCallback();
    if (favP2PPayment) {//SIAPTPE-187 - Avoid unecessary callback when P2P service is not 'ATT'
        if (favP2PPayment.service.status == config.SERVICE_ACTIVE) { //SIAPTPE-187 - Avoid unecessary callback when P2P service is not 'ATT'
            //get balance
            skipLoadingMask = true;
            getDataFromServer('userStatus', '{"flagSafety":false,"flags":{"balanceFlag":true,"lastAccessFlag":false,"lastPaymentsFlag":false,"operatingServicesFlag":true,"settingsFlag":false,"searchPaymentsFlag":false,"visualizationType":' + config.VIEW_P2P + '},' + generateContext() + '}', 'getBalanceLoginCallback');

            skipLoadingMask = true;
            dontnavigate = true;
            getDataFromServer('contacts', '{ "userContacts":' + JSON.stringify(userContactstoServer) + ',"flagSafety":false,' + generateContext() + '}', 'getContactsCallback');
        }
    }

};

window['firstAccessCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            institutionCode = contentObj.institutionCode;
            $('.firstLoginText .text').html(i18Str.FIRST_LOGIN + " " + contentObj.authorizationInfoList[0].authorizationCode + ".");
            authCodeObj = contentObj.authorizationInfoList[0];
            Ext.ComponentQuery.query('container[name="loginForm"]')[0].hide();
            Ext.ComponentQuery.query('container[name="loginFirstTime"]')[0].show();

            // in order to have same session between consecutives "verifyFirstAccess" and "activate" calls.
            refreshSessionCookies(result);
        }
        else {
            checkSpecificErrors(contentObj.result);
            Ext.ComponentQuery.query('passwordfield[name="passwordLoginInput"]')[0].reset();
            Ext.ComponentQuery.query('button[name="loginButton"]')[0].disable();
        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('firstAccessCallback', arguments, e);
        Ext.ComponentQuery.query('passwordfield[name="passwordLoginInput"]')[0].reset();
        Ext.ComponentQuery.query('button[name="loginButton"]')[0].disable();
        AppLoaded = true;
        unMask();
    }
};
window['activateDeviceCallback'] = function(result) {
    try {
        userData = eval('(' + result.Content + ')');
        if (userData.result.success) {
            //TEST
            registrationNumber = "9999999999";
            deviceUniqueId = userData.deviceUniqueID;
            storingKeysFromActivate = true;
            favContactsArr = [];
            unfavContactsArr = [];
            defaultNFCTime = 30;
            skipTutorial = 0;
            enableWidget = 0;
            Appverse.Security.Async.StoreKeyValuePairs([{Key: "UBI_USERNAME", Value: username}, {Key: "UBI_DEVICE_UNIQUE_ID", Value: deviceUniqueId}, {Key: "UBI_INSTITUTION_CODE", Value: institutionCode}, {Key: "UBI_REMEMBER_USER", Value: rememberUser}, {Key: "UBI_SKIP_TUTORIAL", Value: skipTutorial}, {Key: "UBI_NFC_COUNTDOWN", Value: defaultNFCTime}, {Key: "UBI_FAV_CONTACTS", Value: "[]"}, {Key: "UBI_UNFAV_CONTACTS", Value: "[]"}, {Key: "UBI_ENABLE_WIDGET", Value: enableWidget}], null, null);
            //createTableDB(config.DB_NAME, 'authentication' + config.DB_VERSION, ['username text', 'deviceUniqueId text', 'institutionCode text', 'rememberUser integer', 'skipTutorial integer']);
            //var insertUserSql = "INSERT INTO authentication" + config.DB_VERSION + " (username, deviceUniqueId, institutionCode, rememberUser, skipTutorial) values ('" + username + "','" + deviceUniqueId + "','" + institutionCode + "'," + rememberUser + ",0)";
            //dmlDB(config.DB_NAME, insertUserSql);
            activationCallbackResult = result;
            //loginFn(activationCallbackResult);
        }
        else {
            Ext.ComponentQuery.query('passwordfield[name="codeLoginInput"]')[0].reset();
            Ext.ComponentQuery.query('button[name="codeLoginButton"]')[0].disable();
            switch (userData.result.code) {
                case config.CODE_WRONG_AUTH_LOGIN:
                    Ext.Msg.alert(i18Str.ATENTION, userData.result.message);
                    $('.firstLoginText .text').html(i18Str.FIRST_LOGIN + " " + userData.authorizationInfoList[0].authorizationCode + ".");
                    authCodeObj = userData.authorizationInfoList[0];
                    break;
                case config.CODE_USER_LOCKED_LOGIN:
                    Ext.Msg.alert(i18Str.ATENTION, userData.result.message);
                    break;
                case config.CODE_OBSOLETE_VERSION:
                    Ext.Msg.alert(i18Str.ATENTION, userData.result.message);
                    break;
                default:
                    checkSpecificErrors(userData.result);
            }
        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('activateDeviceCallback', arguments, e);
        Ext.ComponentQuery.query('passwordfield[name="codeLoginInput"]')[0].reset();
        Ext.ComponentQuery.query('button[name="codeLoginButton"]')[0].disable();
        AppLoaded = true;
        unMask();
    }
};
window['loginCallback'] = function(result) {
    try {
        console.log(result);
        userData = eval('(' + result.Content + ')');
        if (userData.result.success) {
            var check = Ext.ComponentQuery.query('checkboxfield[name="loginRememberCheck"]')[0].isChecked();
            if (rememberUser != check) {
                if (check) {
                    check = 1;
                }
                else {
                    check = 0;
                }
                Appverse.Security.Async.StoreKeyValuePair({Key: "UBI_REMEMBER_USER", Value: check}, null, null);
                /* var updateSql = "UPDATE authentication" + config.DB_VERSION + " SET rememberUser =" + check + " WHERE username ='" + username + "'";
                 dmlDB(config.DB_NAME, updateSql);*/
            }
            loginFn(result);
        }
        else {
            checkSpecificErrors(userData.result);

            Ext.ComponentQuery.query('passwordfield[name="passwordLoginInput"]')[0].reset();
            Ext.ComponentQuery.query('button[name="loginButton"]')[0].disable();
        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('loginCallback', arguments, e);
        Ext.ComponentQuery.query('passwordfield[name="passwordLoginInput"]')[0].reset();
        Ext.ComponentQuery.query('button[name="loginButton"]')[0].disable();
        AppLoaded = true;
        unMask();
    }


};
window['getMPLoginCallback'] = function(result) {
    skipLoadingMask = false;
    $('.descriptionwallet').show();
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            var services = contentObj.userStatus.operatingServices;
            if (services && services.length) {
                var arrayMPService = $.grep(services, function(e) {
                    return e.service.type == config.MP_SERVICE;
                });
                if (arrayMPService.length) {
                    mpService = arrayMPService;
                }
            }
        }
        if (!mpService) {
            $('.imgwallet').attr("src", './resources/images/ubipay_port_carte_non_attivo.png');
            $('.statuswallet').html('<div class="iconNonSusc"></div><div class="statusText">' + i18Str.SERVICE_DISABLED + '</div>');
            if (textObject["K_SER_MP_DIS"]) {
                $('.descriptionwallet').html(textObject["K_SER_MP_DIS"]);
            }
        }
        else {
            $('.imgwallet').attr("src", './resources/images/ubipay_port_carte_attivo.png');
            $('.statuswallet').html('<div class="statusText">' + i18Str.SERVICE_AVAILABLE + '</div>');
            Ext.ComponentQuery.query('button[name="walletMenu"]')[0].enable();
            $('.descriptionwallet').hide();
        }
    }
    catch (e) {
        callbackTraceUnhandledError('getMPLoginCallback', arguments, e, true);
        $('.imgwallet').attr("src", './resources/images/ubipay_port_carte_non_attivo.png');
        $('.statuswallet').html('<div class="iconNonSusc"></div><div class="statusText">' + i18Str.SERVICE_DISABLED + '</div>');
        if (textObject["K_SER_MP_DIS"]) {
            $('.descriptionwallet').html(textObject["K_SER_MP_DIS"]);
        }
    }
    checkHomeHeight();
};

window['getBalanceLoginCallback'] = function(result) {
    skipLoadingMask = false;
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            var services = contentObj.userStatus.operatingServices;
            if (services && services.length) {
                checkP2PStatus(services);
            }
        }
    }
    catch (e) {
        callbackTraceUnhandledError('getBalanceLoginCallback', arguments, e, true);
    }

};

window['getContactsCallback'] = function(result) {
    skipLoadingMask = false;
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {

            var ownPhoneIndex = null;
            var store = Ext.StoreManager.get('contactsStore');
            //store.setData(contactTest);
            if (contentObj.validContacts && contentObj.validContacts.length) {
                contentObj.validContacts.forEach(function(item) {
                    userContactstoStore.forEach(function(cont, index) {

                        if (item.phoneContact.phoneNumber == cont.phoneContact.phoneNumber) {
                            cont.active = true;
                            cont.favourite = true;
                            cont.showInFavList = true;//TESTING FAVLIST FIX 
                            if (item.status == config.OWN_PHONE) {
                                userContactstoStore.splice(index, 1);
                            }
                        }
                    });
                });
            }

            if (favContactsArr) {
                userContactstoStore.forEach(function(contToStore) {
                    if (favContactsArr.indexOf(contToStore.id) != -1) {
                        contToStore.favourite = true;//TESTING FAVLIST FIX
                        contToStore.showInFavList = true;
                    }

                });
            } else {
                console.log("getContactsCallback#favourites NO FAVOURITES ARRAY INTIALIZED");
            }

            if (unfavContactsArr) {
                userContactstoStore.forEach(function(contToStore) {
                    if (unfavContactsArr.indexOf(contToStore.id) != -1) {
                        contToStore.favourite = false;//TESTING FAVLIST FIX 
                        contToStore.showInFavList = false;
                    }

                });
            } else {
                console.log("getContactsCallback#unfavourites NO UNFAVOURITES ARRAY INTIALIZED");
            }

            store.setData(userContactstoStore);
            store.sort([
                {
                    property: 'listName',
                    direction: 'ASC'
                },
                {
                    property: 'phoneNumber',
                    direction: 'ASC'
                },
                {
                    property: 'id',
                    direction: 'ASC'
                }
            ]);
            store.clearFilter();
            store.filter('showInFavList', true);
            //store.filter('favourite', true);TESTING FAVLIST FIX 

            validateContactsFromServer = true;

            if (!navigatep2p)
                return;
            //AppLoaded in listener painted chooseContact
            Ext.ComponentQuery.query('main')[0].setActiveItem('p2pViewport');
            UBI.app.getController('P2PCtrl').resetP2P();
            Ext.ComponentQuery.query('p2pViewport')[0].setActiveItem('p2pTransferViewport');

            Ext.ComponentQuery.query('titleToolbar[name="p2pTransferToolbar"] button[name="back"]')[0].show();
            serverCrash = false;
            if (updateFlag) {
                AppLoaded = true;
                unMask();
            }
        }
        else {
            AppLoaded = true;
            unMask();
            lastErrorResult = contentObj.result;
            if (serverCrash || updateFlag)
                checkSpecificErrors(lastErrorResult);

            validateContactsFromServer = false;
            serverCrash = true;
        }

    }
    catch (e) {
        AppLoaded = true;
        unMask();

        callbackTraceUnhandledError('getContactsCallback', arguments, e, !serverCrash);
        validateContactsFromServer = false;
        serverCrash = true;
    }

};
window['updateBalanceCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            checkP2PStatus(contentObj.userStatus.operatingServices);
            if (favP2PPayment.balance && typeof (favP2PPayment.balance.amount) == "number") {
                UBI.app.getController('P2PCtrl').getBalanceField().setValue(favP2PPayment.balance.amount.formatMoney(2));
                UBI.app.getController('P2PCtrl').getBalanceFieldFirstStep().setValue(favP2PPayment.balance.amount.formatMoney(2));
                if (favP2PPayment.balance.amount <= 0) {//SIAPTPE-184 Disable confirm if balance is negative
                    UBI.app.getController('P2PCtrl').getConfirmChooseContact().disable();
                }
                else {
                    if ($('.x-list-item.x-item-selected').length) {//SIAPTPE-184 Enable again if balance is positive and there is a contact selected
                        UBI.app.getController('P2PCtrl').getConfirmChooseContact().enable();
                    }
                }

            }
            else {
                UBI.app.getController('P2PCtrl').getBalanceField().setValue(i18Str.BALANCE_UNAVAIL);
                UBI.app.getController('P2PCtrl').getBalanceFieldFirstStep().setValue(i18Str.BALANCE_UNAVAIL);
            }
        }
        else {
            checkSpecificErrors(contentObj.result);
        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('updateBalanceCallback', arguments, e);
        AppLoaded = true;
        unMask();
    }

};
window['authCodeP2PCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            authCodeObj = contentObj.authorizationInfoList[0];
            $('.codePanelTextP2P .text').html(i18Str.CODE_P2P_FIRST + " " + contentObj.authorizationInfoList[0].authorizationCode + ". " + i18Str.CODE_P2P_SECOND + contentObj.authorizationInfoList[0].remainedAttempts);
            Ext.ComponentQuery.query('p2pTransferViewport')[0].setActiveItem('p2pAuth');
        }
        else {
            checkSpecificErrors(contentObj.result);
        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('authCodeP2PCallback', arguments, e);
        AppLoaded = true;
        unMask();
    }
};

window['paymentCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            Ext.ComponentQuery.query('titleToolbar[name="p2pTransferToolbar"] button[name="back"]')[0].hide();
            Ext.ComponentQuery.query('p2pTransferViewport')[0].setActiveItem('p2pResult');
            if (contentObj.result.code == config.CODE_PAYMENT_CONFIRMED) {
                $('.iconP2P').addClass('ok');
                $('.iconP2P').removeClass('pending');
                Ext.ComponentQuery.query('button[name="smsP2P"]')[0].hide();
                $('.operationIDP2P .info').html(contentObj.paymentId);
                $('.operationIDP2P').show();
                $('.dateExecutedP2P .info').html(dateFormat(contentObj.executionDate, "dd/mm/yyyy"));
                $('.dateExecutedP2P').show();
                $('.commisionP2P').show();


            }
            else {
                $('.iconP2P').addClass('pending');
                $('.iconP2P').removeClass('ok');
                Ext.ComponentQuery.query('button[name="smsP2P"]')[0].show();
                $('.commisionP2P').show();
                $('.dateExecutedP2P').hide();
                $('.operationIDP2P').hide();

            }
            $('.iconP2P').removeClass('fail');
            $('.resultP2PText').html(contentObj.result.message);
            getDataFromServer('userStatus', '{"flagSafety":false,"flags":{"balanceFlag":true,"lastAccessFlag":false,"lastPaymentsFlag":false,"operatingServicesFlag":true,"settingsFlag":false,"searchPaymentsFlag":false,"visualizationType":' + config.VIEW_P2P + '},' + generateContext() + '}', 'updateP2PStatusCallback');
        }
        else {
            Ext.ComponentQuery.query('passwordfield[name="codeP2PField"]')[0].reset();
            switch (contentObj.result.code) {
                case config.CODE_WRONG_AUTH_PAYMENT:
                    Ext.Msg.alert(i18Str.ATENTION, contentObj.result.message);
                    authCodeObj = contentObj.authorizationInfoList[0];
                    $('.codePanelTextP2P .text').html(i18Str.CODE_P2P_FIRST + " " + contentObj.authorizationInfoList[0].authorizationCode + ". " + i18Str.CODE_P2P_SECOND + contentObj.authorizationInfoList[0].remainedAttempts);

                    break;
                case config.CODE_USER_LOCKED_PAYMENT:
                default:
                    Ext.ComponentQuery.query('titleToolbar[name="p2pTransferToolbar"] button[name="back"]')[0].hide();
                    Ext.ComponentQuery.query('p2pTransferViewport')[0].setActiveItem('p2pResult');
                    Ext.ComponentQuery.query('button[name="smsP2P"]')[0].hide();
                    $('.resultP2PText').html(contentObj.result.message);
                    $('.iconP2P').addClass('fail');
                    $('.iconP2P').removeClass('pending');
                    $('.iconP2P').removeClass('ok');
                    $('.commisionP2P').hide();
                    //$('.dataReviewPanel').hide();
                    //checkSpecificErrors(contentObj.result);
            }
            AppLoaded = true;
            unMask();
        }

    }
    catch (e) {
        Ext.ComponentQuery.query('passwordfield[name="codeP2PField"]')[0].reset();
        callbackTraceUnhandledError('authCodeP2PCallback', arguments, e);
        AppLoaded = true;
        unMask();
    }
};


/*accountP2PCallback = function(result) {
 Ext.ComponentQuery.query('p2pViewport')[0].setActiveItem('p2pAccount');
 var data = [
 {
 iban: 'IT17 X060 5524 5135 4800 2219 63',
 owner: 'Alberto Rossi'
 }
 ]
 var html = '<div class="defaultPanel">\n\
 <div class="titleText defaultItemText"> ' + i18Str.ACCOUNT_ACTIVE_TITLE + ' </div>';
 html += '<ul class="defaultList">\n\
 <li id="id' + data[0].iban.replace(/\s/g, '') + '" class="listItem">\n\
 <div class="itemContent">\n\
 <div class="type"><span class="title">IBAN</span><span class="value">' + data[0].iban + '</span></div>\n\
 <div class="owner"><span class="title">' + i18Str.ACCOUNT_OWNER + '</span><span class="value">' + data[0].owner + '</span></div>\n\
 </div>\n\
 </li>\n\
 </ul>\n\
 </div>'
 
 
 Ext.ComponentQuery.query('container[name="accountListPanel"]')[0].setHtml(html);
 };*/

window['transfersListCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            Ext.StoreManager.get('contactsStore').clearFilter();
            userData.userStatus.payments = contentObj.userStatus.payments;
            Ext.ComponentQuery.query('main')[0].setActiveItem('p2pViewport');
            Ext.ComponentQuery.query('p2pViewport')[0].setActiveItem('p2pTransfersList');
            //Ext.ComponentQuery.query('secondaryMenu')[0].show();
            var store = Ext.StoreManager.get('transfersStore');
            store.setData(userData.userStatus.payments.payments);
            store.sort('requestedDate', 'DESC');
            Ext.ComponentQuery.query('list[name="transfersList"]')[0].getScrollable().getScroller().scrollTo(0, 0);
            Ext.ComponentQuery.query('container[name="transfersListInfo"]')[0].setHtml(textObject["K_INFO_NUMDISP"] ? textObject["K_INFO_NUMDISP"] : "K_INFO_NUMDISP");//SIAPTPE-185
        }
        else {
            checkSpecificErrors(contentObj.result);
        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('transfersListCallback', arguments, e);
        AppLoaded = true;
        unMask();
    }
};
window['cancelPayCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            var html = '<div class="iconMyWallet ok"></div><div class="resultWalletText">' + contentObj.result.message + '</div>';
            AppLoaded = true;
            unMask();
            Ext.Msg.alert('', html, function() {
                loading();
                getDataFromServer('userStatus', '{"flagSafety":false,"flags":{"balanceFlag":true,"lastAccessFlag":false,"lastPaymentsFlag":true,"operatingServicesFlag":true,"settingsFlag":false,"searchPaymentsFlag":false,"visualizationType":' + config.VIEW_P2P + '},' + generateContext() + '}', 'checkP2PStatusCancelPayment');
            });


        }
        else {
            checkSpecificErrors(contentObj.result);
            var html = '<div class="iconMyWallet fail"></div><div class="resultWalletText">' + contentObj.result.message + '</div>';
            Ext.Msg.alert('', html);
            AppLoaded = true;
            unMask();
        }


    }
    catch (e) {
        callbackTraceUnhandledError('cancelPayCallback', arguments, e);
        AppLoaded = true;
        unMask();
    }

};
window['inviteContactCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
        }
        else {
            checkSpecificErrors(contentObj.result);
        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('checkP2PStatusCancelPayment', arguments, e);
        AppLoaded = true;
        unMask();
    }

};
window['checkP2PStatusCancelPayment'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            userData.userStatus.payments = contentObj.userStatus.payments;
            var p2pServiceNotActive = false;
            var store = Ext.StoreManager.get('transfersStore');
            store.setData(userData.userStatus.payments.payments);
            store.sort('requestedDate', 'DESC');
            if (favP2PPayment.service.status != config.SERVICE_ACTIVE) { //SIAPTPE-187 - Avoid unecessary callback when P2P service is not 'ATT'
                p2pServiceNotActive = true;
            }
            checkP2PStatus(contentObj.userStatus.operatingServices);
            if ((favP2PPayment.service.status == config.SERVICE_ACTIVE) && p2pServiceNotActive) { //SIAPTPE-187 - Avoid unecessary callback when P2P service is not 'ATT'
                navigatep2p = false;
                skipLoadingMask = true;
                dontnavigate = true;
                getDataFromServer('contacts', '{ "userContacts":' + JSON.stringify(userContactstoServer) + ',"flagSafety":false,' + generateContext() + '}', 'getContactsCallback');

            }
            UBI.app.getController('MainCtrl').backTransferDetail();
        }
        else {
            checkSpecificErrors(contentObj.result);
        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('checkP2PStatusCancelPayment', arguments, e);
        AppLoaded = true;
        unMask();
    }

};
window['updateP2PStatusCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            checkP2PStatus(contentObj.userStatus.operatingServices);
        }
        else {
            checkSpecificErrors(contentObj.result);
        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('updateP2PStatusCallback', arguments, e);
        AppLoaded = true;
        unMask();
    }

};
window['updateP2PStatusSettingsCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            var balanceObj = favP2PPayment.balance;
            var p2pServiceNotActive = false;
            if (favP2PPayment.service.status != config.SERVICE_ACTIVE) { //SIAPTPE-187 - Avoid unecessary callback when P2P service is not 'ATT'
                p2pServiceNotActive = true;
            }
            checkP2PStatus(contentObj.userStatus.operatingServices);
            if (favP2PPayment) {
                favP2PPayment.balance = balanceObj;
            }
            if (!favP2PPayment || p2pSOSP) {
                Ext.ComponentQuery.query('container[name="slidersPanelSettings"]')[0].hide();
            }
            if ((favP2PPayment.service.status == config.SERVICE_ACTIVE) && p2pServiceNotActive) { //SIAPTPE-187 - Avoid unecessary callback when P2P service is not 'ATT'
                navigatep2p = false;
                skipLoadingMask = true;
                dontnavigate = true;
                getDataFromServer('contacts', '{ "userContacts":' + JSON.stringify(userContactstoServer) + ',"flagSafety":false,' + generateContext() + '}', 'getContactsCallback');
                //get balance
                skipLoadingMask = true;
                getDataFromServer('userStatus', '{"flagSafety":false,"flags":{"balanceFlag":true,"lastAccessFlag":false,"lastPaymentsFlag":false,"operatingServicesFlag":true,"settingsFlag":false,"searchPaymentsFlag":false,"visualizationType":' + config.VIEW_P2P + '},' + generateContext() + '}', 'getBalanceLoginCallback');
            }

        }
        else {
            checkSpecificErrors(contentObj.result);
        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('updateP2PStatusSettingsCallback', arguments, e);
        AppLoaded = true;
        unMask();
    }

};
window['settingsMainCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            Ext.ComponentQuery.query('main')[0].setActiveItem('settings');
            fillSettingsForm();
            Ext.ComponentQuery.query('container[name="slidersPanelSettings"]')[0].show();
            userData.settings = contentObj.settings;
            //apploaded in settings panel painted listener

        }
        else {
            settingsSlidersError = true;
            checkSpecificErrors(contentObj.result);
            AppLoaded = true;
            unMask();
        }


    }
    catch (e) {
        callbackTraceUnhandledError('settingsMainCallback', arguments, e);
        settingsSlidersError = true;
        AppLoaded = true;
        unMask();
    }

};
window['settingsP2PCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            userData.settings = contentObj.settings;
            Ext.ComponentQuery.query('main')[0].setActiveItem('p2pViewport');
            Ext.ComponentQuery.query('p2pViewport')[0].setActiveItem('p2pSettings');
            //Ext.ComponentQuery.query('container[name="resultMessageSettingsP2P"]')[0].hide();
            //apploaded in settings panel painted listener

        }
        else {
            checkSpecificErrors(contentObj.result);
            AppLoaded = true;
            unMask();
        }


    }
    catch (e) {
        callbackTraceUnhandledError('settingsP2PCallback', arguments, e);
        AppLoaded = true;
        unMask();
    }

};
window['modifyThresholdsCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        //Ext.ComponentQuery.query('container[name="resultMessageSettingsP2P"]')[0].show();
        if (contentObj.result.success) {
            favP2PPayment.thresholds = tempP2PObj.thresholds;
            var html = '<div class="iconMyWallet ok"></div><div class="resultWalletText">' + contentObj.result.message + '</div>';
            Ext.Msg.alert('', html, function() {
                Ext.ComponentQuery.query('main')[0].setActiveItem('home');
            });
            //$('.iconSettingP2P').addClass('ok');
            //$('.iconSettingP2P').removeClass('fail');
            //$('.resultSettingsP2PText').html(contentObj.result.message);
            getDataFromServer('userStatus', '{"flagSafety":false,"flags":{"balanceFlag":false,"lastAccessFlag":false,"lastPaymentsFlag":false,"operatingServicesFlag":true,"settingsFlag":false,"searchPaymentsFlag":false,"visualizationType":' + config.VIEW_P2P + '},' + generateContext() + '}', 'updateP2PStatusSettingsCallback');
        }
        else {
            var html = '<div class="iconMyWallet fail"></div><div class="resultWalletText">' + contentObj.result.message + '</div>';
            //SIAPTPE-168 Fix Reopened issue. This value is not true here
            //errorThresholdsP2P = true;
            Ext.Msg.alert('', html);
            //$('.iconSettingP2P').addClass('fail');
            //$('.iconSettingP2P').removeClass('ok');
            //$('.resultSettingsP2PText').html(contentObj.result.message);
            AppLoaded = true;
            unMask();
        }


    }
    catch (e) {
        callbackTraceUnhandledError('modifyThresholdsCallback', arguments, e);
        AppLoaded = true;
        unMask();
    }
};

window['saveMainSettingsCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            var check = Ext.ComponentQuery.query('checkboxfield[name="tutorialCheck"]')[0].isChecked();
            var nfcValue = Ext.ComponentQuery.query('selectfield[name="selectTimeNFC"]')[0].getValue();
            var widgetValue = Ext.ComponentQuery.query('checkboxfield[name="widgetCheck"]')[0].isChecked();

            if (skipTutorial != check) {
                if (check) {
                    check = 1;
                }
                else {
                    check = 0;
                }
                Appverse.Security.Async.StoreKeyValuePair({Key: "UBI_SKIP_TUTORIAL", Value: check}, null, null);
            }
            if (nfcValue != defaultNFCTime) {
                Appverse.Security.Async.StoreKeyValuePair({Key: "UBI_NFC_COUNTDOWN", Value: nfcValue}, null, null);

            }
            if (widgetValue != enableWidget) {
                if (widgetValue) {
                    widgetValue = 1;
                }
                else {
                    widgetValue = 0;
                }
                Appverse.Security.Async.StoreKeyValuePair({Key: "UBI_ENABLE_WIDGET", Value: widgetValue}, null, null);
            }

            skipTutorial = check;
            defaultNFCTime = nfcValue;
            enableWidget = widgetValue;
            Ext.ComponentQuery.query('selectfield[name="selectTimeNFC"]')[0].removeCls('changed');
            favP2PPayment.thresholds = tempP2PObjSettings.thresholds;
            var html = '<div class="iconMyWallet ok"></div><div class="resultWalletText">' + contentObj.result.message + '</div>';
            Ext.Msg.alert('', html, function() {
                Ext.ComponentQuery.query('main')[0].setActiveItem('home');
            });
            getDataFromServer('userStatus', '{"flagSafety":false,"flags":{"balanceFlag":false,"lastAccessFlag":false,"lastPaymentsFlag":false,"operatingServicesFlag":true,"settingsFlag":false,"searchPaymentsFlag":false,"visualizationType":' + config.VIEW_P2P + '},' + generateContext() + '}', 'updateP2PStatusSettingsCallback');
        }
        else {
            var html = '<div class="iconMyWallet fail"></div><div class="resultWalletText">' + contentObj.result.message + '</div>';
            Ext.Msg.alert('', html);
            AppLoaded = true;
            unMask();
        }


    }
    catch (e) {
        callbackTraceUnhandledError('saveMainSettingsCallback', arguments, e);
        AppLoaded = true;
        unMask();
    }

};
window['saveWalletCallback'] = function(result) {
    try {
        var contentObj = eval('(' + result.Content + ')');
        //Ext.ComponentQuery.query('container[name="resultMessageWallet"]')[0].show();
        if (contentObj.result.success) {
            mpService.forEach(function(item) {
                if (item.instrument.id == $('.defaultList li').attr('id')) {
                    item.favourites.flgFavPayment = true;
                    item.favourites.flgFavReception = true;
                }
                else {
                    item.favourites.flgFavPayment = false;
                    item.favourites.flgFavReception = false;
                }

            });
            var html = '<div class="iconMyWallet ok"></div><div class="resultWalletText">' + contentObj.result.message + '</div>';
            Ext.Msg.alert('', html, function() {
                Ext.ComponentQuery.query('main')[0].setActiveItem('home');
                //SIAPTPE-168 Fix Reopened issue. Save button disables when save
                Ext.ComponentQuery.query('button[name="saveWallet"]')[0].disable();
            });
        }
        else {
            var html = '<div class="iconMyWallet fail"></div><div class="resultWalletText">' + contentObj.result.message + '</div>';
            Ext.Msg.alert('', html);
        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('saveWalletCallback', arguments, e);
        AppLoaded = true;
        unMask();
    }
};

logoutCallback = function(result) {
    try {
        console.log(result);
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            goBackLogin();
        }
        else {
            checkSpecificErrors(contentObj.result);
            AppLoaded = true;
            unMask();
        }
    }
    catch (e) {
        callbackTraceUnhandledError('logoutCallback', arguments, e);
        goBackLogin();
        AppLoaded = true;
        unMask();
    }
};

//PRELOGIN CALLBACKS

/*retrieveSettingsPreloginCallback = function(result) {
 try {
 console.log(result);
 var contentObj = eval('(' + result.Content + ')');
 if (contentObj.result.success) {
 console.log(contentObj);
 if (contentObj.textApp) {
 preloginTextObject = {};
 contentObj.textApp.forEach(function(item) {
 preloginTextObject[item["key"]] = item["text"];
 });
 }
 UBI.app.getController('PreloginCtrl').resetPreloginView();
 Ext.Viewport.setActiveItem('preloginViewport');
 }
 else {
 checkSpecificErrors(contentObj.result);
 
 }
 AppLoaded = true;
 unMask();
 }
 catch (e) {
 callbackTraceUnhandledError('retrieveSettingsPreloginCallback', arguments, e);
 AppLoaded = true;
 unMask();
 }
 };*/

discoverServicesCallback = function(result) {
    try {
        console.log(result);
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            console.log(contentObj);
            Ext.ComponentQuery.query('container[name="preloginContainer"]')[0].setActiveItem('preloginBecomeUser');
            Ext.ComponentQuery.query('button[name="preloginBack"]')[0].hide();
            Ext.ComponentQuery.query('button[name="preloginClose"]')[0].show();
        }
        else {
            checkSpecificErrors(contentObj.result);
            Ext.Viewport.setActiveItem('login');

        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('discoverServicesCallback', arguments, e);
        Ext.Viewport.setActiveItem('login');
        AppLoaded = true;
        unMask();
    }
};

verifyPendingPaymentCallback = function(result) {
    try {
        console.log(result);
        var contentObj = eval('(' + result.Content + ')');
        //UNCOMMENT FOR TEST PURPOSES
        //contentObj.result.success=false;
        //contentObj.result.code=config.CODE_NO_PAYMENTS;
        if (contentObj.result.success) {
            console.log(contentObj);
            Ext.ComponentQuery.query('textfield[name="securityCodePreloginInput"]')[0].enable();
            Ext.ComponentQuery.query('textfield[name="ibanPreloginInput"]')[0].enable();
            Ext.ComponentQuery.query('checkboxfield[name="acceptConditionsIBAN"]')[0].enable();
            Ext.ComponentQuery.query('container[name="preloginContainer"]')[0].setActiveItem('preloginIBANVerify');
            Ext.ComponentQuery.query('container[name="preloginStepHeader"]')[0].show();
            Ext.ComponentQuery.query('button[name="preloginClose"]')[0].show();
            Ext.ComponentQuery.query('container[name="verifyIBANText"]')[0].show();
            Ext.ComponentQuery.query('container[name="pendingPayments"]')[0].show();
            Ext.ComponentQuery.query('container[name="verifyIBANNewText"]')[0].show();
            Ext.ComponentQuery.query('container[name="remainingIBAN"]')[0].hide();
            Ext.ComponentQuery.query('container[name="noPendingPaymentsText"]')[0].hide();
            Ext.ComponentQuery.query('container[name="noPendingPaymentsButton"]')[0].hide();
            if (contentObj.result.code == config.CODE_ONE_PAYMENT) {
                onePendingPayment = true;
                $('.stepPrelogin4').hide();

            }
            else {
                onePendingPayment = false;
                $('.stepPrelogin4').show();
            }
        }
        else {
            if (contentObj.result.code == config.CODE_NO_PAYMENTS) {
                Ext.ComponentQuery.query('textfield[name="securityCodePreloginInput"]')[0].disable();
                Ext.ComponentQuery.query('textfield[name="ibanPreloginInput"]')[0].disable();
                Ext.ComponentQuery.query('checkboxfield[name="acceptConditionsIBAN"]')[0].disable();
                Ext.ComponentQuery.query('container[name="preloginContainer"]')[0].setActiveItem('preloginIBANVerify');
                Ext.ComponentQuery.query('container[name="preloginStepHeader"]')[0].show();
                Ext.ComponentQuery.query('button[name="preloginClose"]')[0].show();
                Ext.ComponentQuery.query('container[name="verifyIBANText"]')[0].hide();
                Ext.ComponentQuery.query('container[name="pendingPayments"]')[0].hide();
                Ext.ComponentQuery.query('container[name="verifyIBANNewText"]')[0].hide();
                Ext.ComponentQuery.query('container[name="remainingIBAN"]')[0].hide();
                Ext.ComponentQuery.query('container[name="noPendingPaymentsText"]')[0].show();
                Ext.ComponentQuery.query('container[name="noPendingPaymentsButton"]')[0].show();
            }
            else {
                checkSpecificErrors(contentObj.result);
                Ext.Viewport.setActiveItem('login');
            }

        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('verifyPendingPaymentCallback', arguments, e);
        Ext.Viewport.setActiveItem('login');
        AppLoaded = true;
        unMask();
    }
};

verifyCollectAuthorizationCodeCallback = function(result) {
    try {
        console.log(result);
        var contentObj = eval('(' + result.Content + ')');
        //UNCOMMENT FOR TEST PURPOSES
        /*contentObj.result.success=false;
         contentObj.result.code=config.CODE_ERROR_PRELOGIN_AUTH;
         contentObj.remainingAttempts=1;*/


        if (contentObj.result.success) {
            console.log(contentObj);
            $('.stepPrelogin2').addClass('stepSelected');
            $('.stepPrelogin1').removeClass('stepSelected');
            var store = Ext.StoreManager.get('transfersStore');
            console.log(contentObj.pendingPayments.payments);

            if (onePendingPayment) {
                store.setData(contentObj.pendingPayments.payments);
                preloginSelectedPayment=store.getAt(0);
                Ext.ComponentQuery.query('container[name="reviewPreloginContainer"]')[0].setData(preloginSelectedPayment.getData());
                Ext.ComponentQuery.query('container[name="preloginContainer"]')[0].setActiveItem('preloginPaymentReview');
            }
            else {
                store.setData(contentObj.pendingPayments.payments);
                Ext.ComponentQuery.query('container[name="preloginContainer"]')[0].setActiveItem('preloginPaymentSelection');
                Ext.ComponentQuery.query('list[name="preloginPaymentsList"]')[0].getScrollable().getScroller().scrollTo(0, 0);
                Ext.ComponentQuery.query('list[name="preloginPaymentsList"]')[0].select(0);

            }

        }
        else {
            if (contentObj.result.code == config.CODE_ERROR_PRELOGIN_AUTH) {
                preloginRemainingAttemps = contentObj.remainingAttempts;
                Ext.ComponentQuery.query('container[name="remainingIBAN"]')[0].setHtml(i18Str.REMAINING_IBAN_TEXT1 + " " + preloginRemainingAttemps + " " + i18Str.REMAINING_IBAN_TEXT2);
                Ext.ComponentQuery.query('container[name="remainingIBAN"]')[0].show();
            }
            else {
                checkSpecificErrors(contentObj.result);
                Ext.Viewport.setActiveItem('login');
            }

        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('verifyCollectAuthorizationCodeCallback', arguments, e);
        Ext.Viewport.setActiveItem('login');
        AppLoaded = true;
        unMask();
    }
};

confirmCollectPendingPaymentCallback = function(result) {
    preloginRemainingAttemps = null;
    try {
        console.log(result);
        var contentObj = eval('(' + result.Content + ')');
        
        if (onePendingPayment) {
            $('.stepPrelogin3').addClass('stepSelected');
            $('.stepPrelogin2').removeClass('stepSelected');        
        }
        else{
            $('.stepPrelogin4').addClass('stepSelected');
            $('.stepPrelogin3').removeClass('stepSelected');
        }

        if (contentObj.result.success) {
            console.log(contentObj);
            Ext.ComponentQuery.query('container[name="preloginContainer"]')[0].setActiveItem('preloginResult');
            $('.resultTextPrelogin').html(contentObj.result.message);
            $('.iconPrelogin').addClass('ok');
            $('.iconPrelogin').removeClass('fail');
            Ext.ComponentQuery.query('button[name="retryPreloginButton"]')[0].hide();
            Ext.ComponentQuery.query('container[name="resultSuccessPrelogin"]')[0].show();

        }
        else {
            $('.resultTextPrelogin').html(contentObj.result.message);
            $('.iconPrelogin').addClass('fail');
            $('.iconPrelogin').removeClass('ok');
            Ext.ComponentQuery.query('button[name="retryPreloginButton"]')[0].show();
            Ext.ComponentQuery.query('container[name="resultSuccessPrelogin"]')[0].hide();

        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('confirmCollectPendingPaymentCallback', arguments, e);
        Ext.Viewport.setActiveItem('login');
        AppLoaded = true;
        unMask();
    }
};

newSecurityCodeCallback = function(result) {
    try {
        console.log(result);
        var contentObj = eval('(' + result.Content + ')');
        if (contentObj.result.success) {
            console.log(contentObj);
            Ext.Msg.alert(i18Str.ATENTION, i18Str.NEW_SECURITY_CODE_POPUP_MSG);
        }
        else {
            checkSpecificErrors(contentObj.result);
            Ext.Viewport.setActiveItem('login');

        }
        AppLoaded = true;
        unMask();
    }
    catch (e) {
        callbackTraceUnhandledError('discoverServicesCallback', arguments, e);
        Ext.Viewport.setActiveItem('login');
        AppLoaded = true;
        unMask();
    }
};


