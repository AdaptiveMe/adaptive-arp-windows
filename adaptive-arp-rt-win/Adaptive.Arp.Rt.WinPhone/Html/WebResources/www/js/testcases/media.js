var imgP = null,
        latestPickedImageUrl = null;
var testMediaFilePath = "res/sample.m4a";
//var testMediaFilePath = "res/Avatar.mov";
//var testMediaFileStream = "http://media.radiosrichinmoy.org/radio/46/46-18-1.m4a";
var testMediaFileStream = "http://trailers.apple.com/movies/fox/avatar/avatar-aug27a_480p.mov";
var testMediaPosition = 10;


/* QR Code */
var testAutoHandleQR = true;
var testQRCode = new Object();
testQRCode.Text = "TEL:123456789";
testQRCode.BarcodeType = Appverse.Media.BARCODETYPE_QR;
testQRCode.QRType = Appverse.Media.QRTYPE_TEL;


//********** UI COMPONENTS

mediaPanelConfig = {
    scroll: 'both',
    width: 250,
    height: 250,
    floating: true,
    centered: true,
    modal: true
};

mediaPanel = null;


mediaButtons = {
    xtype: 'container',
    layout: 'hbox',
    cls: 'mediaButtons',
    docked: 'top',
    items: [{xtype: 'spacer'}, {
            xtype: 'button',
            text: localizationController.localizedUIString("PlayAudio"),
            handler: function() {
                Appverse.Media.Play('res/sample.m4a');
            }
        }, {xtype: 'spacer'}, {
            xtype: 'button',
            text: localizationController.localizedUIString("PlayVideo"),
            handler: function() {
                Appverse.Media.Play('res/Avatar.mov');
            }
        }, {xtype: 'spacer'}, {
            xtype: 'button',
            text: localizationController.localizedUIString("PlayStream"),
            handler: function() {
                Appverse.Media.PlayStream('http://builder.gft.com/appstore/showcase/sample.m4a');
            }
        }, {xtype: 'spacer'}]
};

openMedia = function() {

    if (mediaPanel) {
        mediaPanel.show('pop');
        mediaPanel.scroller.scrollTo({x: 0, y: 0});
    }
};


//********** MEDIA TESTCASES
var TestCase_Media = [Appverse.Media,
    [['GetMetadata', '{"param1":' + JSON.stringify(testMediaFilePath) + '}'],
        ['GetCurrentMedia', ''],
        ['GetSnapshot', ''],
        ['GetState', ''],
        ['Play', '{"param1":' + JSON.stringify(testMediaFilePath) + '}'],
        ['PlayStream', '{"param1":' + JSON.stringify(testMediaFileStream) + '}'],
        ['Pause', ''],
        ['SeekPosition', '{"param1":' + JSON.stringify(testMediaPosition) + '}'],
        ['Stop', ''],
        ['DetectQRCode', '{"param1":' + JSON.stringify(testAutoHandleQR) + '}'],
        ['HandleQRCode', '{"param1":' + JSON.stringify(testQRCode) + '}'],
        ['TakeSnapshot', '']], mediaButtons];


//********** HANDLING CALLBACKS

Appverse.Media.onFinishedPickingImage = function(mediaMetadata) {
    console.log(JSON.stringify(mediaMetadata));
    try {
        //Ext.Msg.alert("Image picked!");
        Showcase.app.getController('Main').toast("Image picked!");
        // remove previous picked image, if exists
        if (latestPickedImageUrl) {
            var file = new Object();
            file.FullName = latestPickedImageUrl;
            Appverse.FileSystem.DeleteFile(file);
        }
        formPanel = Ext.ComponentQuery.query('formpanel')[0];
        var record = formPanel.getRecord();
        var resultJsonString = null;
        if (mediaMetadata != null) {
            resultJsonString = JSON.stringify(mediaMetadata);
        }

        //Ext.ComponentQuery.query('panel[cls="resultPanel"]')[0].setHtml('<img src="' + Appverse.DOCUMENTS_RESOURCE_URI + mediaMetadata.ReferenceUrl + '"/>');
        imgP = Ext.create('Ext.Container',{html:'<img src="' + Appverse.DOCUMENTS_RESOURCE_URI + mediaMetadata.ReferenceUrl + '"/>'});

        
        latestPickedImageUrl = mediaMetadata.ReferenceUrl;

        //mediaPanel = Ext.create('Ext.Panel',mediaPanelConfig);

        record.setResult(resultJsonString, true);
        formPanel.load(record);
        resultP.add(imgP);
    } catch (e) {
        Ext.Msg.alert(e);
    }
};

//********** HANDLING QR CODES
Appverse.Media.onQRCodeDetected = function(QRCodeContent) {
    testQRCode = QRCodeContent;
    console.log("onQRCodeDetected");
    console.dir(QRCodeContent);
    formPanel = Ext.ComponentQuery.query('formpanel')[0];
    var record = formPanel.getRecord();
    var resultJsonString = null;
    if (QRCodeContent != null) {
        try {
            resultJsonString = JSON.stringify(QRCodeContent);
        } catch (e) {
            Ext.Msg.alert(e);
        }
    }
    record.setResult(resultJsonString, true);
    formPanel.load(record);

};


