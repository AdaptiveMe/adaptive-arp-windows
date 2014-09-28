var testModuleUUID = '85FC11DD-4CCA-4B27-AFB3-876854BB5C3B';
// Beacon TEST CASE
var TestCase_Beacon = [Appverse.Beacon,
    [['StartMonitoringAllRegions', ''],
        ['StartMonitoringRegion', '{"param1":' + JSON.stringify(testModuleUUID) + '}'],
        ['StopListening', '']]
];

//********** HANDLING Beacon Events

Appverse.Beacon.OnEntered = function(beacons) {
    Showcase.app.getController('Main').toast("SmartBeacon OnEntered " + JSON.stringify(arguments));
    console.log("SmartBeacon OnEntered " + JSON.stringify(arguments));
}

Appverse.Beacon.OnExited = function(beacons) {
    Showcase.app.getController('Main').toast("SmartBeacon OnExited " + JSON.stringify(arguments));
    console.log("SmartBeacon OnExited " + JSON.stringify(arguments));
}

Appverse.Beacon.OnDiscover = function(beacons) {
    Showcase.app.getController('Main').toast("SmartBeacon OnDiscover " + JSON.stringify(arguments));
    console.log("SmartBeacon OnDiscover " + JSON.stringify(arguments));
}

Appverse.Beacon.OnUpdateProximity = function(Beacon, from, to) {
    Showcase.app.getController('Main').toast("SmartBeacon OnUpdateProximity " + JSON.stringify(arguments));
    console.log("SmartBeacon OnUpdateProximity " + JSON.stringify(arguments));
}

