using Adaptive.Arp.Impl.WinPhone.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage.Streams;

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
            appverseFunctions.Add(new Regex("^/service/pim/ListContacts"), AppverseListContacts);
            appverseFunctions.Add(new Regex("^/service/io/InvokeService"), AppverseInvokeService);
        }

        private async Task<AppServerRequestResponse> AppverseInvokeService(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            DataReader dataReader = new DataReader(request.httpContent.AsInputStream());
            dataReader.InputStreamOptions = InputStreamOptions.Partial;
            //request.httpContent.Position = request.httpContent.Length - Convert.ToInt32(request.httpHeaders["Content-Length"]);
            foreach (string key in request.httpHeaders.Keys)
            {
                Debug.WriteLine("Key: {0}  Value: {1}", key, request.httpHeaders[key]);
            }

            // Data dataReader flags and options
            bool readFinished = false;
            uint readMaxBufferSize = 4096;

            while (!readFinished)
            {
                await dataReader.LoadAsync(readMaxBufferSize);

                if (dataReader.UnconsumedBufferLength > 0)
                {
                    // Read buffer
                    uint readLength = dataReader.UnconsumedBufferLength;
                    byte[] readBuffer = new byte[dataReader.UnconsumedBufferLength];
                    dataReader.ReadBytes(readBuffer);
                    // Write buffer
                    Debug.WriteLine("Data: {0}", Encoding.UTF8.GetString(readBuffer, 0, readBuffer.Length));

                    // Not full buffer, reached eof
                    if (readLength < readMaxBufferSize) readFinished = true;
                }
                else
                {
                    // Reached eof 
                    readFinished = true;
                }
            }
            if (readFinished == true)
            {
                
            }
            dataWriter.WriteString("true");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseListContacts(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            // TODO: Call Listener
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseGetStoredKeyValuePairs(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            // TODO: Call Listener
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
            dataWriter.WriteString("{\"Emulator\":true,\"EmulatorOrientation\":0,\"EmulatorScreen\":\"{height:1232,widht:720}\",\"Windows\":false,\"iPod\":false,\"iPad\":false,\"iPhone\":false,\"Android\":true,\"Blackberry\":false,\"TabletDevice\":false,\"Tablet\":false,\"Phone\":true,\"iOS\":false}");
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
            dataWriter.WriteString("{\"Name\":\"Galaxy S3\",\"Vendor\":null,\"UUID\":\"623456789012345678901234567890123456789c\",\"Version\":\"Galaxy S3\"}");
            await dataWriter.FlushAsync();
            await dataWriter.StoreAsync();
            return response;
        }

        private async Task<AppServerRequestResponse> AppverseGetOSInfo(Stream responseStream, AppServerRequestResponse response, AppServerRequestResponse request)
        {
            DataWriter dataWriter = new DataWriter(responseStream.AsOutputStream());
            dataWriter.WriteString("{\"Name\":\"Android\",\"Vendor\":\"Google\",\"Version\":\"4.0.4\"}");
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

                //Debug.WriteLine("API Request: {0} {1}", request.httpMethod, request.httpUri);
                if (request.httpMethod == "OPTIONS")
                {
                    foreach (string key in request.httpHeaders.Keys)
                    {
                        //Debug.WriteLine("Key: {0}  Value: {1}", key, request.httpHeaders[key]);
                    }

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
                        // For each rule
                        newResponse.httpHeaders.Add("Access-Control-Allow-Origin", request.httpHeaders["Origin"]);
                        newResponse.httpHeaders.Add("Content-Type", "application/json");
                        newResponse.httpHeaders.Add("Connection", "close");
                        newResponse.httpHeaders.Add("Pragma", "no-cache");

                        foreach (Regex function in appverseFunctions.Keys)
                        {
                            //if it matches the URL
                            if (function.IsMatch(request.httpUri))
                            {
                                try
                                {
                                    AppServerRequestResponse toSend = await appverseFunctions[function](responseStream, newResponse, request);
                                    mappingFound = true;
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine("Appverse Function Exception: {0} {1}", ex.GetType(), ex.Message);
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
