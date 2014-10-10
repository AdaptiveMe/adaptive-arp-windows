package it.ubiss.mpay.widget.services;

import it.ubiss.mpay.widget.Constants;
import it.ubiss.mpay.widget.utils.AndroidUtils;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.IBinder;
import android.util.Log;

/**
 * This class defines a broadcast receiver than hears all the changes produced in the NFC network
 * status changes. The declaration of this class is defined in the Android Manifest.
 */
public class NFCStatusChangeReceiver extends BroadcastReceiver {

	/**
	 * This method is called when the BroadcastReceiver is receiving an Intent broadcast. When it
	 * runs on the main thread you should never perform long-running operations in it (there is a
	 * timeout of 10 seconds that the system allows before considering the receiver to be blocked
	 * and a candidate to be killed). You cannot launch a popup dialog in your implementation of
	 * onReceive().
	 */
	@Override
	public void onReceive(Context ctx, Intent intent) {
		
		AndroidUtils.log(Log.VERBOSE, "The NFC broadcast receiver recives and event of changinf the NFC state.");

		// If the payment service is started, we recheck the NFC status, if is not enabled, we do
		// nothing
		if (AndroidUtils.getBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS)) {
			
			AndroidUtils.log(Log.WARN, "The NFC payment is started, so it is necessary to check the NFC status.");

			// Start the service in order to execute the action
			Intent service = new Intent(ctx, ActionDispacherService.class);
			service.putExtra(Constants.ACTION_ID, Constants.CHECK_NFC_ENABLED);
			ctx.startService(service);
		}
	}

	/**
	 * Provide a binder to an already-running service. This method is synchronous and will not start
	 * the target service if it is not present, so it is safe to call from onReceive(Context,
	 * Intent).
	 */
	@Override
	public IBinder peekService(Context ctx, Intent intent) {

		return super.peekService(ctx, intent);
	}

}
