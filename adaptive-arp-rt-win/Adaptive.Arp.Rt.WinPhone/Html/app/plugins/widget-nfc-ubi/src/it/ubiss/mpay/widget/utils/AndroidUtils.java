package it.ubiss.mpay.widget.utils;

import it.ubiss.mpay.R;
import it.ubiss.mpay.widget.Constants;
import it.ubiss.mpay.widget.WidgetProvider;
import android.content.Context;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
import android.util.Log;
import android.widget.Toast;

/**
 * Utils class for widget utilities
 */
public class AndroidUtils {

	/**
	 * Common function for logging using the android log on the project
	 * 
	 * @param level Log level
	 * @param message Message to be displayed
	 */
	public static void log(int level, String message) {

		switch (level) {
		case Log.VERBOSE:
			Log.v(Constants.DEBUG_TAG, message);
			break;
		case Log.DEBUG:
			Log.d(Constants.DEBUG_TAG, message);
			break;
		case Log.INFO:
			Log.i(Constants.DEBUG_TAG, message);
			break;
		case Log.WARN:
			Log.w(Constants.DEBUG_TAG, message);
			break;
		case Log.ERROR:
			Log.e(Constants.DEBUG_TAG, message);
			break;
		default:
			Log.e(Constants.DEBUG_TAG, "Log level not supported");
			break;
		}
	}

	/**
	 * Method that throws a toast on the smartphone screen
	 * 
	 * @param ctx Android context
	 * @param message Message show (strings.xml)
	 * @param lenght Toast Lenght (short|long)
	 */
	public static void toast(Context ctx, int message, int lenght) {

		Toast toast = Toast.makeText(ctx, ctx.getString(message), lenght);
		toast.show();
	}

	public static void toast(Context ctx, String message, int lenght) {

		Toast toast = Toast.makeText(ctx, message, lenght);
		toast.show();
	}

	/**
	 * Method that return the message of the payment result in case of negative result. At this time
	 * is not using the error code. But is defined
	 * 
	 * @param errorCode Error code returned by the library
	 * @return String resource
	 */
	public static int getPaymentErrorListenerMessage(int errorCode) {
		if(errorCode==Constants.P_OPERATION_FAILED){
			return R.string.ubiwidget_K_NFC_OPEFAILED;
		}

		return R.string.ubiwidget_error_payment;
	}

	/**
	 * Method that return the message of the engine start result in case of negative result. Not all
	 * the errors are supported by project requirements but there is a default value
	 * 
	 * @param errorCode Error code returned by the library
	 * @return String resource for the title (first param) and the message (second param)
	 */
	public static int[] getEngineStartErrorListenerMessage(int errorCode) {

		switch (errorCode) {

		case Constants.E_OPERATION_ERROR_CARDLET_NOT_FOUND:
			return new int[] { R.string.ubiwidget_error_settings_title, R.string.ubiwidget_K_NFC_CARDLNF };

		case Constants.E_OPERATION_ERROR_CARD:
			return new int[] { R.string.ubiwidget_error_settings_title, R.string.ubiwidget_K_NFC_ERRCARD };
			
		case Constants.E_OPERATION_ERROR_CARDLET_SECURITY:
			return new int[] { R.string.ubiwidget_error_settings_title, R.string.ubiwidget_K_NFC_SECERR };

		case Constants.E_WALLET_NOT_FOUND:
			return new int[] { R.string.ubiwidget_error_settings_title, R.string.ubiwidget_K_NFCWALTELCO };
			
		case Constants.E_OPERATION_FAILED:
			return new int[] { R.string.ubiwidget_error_settings_title, R.string.ubiwidget_K_NFC_OPEFAILED };

		default:
			return new int[] { R.string.ubiwidget_error_settings_title, R.string.ubiwidget_K_NFC_GENERR };
		}
	}

	/**
	 * 
	 * @param ctx Android Context
	 * @param variable Variable name
	 * @param value Value for the variable
	 */
	public static void setBooleanSharedPreferences(String variable, Boolean value) {

		try {
			SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(WidgetProvider.getAppContext());
			SharedPreferences.Editor editor = sharedPreferences.edit();
			editor.putBoolean(variable, value);
			editor.commit();
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "Unhandled exception setting data to the shared preferences: " + e.getMessage());
		}
	}

	/**
	 * Method for retrieving a variable from the shared preferences of Android.
	 * 
	 * @param ctx Android Context
	 * @param variable Variable Name
	 * @return Variable value
	 */
	public static Boolean getBooleanSharedPreferences(String variable) {

		Boolean ret = null;
		try {
			SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(WidgetProvider.getAppContext());
			ret = sharedPreferences.getBoolean(variable, false);
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "Unhandled exception retrieving data from the shared preferences: " + e.getMessage());
		}

		return ret;
	}
}
