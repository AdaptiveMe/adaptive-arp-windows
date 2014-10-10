package it.ubiss.mpay.widget.services;

import it.ubiss.mpay.R;
import it.ubiss.mpay.widget.Constants;
import it.ubiss.mpay.widget.WidgetProvider;
import it.ubiss.mpay.widget.payment.AndroidNFCPayment;
import it.ubiss.mpay.widget.security.AndroidSecurity;
import it.ubiss.mpay.widget.security.KeyPair;
import it.ubiss.mpay.widget.utils.AndroidUtils;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.Random;

import nfc.payment.engine.NFCPaymentEngine;
import nfc.payment.engine.settings.NFCManager;
import nfc.payment.engine.settings.PropertiesManager;
import android.app.PendingIntent;
import android.app.Service;
import android.appwidget.AppWidgetManager;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.res.Resources.NotFoundException;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.os.IBinder;
import android.util.Log;
import android.view.View;
import android.widget.RemoteViews;

public class ActionDispacherService extends Service {

	private DateFormat dateFormat;
	private DateFormat timeFormat;

	private static AndroidNFCPayment nfcPayment;
	private static CountDownTimer waitTimer = null;

	AndroidSecurity androidSecurity = null;

	/**
	 * The system calls this method when the service is first created, to perform one-time setup
	 * procedures (before it calls either onStartCommand() or onBind()). If the service is already
	 * running, this method is not called.
	 */
	@Override
	public void onCreate() {

		super.onCreate();

		// Creates the instance for interaction with the library
		nfcPayment = new AndroidNFCPayment();

		dateFormat = new SimpleDateFormat(Constants.DATE_FORMAT, Constants.LOCALE);
		timeFormat = new SimpleDateFormat(Constants.TIME_FORMAT, Constants.LOCALE);

		AndroidUtils.log(Log.VERBOSE, "The ActionDispacherService for the widget is created");
	}

	@Override
	protected void finalize() throws Throwable {

		super.finalize();

		AndroidUtils.log(Log.VERBOSE, "The ActionDispacherService for the widget is finalized.");
	}

	@Override
	public void onLowMemory() {

		super.onLowMemory();

		AndroidUtils.log(Log.VERBOSE, "The ActionDispacherService for the widget is on Low Memory.");
	}

	@Override
	public void onStart(Intent intent, int startId) {

		super.onStart(intent, startId);

		AndroidUtils.log(Log.VERBOSE, "The ActionDispacherService for the widget is Started.");
	}

	/**
	 * The system calls this method when another component, such as an activity, requests that the
	 * service be started, by calling startService(). Once this method executes, the service is
	 * started and can run in the background indefinitely. If you implement this, it is your
	 * responsibility to stop the service when its work is done, by calling stopSelf() or
	 * stopService().
	 */
	@Override
	public int onStartCommand(Intent intent, int flags, int startId) {

		if (intent != null && intent.getExtras()!=null) {
			AndroidUtils.log(Log.VERBOSE, "The ActionDispacherService executes the start command with an action: " + intent.getExtras().getInt(Constants.ACTION_ID));

			
			try {
				// Executes the method that handles the screen or action management
				this.actionDispacher(intent.getExtras().getInt(Constants.ACTION_ID), intent.getExtras());
			} catch (Exception e) {
				AndroidUtils.log(Log.ERROR, "Unhandled exception retrieving the data from the extras: " + e.getMessage());
			}
		}

		return super.onStartCommand(intent, flags, startId);
	}

	/**
	 * Method that handles the actions to do in the widget apperance in order to perfom the system
	 * and the user actions.
	 * 
	 * @param actionId Action identifier
	 */
	private void actionDispacher(int actionId, Bundle extras) {

		Context ctx = WidgetProvider.getAppContext();

		RemoteViews views = null;
		Intent intent = null;
		PendingIntent pendingIntent = null;

		try {
			// Get the current widget by class name
			ComponentName thisWidget = new ComponentName(ctx, WidgetProvider.class);
			AppWidgetManager manager = AppWidgetManager.getInstance(ctx);

			switch (actionId) {

			/*
			 * *****************************************************************************************
			 * CASE 1: Load Start Screen. Load layout for the first screen of the widget. If the
			 * user clicks the widget, check if NFC is enabled
			 * **************************************
			 * ***************************************************
			 */
			case Constants.LOAD_START_SCREEN:

				AndroidUtils.log(Log.VERBOSE, "The Widget is loading the Start Screen");

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, false);

				views = new RemoteViews(ctx.getPackageName(), R.layout.ubiwidget_home);

				// Creates a Intent in order to Check the NFC status if the user taps the widget
				intent = new Intent(ctx, ActionDispacherService.class);
				intent.removeExtra(Constants.ACTION_ID);
				intent.putExtra(Constants.ACTION_ID, Constants.CHECK_NFC_ENABLED);

				pendingIntent = PendingIntent.getService(ctx, Constants.CHECK_NFC_ENABLED, intent, 0);
				//[SIAPTPE-148] new widget style changes views.setOnClickPendingIntent(R.id.home_layout1, pendingIntent);
				views.setOnClickPendingIntent(R.id.home_button, pendingIntent);

				// Create the view and update the widget
				manager.updateAppWidget(thisWidget, views);

				// Cancel all the payments in course
				cancelPaymentsAndEngines();

				// Cancel the simulator countdown
				if (ctx.getResources().getBoolean(R.bool.ubiwidget_debug_flag)) {
					if (ActionDispacherService.waitTimer != null) {
						ActionDispacherService.waitTimer.cancel();
						ActionDispacherService.waitTimer = null;
					}
				}

				break;
			/*
			 * *****************************************************************************************
			 * CASE 2: Check if NFC service is enabled. - If NFC not enabled, shows the error page
			 * in order to start NFC Settings - If it is enabled, check the wallet application is
			 * installed
			 * ****************************************************************************
			 * *************
			 */
			case Constants.CHECK_NFC_ENABLED:

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);
				
				// New CHECK (SIAPTPE-159): Check if the NFC Payments for the widget are enabled
				// from the settings page of the app
				int value = this.checkWidgetEnabled();
				
				// (fnva: 14/08/2014): Enabled property simulation
				if (ctx.getResources().getBoolean(R.bool.ubiwidget_debug_flag) && ctx.getResources().getBoolean(R.bool.ubiwidget_debug_enabled)) {
					value = Constants.WIDGET_PAYMENTS_ENABLED;
				}

				if (value == Constants.WIDGET_PAYMENTS_DISABLED || value == Constants.WIDGET_PAYMENTS_NOT_FOUND) {
					
					showEngineErrorPage(ctx, thisWidget, manager, Constants.LOAD_START_SCREEN, R.string.ubiwidget_btn_annulla, R.string.ubiwidget_error_settings_title,
							R.string.ubiwidget_K_NFC_PAYSTS, R.drawable.ubiwidget_button_gray);

				} else {

					if (NFCManager.isNFCEnabled(ctx)) {
	
						// NFC enabled
	
						AndroidUtils.log(Log.VERBOSE, "NFC enabled, next step (wallet and rooted)...");
	
						this.actionDispacher(Constants.CHECK_WALLET_AND_ROOTED_APP, extras);
	
					} else {
	
						// NFC disabled (show error page)
	
						AndroidUtils.log(Log.WARN, "NFC disabled, showing the settings page of the phone");
	
						// Cancel all the payments in course
						cancelPaymentsAndEngines();
	
						showNFCErrorPage(ctx, thisWidget, manager, Constants.LOAD_START_SCREEN, Constants.LOAD_NFC_SETTINGS, R.string.ubiwidget_btn_annulla, R.string.ubiwidget_btn_enable_nfc,
								R.string.ubiwidget_error_settings_title, R.string.ubiwidget_K_NFCNOTSUP);
	
					}
				
				}

				break;

			/*
			 * *****************************************************************************************
			 * CASE 3: Opens the NFC Settings to turn on the NFC service
			 * ****************************
			 * *************************************************************
			 */
			case Constants.LOAD_NFC_SETTINGS:

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);

				AndroidUtils.log(Log.VERBOSE, "Showing the NFC phone settings page.");

				// Opens the NFC Settings page on the device
				nfcPayment.StartNFCSettings();

				break;

			/*
			 * *****************************************************************************************
			 * CASE 4: Check if the Wallet Application is installed. - YES: Start the NFC payment
			 * engine - NO: Show error page with the custom error.
			 * **********************************
			 * *******************************************************
			 */
			case Constants.CHECK_WALLET_AND_ROOTED_APP:

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);
				
				// New CHECK (SIAPTPE-159): Check if the NFC Payments for the widget are enabled
				// from the settings page of the app
				int value2 = this.checkWidgetEnabled();
				
				// (fnva: 14/08/2014): Enabled property simulation
				if (ctx.getResources().getBoolean(R.bool.ubiwidget_debug_flag) && ctx.getResources().getBoolean(R.bool.ubiwidget_debug_enabled)) {
					value2 = Constants.WIDGET_PAYMENTS_ENABLED;
				}

				if (value2 == Constants.WIDGET_PAYMENTS_DISABLED || value2 == Constants.WIDGET_PAYMENTS_NOT_FOUND) {
					
					showEngineErrorPage(ctx, thisWidget, manager, Constants.LOAD_START_SCREEN, R.string.ubiwidget_btn_annulla, R.string.ubiwidget_error_settings_title,
							R.string.ubiwidget_K_NFC_PAYSTS, R.drawable.ubiwidget_button_gray);

				} else {

					if (nfcPayment.IsWalletAppInstalled(getResources().getString(R.string.ubiwidget_wallet_app_pckg))) {
	
						// Application Installed
	
						AndroidUtils.log(Log.VERBOSE, "The wallet applications is intalled correctly (" + getResources().getString(R.string.ubiwidget_wallet_app_pckg) + ")");
	
						AndroidSecurity androidSecurity = new AndroidSecurity();
						if (!androidSecurity.IsDeviceModified()) {
	
							AndroidUtils.log(Log.VERBOSE, "The phone is not rooted, allowed");
	
							// Phone not rooted
	
							// The listener of the call is defined in the WidgetProvider Listener
							startNFCPaymentEngine();
	
						} else {
							
							// (fnva: 14/08/2014): Rooted phone simulator
							if (ctx.getResources().getBoolean(R.bool.ubiwidget_debug_flag)) {
								
								if(ctx.getResources().getBoolean(R.bool.ubiwidget_debug_rooted)){
									// debug and rooted
									AndroidUtils.log(Log.ERROR, "The phone is rooted, this is not allowed");
									showEngineErrorPage(ctx, thisWidget, manager, Constants.LOAD_START_SCREEN, R.string.ubiwidget_btn_annulla, R.string.ubiwidget_error_settings_title,
											R.string.ubiwidget_K_NFCROOTPRIV, R.drawable.ubiwidget_button_gray);
								}else{
									// debug and unrooted
									AndroidUtils.log(Log.VERBOSE, "The phone is not rooted, allowed");
									startNFCPaymentEngine();
								}
							} else{
	
							// Phone rooted
	
								AndroidUtils.log(Log.ERROR, "The phone is rooted, this is not allowed");
		
								/*
								 * ferran.vila (07/07/2014) Eliminamos el botón de riprova. Petición mail
								 * Anna
								 */
								/*
								 * showEngineErrorPage(ctx, thisWidget, manager,
								 * Constants.LOAD_START_SCREEN, Constants.CHECK_NFC_ENABLED,
								 * R.string.ubiwidget_btn_annulla, R.string.ubiwidget_btn_riprova,
								 * R.string.ubiwidget_error_settings_title,
								 * R.string.ubiwidget_K_NFCROOTPRIV);
								 */
								showEngineErrorPage(ctx, thisWidget, manager, Constants.LOAD_START_SCREEN, R.string.ubiwidget_btn_annulla, R.string.ubiwidget_error_settings_title,
										R.string.ubiwidget_K_NFCROOTPRIV, R.drawable.ubiwidget_button_gray);
								
							}
						}
	
					} else {
	
						AndroidUtils.log(Log.ERROR, "The wallet application is not installed. Package name for the wallet (" + getResources().getString(R.string.ubiwidget_wallet_app_pckg) + ")");
	
						// Aplication not installed
	
						/* ferran.vila (07/07/2014) Eliminamos el botón de riprova. Petición mail Anna */
						/*
						 * showEngineErrorPage(ctx, thisWidget, manager, Constants.LOAD_START_SCREEN,
						 * Constants.CHECK_NFC_ENABLED, R.string.ubiwidget_btn_annulla,
						 * R.string.ubiwidget_btn_riprova, R.string.ubiwidget_error_settings_title,
						 * R.string.ubiwidget_K_NFCWALTELCO);
						 */
						showEngineErrorPage(ctx, thisWidget, manager, Constants.LOAD_START_SCREEN, R.string.ubiwidget_btn_annulla, R.string.ubiwidget_error_settings_title,
								R.string.ubiwidget_K_NFCWALTELCO, R.drawable.ubiwidget_button_gray);
					}
				}

				break;

			/*
			 * *****************************************************************************************
			 * CASE 5: Show Error Payment screen with some message and some actions
			 * *****************
			 * ************************************************************************
			 */

			case Constants.LOAD_ERROR_ENGINE:

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);

				/* ferran.vila (07/07/2014) Eliminamos el botón de riprova. Petición mail Anna */
				/*
				 * showEngineErrorPage(ctx, thisWidget, manager,
				 * extras.getInt(Constants.LEFT_ACTION_BUTTON),
				 * extras.getInt(Constants.RIGHT_ACTION_BUTTON),
				 * extras.getInt(Constants.LEFT_TEXT_BUTTON),
				 * extras.getInt(Constants.RIGHT_TEXT_BUTTON), extras.getInt(Constants.ERROR_TITLE),
				 * extras.getInt(Constants.ERROR_DETAIL));
				 */
				showEngineErrorPage(ctx, thisWidget, manager, extras.getInt(Constants.LEFT_ACTION_BUTTON), extras.getInt(Constants.LEFT_TEXT_BUTTON), extras.getInt(Constants.ERROR_TITLE),
						extras.getInt(Constants.ERROR_DETAIL), R.drawable.ubiwidget_button_gray);
				break;

			/*
			 * *****************************************************************************************
			 * CASE 6: Restart (stop-start) the NFC Engine
			 * ******************************************
			 * ***********************************************
			 */
			case Constants.RESTART_ENGINE:

				AndroidUtils.log(Log.VERBOSE, "Restarting the NFC engine...");

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);

				nfcPayment.StopNFCPaymentEngine();
				startNFCPaymentEngine();

				// The listener of the previous call is defined in the WidgetProvider Listener

				break;

			/*
			 * *****************************************************************************************
			 * CASE 7: Load the carta screen. It has to be a call to the library in order to set de
			 * numero di carta. ANULLA: Go to Home, PAGA goes to StartPayment
			 * ***********************
			 * ******************************************************************
			 */
			case Constants.LOAD_CARTA_SCREEN:

				AndroidUtils.log(Log.VERBOSE, "Showing the Carta page in order to make a payment...");

				// Before making a payment check the COUNTDOWN in the SecurityKeyChain
				updateValuesKeyChain();

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);

				views = new RemoteViews(ctx.getPackageName(), R.layout.ubiwidget_carta);

				// NUMERO DI CARTA
				views.setTextViewText(R.id.numero_di_carta, nfcPayment.GetPrimaryAccountNumber());

				// if debug, we set a random numero di carta
				if (ctx.getResources().getBoolean(R.bool.ubiwidget_debug_numero_di_carta_flag) && ctx.getResources().getBoolean(R.bool.ubiwidget_debug_flag)) {
					views.setTextViewText(R.id.numero_di_carta, "**** **** **** " + String.format("%04d", new Random().nextInt(9999)));
				}

				// HOME PAGE BUTTON (ANULLA)
				// Creates a Intent in order to go to the home page
				intent = new Intent(ctx, ActionDispacherService.class);
				intent.removeExtra(Constants.ACTION_ID);
				intent.putExtra(Constants.ACTION_ID, Constants.LOAD_START_SCREEN);

				pendingIntent = PendingIntent.getService(ctx, Constants.LOAD_START_SCREEN, intent, 0);
				views.setOnClickPendingIntent(R.id.carta_button_left, pendingIntent);

				// PAGA BUTTON
				// Creates a Intent in order to go to start the paymen
				intent = new Intent(ctx, ActionDispacherService.class);
				intent.removeExtra(Constants.ACTION_ID);
				intent.putExtra(Constants.ACTION_ID, Constants.START_PAYMENT);

				pendingIntent = PendingIntent.getService(ctx, Constants.START_PAYMENT, intent, 0);
				views.setOnClickPendingIntent(R.id.carta_button_right, pendingIntent);

				// Create the view and update the widget
				manager.updateAppWidget(thisWidget, views);

				break;

			/*
			 * *****************************************************************************************
			 * CASE 8: Method that starts the payment the listeners of this method are defined in
			 * the WidgetProvider
			 * *******************************************************************
			 * **********************
			 */
			case Constants.START_PAYMENT:

				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);

				// Payment simulation
				if (ctx.getResources().getBoolean(R.bool.ubiwidget_debug_nfc_payment_flag) && ctx.getResources().getBoolean(R.bool.ubiwidget_debug_flag)) {

					if (ctx.getResources().getBoolean(R.bool.ubiwidget_debug_nfc_payment_result_value)) {

						nfcPayment.onPaymentStarted();

						// Simulator: countdown

						final int secondsToFirePayment = PropertiesManager.getInstance().getTimerDurationInSec() - (new Random().nextInt(5) + 5);

						ActionDispacherService.waitTimer = new CountDownTimer(PropertiesManager.getInstance().getTimerDurationInSec() * 1000, 1000) {

							private boolean active = true;

							// Every second updates the screen
							public void onTick(long millisUntilFinished) {

								if (active) {

									int remainingSeconds = (int) (millisUntilFinished / 1000);
									nfcPayment.onUpdateCountDown(remainingSeconds);

									// If the flag for countdown is set to false and the remaining
									// seconds
									// are the same of the seconds for firing the event, throw a
									// listener
									if (remainingSeconds == secondsToFirePayment && !WidgetProvider.getAppContext().getResources().getBoolean(R.bool.ubiwidget_debug_nfc_payment_countdown_flag)) {

										nfcPayment.onPaymentSuccess(String.format("%.2f", 20 * new Random().nextDouble()), dateFormat.format(new Date()),
												timeFormat.format(new Date()));

										// Ends the timer
										active = false;
									}
								}
							}

							// When the timer ends, fires the event of onCountdownfinish
							public void onFinish() {

								if (active) {
									nfcPayment.onCountDownFinished();
								}
							}
						}.start();

					} else {
						// Payment simulation: incorrect
						nfcPayment.onPaymentFailed(ctx.getResources().getInteger(R.integer.ubiwidget_debug_nfc_payment_result_value) * (-1));
					}

				} else {

					AndroidUtils.log(Log.VERBOSE, "Starting the payment...");

					// Starts the NFC payment in the library
					nfcPayment.StartNFCPayment();
				}

				break;

			/*
			 * *****************************************************************************************
			 * CASE 9: Method that shows the waiting screen in order to do the payment
			 * **************
			 * ***************************************************************************
			 */
			case Constants.LOAD_WAITING_SCREEN:

				AndroidUtils.log(Log.VERBOSE, "Loading the waiting screen...");

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);

				views = new RemoteViews(ctx.getPackageName(), R.layout.ubiwidget_waiting);
				
				// SIAPTPE-165 (fnva:14/08/2014): Show the mastercard image
				views.setViewVisibility(R.id.waiting_masterCardImage_image, View.VISIBLE);
				

				// SIAPTPE-175 (esta:17/09/2014)
				// FAIL ICON
				views.setViewVisibility(R.id.imageView2, View.INVISIBLE);
				// SIAPTPE-175 (esta:17/09/2014)
				// NUMERO DI CARTA
				views.setTextViewText(R.id.numero_di_carta, nfcPayment.GetPrimaryAccountNumber());
				// SIAPTPE-175 (esta:17/09/2014)
				// if debug, we set a random numero di carta
				if (ctx.getResources().getBoolean(R.bool.ubiwidget_debug_numero_di_carta_flag) && ctx.getResources().getBoolean(R.bool.ubiwidget_debug_flag)) {
					views.setTextViewText(R.id.numero_di_carta, "**** **** **** " + String.format("%04d", new Random().nextInt(9999)));
				}

				// Anulla Button functionality (shows the error page)
				intent = new Intent(ctx, ActionDispacherService.class);

				intent.putExtra(Constants.ACTION_ID, Constants.LOAD_ERROR_PAYMENT);
				intent.putExtra(Constants.LEFT_ACTION_BUTTON, Constants.CHECK_NFC_ENABLED);
				intent.putExtra(Constants.RIGHT_ACTION_BUTTON, Constants.LOAD_START_SCREEN);
				intent.putExtra(Constants.LEFT_TEXT_BUTTON, R.string.ubiwidget_btn_nuovo);
				intent.putExtra(Constants.RIGHT_TEXT_BUTTON, R.string.ubiwidget_btn_home);
				intent.putExtra(Constants.ERROR_DETAIL, R.string.ubiwidget_cancel_payment);

				PendingIntent pendingIntent1 = PendingIntent.getService(ctx, Constants.LOAD_ERROR_PAYMENT, intent, 0);
				views.setOnClickPendingIntent(R.id.button1, pendingIntent1);
				
				// [SIAPTPE-145]  Put default waiting text again
				views.setTextViewText(R.id.textView2, ctx.getResources().getString(R.string.ubiwidget_scn3_text1));

				// Create the view and update the widget
				manager.updateAppWidget(thisWidget, views);

				break;

			/*
			 * *****************************************************************************************
			 * CASE 10: Show Error Payment screen with some message and some actions
			 * ****************
			 * *************************************************************************
			 */

			case Constants.LOAD_ERROR_PAYMENT:

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);

				showPaymentErrorPage(ctx, thisWidget, manager, extras.getInt(Constants.LEFT_ACTION_BUTTON), extras.getInt(Constants.RIGHT_ACTION_BUTTON), extras.getInt(Constants.LEFT_TEXT_BUTTON),
						extras.getInt(Constants.RIGHT_TEXT_BUTTON), extras.getInt(Constants.ERROR_DETAIL));

				// Cancel all the payments in course
				cancelPaymentsAndEngines();

				// Cancel the simulator countdown
				if (ctx.getResources().getBoolean(R.bool.ubiwidget_debug_flag)) {
					if (ActionDispacherService.waitTimer != null) {
						ActionDispacherService.waitTimer.cancel();
						ActionDispacherService.waitTimer = null;
					}
				}

				break;

			/*
			 * *****************************************************************************************
			 * CASE 11: Method that shows the waiting screen and updates the countdown timer
			 * ********
			 * *********************************************************************************
			 */
			case Constants.UPDATE_WAITING_COUNTDOWN_SCREEN:

				AndroidUtils.log(Log.VERBOSE, "Updating the waiting screen...");

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);

				views = new RemoteViews(ctx.getPackageName(), R.layout.ubiwidget_waiting);
				views.setTextViewText(R.id.textView1, extras.getString("seconds"));
				

				// Anulla Button functionality (shows the home page and annulla all the payments)
				views.setTextViewText(R.id.button1, ctx.getResources().getString(R.string.ubiwidget_btn_annulla));
				intent = new Intent(ctx, ActionDispacherService.class);

				intent.putExtra(Constants.ACTION_ID, Constants.LOAD_ERROR_PAYMENT);
				intent.putExtra(Constants.LEFT_ACTION_BUTTON, Constants.CHECK_NFC_ENABLED);
				intent.putExtra(Constants.RIGHT_ACTION_BUTTON, Constants.LOAD_START_SCREEN);
				intent.putExtra(Constants.LEFT_TEXT_BUTTON, R.string.ubiwidget_btn_nuovo);
				intent.putExtra(Constants.RIGHT_TEXT_BUTTON, R.string.ubiwidget_btn_home);
				intent.putExtra(Constants.ERROR_DETAIL, ctx.getResources().getString(R.string.ubiwidget_cancel_payment));

				PendingIntent pendingIntent3 = PendingIntent.getService(ctx, Constants.LOAD_ERROR_PAYMENT, intent, 0);
				views.setOnClickPendingIntent(R.id.button1, pendingIntent3);

				// Create the view and update the widget
				manager.updateAppWidget(thisWidget, views);

				break;

			/*
			 * *****************************************************************************************
			 * CASE 12: Method that shows the waiting screen and updates the screen when the
			 * oncountdownfinish event is launched
			 * **************************************************
			 * ***************************************
			 */
			case Constants.FINISH_WAITING_COUNTDOWN_SCREEN:

				AndroidUtils.log(Log.VERBOSE, "The countdown is finished. Showing the 0 seconds and the riprova button.");

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);

				views = new RemoteViews(ctx.getPackageName(), R.layout.ubiwidget_waiting);
				views.setTextViewText(R.id.textView1, "0");
				
				// SIAPTPE-165 (fnva:14/08/2014): Hide the mastercard image
				//views.setViewVisibility(R.id.waiting_masterCardImage_image, View.INVISIBLE);
				// SIAPTPE-175 (esta:17/09/2014): Show the Fail icon
				// FAIL ICON
				views.setViewVisibility(R.id.imageView2, View.VISIBLE);
								
				// Anulla Button functionality (shows the home page and annulla all the payments)
				intent = new Intent(ctx, ActionDispacherService.class);
				intent.removeExtra(Constants.ACTION_ID);
				intent.putExtra(Constants.ACTION_ID, Constants.START_PAYMENT);
				views.setOnClickPendingIntent(R.id.button1, PendingIntent.getService(ctx, Constants.START_PAYMENT, intent, 0));
				views.setTextViewText(R.id.button1, ctx.getResources().getString(R.string.ubiwidget_btn_riprova));

				// Change the text for the reason
				views.setTextViewText(R.id.textView2, ctx.getResources().getString(R.string.ubiwidget_scn3_text2));

				// Create the view and update the widget
				manager.updateAppWidget(thisWidget, views);

				break;

			/*
			 * *****************************************************************************************
			 * CASE 13: Method that shows the success screen and shows the amount and the time of
			 * the operation
			 * ************************************************************************
			 * *****************
			 */
			case Constants.LOAD_SUCCESS_PAYMENT:

				AndroidUtils.log(Log.VERBOSE, "Showing the success payment. Setting the values of the payment on the screen.");

				// Set the widget status
				AndroidUtils.setBooleanSharedPreferences(Constants.WIDGET_PAYMENT_STATUS, true);

				views = new RemoteViews(ctx.getPackageName(), R.layout.ubiwidget_payment_ok);

				// NUMERO DI CARTA
				views.setTextViewText(R.id.payment_text6, nfcPayment.GetPrimaryAccountNumber());

				// if debug, we set a random numero di carta
				if (ctx.getResources().getBoolean(R.bool.ubiwidget_debug_numero_di_carta_flag) && ctx.getResources().getBoolean(R.bool.ubiwidget_debug_flag)) {
					views.setTextViewText(R.id.payment_text6, "**** **** **** " + String.format("%04d", new Random().nextInt(9999)));
				}

				// AMOUNT
				views.setTextViewText(R.id.payment_text2, extras.getString("amount") + " " + ctx.getResources().getString(R.string.ubiwidget_euro));

				// DATA
				String dateformated = extras.getString("date") + " " + ctx.getString(R.string.ubiwidget_scn4_text5) + " " + extras.getString("time");
				views.setTextViewText(R.id.payment_text4, dateformated);

				// NUOVO PAGAMENTO
				// Creates a Intent in order to go to the carta page
				intent = new Intent(ctx, ActionDispacherService.class);
				intent.removeExtra(Constants.ACTION_ID);
				intent.putExtra(Constants.ACTION_ID, Constants.CHECK_NFC_ENABLED);

				pendingIntent = PendingIntent.getService(ctx, Constants.CHECK_NFC_ENABLED, intent, 0);
				views.setOnClickPendingIntent(R.id.payment_button_left, pendingIntent);

				// HOME BUTTON
				// Creates a Intent in order to go to the home screen
				intent = new Intent(ctx, ActionDispacherService.class);
				intent.removeExtra(Constants.ACTION_ID);
				intent.putExtra(Constants.ACTION_ID, Constants.LOAD_START_SCREEN);

				pendingIntent = PendingIntent.getService(ctx, Constants.LOAD_START_SCREEN, intent, 0);
				views.setOnClickPendingIntent(R.id.payment_button_right, pendingIntent);

				// Create the view and update the widget
				manager.updateAppWidget(thisWidget, views);

				break;

			/*
			 * *****************************************************************************************
			 * DEFAULT CASE: Error...
			 * ***************************************************************
			 * **************************
			 */
			default:

				AndroidUtils.log(Log.ERROR, "Action NOT FOUND");

				break;
			}
		} catch (NotFoundException e) {
			AndroidUtils.log(Log.ERROR, "Unhandled exception handling the widget action(" + actionId + ") with this message: " + e.getMessage());
		}
	}
	
	private void startNFCPaymentEngine() {
		updateValuesKeyChain();  // AID needs to be settled as per user data (institution code stored in the login/keychain) before starting payment engine (SIAPTPE-180)
		nfcPayment.StartNFCPaymentEngine();
	}

	/**
	 * Function that shows a screen on the widget with a custom error and two buttons with different
	 * functionalities
	 * 
	 * @param ctx Android context
	 * @param leftActionButton Action for left Button (ActionDispacherService)
	 * @param rightActionButton Action for right Button (ActionDispacherService)
	 * @param leftTextButton Text for left Button (strings.xml)
	 * @param rightTextButton Text for right Button (strings.xml)
	 * @param errorTitle Text for Error title (strings.xml)
	 * @param errorDetail Text for Error detail (strings.xml)
	 * 
	 *            ferran.vila (07/07/2014) Eliminamos el botón de riprova. Petición mail Anna
	 */
	public void showEngineErrorPage(Context ctx, ComponentName widget, AppWidgetManager manager, int leftActionButton, /*
																														 * int
																														 * rightActionButton
																														 * ,
																														 */int leftTextButton, /*
																																				 * int
																																				 * rightTextButton
																																				 * ,
																																				 */int errorTitle, int errorDetail, int buttonColor) {

		RemoteViews views = new RemoteViews(ctx.getPackageName(), R.layout.ubiwidget_error_engine);
		Intent intent1 = new Intent(ctx, ActionDispacherService.class);
		/* ferran.vila (07/07/2014) Eliminamos el botón de riprova. Petición mail Anna */
		// Intent intent2 = new Intent(ctx, ActionDispacherService.class);

		// Left Button functionality
		intent1.removeExtra(Constants.ACTION_ID);
		intent1.putExtra(Constants.ACTION_ID, leftActionButton);
		PendingIntent pendingIntent1 = PendingIntent.getService(ctx, leftActionButton, intent1, 0);
		views.setOnClickPendingIntent(R.id.error_settings_button_left, pendingIntent1);

		/* ferran.vila (07/07/2014) Eliminamos el botón de riprova. Petición mail Anna */
		// Right Button functionality
		// intent2.removeExtra(Constants.ACTION_ID);
		// intent2.putExtra(Constants.ACTION_ID, rightActionButton);
		// PendingIntent pendingIntent2 = PendingIntent.getService(ctx, rightActionButton, intent2,
		// 0);
		// views.setOnClickPendingIntent(R.id.error_settings_button_right, pendingIntent2);

		// Set the buttons text
		views.setTextViewText(R.id.error_settings_button_left, getResources().getText(leftTextButton));
		views.setInt(R.id.error_settings_button_left, "setBackgroundResource", buttonColor);
		/* ferran.vila (07/07/2014) Eliminamos el botón de riprova. Petición mail Anna */
		// views.setTextViewText(R.id.error_settings_button_right,
		// getResources().getText(rightTextButton));

		// Change the error text
		views.setTextViewText(R.id.error_settings_text_error, getResources().getText(errorTitle));
		views.setTextViewText(R.id.error_settings_text_error_reason, getResources().getText(errorDetail));

		manager.updateAppWidget(widget, views);
	}

	public void showNFCErrorPage(Context ctx, ComponentName widget, AppWidgetManager manager, int leftActionButton, int rightActionButton, int leftTextButton, int rightTextButton, int errorTitle,
			int errorDetail) {

		RemoteViews views = new RemoteViews(ctx.getPackageName(), R.layout.ubiwidget_error_nfc);
		Intent intent1 = new Intent(ctx, ActionDispacherService.class);
		Intent intent2 = new Intent(ctx, ActionDispacherService.class);

		// Left Button functionality
		intent1.removeExtra(Constants.ACTION_ID);
		intent1.putExtra(Constants.ACTION_ID, leftActionButton);
		PendingIntent pendingIntent1 = PendingIntent.getService(ctx, leftActionButton, intent1, 0);
		views.setOnClickPendingIntent(R.id.error_nfc_button_left, pendingIntent1);

		// Right Button functionality
		intent2.removeExtra(Constants.ACTION_ID);
		intent2.putExtra(Constants.ACTION_ID, rightActionButton);
		PendingIntent pendingIntent2 = PendingIntent.getService(ctx, rightActionButton, intent2, 0);
		views.setOnClickPendingIntent(R.id.error_nfc_button_right, pendingIntent2);

		// Set the buttons text
		views.setTextViewText(R.id.error_nfc_button_left, getResources().getText(leftTextButton));
		views.setTextViewText(R.id.error_nfc_button_right, getResources().getText(rightTextButton));

		// Change the error text
		views.setTextViewText(R.id.error_nfc_text_error, getResources().getText(errorTitle));
		views.setTextViewText(R.id.error_nfc_text_error_reason, getResources().getText(errorDetail));

		manager.updateAppWidget(widget, views);
	}

	/**
	 * Function that shows a screen on the widget with a custom error and two buttons with different
	 * functionalities
	 * 
	 * @param ctx Android context
	 * @param leftActionButton Action for left Button (ActionDispacherService)
	 * @param rightActionButton Action for right Button (ActionDispacherService)
	 * @param leftTextButton Text for left Button (strings.xml)
	 * @param rightTextButton Text for right Button (strings.xml)
	 * @param errorDetail Text for Error detail (strings.xml)
	 */
	public void showPaymentErrorPage(Context ctx, ComponentName widget, AppWidgetManager manager, int leftActionButton, int rightActionButton, int leftTextButton, int rightTextButton, int errorDetail) {

		RemoteViews views = new RemoteViews(ctx.getPackageName(), R.layout.ubiwidget_error_payment);
		Intent intent1 = new Intent(ctx, ActionDispacherService.class);
		Intent intent2 = new Intent(ctx, ActionDispacherService.class);

		// Left Button functionality
		intent1.removeExtra(Constants.ACTION_ID);
		intent1.putExtra(Constants.ACTION_ID, leftActionButton);
		PendingIntent pendingIntent1 = PendingIntent.getService(ctx, leftActionButton, intent1, 0);
		views.setOnClickPendingIntent(R.id.error_payment_button_left, pendingIntent1);

		// Right Button functionality
		intent2.removeExtra(Constants.ACTION_ID);
		intent2.putExtra(Constants.ACTION_ID, rightActionButton);
		PendingIntent pendingIntent2 = PendingIntent.getService(ctx, rightActionButton, intent2, 0);
		views.setOnClickPendingIntent(R.id.error_payment_button_right, pendingIntent2);

		// Set the buttons text
		views.setTextViewText(R.id.error_payment_button_left, getResources().getText(leftTextButton));
		views.setTextViewText(R.id.error_payment_button_right, getResources().getText(rightTextButton));

		// Change the error text
		views.setTextViewText(R.id.error_payment_text_error_reason, getResources().getText(errorDetail));

		manager.updateAppWidget(widget, views);

		// Stops the payment engine initialitzation because there is an error
		nfcPayment.StopNFCPaymentEngine();
	}

	/**
	 * Method that cancells all payments in the current course
	 */
	public static void cancelPaymentsAndEngines() {

		AndroidUtils.log(Log.VERBOSE, "Cancelling the NFC payments and Engines...");

		try {
			// Cancel all the payments in course
			nfcPayment.CancelNFCPayment();
			NFCPaymentEngine.getInstance().unregisterPaymentListener(nfcPayment);
			nfcPayment.StopNFCPaymentEngine();
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "Unhandled exception stopping the payment and the engines: " + e.getMessage());
		}
	}

	/**
	 * The system calls this method when another component wants to bind with the service (such as
	 * to perform RPC), by calling bindService(). In your implementation of this method, you must
	 * provide an interface that clients use to communicate with the service, by returning an
	 * IBinder. You must always implement this method, but if you don't want to allow binding, then
	 * you should return null.
	 */
	@Override
	public IBinder onBind(Intent intent) {

		return null;
	}

	/**
	 * The system calls this method when the service is no longer used and is being destroyed. Your
	 * service should implement this to clean up any resources such as threads, registered
	 * listeners, receivers, etc. This is the last call the service receives.
	 */
	@Override
	public void onDestroy() {

		super.onDestroy();

		AndroidUtils.log(Log.VERBOSE, "The ActionDispacherService for the widget is destroyed.");
	}
	
	/**
	 * Function that checks if the payments in the widget are enabled throught the setings in the
	 * app. This CR (SIAPTPE-159) is introduced because the client could enable/disable the payments
	 * in the widget in runtime of the app.
	 * 
	 * @see https://jira-it.gft.com/browse/SIAPTPE-159
	 * 
	 * @return Integer with the result operation: 0: DISABLED; 1: ENABLED; 2: NOT FOUND
	 */
	private int checkWidgetEnabled() {

		int ret = Constants.WIDGET_PAYMENTS_NOT_FOUND;

		try {

			if (androidSecurity == null) {
				androidSecurity = new AndroidSecurity();
			}

			// Getting the property from the keychain...
			List<KeyPair> keys = androidSecurity.GetStoredKeyValuePair(Constants.WIDGET_PAYMENTS_VARIABLE);

			if (!keys.isEmpty()) {
				// Property founded in the keychain
				AndroidUtils.log(Log.DEBUG, "Founded " + Constants.WIDGET_PAYMENTS_VARIABLE + " on the SecurityKeyChain: " + keys.get(0).getValue());

				int value = Integer.parseInt(keys.get(0).getValue());

				if (value == 0) {

					// Widget payments disabled...
					AndroidUtils.log(Log.DEBUG, "The payments on the widget are DISABLED from the Setting Page of the app");
					ret = Constants.WIDGET_PAYMENTS_DISABLED;

				} else if (value == 1) {

					// Widget payments enabled...
					AndroidUtils.log(Log.DEBUG, "The payments on the widget are ENABLED from the Setting Page of the app");
					ret = Constants.WIDGET_PAYMENTS_ENABLED;

				} else {

					// Strange value in the keychain
					AndroidUtils.log(Log.WARN, Constants.WIDGET_PAYMENTS_VARIABLE + " founded in the SecurityKeyChain but with a strange value.");
					ret = Constants.WIDGET_PAYMENTS_NOT_FOUND;
				}
			} else {
				// Property not founded in the keychain
				AndroidUtils.log(Log.WARN, Constants.WIDGET_PAYMENTS_VARIABLE + " not founded in the SecurityKeyChain. Showing warning page in order to access the app and enable the widget");
				ret = Constants.WIDGET_PAYMENTS_NOT_FOUND;
			}

		} catch (Exception e) {

			ret = Constants.WIDGET_PAYMENTS_NOT_FOUND;
			AndroidUtils.log(Log.ERROR, "Unhandled exception consulting the Widget Enabled property in the Security Key Chain: " + e.getMessage());
		}

		return ret;
	}

	/**
	 * Function that updates the values on the NFC payment service with the values stored in the
	 * Security Key Chain
	 */
	private void updateValuesKeyChain() {

		/*
		 * fnva: Now we are going to override some properties modified for the application or stored
		 * in the security key chain
		 */

		try {

			if (androidSecurity == null) {
				androidSecurity = new AndroidSecurity();
			}

			// If there is stored a timeout in the securitykeychain on the phone, we
			// override
			// this property on the NFCEngine
			List<KeyPair> keys = androidSecurity.GetStoredKeyValuePair(Constants.UBI_NFC_COUNTDOWN);
			if (!keys.isEmpty()) {
				AndroidUtils.log(Log.DEBUG, "Founded " + Constants.UBI_NFC_COUNTDOWN + " on the SecurityKeyChain: " + keys.get(0).getValue());
				PropertiesManager.getInstance().setTimerDurationInSec(Integer.parseInt(keys.get(0).getValue()));
			} else {
				AndroidUtils.log(Log.WARN, Constants.UBI_NFC_COUNTDOWN + " not founded in the SecurityKeyChain. Setting into the NFC properties manager the value of the "
						+ Constants.NFC_PROPERTIES_NAME + " file.");
			}

			// Web set the AID of the user by getting the intitution code from the security
			// key
			// chain and then getting the aid from a static mapping
			keys = androidSecurity.GetStoredKeyValuePair(Constants.UBI_INSTITUTION_CODE);
			if (!keys.isEmpty()) {

				String uic = keys.get(0).getValue();
				AndroidUtils.log(Log.DEBUG, "Founded " + Constants.UBI_INSTITUTION_CODE + " on the SecurityKeyChain: " + uic);

				PropertiesManager.getInstance().setApplicationID(getAIDFromInstitutionCode(uic));

			} else {

				// store
				AndroidUtils.log(Log.WARN, "There is no " + Constants.UBI_INSTITUTION_CODE + " on the SecurityKeyChain, storing the default value from resources (attr.xml)");
				androidSecurity.StoreKeyValuePair(new KeyPair(Constants.UBI_INSTITUTION_CODE, WidgetProvider.getAppContext().getResources().getString(R.string.ubiwidget_store_aid_value)));

				// retrieve
				keys = androidSecurity.GetStoredKeyValuePair(Constants.UBI_INSTITUTION_CODE);
				String uic = keys.get(0).getValue();
				AndroidUtils.log(Log.DEBUG, "Founded " + Constants.UBI_INSTITUTION_CODE + " on the SecurityKeyChain: " + uic + ". Setting into the NFC properties manager");
				PropertiesManager.getInstance().setApplicationID(getAIDFromInstitutionCode(uic));

			}
		} catch (Exception e) {

			AndroidUtils.log(Log.ERROR, "Unhandled exception loading and setting the Security Key Chain: " + e.getMessage());
		}
	}

	/**
	 * Method that retireves the AID from a UBI intituzion code by the mapping stored in the
	 * aid_mapping.xml
	 * 
	 * @param icode UBI_INSTITUTION_CODE
	 * @return AID
	 */
	private String getAIDFromInstitutionCode(String icode) {

		String[] aid_mapping = WidgetProvider.getAppContext().getResources().getStringArray(R.array.ubiwidget_aid_mapping);

		// The format of the line is INSTITUTION_CODE|AID|USER_CODE|?
		for (String line : aid_mapping) {
			String[] elements = line.split("\\|");

			if (elements[0].equals(icode)) {

				AndroidUtils.log(Log.DEBUG, "AID from the mapping: " + elements[1]);

				return elements[1];
			}
		}

		return "";
	}

}
