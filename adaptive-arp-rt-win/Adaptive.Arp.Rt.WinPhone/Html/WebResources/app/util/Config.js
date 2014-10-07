// wallet package name
var WALLET_PACKAGE_NAKED = "com.telecomitalia.paymentintentconsole";
var WALLET_PACKAGE_UAT = "it.telecomitalia.timwallet.beta";
var WALLET_PACKAGE_PROD = "it.telecomitalia.wallet";

var prop_DEV = {
    connectivityCheck: "google.com", // as requested in SIAPTPE-62 to avoid connectivity issues to that SIT environment
    //connectivityCheck: (Appverse.is.Android ? "http://" : "") + "156.54.155.74",
    walletPackage: WALLET_PACKAGE_NAKED,
    server: "server_DEV",
    env: "DEV",
    nfcDebugMode: "true",
    allowLog: false
};

var prop_SIT = {
    connectivityCheck: "google.com", // as requested in SIAPTPE-62 to avoid connectivity issues to that SIT environment
    // connectivityCheck: (Appverse.is.Android ? "https://tst.vaservices.eu:1443" : "tst.vaservices.eu")
    walletPackage: WALLET_PACKAGE_PROD,
    server: "server_SIT",
    env: "SIT",
    useEnvironmentOnVersion: true,
    nfcDebugMode: "false",
    allowLog: false
};

var prop_UAT = {
    connectivityCheck: "google.com", // as requested in SIAPTPE-62 to avoid connectivity issues to that SIT environment
    //connectivityCheck: (Appverse.is.Android ? "https://" : "") + "tst.vaservices.eu",
    walletPackage: WALLET_PACKAGE_PROD,
    server: "server_UAT",
    env: "UAT",
    useEnvironmentOnVersion: true,
    nfcDebugMode: "false",
    allowLog: false
};

var prop_PROD = {
    //connectivityCheck: "google.com", // as requested in SIAPTPE-62 to avoid connectivity issues to that SIT environment
    connectivityCheck: (Appverse.is.Android ? "https://" : "") + "web.vaservices.eu",
    walletPackage: WALLET_PACKAGE_PROD,
    server: "server_PROD",
    env: "PROD",
    useEnvironmentOnVersion: true,
    nfcDebugMode: "false",
    allowLog: false
};

//// ATTENTION: check this variable for each environment build
var environmentProperties = prop_DEV;

config = {
    //app
    VERSION_APP: "1.1.1" + (environmentProperties.useEnvironmentOnVersion ? "_" + environmentProperties.env : ""),
    VERSION_ENVIRONMENT: environmentProperties.env,
    VERSION_DISCLAIMER: " &copy; 2014 GFT IT Consulting, S.L.U.",
    SUPPORT_WEBPAGE: "http://www.ubibanca.com/page/servizio-clienti",
    SUPPORT_NUMBER: "800500200",
    GROUP_CODE: "03111",
    CONNECTIVITY_HOST: environmentProperties.connectivityCheck,
    //server
    SERVER: environmentProperties.server, //"mockServer",
    // wallet package
    WALLET_PACKAGE: environmentProperties.walletPackage,
    //
    CODE_WRONG_AUTH_LOGIN: "-02",
    CODE_USER_LOCKED_LOGIN: "-03",
    CODE_OBSOLETE_VERSION: "-05",
    CODE_WRONG_AUTH_PAYMENT: "-02",
    CODE_USER_LOCKED_PAYMENT: "-03",
    CODE_PAYMENT_CONFIRMED: "02",
    CODE_PAYMENT_PENDING: "03",
    CODE_SESSION_EXPIRATION: "92",
    CODE_UNMATCHING_VERSION: "100", //TODO manage this error
    INACTIVITY_TIMEOUT: 15, //in minutes
    //
    //languages
    DEFAULT_LANGUAGE: "it",
    ITALIAN_ENUM: 0,
    ENGLISH_ENUM: 1,
    //service enum
    P2P_SERVICE: 0,
    MP_SERVICE: 1,
    //visualization
    VIEW_P2P: 0,
    VIEW_MP: 1,
    VIEW_ALL: 2,
    //service status
    SERVICE_ACTIVE: 0,
    SERVICE_DISABLED: 1,
    SERVICE_SAT_GG: 2,
    SERVICE_SAT_MM: 3,
    SERVICE_SAT_NO: 4,
    SERVICE_SUSPENDED: 5,
    //status payment
    PAYMENT_RIC: 0,
    PAYMENT_EFF: 1,
    PAYMENT_PND: 2,
    PAYMENT_ANN: 3,
    PAYMENT_ERR: 4,
    //instrument type
    CC: 0,
    CREDIT_CARD: 1,
    //credit card type
    VISA: 0,
    MASTERCARD: 1,
    MAESTRO: 2,
    DINERS: 3,
    AMEX: 4,
    JCB: 5,
    CUP: 6,
    POSTAMAT: 7,
    //phone operator code
    OP_TIM: 0,
    OP_WIND: 1,
    OP_VODAFON: 2,
    OP_TRE: 3,
    //thresholds
    SINGLE_PAYMENT: 0,
    DAY_PAYMENT: 1,
    MONTH_PAYMENT: 2,
    NO_AUTH: 3,
    MINIMUM_PAYMENT: 4,
    //NFC properties
    NFC_APP_ID: {"05428": ["A0000000041010BB43495042B1000901", null, null],
        "05048": ["A0000000041010BB43495042B1000F01", null, null],
        "05308": ["A0000000041010BB43495042B1001001", null, null],
        "03067": ["A0000000041010BB43495042B1001101", null, null],
        "03500": ["A0000000041010BB43495042B1001201", null, null],
        "06906": ["A0000000041010BB43495042B1001301", "40", "A0000000041010BB43495042B1001501"],
        "03244": ["A0000000041010BB43495042B1001401", null, null],
        "03083": ["A0000000041010BB43495042B1001601", null, null]
    },
    NFC_TIMEOUT: 6000,
    NFC_DEBUG_MODE: environmentProperties.nfcDebugMode,
    ALLOW_LOG: environmentProperties.allowLog,
    //ENUM OWN PHONE SIAPTPE-178
    OWN_PHONE: 2,
    //PRELOGIN SERVER CODES
    CODE_ONE_PAYMENT: "04",
    CODE_MANY_PAYMENTS: "05",
    CODE_NO_PAYMENTS: "-06",
    CODE_ERROR_PRELOGIN_AUTH:"-02"
};
