/*
 Copyright (c) 2012 GFT Appverse, S.L., Sociedad Unipersonal.

 This Source  Code Form  is subject to the  terms of  the Appverse Public License 
 Version 2.0  ("APL v2.0").  If a copy of  the APL  was not  distributed with this 
 file, You can obtain one at http://appverse.org/legal/appverse-license/.

 Redistribution and use in  source and binary forms, with or without modification, 
 are permitted provided that the  conditions  of the  AppVerse Public License v2.0 
 are met.

 THIS SOFTWARE IS PROVIDED BY THE  COPYRIGHT HOLDERS  AND CONTRIBUTORS "AS IS" AND
 ANY EXPRESS  OR IMPLIED WARRANTIES, INCLUDING, BUT  NOT LIMITED TO,   THE IMPLIED
 WARRANTIES   OF  MERCHANTABILITY   AND   FITNESS   FOR A PARTICULAR  PURPOSE  ARE
 DISCLAIMED. EXCEPT IN CASE OF WILLFUL MISCONDUCT OR GROSS NEGLIGENCE, IN NO EVENT
 SHALL THE  COPYRIGHT OWNER  OR  CONTRIBUTORS  BE LIABLE FOR ANY DIRECT, INDIRECT,
 INCIDENTAL,  SPECIAL,   EXEMPLARY,  OR CONSEQUENTIAL DAMAGES  (INCLUDING, BUT NOT
 LIMITED TO,  PROCUREMENT OF SUBSTITUTE  GOODS OR SERVICES;  LOSS OF USE, DATA, OR
 PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT(INCLUDING NEGLIGENCE OR OTHERWISE) 
 ARISING  IN  ANY WAY OUT  OF THE USE  OF THIS  SOFTWARE,  EVEN  IF ADVISED OF THE 
 POSSIBILITY OF SUCH DAMAGE.
 */
package it.ubiss.mpay.widget.payment;

import it.ubiss.mpay.R;
import it.ubiss.mpay.widget.Constants;
import it.ubiss.mpay.widget.WidgetProvider;
import it.ubiss.mpay.widget.services.ActionDispacherService;
import it.ubiss.mpay.widget.utils.AndroidUtils;
import nfc.payment.engine.NFCPaymentEngine;
import nfc.payment.engine.exception.CardException;
import nfc.payment.engine.exception.CardletNotFoundException;
import nfc.payment.engine.exception.CardletSecurityException;
import nfc.payment.engine.listener.OnEngineStartListener;
import nfc.payment.engine.listener.OnPaymentListener;
import nfc.payment.engine.security.SecurityManager;
import nfc.payment.engine.security.exception.ADBEnabledException;
import nfc.payment.engine.security.exception.DeviceRootedException;
import nfc.payment.engine.security.exception.LockDisabledException;
import nfc.payment.engine.settings.NFCManager;
import nfc.payment.engine.settings.PropertiesManager;
import nfc.payment.engine.settings.Utils;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.Resources.NotFoundException;
import android.util.Log;

public class AndroidNFCPayment implements INFCPayment, OnEngineStartListener, OnPaymentListener{
	
	private NFCPaymentSecurityException exceptionRaised = null;

	public AndroidNFCPayment() {

		AndroidUtils.log(Log.DEBUG, "Loading NFC Payment Engine properties (via config file)");

		try {
			Context ctx = WidgetProvider.getAppContext();
			int resourceId = ctx.getResources().getIdentifier(Constants.NFC_PROPERTIES_NAME, Constants.NFC_PROPERTIES_RAW_TYPE, ctx.getPackageName());

			if (resourceId != 0) {
				AndroidUtils.log(Log.DEBUG, "Loading config file from: " + Constants.NFC_PROPERTIES_NAME);

				PropertiesManager.getInstance().setProperties(ctx, resourceId);

				

			} else {
				AndroidUtils.log(Log.ERROR, "Not found config file at: " + Constants.NFC_PROPERTIES_NAME);
			}
			
			performSecurityChecks(ctx);
			
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "Unhandled exception while loading nfc properties file. Exception message: " + e.getMessage());
		}
	}

	/**
	 * Checks the presence/installation of the Wallet app by the given "packageName".
	 * 
	 * @param packageName The package name of the application that needs to be checked
	 * @return true if installed on the current device, fale otherwise.
	 */
	@Override
	public boolean IsWalletAppInstalled(String packageName) {
		try {
			AndroidUtils.log(Log.DEBUG, "Checking if wallet app is installed by package name: " + packageName);

			/* Using NFC library implementation */
			Context ctx = WidgetProvider.getAppContext();
			return Utils.isAppInstalled(ctx, packageName);

		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "Unhandled exception while checking wallet app installed. Exception message: " + e.getMessage());
		}
		AndroidUtils.log(Log.DEBUG, "Not Found. Wallet app is not installed by package name: " + packageName);
		return false;
	}

	@Override
	public void SetNFCPaymentProperties(NFCPaymentProperty[] properties) {

		AndroidUtils.log(Log.DEBUG, "Loading NFC Payment Engine properties (per app demand)");

		if (properties != null) {
			try {

				for (NFCPaymentProperty property : properties) {

					String key = property.getKey();
					String value = property.getValue();
					NFCPaymentPropertyKey keyEnum = NFCPaymentPropertyKey.valueOf(key);

					switch (keyEnum) {
					case application_id:
						AndroidUtils.log(Log.DEBUG, "Setting NFC property - application_id");
						PropertiesManager.getInstance().setApplicationID(value);
						break;
					case application_label:
						AndroidUtils.log(Log.DEBUG, "Setting NFC property - application_label");
						PropertiesManager.getInstance().setApplicationLabel(value);
						break;
					case aid_name_length:
						AndroidUtils.log(Log.DEBUG, "Setting NFC property - aid_name_length");
						PropertiesManager.getInstance().setAIDNameLength(Integer.parseInt(value));
						break;
					case debug_mode:
						AndroidUtils.log(Log.DEBUG, "Setting NFC property - debug_mode");
						PropertiesManager.getInstance().setDebugMode(Boolean.parseBoolean(value));
						break;
					case vibration_duration_in_msec:
						AndroidUtils.log(Log.DEBUG, "Setting NFC property - vibration_duration_in_msec");
						PropertiesManager.getInstance().setVibrationDurationInMSec(Integer.parseInt(value));
						break;
					case timer_period_in_sec:
						AndroidUtils.log(Log.DEBUG, "Setting NFC property - timer_period_in_sec");
						PropertiesManager.getInstance().setTimerDurationInSec(Integer.parseInt(value));
						break;
					default:
						AndroidUtils.log(Log.DEBUG, "Error setting NFC property - not valid key name: " + key);
						break;
					}

				}

			} catch (Exception e) {
				AndroidUtils.log(Log.ERROR, "Unhandled exception while loading nfc properties on demand. Exception message: " + e.getMessage());
			}

		} else {
			AndroidUtils.log(Log.ERROR, "No properties bean object available");
		}

	}

	@Override
	public NFCPaymentSecurityException StartNFCPaymentEngine() {

		final Context ctx = WidgetProvider.getAppContext();
		
		String messageError = "Unhandled exception while starting payment engine";
		NFCPaymentSecurityException securityException = new NFCPaymentSecurityException();
		
		try {
			
			// double check USB debugging enabled (user could enable it during the app lifecycle)
			boolean isDebuggable = 0 != (ctx.getApplicationInfo().flags &= ApplicationInfo.FLAG_DEBUGGABLE);
			if(!isDebuggable && !PropertiesManager.getInstance().isDebugMode() && isUSBDebuggingEnabled(ctx))  {
				
				AndroidUtils.log(Log.DEBUG, "The ADB settings are started. User should disable the NFC Settings");
				//Launches the Settings section of the device in which the user can disable the USB debugging mode
				SecurityManager.startADBSettings(ctx);
				
				NFCPaymentSecurityException usbDebuggingEnableException = new NFCPaymentSecurityException();
				String messageErrorUSB = "NFC Security Checkings failed: device is in USB debugging mode";
				AndroidUtils.log(Log.DEBUG, messageErrorUSB);
				usbDebuggingEnableException.setType(NFCPaymentSecurityExceptionType.USBDebuggingEnabled);
				usbDebuggingEnableException.setMessage(messageErrorUSB);
				return usbDebuggingEnableException;
				
			} else {
				if(exceptionRaised!=null && exceptionRaised.getType() == NFCPaymentSecurityExceptionType.USBDebuggingEnabled)
					exceptionRaised = null; // ADB enabled already checked
			}
			
			// check stored result for initial perform security checks
			if(exceptionRaised==null) {

				AndroidUtils.log(Log.DEBUG, "The device is secure. The NFC Payment Engine is starting...");

				// Selects the CPMA and enables the engine to be able to make payments.
				NFCPaymentEngine.getInstance().start(ctx, this);

				securityException = null;
			} else {
				AndroidUtils.log(Log.DEBUG, "Exception raised on the intial cached performSecurityChecks. Exception type: " + exceptionRaised.getType());
				securityException = exceptionRaised;
			}

		} catch (Exception e) {
			messageError = "Unhandled exception while starting payment engine. Exception message: " + e.getMessage();
			AndroidUtils.log(Log.ERROR, messageError);
			securityException.setType(NFCPaymentSecurityExceptionType.Unhandled);
		}
		
		if(securityException!=null) 
			securityException.setMessage(messageError);
		
		return securityException;
	}
	
	public void performSecurityChecks(Context ctx) {
		exceptionRaised = new NFCPaymentSecurityException();
		String messageError = null;
		try {
			
			boolean isDebuggable = 0 != (ctx.getApplicationInfo().flags &= ApplicationInfo.FLAG_DEBUGGABLE);
			if(!isDebuggable) {
				AndroidUtils.log(Log.DEBUG, "Perfoming NFC Security Checkings...");
				AndroidUtils.log(Log.DEBUG, "NFC properties debug_mode = " + PropertiesManager.getInstance().isDebugMode());
				// Checks that the device does not have root privileges, 
				// is not protected by lock 
				// and is not in USB debugging mode
				SecurityManager.performSecurityChecks(ctx);
				
			} else {
				// 
				AndroidUtils.log(Log.DEBUG, "Application is in debug mode, we have removed the security checkings for TESTING.");
			}
			
			exceptionRaised = null;
			
		} catch (DeviceRootedException e) {
			messageError = "NFC Security Checkings failed: device is rooted";
			AndroidUtils.log(Log.DEBUG, messageError);
			exceptionRaised.setType(NFCPaymentSecurityExceptionType.DeviceRooted);
		} catch (LockDisabledException e) {
			messageError = "NFC Security Checkings failed: device is not protected by lock";
			AndroidUtils.log(Log.DEBUG, messageError);
			exceptionRaised.setType(NFCPaymentSecurityExceptionType.DeviceLockDisabled);
		} catch (ADBEnabledException e) {
			messageError = "NFC Security Checkings failed: device is in USB debugging mode";
			AndroidUtils.log(Log.DEBUG, messageError);
			exceptionRaised.setType(NFCPaymentSecurityExceptionType.USBDebuggingEnabled);
		} catch (Exception e) {
			messageError = "Unhandled exception while starting payment engine. Exception message: " + e.getMessage();
			AndroidUtils.log(Log.DEBUG, messageError);
			exceptionRaised.setType(NFCPaymentSecurityExceptionType.Unhandled);
		}
		
		if(exceptionRaised!=null) 
			exceptionRaised.setMessage(messageError);
		else
			AndroidUtils.log(Log.DEBUG, "NFC Security Checkings PASSED");
	}
	
	private boolean isUSBDebuggingEnabled(Context ctx) {
		
		AndroidUtils.log(Log.DEBUG, "Checking USB Debugging enabled...");
		if (android.provider.Settings.System.getInt(ctx.getContentResolver(), "adb_enabled", 0) == 1) {
			AndroidUtils.log(Log.DEBUG, "USB Debugging ENABLED");
			return true;
		}
		
		AndroidUtils.log(Log.DEBUG,"USB Debugging DISABLED");
		return false;
	}

	@Override
	public boolean StopNFCPaymentEngine() {
		try {
			AndroidUtils.log(Log.DEBUG, "Unregistering Payment Listener...");
			NFCPaymentEngine.getInstance().unregisterPaymentListener(this);

			AndroidUtils.log(Log.DEBUG, "The NFC Payment Engine is being stopped...");
			// Closes the channel of communication with the NFC SIM and ends on the engine.
			// It 'must be called after the engine start.
			NFCPaymentEngine.getInstance().stop();
			return true;
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "Unhandled exception while stopping payment engine. Exception message: " + e.getMessage());
		}
		return false;
	}

	@Override
	public String GetPrimaryAccountNumber() {
		try {

			AndroidUtils.log(Log.DEBUG, "Getting the Primary Account Number (PAN)...");
			// Returns the last 4 digits of the Primary Account Number (PAN) from the SIM
			// obfuscating the first 16
			String pan = NFCPaymentEngine.getInstance().getPAN();
			
			AndroidUtils.log(Log.DEBUG, "The Primary Account Number (PAN) obtained with the library: " + pan);
			
			return pan;

		} catch (CardException ce) {
			// The exception is raised when no SIM is not detected within the player or there are
			// communication problems with the SIM
			AndroidUtils.log(Log.ERROR, "CardException while getting PAN (Sim not detected or communication problems). " + "Exception message: " + ce.getMessage());
		} catch (CardletNotFoundException cnfe) {
			// The exception is raised when the CPMA is not found within the Secure Element.
			AndroidUtils.log(Log.ERROR, "CardletNotFoundException while getting PAN (CPMA is not found within the Secure Element). " + "Exception message: " + cnfe.getMessage());
		} catch (CardletSecurityException cse) {
			// The exception is raised when errors are detected on security regarding the CPMA
			// within the Secure Element..
			AndroidUtils.log(Log.ERROR, "CardletSecurityException while getting PAN (security errors regarding CPMA within the Secure Element). " + "Exception message: " + cse.getMessage());
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "Unhandled exception while getting PAN. Exception message: " + e.getMessage());
		}
		return null;
	}

	@Override
	public boolean IsNFCEnabled() {
		try {
			Context ctx = WidgetProvider.getAppContext();

			// Checks that the device has the NFC interface active
			return NFCManager.isNFCEnabled(ctx);
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "Unhandled exception while checking NFC enabled. Exception message: " + e.getMessage());
		}
		return false;
	}

	@Override
	public void StartNFCSettings() {
		try {
			Context ctx = WidgetProvider.getAppContext();

			// Launches the Settings section of the device in which the user can enable the NFC
			// interface
			NFCManager.startNFCSettings(ctx);
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "Unhandled exception while starting NFC settings. Exception message: " + e.getMessage());
		}
	}

	@Override
	public boolean StartNFCPayment() {
		Context ctx = WidgetProvider.getAppContext();
		boolean result = false;
		try {
			// Checks that the device has the NFC interface active
			if (NFCManager.isNFCEnabled(ctx)) {
				AndroidUtils.log(Log.DEBUG, "NFC is enabled. Starting NFC Payment...");

				try {

					// Activates an NFC payment with the Point Of Sale (POS)
					// NFCPaymentEngine.getInstance().startPayment();

					// ferran.vila: 01/07/2014 Add the context to the parameters because the new
					// method on the library
					NFCPaymentEngine.getInstance().startPayment(ctx);

				} catch (CardException ce) {
					// The exception is raised when no SIM is not detected within the player
					// or there are communication problems with the SIM
					AndroidUtils.log(Log.ERROR, "CardException while starting the NFC payment (Sim not detected or communication problems). " + "Exception message: " + ce.getMessage());
				} catch (CardletNotFoundException cnfe) {
					// The exception is raised when the CPMA is not found within the Secure
					// Element.
					AndroidUtils.log(Log.ERROR, "CardletNotFoundException while starting the NFC payment (CPMA is not found within the Secure Element). " + "Exception message: " + cnfe.getMessage());
				} catch (CardletSecurityException cse) {
					// The exception is raised when errors are detected on security
					// regarding the CPMA within the Secure Element..
					AndroidUtils.log(Log.ERROR,
							"CardletSecurityException while starting the NFC payment (security errors regarding CPMA within the Secure Element). " + "Exception message: " + cse.getMessage());
				} catch (Exception e) {
					AndroidUtils.log(Log.ERROR, "Unhandled exception while starting payment (runOnUiThread). Exception message: " + e.getMessage());
				}

				result = true;

			} else {
				AndroidUtils.log(Log.WARN, "NFC is NOT enabled. Starting NFC Settings...");
				// Launches the Settings section of the device in which the user can enable the NFC
				// interface
				NFCManager.startNFCSettings(ctx);
			}
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "Unhandled exception while starting the NFC payment. Exception message: " + e.getMessage());
		}

		return result;
	}

	@Override
	public void CancelNFCPayment() {

		try {
			// Disables any NFC payment and resets the timer.
			// If the transaction is already started at the POS side this method can not
			// finish the payment, but only reset the timer.
			//
			NFCPaymentEngine.getInstance().cancelPayment();
		} catch (CardException ce) {
			// The exception is raised when no SIM is not detected within the player or
			// there are communication problems with the SIM
			AndroidUtils.log(Log.ERROR, "CardException while canceling the NFC payment (Sim not detected or communication problems). " + "Exception message: " + ce.getMessage());

		} catch (CardletNotFoundException cnfe) {
			// The exception is raised when the CPMA is not found within the Secure Element.
			AndroidUtils.log(Log.ERROR, "CardletNotFoundException while canceling the NFC payment (CPMA is not found within the Secure Element). " + "Exception message: " + cnfe.getMessage());

		} catch (CardletSecurityException cse) {
			// The exception is raised when errors are detected on security regarding the
			// CPMA within the Secure Element..
			AndroidUtils.log(Log.ERROR,
					"CardletSecurityException while canceling the NFC payment (security errors regarding CPMA within the Secure Element). " + "Exception message: " + cse.getMessage());

		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "");
		}
	}
	
	
	/*
	 * *****************************************************************************************
	 * NFC Library Listeners: ENGINE
	 * *****************************************************************************************
	 */

	/*
	 * (non-Javadoc)
	 * @see nfc.payment.engine.listener.OnEngineStartListener#onEngineStartError(int)
	 */
	@Override
	public void onEngineStartError(int arg0) {

		Context ctx = WidgetProvider.getAppContext();

		if (!(ctx.getResources().getBoolean(R.bool.ubiwidget_debug_nfc_engine_flag) && ctx.getResources().getBoolean(R.bool.ubiwidget_debug_flag))) {

			AndroidUtils.log(Log.ERROR, "The NFC Engine Start method has produced an error: " + arg0);

			// Handle the error page with the engine start error
			try {
				onEngineStartErrorManager(arg0);
			} catch (Exception e) {
				AndroidUtils.log(Log.ERROR, "Error handling the engine start error: " + e.getMessage());
			}

		} else {

			// if emulation is enabled

			if (ctx.getResources().getBoolean(R.bool.ubiwidget_debug_nfc_engine_result_value)) {
				// Result Correct
				onEngineStartSuccess();
			} else {
				// Result incorrect
				onEngineStartErrorManager(ctx.getResources().getInteger(R.integer.ubiwidget_debug_nfc_engine_result_value) * (-1));
			}

		}
	}

	/**
	 * Method that handles the screen for the engine start error payment
	 * 
	 * @param errorCode Error code produced in the start engine
	 */
	private void onEngineStartErrorManager(int errorCode) {

		Context ctx = WidgetProvider.getAppContext();

		// If the engine is alredy started we should stop the engine and reopened
		if (errorCode == Constants.E_ENGINE_ALREADY_STARTED) {

			AndroidUtils.log(Log.WARN, "The NFC Engine Start method has produced an error because the engine is alredy started. Restarting the engine...");

			Intent service = new Intent(ctx, ActionDispacherService.class);
			service.putExtra(Constants.ACTION_ID, Constants.RESTART_ENGINE);
			ctx.startService(service);

			return;
		}

		// Creates an Intent to the service and pass all properties for the screen
		Intent service = new Intent(ctx, ActionDispacherService.class);

		service.putExtra(Constants.ACTION_ID, Constants.LOAD_ERROR_ENGINE);
		service.putExtra(Constants.LEFT_ACTION_BUTTON, Constants.LOAD_START_SCREEN);
		service.putExtra(Constants.RIGHT_ACTION_BUTTON, Constants.CHECK_NFC_ENABLED);
		service.putExtra(Constants.LEFT_TEXT_BUTTON, R.string.ubiwidget_btn_annulla);
		service.putExtra(Constants.RIGHT_TEXT_BUTTON, R.string.ubiwidget_btn_riprova);
		service.putExtra(Constants.ERROR_TITLE, AndroidUtils.getEngineStartErrorListenerMessage(errorCode)[0]);
		service.putExtra(Constants.ERROR_DETAIL, AndroidUtils.getEngineStartErrorListenerMessage(errorCode)[1]);

		ctx.startService(service);
	}

	/*
	 * (non-Javadoc)
	 * @see nfc.payment.engine.listener.OnEngineStartListener#onEngineStartSuccess()
	 */
	@Override
	public void onEngineStartSuccess() {

		Context ctx = WidgetProvider.getAppContext();

		// Register the listeners to the engine
		NFCPaymentEngine.getInstance().registerPaymentListener(this);

		AndroidUtils.log(Log.DEBUG, "The NFC Engine started correctly.");

		try {
			// If engine start correctly, show the paga button page
			Intent service = new Intent(ctx, ActionDispacherService.class);
			service.putExtra(Constants.ACTION_ID, Constants.LOAD_CARTA_SCREEN);
			ctx.startService(service);
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "There is being a problem starting the widget action dispacher service: " + e.getMessage());
		}
	}

	/*
	 * *****************************************************************************************
	 * NFC Library Listeners: COUNTDOWN
	 * *****************************************************************************************
	 */

	/*
	 * (non-Javadoc)
	 * @see nfc.payment.engine.listener.OnPaymentListener#onUpdateCountDown(int)
	 */
	@Override
	public void onUpdateCountDown(int arg0) {

		Context ctx = WidgetProvider.getAppContext();

		AndroidUtils.log(Log.VERBOSE, "Updating the NFC countdown... " + arg0 + " seconds.");

		try {
			// When the payment starts, we show the waiting screen
			Intent service = new Intent(ctx, ActionDispacherService.class);
			service.putExtra(Constants.ACTION_ID, Constants.UPDATE_WAITING_COUNTDOWN_SCREEN);
			service.putExtra("seconds", arg0 + "");
			ctx.startService(service);
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "There is being a problem starting the widget action dispacher service: " + e.getMessage());
		}
	}

	/*
	 * (non-Javadoc)
	 * @see nfc.payment.engine.listener.OnPaymentListener#onCountDownFinished()
	 */
	@Override
	public void onCountDownFinished() {

		Context ctx = WidgetProvider.getAppContext();

		AndroidUtils.log(Log.DEBUG, "The NFC countdown is finished without any payment result.");

		try {
			// When the payment starts, we show the waiting screen and update some variables
			Intent service = new Intent(ctx, ActionDispacherService.class);
			service.putExtra(Constants.ACTION_ID, Constants.FINISH_WAITING_COUNTDOWN_SCREEN);
			ctx.startService(service);
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "There is being a problem starting the widget action dispacher service: " + e.getMessage());
		}
	}

	/*
	 * *****************************************************************************************
	 * NFC Library Listeners: PAYMENT
	 * *****************************************************************************************
	 */

	/*
	 * (non-Javadoc)
	 * @see nfc.payment.engine.listener.OnPaymentListener#onPaymentStarted()
	 */
	@Override
	public void onPaymentStarted() {

		Context ctx = WidgetProvider.getAppContext();

		AndroidUtils.log(Log.DEBUG, "The NFC payment started correctly.");

		try {
			// When the payment starts, we show the waiting screen
			Intent service = new Intent(ctx, ActionDispacherService.class);
			service.putExtra(Constants.ACTION_ID, Constants.LOAD_WAITING_SCREEN);
			ctx.startService(service);
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "There is being a problem starting the widget action dispacher service: " + e.getMessage());
		}
	}

	/*
	 * (non-Javadoc)
	 * @see nfc.payment.engine.listener.OnPaymentListener#onPaymentCanceled()
	 */
	@Override
	public void onPaymentCanceled() {

		Context ctx = WidgetProvider.getAppContext();

		AndroidUtils.log(Log.ERROR, "The NFC Payment is being cancelled.");

		try {
			// Creates an Intent to the service and pass all properties for the screen
			Intent service = new Intent(ctx, ActionDispacherService.class);

			// Show the payment error page
			service.putExtra(Constants.ACTION_ID, Constants.LOAD_ERROR_PAYMENT);
			service.putExtra(Constants.LEFT_ACTION_BUTTON, Constants.CHECK_NFC_ENABLED);
			service.putExtra(Constants.RIGHT_ACTION_BUTTON, Constants.LOAD_START_SCREEN);
			service.putExtra(Constants.LEFT_TEXT_BUTTON, R.string.ubiwidget_btn_nuovo);
			service.putExtra(Constants.RIGHT_TEXT_BUTTON, R.string.ubiwidget_btn_home);
			service.putExtra(Constants.ERROR_DETAIL, ctx.getResources().getString(R.string.ubiwidget_cancel_payment));

			ctx.startService(service);
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "There is being a problem starting the widget action dispacher service: " + e.getMessage());
		}
	}

	/*
	 * (non-Javadoc)
	 * @see nfc.payment.engine.listener.OnPaymentListener#onPaymentFailed(int)
	 */
	@Override
	public void onPaymentFailed(int arg0) {

		Context ctx = WidgetProvider.getAppContext();

		AndroidUtils.log(Log.ERROR, "The NFC payment finished with an error. Errorcode: " + arg0);

		try {
			// If the error is due to the wallet, we send to engine start error page
			if (arg0 == Constants.P_WALLET_NOT_FOUND) {

				AndroidUtils.log(Log.ERROR, "The NFC wallet application [" + ctx.getResources().getString(R.string.ubiwidget_wallet_app_pckg) + "] is not installed on the device." + arg0);

				// Creates an Intent to the service and pass all properties for the screen
				Intent service = new Intent(ctx, ActionDispacherService.class);

				service.putExtra(Constants.ACTION_ID, Constants.LOAD_ERROR_ENGINE);
				service.putExtra(Constants.LEFT_ACTION_BUTTON, Constants.LOAD_START_SCREEN);
				service.putExtra(Constants.RIGHT_ACTION_BUTTON, Constants.CHECK_NFC_ENABLED);
				service.putExtra(Constants.LEFT_TEXT_BUTTON, R.string.ubiwidget_btn_annulla);
				service.putExtra(Constants.RIGHT_TEXT_BUTTON, R.string.ubiwidget_btn_riprova);
				service.putExtra(Constants.ERROR_TITLE, R.string.ubiwidget_error_settings_title);
				service.putExtra(Constants.ERROR_DETAIL, R.string.ubiwidget_K_NFCWALTELCO);

				ctx.startService(service);

				return;
			}

			// Creates an Intent to the service and pass all properties for the screen
			Intent service = new Intent(ctx, ActionDispacherService.class);

			service.putExtra(Constants.ACTION_ID, Constants.LOAD_ERROR_PAYMENT);
			service.putExtra(Constants.LEFT_ACTION_BUTTON, Constants.CHECK_NFC_ENABLED);
			service.putExtra(Constants.RIGHT_ACTION_BUTTON, Constants.LOAD_START_SCREEN);
			service.putExtra(Constants.LEFT_TEXT_BUTTON, R.string.ubiwidget_btn_nuovo);
			service.putExtra(Constants.RIGHT_TEXT_BUTTON, R.string.ubiwidget_btn_home);
			service.putExtra(Constants.ERROR_DETAIL, AndroidUtils.getPaymentErrorListenerMessage(arg0));

			ctx.startService(service);
		} catch (NotFoundException e) {
			AndroidUtils.log(Log.ERROR, "There is being a problem finding one resource: " + e.getMessage());
			e.printStackTrace();
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "There is being a problem starting the widget action dispacher service: " + e.getMessage());
		}
	}

	/*
	 * (non-Javadoc)
	 * @see nfc.payment.engine.listener.OnPaymentListener#onPaymentSuccess(java.lang.String,
	 * java.lang.String, java.lang.String)
	 */
	@Override
	public void onPaymentSuccess(String amount, String date, String time) {

		Context ctx = WidgetProvider.getAppContext();

		AndroidUtils.log(Log.DEBUG, "The NFC Payment is finished correctly {amount:" + amount + "&#8364;; date:" + date + "; time:" + time);

		try {
			// Creates an Intent to the service and pass all properties for the screen
			Intent service = new Intent(ctx, ActionDispacherService.class);

			service.putExtra(Constants.ACTION_ID, Constants.LOAD_SUCCESS_PAYMENT);
			service.putExtra("amount", amount);
			service.putExtra("time", time);
			service.putExtra("date", date);

			ctx.startService(service);
		} catch (Exception e) {
			AndroidUtils.log(Log.ERROR, "There is being a problem starting the widget action dispacher service: " + e.getMessage());
		}
	}

	/*
	 * (non-Javadoc)
	 * @see nfc.payment.engine.listener.OnPaymentListener#onTapToPay()
	 */
	@Override
	public void onTapToPay() {

		AndroidUtils.log(Log.DEBUG, "The NFC library fires an event onTapToPay");
	}

}
