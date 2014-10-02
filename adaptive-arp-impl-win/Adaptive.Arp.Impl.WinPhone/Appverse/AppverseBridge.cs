﻿using Adaptive.Arp.Impl.WinPhone.Internals;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public delegate Task<AppServerRequestResponse> MappingDeletage(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request);

    public class AppverseBridge
    {
        private Dictionary<Regex, MappingDeletage> appverseFunctions = null;

        public AppverseBridge()
        {
            appverseFunctions = new Dictionary<Regex, MappingDeletage>();
            appverseFunctions.Add(new Regex("^/service/system/GetOSInfo"), AppverseGetOSInfo);
            appverseFunctions.Add(new Regex("^/service/system/GetOSHardwareInfo"), AppverseGetOSHardwareInfo);
            appverseFunctions.Add(new Regex("^/service/system/GetLocaleCurrent"), AppverseGetLocaleCurrent);
            appverseFunctions.Add(new Regex("^/service/i18n/GetLocaleSupportedDescriptors"), AppverseGetLocalSupportedDescriptors);
            appverseFunctions.Add(new Regex("^/service/i18n/GetResourceLiterals"), AppverseGetResourceLiterals);
            appverseFunctions.Add(new Regex("^/service/io/GetService"), AppverseGetService);
            appverseFunctions.Add(new Regex("^/service/system/GetUnityContext"), AppverseGetUnityContext);
            appverseFunctions.Add(new Regex("^/service/system/DismissSplashScreen"), AppverseDismissSplashScreen);
            appverseFunctions.Add(new Regex("^/service/security/IsDeviceModified"), AppverseIsDeviceModified);
            appverseFunctions.Add(new Regex("^/service/net/IsNetworkReachable"), AppverseIsNetworkReachable);
            appverseFunctions.Add(new Regex("^/service/security/GetStoredKeyValuePairs"), AppverseGetStoredKeyValuePairs);
            appverseFunctions.Add(new Regex("^/service-async/security/GetStoredKeyValuePairs"), AppverseGetStoredKeyValuePairs);
            appverseFunctions.Add(new Regex("^/service/pim/ListContacts"), AppverseListContacts);
            appverseFunctions.Add(new Regex("^/service-async/pim/ListContacts"), AppverseListContacts);
            appverseFunctions.Add(new Regex("^/service/io/InvokeService"), AppverseInvokeService);
            appverseFunctions.Add(new Regex("^/service-async/io/InvokeService"), AppverseInvokeService);
        }

        private async Task<AppServerRequestResponse> AppverseInvokeService(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {           
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            DataReader dataReader = new DataReader(request.httpContent.AsInputStream());
            dataReader.InputStreamOptions = InputStreamOptions.Partial;

            bool readFinished = false;
            uint readMaxBufferSize = 1024;
            StringBuilder stringBuilder = new StringBuilder();

            while (!readFinished)
            {
                await dataReader.LoadAsync(readMaxBufferSize);
                if (dataReader.UnconsumedBufferLength > 0)
                {
                    uint readLength = dataReader.UnconsumedBufferLength;
                    byte[] readBuffer = new byte[dataReader.UnconsumedBufferLength];
                    dataReader.ReadBytes(readBuffer);
                    stringBuilder.Append(Encoding.UTF8.GetString(readBuffer, 0, readBuffer.Length));
                    if (readLength < readMaxBufferSize) readFinished = true;
                }
                else
                {
                    readFinished = true;
                }
            }

            IORequest ioRequest = null;
            IOService ioService = null;
            string callbackFunction = null;
            string callbackId = null;
            string jsonRequestString = null;

            if (readFinished == true)
            {
                WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(stringBuilder.ToString());
                callbackFunction = decoder.GetFirstValueByName("callback");
                callbackId = decoder.GetFirstValueByName("callbackid");
                jsonRequestString = decoder.GetFirstValueByName("json");

                Debug.WriteLine("callback: {0}   callbackid: {1}   json: {2}", callbackFunction, callbackId, jsonRequestString);
                var definition = new { param1 = new IORequest(), param2 = new IOService() };
                var appverseParams = JsonConvert.DeserializeAnonymousType(jsonRequestString, definition);
                ioRequest = appverseParams.param1;
                ioService = appverseParams.param2;
            }
            if (ioService != null)
            {
                if (ioService.Endpoint.Path.EndsWith("/rest/sec/login"))
                {
                    string jsonResultString = "{\"ContentBinary\":null,\"GetContentLengthBinary\":0,\"ContentType\":\"application/json\",\"Headers\":[{\"Name\":\"Cache-Control\",\"Value\":\"max-age=0\"}],\"Content\":\"\",\"Session\":{\"Cookies\":[{\"Name\":\"JSESSIONID\",\"Value\":\"BJLuDz1UkfrbXyseEzeAyNGv.sit-p2pMobileServer1\"},null,null,null,null,null,null]}}";
                    string jsonContent = "{\"result\":{\"success\":true,\"code\":\"0\",\"message\":\"Esito positivo\",\"warning\":false,\"guid\":null},\"userStatus\":{\"name\":\"Andrea\",\"surname\":\"Gotti\",\"balance\":null,\"lastAccessDate\":1411729244380,\"payments\":null,\"operatingServices\":[{\"service\":{\"id\":\"22063\",\"type\":0,\"status\":0,\"purchaseAddress\":null},\"instrument\":{\"id\":\"22062\",\"type\":0,\"cardNumber\":null,\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null},\"thresholds\":[{\"type\":0,\"value\":250.0},{\"type\":1,\"value\":250.0},{\"type\":2,\"value\":1310.0},{\"type\":3,\"value\":80.0}],\"balance\":null,\"periodBalance\":{\"amount\":76.51,\"balanceDate\":1411729557273},\"payments\":{\"payments\":[{\"amount\":5.0,\"reason\":\"P\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail1.facondini@ubiss.it\",\"status\":null},\"operationDate\":1409263200000,\"serviceID\":\"22044\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":5.0,\"requestedDate\":1409321967101,\"confirmationDate\":1409263200000,\"id\":558,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":5.0,\"reason\":\"P\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail1.facondini@ubiss.it\",\"status\":null},\"operationDate\":1409263200000,\"serviceID\":\"22044\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":5.0,\"requestedDate\":1409321969751,\"confirmationDate\":1409263200000,\"id\":559,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":6.0,\"reason\":\"Bbbbh\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail1.facondini@ubiss.it\",\"status\":null},\"operationDate\":1409263200000,\"serviceID\":\"22044\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":6.0,\"requestedDate\":1409322359380,\"confirmationDate\":1409263200000,\"id\":560,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":2.0,\"reason\":\"p\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393209589133\",\"operatorCode\":null},\"mail\":\"provamail3.facondini@ubiss.it\",\"status\":null},\"operationDate\":1409522400000,\"serviceID\":\"3\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":2.0,\"requestedDate\":1409549783822,\"confirmationDate\":1409522400000,\"id\":568,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":5.0,\"reason\":\"Proviamo%20a%20pagare%20con%20data%20esecuz%20a%20domank\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail1.facondini@ubiss.it\",\"status\":null},\"operationDate\":1409522400000,\"serviceID\":\"22044\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":5.0,\"requestedDate\":1409600018775,\"confirmationDate\":1409522400000,\"id\":573,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":22.0,\"reason\":\"Yy\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail1.facondini@ubiss.it\",\"status\":null},\"operationDate\":1410300000000,\"serviceID\":\"22044\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":22.0,\"requestedDate\":1410349586353,\"confirmationDate\":1410300000000,\"id\":590,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":1.0,\"reason\":\"Ciao\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail1.facondini@ubiss.it\",\"status\":null},\"operationDate\":1410732000000,\"serviceID\":\"22044\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":1.0,\"requestedDate\":1410764374425,\"confirmationDate\":1410732000000,\"id\":595,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":-2.0,\"reason\":\"Pp\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail1.facondini@ubiss.it\",\"status\":null},\"operationDate\":1410732000000,\"serviceID\":\"22049\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":-2.0,\"requestedDate\":1410786715281,\"confirmationDate\":1410732000000,\"id\":597,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":4.53,\"reason\":\"Hola\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393209589133\",\"operatorCode\":null},\"mail\":\"provamail3.facondini@ubiss.it\",\"status\":null},\"operationDate\":1410732000000,\"serviceID\":\"3\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":4.53,\"requestedDate\":1410792740596,\"confirmationDate\":1410732000000,\"id\":600,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":-0.11,\"reason\":\"Hola%20hola%20que%20ase%20\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393484718497\",\"operatorCode\":null},\"mail\":\"carlo@libero.it\",\"status\":null},\"operationDate\":1410732000000,\"serviceID\":\"22049\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":-0.11,\"requestedDate\":1410793344177,\"confirmationDate\":1410732000000,\"id\":601,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":-15.0,\"reason\":\"Gfcvghgichdbdjxhsbnshxjxndhxjxdnicdjjdjcjdjxjcjdjxjxjcjdjkdjdjsjdjdjexfufndhmdymeymeymeymeyjwjwentwnsrmdmsnyssnaatntsnyssffumydysmtadustmatu\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393484718497\",\"operatorCode\":null},\"mail\":\"carlo@libero.it\",\"status\":null},\"operationDate\":1410732000000,\"serviceID\":\"22049\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":-15.0,\"requestedDate\":1410793693061,\"confirmationDate\":1410732000000,\"id\":602,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":20.0,\"reason\":\"BALANCE%20TEST\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail3.facondini@ubiss.it\",\"status\":null},\"operationDate\":1411077600000,\"serviceID\":\"22061\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":20.0,\"requestedDate\":1411124424966,\"confirmationDate\":1411077600000,\"id\":611,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":7.0,\"reason\":\"balance%20test%202\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail3.facondini@ubiss.it\",\"status\":null},\"operationDate\":1411077600000,\"serviceID\":\"22061\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":7.0,\"requestedDate\":1411124459357,\"confirmationDate\":1411077600000,\"id\":612,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":2.0,\"reason\":\"Prova%20s3%20ril%2024\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail3.facondini@ubiss.it\",\"status\":null},\"operationDate\":1411336800000,\"serviceID\":\"22061\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":2.0,\"requestedDate\":1411392538717,\"confirmationDate\":1411336800000,\"id\":617,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":5.0,\"reason\":\"fff\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail3.facondini@ubiss.it\",\"status\":null},\"operationDate\":1411423200000,\"serviceID\":\"22061\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":5.0,\"requestedDate\":1411459417306,\"confirmationDate\":1411423200000,\"id\":624,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":2.0,\"reason\":\"Ytt\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail3.facondini@ubiss.it\",\"status\":null},\"operationDate\":1411423200000,\"serviceID\":\"22061\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":2.0,\"requestedDate\":1411459577193,\"confirmationDate\":1411423200000,\"id\":625,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":2.0,\"reason\":\"Prova%20sit\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail3.facondini@ubiss.it\",\"status\":null},\"operationDate\":1411509600000,\"serviceID\":\"22061\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":2.0,\"requestedDate\":1411544641028,\"confirmationDate\":1411509600000,\"id\":626,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":9.09,\"reason\":\"Prova%20sit%20pnd\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail3.facondini@ubiss.it\",\"status\":null},\"operationDate\":1411509600000,\"serviceID\":\"22061\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":9.09,\"requestedDate\":1411544680126,\"confirmationDate\":1411509600000,\"id\":627,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":-9.0,\"reason\":\"Tie\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail3.facondini@ubiss.it\",\"status\":null},\"operationDate\":1411509600000,\"serviceID\":\"22063\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":-9.0,\"requestedDate\":1411545662424,\"confirmationDate\":1411509600000,\"id\":629,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}},{\"amount\":5.0,\"reason\":\"Hhh\",\"contactAddressee\":{\"phoneContact\":{\"phoneNumber\":\"+393913287575\",\"operatorCode\":null},\"mail\":\"provamail3.facondini@ubiss.it\",\"status\":null},\"operationDate\":1411596000000,\"serviceID\":\"22061\",\"paymentStatus\":1,\"commisionCost\":0.0,\"total\":5.0,\"requestedDate\":1411632862701,\"confirmationDate\":1411596000000,\"id\":635,\"instrument\":{\"id\":\"4\",\"type\":0,\"cardNumber\":\"\",\"iban\":\"IT34W0542853510000000097923\",\"alias\":null,\"creditCardType\":null}}],\"flagPartialResult\":false},\"favourites\":{\"flgFavPayment\":true,\"flgFavReception\":true}}],\"periodBalance\":null},\"settings\":null,\"textApp\":[{\"key\":\"K_SER_SAT_GG\",\"text\":\"Operazione non ammessa: superato importo giornaliero consentito\"},{\"key\":\"K_SER_SAT_MM\",\"text\":\"Operazione non ammessa: superato importo mensile consentito\"},{\"key\":\"K_SER_SAT_NO\",\"text\":\"Operazione non ammessa: superato numero operazioni giornaliere consentite\"},{\"key\":\"K_NFCNOTSUP\",\"text\":\"Servizio indisponibile: antenna NFC disattiva, verificare le impostazioni del telefono\"},{\"key\":\"K_TIT_NFCROOT\",\"text\":\"Sistema operativo non conforme per utilizzare il servizio Pago contactless.\"},{\"key\":\"K_NFCVIRTCARD\",\"text\":\"Servizio indisponibile: nessuna carta UBI caricata sulla SIM\"},{\"key\":\"K_NFCSIMNFC_MIS\",\"text\":\"Servizio indisponibile: la SIM del telefono non consente l\u0027 utilizzo del servizio \"},{\"key\":\"K_SMS_INVITO\",\"text\":\"Scopri il servizio Invio denaro di UBI Banca, per inviare soldi semplicemente con il n. di cellulare del beneficiario, Info su ubibanca.com e chiamando il 800.500.200.\"},{\"key\":\"K_AMNT_GR_BAL\",\"text\":\"Importo inserito superiore al saldo disponibile\"},{\"key\":\"K_AMNT_GR_TH\",\"text\":\"Importo inserito superiore alla soglia giornaliera\"},{\"key\":\"K_AMNT_ZERO\",\"text\":\"Importo inserito non valido: deve essere maggiore di 0\"},{\"key\":\"K_EXPLAIN_PAGAM\",\"text\":\"Manda un SMS al destinatario per avvisarlo dell\u0027opportunitài riscuotere il pagamento\"},{\"key\":\"K_CONF_CANCEL\",\"text\":\"Confermi cancellazione del pagamento?\"},{\"key\":\"K_NFCWALTELCO\",\"text\":\"Per utilizzare la funzione &#232 necessario installare la app del proprio operatore telefonico\"},{\"key\":\"K_SER_MP_DIS\",\"text\":\"Il servizio Portafoglio carte non &#232 attivo\"},{\"key\":\"K_SER_P2P_DIS\",\"text\":\"Il servizio Invio denaro non &#232 attivo\"},{\"key\":\"K_HELP_CAUSE\",\"text\":\"Il limite massimo di caratteri che puoi utilizzare &#232 di 140.Sono ammessi tutti i caratteri normalmente disponibili sulla tastiera, eccetto &#124, &#8361, &#12298, &#12299.\"},{\"key\":\"K_NFCDEBUGON\",\"text\":\"Impossibile utilizzare la funzione perch&#233 attivo il debug USB\"},{\"key\":\"K_BAL_UNAVAIL\",\"text\":\"Non &#232 possibile proseguire perch&#233 non &#232 stato possibile recuperare il saldo disponibile\"},{\"key\":\"K_HELP_AMNT\",\"text\":\"Inserisci l\u0027importo dell\u0027operazione. Ti ricordiamo che il limite massimo giornaliero &#232 di 250&#8364 e mensile di 1.500&#8364, che pu&#242 essere da te ridotto ulteriormente dalle impostazioni. Inoltre per le operazioni sopra i 150 &#8364 &#232 richiesta la digitazione del codice della tessera Qui UBI\"},{\"key\":\"K_SMS_PAGAMENTO\",\"text\":\"Ciao {NS}, ti ho inviato {AMT} euro tramite il servizio Invio denaro di UBI PAY. Per ricevere il denaro, %E8 necessario sottoscrivere UBI PAY. Chiama il numero verde 800.500.200 (+39.030.2471209).\"},{\"key\":\"K_SER_SAT_SOSP\",\"text\":\"Il servizio &#232 stato sospeso \"},{\"key\":\"K_NFC_TIT_ERR\",\"text\":\"UBI PAY\"},{\"key\":\"K_NFC_GENERR\",\"text\":\"Errore generico\"},{\"key\":\"K_NFC_SECERR\",\"text\":\"Controlli di sicurezza non superati \"},{\"key\":\"K_NFC_CARDLNF\",\"text\":\"Servizio indisponibile: nessuna carta UBI caricata sulla SIM\"},{\"key\":\"K_ERR_SETT\",\"text\":\"Errore nella ricezione dati per le impostazioni.\"},{\"key\":\"K_NFC_CANC\",\"text\":\"Richiesta di pagamento annullata correttamente.\"},{\"key\":\"K_NFC_OPEFAILED\",\"text\":\"Per utilizzare la funzione Pago Contactless effettuare l’accesso al Wallet TIM\"},{\"key\":\"K_NFCROOTPRIV\",\"text\":\"Il servizio Pago contactless di UBI PAY non &#232 utilizzabile. Il tuo dispositivo risulta avere un sistema operativo non conforme. Aggiorna il tuo sistema operativo scaricando sul telefono l\u0027ultimo aggiornamento della versione ufficiale . Per qualsiasi chiarimento contatta la tua filiale oppure il  Servizio Clienti al numero verde 800.500.200 (dall\u0027estero +39.030.2471209).\"},{\"key\":\"K_NFC_ERRCARD\",\"text\":\"Servizio indisponibile: errore di comunicazione con la SIM \"},{\"key\":\"K_BAL_ZERO\",\"text\":\"Non &#232 possibile proseguire perch&#233  il saldo disponibile non &#232 positivo\"},{\"key\":\"K_INFO_NUMDISP\",\"text\":\"Per visualizzare un numero maggiore di movimenti accedere da Qui UBI nella sezione \u0027Il mio UBI PAY\u0027\"}]}";
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.StringEscapeHandling = StringEscapeHandling.EscapeHtml;
                    IOResponse jsonResultObject = JsonConvert.DeserializeObject<IOResponse>(jsonResultString);
                    jsonResultObject.Content = jsonContent;
                    
                    string jsCallbackFunction = "try{if(" + callbackFunction + "){" + callbackFunction + "(" +  JsonConvert.SerializeObject(jsonResultObject, settings) + ", '" + callbackId + "');}}catch(e) {console.log('error executing javascript callback: ' + e)}";
                    Task.Run(async () =>
                    {
                        await (AppContextWebviewImpl.Instance.GetWebviewPrimary() as WebView).Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            IAsyncOperation<string> result = (AppContextWebviewImpl.Instance.GetWebviewPrimary() as WebView).InvokeScriptAsync("eval", new string[1] { jsCallbackFunction });
                            result.Completed = new AsyncOperationCompletedHandler<string>((operation, status) =>
                            { 
                                Debug.WriteLine("Completed: '{0}'  Status: {1}  Result: {2}", callbackFunction, operation.Status, operation.ErrorCode);
                                if (operation.Status == AsyncStatus.Completed)
                                {
                                    Debug.WriteLine("Result: {0}", operation.GetResults());
                                }
                            });
                        });
                    });
                }
                else
                {
                    Debug.WriteLine("Not implemented service: {0}", ioService.Endpoint.Path);
                }
            }
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseListContacts(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            Debug.WriteLine("Stream Pos {0}, Len {1}", request.httpContent.Position, request.httpContent.Length);
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            DataReader dataReader = new DataReader(request.httpContent.AsInputStream());
            dataReader.InputStreamOptions = InputStreamOptions.Partial;
            // Data dataReader flags and options
            bool readFinished = false;
            uint readMaxBufferSize = 1024;
            StringBuilder stringBuilder = new StringBuilder();
            Debug.WriteLine("Waypoint {0}", 1);
            while (!readFinished)
            {
                Debug.WriteLine("Waypoint {0}", 2);
                Debug.WriteLine("Stream Pos {0}, Len {1}", request.httpContent.Position, request.httpContent.Length);
                await dataReader.LoadAsync(readMaxBufferSize);
                Debug.WriteLine("Waypoint {0}", 3);
                Debug.WriteLine("Stream Pos {0}, Len {1}", request.httpContent.Position, request.httpContent.Length);
                if (dataReader.UnconsumedBufferLength > 0)
                {
                    Debug.WriteLine("Waypoint {0}", 4);
                    uint readLength = dataReader.UnconsumedBufferLength;
                    Debug.WriteLine("Waypoint {0}", 5);
                    byte[] readBuffer = new byte[dataReader.UnconsumedBufferLength];
                    Debug.WriteLine("Waypoint {0}", 6);
                    dataReader.ReadBytes(readBuffer);
                    Debug.WriteLine("Waypoint {0}", 7);
                    stringBuilder.Append(Encoding.UTF8.GetString(readBuffer, 0, readBuffer.Length));
                    Debug.WriteLine("Waypoint {0}", 8);
                    if (readLength < readMaxBufferSize) readFinished = true;
                }
                else
                {
                    Debug.WriteLine("Waypoint {0}", 9);
                    readFinished = true;
                }
            }

            string callbackFunction = null;
            string callbackId = null;
            string jsonRequestString = null;

            if (readFinished == true)
            {
                WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(stringBuilder.ToString());
                Debug.WriteLine("Parameters: {0}", decoder.Count);
                callbackFunction = decoder.GetFirstValueByName("callback");
                callbackId = decoder.GetFirstValueByName("callbackid");
                jsonRequestString = decoder.GetFirstValueByName("json");

                if (callbackFunction == null || callbackFunction == "NULL") callbackFunction = "Appverse.Pim.onListContactsEnd";
                Debug.WriteLine("callback: {0}   callbackid: {1}   json: {2}", callbackFunction, callbackId, jsonRequestString);
                string jsonResultString = "{\"0\":[{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"2\",\"Name\":\"Alberto\",\"Firstname\":null,\"Lastname\":\"Rossi\",\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"611252361\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"3\",\"Name\":\"Emanuele Seregni\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":\"Emanuele Seregni\",\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"600259612\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"4\",\"Name\":\"Alessandra Neri\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":\"Marga GFT\",\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"3354122311\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"5\",\"Name\":\"Phillip Lahm\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":\"Phillip Lahm\",\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"33215635\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"6\",\"Name\":\"Ernst Tarabh\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":\"Ernst Tarabh\",\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"06 8200 4437\",\"IsPrimary\":true},{\"Type\":0,\"Number\":\"55396061\",\"IsPrimary\":false},{\"Type\":0,\"Number\":\"55396262\",\"IsPrimary\":false}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"7\",\"Name\":\"Laura Pause\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":\"JuanGFT\",\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"3206655213\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"8\",\"Name\":\"Gart Newmon\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":\"Gart Newmon\",\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"06 8211 4437\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"9\",\"Name\":\"Celia Varese\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":\"Celia Varese\",\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"85421326\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"10\",\"Name\":\"John Decon\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":\"John Decon\",\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"555236541\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"11\",\"Name\":\"john\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":\"jo\",\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"677444521\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"12\",\"Name\":\"john\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"302444521\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"13\",\"Name\":\"john22\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"23515521\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"14\",\"Name\":\"john KD\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"30234156\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"15\",\"Name\":\"ADOOjohn\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"316874112\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"16\",\"Name\":\"johnJU\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"4559632417\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"17\",\"Name\":\"johnws\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"3222659854\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"18\",\"Name\":\"johnY\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"4011250366\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"19\",\"Name\":\"Kiola\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"3126814536\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"20\",\"Name\":\"sue\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"023541635\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"21\",\"Name\":\"tohn\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"112552621\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"22\",\"Name\":\"buemi\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"122263589\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"23\",\"Name\":\"sohn\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"1292874382\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"24\",\"Name\":\"yohn\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"5223949552\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"25\",\"Name\":\"zohn\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"1222968852\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"26\",\"Name\":\"estaGFT\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"3284007991\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"27\",\"Name\":\"kohn\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"4712544225\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"28\",\"Name\":\"qohn\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"9888922385\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"29\",\"Name\":\"wohn\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"411125442\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"30\",\"Name\":\"Clemente\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"3913287575\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"34\",\"Name\":\"Clemente2\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"3913287575\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"35\",\"Name\":\"Clemente3\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"3913287575\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"31\",\"Name\":\"Samuele\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"3395098623\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"32\",\"Name\":\"Andrea\",\"Firstname\":null,\"Lastname\":\"Gotti\",\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"3401461939\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]},{\"Company\":null,\"JobTitle\":\"director\",\"Department\":null,\"WebSites\":null,\"Notes\":null,\"Relationship\":0,\"Addresses\":[{\"Type\":2,\"Address\":\"adrddsd\",\"AddressNumber\":null,\"PostCode\":\"08389\",\"City\":\"terrassa\",\"Country\":\"esp\"}],\"Photo\":null,\"PhotoBase64Encoded\":null,\"ID\":\"40\",\"Name\":\"APendingPayments\",\"Firstname\":null,\"Lastname\":null,\"DisplayName\":null,\"Group\":null,\"Phones\":[{\"Type\":0,\"Number\":\"+393999999998\",\"IsPrimary\":true}],\"Emails\":[{\"Type\":2,\"IsPrimary\":false,\"Firstname\":null,\"Surname\":null,\"CommonName\":\"commonName\",\"Address\":\"guu@gmail.com\"}]}]}";
                string jsCallbackFunction = "try{if(" + callbackFunction + "){" + callbackFunction + "(" + jsonResultString + ", '" + callbackId + "');}}catch(e) {console.log('error executing javascript callback: ' + e)}";
                Task.Run(async () => {
                    await (AppContextWebviewImpl.Instance.GetWebviewPrimary() as WebView).Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        IAsyncOperation<string> result = (AppContextWebviewImpl.Instance.GetWebviewPrimary() as WebView).InvokeScriptAsync("eval", new string[1] { jsCallbackFunction });
                        result.Completed = new AsyncOperationCompletedHandler<string>((operation, status) =>
                        {
                            Debug.WriteLine("Completed: '{0}'  Status: {1}  Result: {2}", callbackFunction, operation.Status, operation.ErrorCode);
                            if (operation.Status == AsyncStatus.Completed)
                            {
                                Debug.WriteLine("Result: {0}", operation.GetResults());
                            }
                        });
                    });
                });
            }
            dataWriter.WriteString("");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseGetStoredKeyValuePairs(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            DataReader dataReader = new DataReader(request.httpContent.AsInputStream());
            dataReader.InputStreamOptions = InputStreamOptions.Partial;
            // Data dataReader flags and options
            bool readFinished = false;
            uint readMaxBufferSize = 1024;
            StringBuilder stringBuilder = new StringBuilder();

            while (!readFinished)
            {
                await dataReader.LoadAsync(readMaxBufferSize);
                if (dataReader.UnconsumedBufferLength > 0)
                {
                    uint readLength = dataReader.UnconsumedBufferLength;
                    byte[] readBuffer = new byte[dataReader.UnconsumedBufferLength];
                    dataReader.ReadBytes(readBuffer);
                    stringBuilder.Append(Encoding.UTF8.GetString(readBuffer, 0, readBuffer.Length));
                    if (readLength < readMaxBufferSize) readFinished = true;
                }
                else
                {
                    readFinished = true;
                }
            }

            string callbackFunction = null;
            string callbackId = null;
            string jsonRequestString = null;

            if (readFinished == true)
            {
                WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(stringBuilder.ToString());
                Debug.WriteLine("Parameters: {0}", decoder.Count);
                callbackFunction = decoder.GetFirstValueByName("callback");
                callbackId = decoder.GetFirstValueByName("callbackid");
                jsonRequestString = decoder.GetFirstValueByName("json");

                if (callbackFunction == null || callbackFunction == "NULL") callbackFunction = "Appverse.OnKeyValuePairsStoreCompleted";
                Debug.WriteLine("callback: {0}   callbackid: {1}   json: {2}", callbackFunction, callbackId, jsonRequestString);
                string jsonResultString = "{\"0\":[{\"Key\":\"UBI_USERNAME\",\"Value\":\"12020990\"},{\"Key\":\"UBI_DEVICE_UNIQUE_ID\",\"Value\":\"20755j17506dpgc8l3i4ea39sf2e4hi\"},{\"Key\":\"UBI_INSTITUTION_CODE\",\"Value\":\"05428\"},{\"Key\":\"UBI_REMEMBER_USER\",\"Value\":\"1\"},{\"Key\":\"UBI_SKIP_TUTORIAL\",\"Value\":\"0\"},{\"Key\":\"UBI_NFC_COUNTDOWN\",\"Value\":\"30\"},{\"Key\":\"UBI_FAV_CONTACTS\",\"Value\":\"[]\"},{\"Key\":\"UBI_UNFAV_CONTACTS\",\"Value\":\"[]\"},{\"Key\":\"UBI_ENABLE_WIDGET\",\"Value\":\"0\"}],\"1\":[]}";
                string jsCallbackFunction = "try{if(" + callbackFunction + "){" + callbackFunction + "(" + jsonResultString + ", '" + callbackId + "');}}catch(e) {console.log('error executing javascript callback: ' + e)}";
                Task.Run(async () =>
                {
                    (AppContextWebviewImpl.Instance.GetWebviewPrimary() as WebView).Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        IAsyncOperation<string> result = (AppContextWebviewImpl.Instance.GetWebviewPrimary() as WebView).InvokeScriptAsync("eval", new string[1] { jsCallbackFunction });
                        result.Completed = new AsyncOperationCompletedHandler<string>((operation, status) =>
                        {
                            Debug.WriteLine("Completed: '{0}'  Status: {1}  Result: {2}", callbackFunction, operation.Status, operation.ErrorCode);
                            if (operation.Status == AsyncStatus.Completed)
                            {
                                Debug.WriteLine("Result: {0}", operation.GetResults());
                            }
                        });
                    });
                });
            }
            
            dataWriter.WriteString("");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;

        }

        private async Task<AppServerRequestResponse> AppverseIsNetworkReachable(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            dataWriter.WriteString("true");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseIsDeviceModified(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            dataWriter.WriteString("false");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseDismissSplashScreen(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseGetUnityContext(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            dataWriter.WriteString("{\"Emulator\":false,\"EmulatorOrientation\":0,\"EmulatorScreen\":\"{height:1232,widht:720}\",\"Windows\":false,\"iPod\":false,\"iPad\":false,\"iPhone\":true,\"Android\":true,\"Blackberry\":false,\"TabletDevice\":false,\"Tablet\":false,\"Phone\":true,\"iOS\":false}");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseGetService(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            dataWriter.WriteString("{\"Name\":\"server_SIT\",\"Type\":4,\"Endpoint\":{\"Scheme\":null,\"Host\":\"https://tst.vaservices.eu:1443\",\"Port\":0,\"Path\":\"/sit-p2pMobileServer_1\",\"ProxyUrl\":null},\"RequestMethod\":0}");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseGetResourceLiterals(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            dataWriter.WriteString("{\"LOADING\":\"Caricamento\",\"ERROR\":\"Errore\",\"ATENTION\":\"Attenzione\",\"NO_CONNECTIVITY\":\"Nessuna connettività\",\"USER_LOGIN_PLACEHOLDER\":\"Codice cliente\",\"PASSWORD_LOGIN_PLACEHOLDER\":\"Password\",\"LOGIN\":\"Login\",\"REMEMBER_LOGIN_CHECK\":\"Ricorda il mio codice cliente\",\"NON_REGISTERED_TITLE\":\"NON CLIENTE UBI PAY\",\"NON_REGISTERED_TEXT\":\"Non sei cliente UBI PAY? Hai ricevuto un SMS di Invio denaro da un tuo contatto? Clicca qui sotto\",\"FORGOT_PASSWORD\":\"Forgot password?\",\"HELP_LINK\":\"Contattaci\",\"FIRST_LOGIN\":\"Questa è la prima volta che accedi all\u0027applicazione UBI PAY.\u003cbr/\u003ePer accedere ai servizi è necessario inserire il numero del codice\",\"CODE_LOGIN_PLACEHOLDER\":\"Codice dispositivo Qui UBI\",\"TUTORIAL_TITLE\":\"Benvenuto\",\"NO_MORE_TUTORIAL\":\"Non mostrarmi più questo tutorial\",\"HELP_TITLE\":\"Help\",\"HOME_MENU\":\"HOME\",\"NFC_MENU\":\"PAGO CONTACTLESS\",\"P2P_MENU\":\"INVIO DENARO\",\"WALLET_MENU\":\"PORTAFOGLIO CARTE\",\"SETTINGS_MENU\":\"IMPOSTAZIONI\",\"HELP_MENU\":\"Tutorial\",\"LOGOUT_MENU\":\"Esci\",\"LAST_ACCESS\":\"Ultimo accesso eseguito:\",\"SEC_MENU_INIT_TRANSFER\":\"Invia denaro adesso\",\"SEC_MENU_TRANSFERS_LIST\":\"Lista disposizioni\",\"SEC_MENU_ACCOUNT\":\"Dettagli invio denaro\",\"SEC_MENU_SETTINGS\":\"Soglie disposizioni\",\"SAVE\":\"Salva\",\"REVERT\":\"Annulla\",\"INVITE\":\"Invita\",\"CANCEL\":\"Cancella\",\"CONFIRM\":\"Conferma\",\"FORWARD\":\"Avanti\",\"DONE\":\"Done\",\"OK\":\"Ok\",\"BACK\":\"Back\",\"HOME_BUTTON\":\"Home\",\"YES\":\"Sì\",\"NO\":\"No\",\"CANCEL_PAYMENT\":\"Annulla invio\",\"FINE\":\"Avanti\",\"PROCEED\":\"Procedi\",\"MORE_INFO\":\"Scopri di più\",\"WATCH_DEMO\":\"Guarda la demo\",\"HOME_TITLE\":\"Home Page\",\"RECEIVING_DATA\":\"Caricamento servizio...\",\"SERVICE_AVAILABLE\":\"Abilitato\",\"SERVICE_DISABLED\":\"Servizio non disponibile\",\"NFC_SERVICE_NAME\":\"Pago contactless\",\"P2P_SERVICE_NAME\":\"Invio denaro\",\"WALLET_SERVICE_NAME\":\"Portafoglio carte\",\"P2P_TITLE\":\"Invio denaro\",\"P2P_TRANSFER_STEP1\":\"Seleziona un beneficiario\",\"SEARCH_CONTACT_LABEL\":\"Inserire nome o numero del beneficiario\",\"SEARCH_CONTACT_PLACEHOLDER\":\"Ricerca\",\"NAME_NOT_FOUND\":\"Nome non trovato!\",\"NUMBER_NOT_FOUND\":\"Numero non trovato!\",\"REGISTERED_CONTACTS\":\"Preferiti\",\"ALL_CONTACTS\":\"Tutti\",\"P2P_TRANSFER_STEP2\":\"Imposta i dettagli dell’operazione\",\"P2P_AMOUNT_LABEL\":\"Importo \",\"P2P_AMOUNT_PLACEHOLDER\":\"Inserire l\u0027importo\",\"P2P_REASON_LABEL\":\"Causale\",\"P2P_REASON_PLACEHOLDER\":\"Inserire causale\",\"P2P_TRANSFER_AUTH\":\"Verifica e conferma\",\"CODE_P2P_FIRST\":\"Per confermare la transazione, inserisci il codice della tessera Qui UBI n. \",\"CODE_P2P_SECOND\":\"Tentativi rimasti:\",\"CODE_P2P_PLACEHOLDER\":\"Codice dispositivo Qui UBI\",\"P2P_TRANSFER_STEP3\":\"Risultato del pagamento\",\"SMS_TEXT_BUTTON\":\"Invia SMS al destinatario\",\"NEW_P2P\":\"Nuovo pagamento\",\"TRANSFERS_LIST_P2P\":\"Lista pagamenti\",\"PAYMENT_MADE\":\"Effettuato\",\"PAYMENT_PENDING\":\"In attesa\",\"P2P_TRANSFERS_LIST_TITLE\":\"Lista disposizioni\",\"DATE_TRANSFERS_LIST\":\"Data\",\"AMOUNT_TRANSFERS_LIST\":\"Importo\",\"RECIPIENT_TRANSFERS_LIST\":\"Ordinante / Beneficiario\",\"TRANSFER_DETAIL_TITLE\":\"Dettaglio pagamento\",\"TRANSFER_DEBIT_ACCOUNT\":\"Debit account\",\"ACCOUNT_LABEL\":\"Conto/carta di addebito\",\"ACCOUNT_LABEL_CREDIT\":\"Conto/carta di accredito\",\"TRANSFER_ACCOUNT_OWNER\":\"Proprietario\",\"TRANSFER_DETAILS\":\"Dettagli\",\"TRANSFER_BALANCE\":\"Saldo disponibile\",\"TRANSFER_ID\":\"Identificativo operazione\",\"TRANSFER_RECIPIENT\":\"Nome beneficiario\",\"TRANSFER_RECIPIENT_NUMBER\":\"N. cellulare beneficiario\",\"TRANSFER_NAME_RECIPIENT\":\"Nome beneficiario\",\"TRANSFER_NAME_ORDINANT\":\"Nome ordinante\",\"TRANSFER_NUMBER_RECIPIENT\":\"N. cellulare beneficiario\",\"TRANSFER_NUMBER_ORDINANT\":\"N. cellulare ordinante\",\"TRANSFER_AMOUNT\":\"Importo\",\"TRANSFER_COMMISSION\":\"Commissioni\",\"TRANSFER_TOTAL\":\"Importo totale\",\"TRANSFER_DESCRIPTION\":\"Causale\",\"TRANSFER_DATE_REQUESTED\":\"Data operazione\",\"TRANSFER_DATE_EXECUTED\":\"Data esecuzione\",\"TRANSFER_STATUS\":\"Stato\",\"TRANSFER_TYPE\":\"Tipo operazione\",\"TRANSFER_ADDEB\":\"In addebito\",\"TRANSFER_ACRED\":\"In accredito\",\"P2P_ACCOUNT_TITLE\":\"P2P account\",\"ACCOUNT_ACTIVE_TITLE\":\"Active P2P account\",\"ACCOUNT_OWNER\":\"Card owner\",\"P2P_SETTINGS_TITLE\":\"Soglie disposizioni\",\"P2P_SETTINGS_SUBTITLE\":\"Modifica soglie servizio Invio denaro\",\"SINGLE_TRANSFERENCE_LIMIT\":\"Singola transazione\",\"DAY_LIMIT\":\"Soglia massima giornaliera\",\"MONTH_LIMIT\":\"Soglia massima mensile\",\"SETTINGS_TITLE\":\"Impostazioni\",\"TUTORIAL_TITLE_SETTINGS\":\"Tutorial\",\"TUTORIAL_CHECK_SETTINGS\":\"Non mostrare il tutorial all\u0027avvio\",\"WIDGET_CHECK_SETTINGS\":\"Consenti pagamenti tramite NFC dal widget\",\"TUTORIAL_START\":\"Avvia tutorial\",\"NFC_TITLE_SETTINGS\":\"Pago contactless\",\"NFC_CHECK_SETTINGS\":\"Turn on NFC sensor\",\"NFC_SELECT_SETTINGS\":\"Timeout NFC\",\"SAVE_SETTINGS\":\"Salva\",\"SUCCESS_OPERATION\":\"Operazione completata correttamente.\",\"FAILED_OPERATION\":\"Operazione non completata.\",\"WALLET_TITLE\":\"Portafoglio carte\",\"WALLET_DEFAULT_TITLE\":\"Carta di default\",\"WALLET_OTHERS_TITLE\":\"Altre carte nel tuo portafoglio \",\"WALLET_CARD_TYPE\":\"Tipo carta\",\"WALLET_CARD_OWNER\":\"Proprietario\",\"WALLET_CARD_NUMBER\":\"Numero carta\",\"NFC_TITLE\":\"Pago Contactless\",\"NFC_NOT_AVAILABLE\":\"Il device non supporta l\u0027NFC\",\"NFC_TELCO_ERROR\":\"Il wallet della Telco non è installato\",\"NFC_ROOT_DEVICE\":\"Il dispositivo ha privilegi di root, NFC non può essere utilizzato\",\"NFC_SECURITY_ERROR\":\"Controlli di sicurezza non superati\",\"NFC_STEP1\":\"Avvia pagamento\",\"NFC_CARD_NUMBER\":\"Numero di carta\",\"NFC_TURN_TEXT\":\"Il sensore NFC deve essere acceso\",\"TURN_NFC\":\"Accendi NFC\",\"PAY_NFC\":\"Paga\",\"NFC_STEP2\":\"Avvicina lo smartphone al POS\",\"NFC_COUNTDOWN\":\"secondi disponibili per connettersi al POS\",\"ABORT\":\"Annulla\",\"RETRY\":\"Riprova\",\"NFC_RETRY_TEXT\":\"Impossibile connettersi al POS\",\"NFC_STEP3\":\"Risultato pagamento\",\"NFC_SUCCESS\":\"Richiesta di pagamento effettuata.\u003cbr/\u003e Si prega di verificare l\u0027esito del pagamento sul terminale POS.\",\"NFC_FAIL\":\"Richiesta di pagamento non riuscita.\u003cbr/\u003e Verificare motivo sul terminale POS dell\u0027esercente\",\"AT\":\"alle\",\"NFC_AMOUNT\":\"Importo\",\"NFC_DATE\":\"Data\",\"NEW_NFC\":\"Nuovo pagamento\",\"NFC_GENERIC_ERROR\":\"Errore generico NFC engine\",\"BALANCE_UNAVAIL\":\"n.d.\",\"SINGLE_PAYMENT_EXCEED\":\"L\u0027importo supera il massimo consentito in una singola transazione\",\"SESSION_EXPIRED\":\"Sessione scaduta\",\"INVALID_USER\":\"Utenza non censita\",\"UNHANDLED_SERVER_ERROR\":\"Errore generico\",\"WRONG_VERSION\":\"La versione attuale non corrisponde all\u0027ultima disponibile.\u003cbr/\u003e\u003cbr/\u003ePrega di contattare l\u0027amministratore.\",\"WRONG_SETTINGS\":\"C\u0027e stato un errore di ricezione dati in impostazioni.\u003cbr/\u003e\u003cbr/\u003ePrega di contattare l\u0027amministratore.\",\"ERROR_SYNC_CONTACTS\":\"Errore durante la sincronizzazione dei contatti\",\"ERROR_NO_CONTACTS\":\"Nessun contatto trovato sul dispositivo\",\"DEVICE_JAILBREAK_ERROR\":\"Il servizio Pago contactless di UBI PAY non è utilizzabile. Il tuo dispositivo risulta avere un sistema operativo non conforme. Aggiorna il tuo sistema operativo scaricando sul telefono l\u0027ultimo aggiornamento della versione ufficiale. Per qualsiasi chiarimento contatta la tua filiale oppure il Servizio Clienti al numero verde 800.500.200 (dall\u0027estero +39.030.2471209).\",\"DEVICE_ROOT_ERROR\":\"Il servizio Pago contactless di UBI PAY non è utilizzabile. Il tuo dispositivo risulta avere un sistema operativo non conforme. Aggiorna il tuo sistema operativo scaricando sul telefono l\u0027ultimo aggiornamento della versione ufficiale. Per qualsiasi chiarimento contatta la tua filiale oppure il Servizio Clienti al numero verde 800.500.200 (dall\u0027estero +39.030.2471209).\",\"NO_CARD_DEFAULT\":\"Nessuna carta di default.\",\"MAIN_BECOME_CLIENT_TEXT\":\"Voglio diventare cliente UBI PAY (se hai ricevuto del denaro ti verrà accreditato in automatico alla conclusione del contratto)\",\"RADIO_ALREADY_CLIENT\":\"Sono cliente UBI Banca\",\"RADIO_NON_CLIENT\":\"Non sono cliente UBI Banca\",\"RADIO_NON_BECOME_CLIENT\":\"Ho ricevuto del denaro man non voglio diventare cliente UBIPAY\",\"CHECK_AUTH_PRELOGIN\":\"Autorizzo il trattamento dei dati personali ai sensi del dlgs. XX YY\",\"NAME_PRELOGIN_PLACEHOLDER\":\"Nome\",\"LASTNAME_PRELOGIN_PLACEHOLDER\":\"Cognome\",\"PHONE_PRELOGIN_PLACEHOLDER\":\"Numero di telefono\",\"MAIL_PRELOGIN_PLACEHOLDER\":\"Email\",\"VERIFY_IBAN_TEXT\":\"Inserisci il codice di sicurezza che hai ricevuto via SMS e compila con il codice IBAN sul quale desideri ricevere le somme in sospeso a tuo favore. Per proseguire clicca su «Verifica IBAN»\",\"VERIFY_IBAN_TEXT2\":\"Hai inserito il tuo IBAN per ricever le somme sospese a tuo favore: premi «Verifica IBAN»\",\"SECURITY_CODE_PRELOGIN_PLACEHOLDER\":\"Codice di Sicurezza\",\"IBAN_PRELOGIN_PLACEHOLDER\":\"Codice IBAN\",\"IBAN_VERIFY\":\"Verifica IBAN\",\"NEW_SECURITY_TEXT\":\"Premi qui per richiedere un nuovo codice di sicurezza.\",\"PAYMENT_SELECTION_TOP_TEXT1\":\"Ci sono\",\"PAYMENT_SELECTION_TOP_TEXT2\":\"in sospeso per te disponibili:\",\"PAYMENT_SELECTION_TEXT\":\"Seleziona l\u0027importo che vuoi incassare per primo. Cliccando su «Procedi» si passa alla schermata successiva di riepilogo dell\u0027operazione.Se vuoi incassare anche le altre somme sospese, dopo aver ultimato questa operazione, riapri l\u0027App per inviare nuovamente il tuo IBAN e ottenere un nuovo codice di sicurezza\",\"PAYMENT_ORD\":\"Ordinante\",\"PAYMENT_TOTAL\":\"Somma da ricevere\",\"PAYMENT_BANK\":\"Banca Beneficiario\",\"PAYMENT_OFFICE\":\"Filiale\",\"PAYMENT_REASON\":\"Causale\",\"PAYMENT_DATE\":\"Data operazione\",\"PAYMENT_REVIEW_TEXT\":\"Di seguito i dati di riepilogo dell\u0027invio IBAN e incasso della somma:\",\"PAYMENT_REVIEW_TEXT2\":\"Verifica la correttezza dei dati del tuo conto corrente e clicca su «Conferma» per ricevere la somma\",\"DEMO_RESULT_PRELOGIN\":\"Vuoi saperne di più su UBI PAY?\",\"DEMO_UBI\":\"Demo UBI PAY\",\"ATTENTION_IBAN\":\"ATTENZIONE!\",\"NO_PENDING_TEXT\":\"I dati che hai inserito non corrispondono ad alcuna operazione sospesa\",\"TEXT_BACK_IBAN\":\"Clicca sul botone «Indietro» per inserire nuovamente i toui dati\",\"BACK_IBAN\":\"Indietro\",\"REMAINING_IBAN_TEXT1\":\"Hai ancora\",\"REMAINING_IBAN_TEXT2\":\"tentativi.\",\"BECOME_USER_FIRST_TEXT\":\"UBI PAY è lo strumento che permette di gestire el tuo denaro in mobilità, in modo semplice, veloce e sicuro.\",\"BECOME_USER_SECOND_TEXT\":\"Aderire ad UBI PAY è facile. Riceverai una mail con tutte le istruzioni per sottoscrivere ed attivare il servizio.\",\"BECOME_USER_THIRD_TEXT\":\"Controlla la tua casella di posta elettronica!\",\"SUCCESS_RESULT_PRELOGIN\":\"Grazie per aver utilizzato la App UBI PAY! Riceverai a breve la somma che ti spetta direttamente sul tuo conto corrente, tramite Bonifico Bancario e una email de riepilogo dell\u0027operazione.\",\"NEW_SECURITY_CODE_POPUP_MSG\":\"New security code popup msg\"}");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseGetLocalSupportedDescriptors(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            dataWriter.WriteString("[\"it\",\"ita\",\"en\",\"eng\"]");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseGetLocaleCurrent(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            dataWriter.WriteString("{\"Language\":\"en\",\"Country\":\"US\"}");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseGetOSHardwareInfo(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            dataWriter.WriteString("{\"Name\":\"iPhone\",\"Vendor\":null,\"UUID\":\"623456789012345678901234567890123456789c\",\"Version\":\"6\"}");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseGetOSInfo(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            dataWriter.WriteString("{\"Name\":\"iOS\",\"Vendor\":\"Apple\",\"Version\":\"8.0.2\"}");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        public AppServerRequestResponse HandleAppverse(AppServerRequestResponse request)
        {
            AppServerRequestResponse newResponse = new AppServerRequestResponse();
            Stream responseStream = new MemoryStream();

            Task task = new Task(async () =>
            {

                newResponse.httpHeaders = new Dictionary<string, string>();
                StreamWriter contentWriter = new StreamWriter(responseStream);
                DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
                contentWriter.AutoFlush = false;

                Debug.WriteLine("API Request: {0} {1}", request.httpMethod, request.httpUri);
                if (request.httpMethod == "OPTIONS")
                {
                    //Debug.WriteLine("----------------------------------------------");
                    //Debug.WriteLine("Request: {0} {1}" + request.httpMethod, request.httpUri);
                    foreach (string key in request.httpHeaders.Keys)
                    {
                        Debug.WriteLine("Key: {0}  Value: {1}", key, request.httpHeaders[key]);
                    }
                    Debug.WriteLine("----------------------------------------------");
                    
                    newResponse.httpHeaders.Add("Access-Control-Allow-Origin", request.httpHeaders["Origin"]);
                    newResponse.httpHeaders.Add("Access-Control-Allow-Methods", request.httpHeaders["Access-Control-Request-Method"]);
                    newResponse.httpHeaders.Add("Access-Control-Allow-Headers", request.httpHeaders["Access-Control-Request-Headers"]);
                    newResponse.httpHeaders.Add("Connection", "close");
                    newResponse.httpHeaders.Add("Pragma", "no-cache");
                    await dataWriter.FlushAsync();
                    await dataWriter.StoreAsync();
                }
                else
                {
                    // Process rules.
                    bool mappingFound = false;
                    if (appverseFunctions != null)
                    {
                        //newResponse.httpHeaders.Add("Access-Control-Allow-Origin", request.httpHeaders["Origin"]);
                        //newResponse.httpHeaders.Add("Access-Control-Allow-Method", "POST");
                        //newResponse.httpHeaders.Add("Access-Control-Allow-Headers", request.httpHeaders["Access-Control-Request-Headers"]);                        
                        //newResponse.httpHeaders.Add("Content-Type", "application/json");
                        //newResponse.httpHeaders.Add("Connection", "close");
                        //newResponse.httpHeaders.Add("Pragma", "no-cache");
                       
                        foreach (Regex function in appverseFunctions.Keys)
                        {
                            //if it matches the URL
                            if (function.IsMatch(request.httpUri))
                            {
                                try
                                {
                                    request.httpContent.Position = 0;
                                    AppServerRequestResponse toSend = await appverseFunctions[function](responseStream, newResponse, request);
                                    toSend.httpHeaders.Add("Access-Control-Allow-Origin", request.httpHeaders["Origin"]);
                                    toSend.httpHeaders.Add("Access-Control-Allow-Method", "POST");
                                    toSend.httpHeaders.Add("Access-Control-Allow-Headers", "content-type");//"request.httpHeaders["Access-Control-Request-Headers"]);                        
                                    toSend.httpHeaders.Add("Content-Type", "application/json; charset=utf-8");
                                    toSend.httpHeaders.Add("Connection", "close");
                                    toSend.httpHeaders.Add("Pragma", "no-cache");
                                    
                                    mappingFound = true;
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine("Appverse Function Exception: {0} {1} {2}", ex.GetType(), ex.Message, ex.StackTrace);
                                    mappingFound = false;
                                }
                                if (mappingFound) break;
                            }
                        }
                    }
                    if (!mappingFound)
                    {
                        Debug.WriteLine("API Request Not Implemented: {0} {1}", request.httpMethod, request.httpUri);
                        dataWriter.WriteString("HTTP/1.1 404 Not Found\r\n");
                        dataWriter.WriteString("Content-Type: text/html\r\n");
                        dataWriter.WriteString("Content-Length: 9\r\n");
                        dataWriter.WriteString("Pragma: no-cache\r\n");
                        dataWriter.WriteString("Connection: close\r\n");
                        dataWriter.WriteString("\r\n");
                        dataWriter.WriteString("Not found");
                        await dataWriter.FlushAsync();
                        await dataWriter.StoreAsync();
                    }
                }
            });
            task.Start();
            task.Wait();
            newResponse.httpContent = responseStream;
            return newResponse;
        }
    }
}