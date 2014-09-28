config = {
    VERSION_APP: "4.5",
    VERSION_DISCLAIMER: " &copy; 2013 GFT IT Consulting, S.L.U.",
    CONNECTIVITY_HOST: "unity.gft.com",
    menuOptions: [
        {name: 'Network', icon: 'resources/images/network_icon.png', api: TestCase_Network, platformAvailable: true},
        {name: 'Display', icon: 'resources/images/sys_display_icon.png', api: TestCase_System_Display, platformAvailable: true},
        {name: 'HumanInteraction', icon: 'resources/images/sys_humaninteract_icon.png', api: TestCase_System_HumanInteraction, platformAvailable: true},
        {name: 'Memory', icon: 'resources/images/sys_memory_icon.png', api: TestCase_System_Memory, platformAvailable: true},
        {name: 'OperatingSystem', icon: 'resources/images/sys_os_icon.png', api: TestCase_System_OS, platformAvailable: true},
        {name: 'Power', icon: 'resources/images/sys_power_icon.png', api: TestCase_System_Power, platformAvailable: true},
        {name: 'Processor', icon: 'resources/images/sys_processor_icon.png', api: TestCase_System_Processor, platformAvailable: true},
        {name: 'Database', icon: 'resources/images/database_icon.png', api: TestCase_Database, platformAvailable: true},
        {name: 'FileSystem', icon: 'resources/images/filesystem_icon.png', api: TestCase_Filesystem, platformAvailable: true},
        {name: 'Notification', icon: 'resources/images/notification_icon.png', api: TestCase_Notification, platformAvailable: true},
        {name: 'IOServices', icon: 'resources/images/io_services_icon.png', api: TestCase_IOServices, platformAvailable: true},
        {name: 'Geolocation', icon: 'resources/images/geo_icon.png', api: TestCase_Geolocation, platformAvailable: true},
        {name: 'Media', icon: 'resources/images/media_icon.png', api: TestCase_Media, platformAvailable: true},
        {name: 'Messaging', icon: 'resources/images/messaging_icon.png', api: TestCase_Messaging, platformAvailable: true},
        {name: 'Contacts', icon: 'resources/images/contacts_icon.png', api: TestCase_Pim_Contacts, platformAvailable: true},
        {name: 'Calendar', icon: 'resources/images/calendar_icon.png', api: TestCase_Pim_Calendar, platformAvailable: true},
        {name: 'Telephony', icon: 'resources/images/phone_icon.png', api: TestCase_Telephony, platformAvailable: true},
        {name: 'Internationalization', icon: 'resources/images/i18n_icon.png', api: TestCase_i18n, platformAvailable: true},
        {name: 'Analytics', icon: 'resources/images/analytics_icon.png', api: TestCase_Analytics, platformAvailable: true},
        {name: 'Security', icon: 'resources/images/security_icon.png', api: TestCase_Security, platformAvailable: true},
        {name: 'Webtrekk', icon: 'resources/images/webtrekk_icon.png', api: TestCase_Webtrekk, platformAvailable: true},
        {name: 'AppLoader', icon: 'resources/images/app_loader_icon.png', api: TestCase_AppLoader, platformAvailable: true},
        {name: 'NFC', icon: 'resources/images/nfc_icon.png', api: TestCase_NFC, platformAvailable: (!Appverse.is.iOS)},
        {name: 'SmartBeacon', icon: 'resources/images/beacon_icon.png', api: TestCase_Beacon, platformAvailable: (!Appverse.is.iOS)}
    ]
}

