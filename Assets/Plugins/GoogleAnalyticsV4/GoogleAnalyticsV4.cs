using UnityEngine;

public class GoogleAnalyticsV4 : MonoBehaviour
{
    private string uncaughtExceptionStackTrace = null;
    private bool initialized = false;

    public enum DebugMode { ERROR, WARNING, INFO, VERBOSE };

    [Tooltip("The tracking code to be used for platform. Example value: UA-XXXX-Y.")]
    public string trackingCode;

    [Tooltip("The application name. This value should be modified in the Unity Player Settings.")]
    public string productName;

    [Tooltip("The application identifier. Example value: com.company.app.")]
    public string bundleIdentifier;

    [Tooltip("The application version. Example value: 1.2")]
    public string bundleVersion;

    [RangedTooltip("The dispatch period in seconds. Only required for Android and iOS.", 0, 3600)]
    public int dispatchPeriod = 5;

    [RangedTooltip("The sample rate to use. Only required for Android and iOS.", 0, 100)]
    public int sampleFrequency = 100;

    [Tooltip("The log level. Default is WARNING.")]
    public DebugMode logLevel = DebugMode.WARNING;

    [Tooltip("If checked, the IP address of the sender will be anonymized.")]
    public bool anonymizeIP = false;

    [Tooltip("Automatically report uncaught exceptions.")]
    public bool UncaughtExceptionReporting = false;

    [Tooltip("Automatically send a launch event when the game starts up.")]
    public bool sendLaunchEvent = false;

    [Tooltip("If checked, hits will not be dispatched. Use for testing.")]
    public bool dryRun = false;

    // TODO: Create conditional textbox attribute
    [Tooltip("The amount of time in seconds your application can stay in" +
        "the background before the session is ended. Default is 30 minutes" +
        " (1800 seconds). A value of -1 will disable session management.")]
    public int sessionTimeout = 1800;

    public static GoogleAnalyticsV4 instance = null;

    [HideInInspector]
    public readonly static string currencySymbol = "USD";
    public readonly static string EVENT_HIT = "createEvent";
    public readonly static string APP_VIEW = "createAppView";
    public readonly static string SET = "set";
    public readonly static string SET_ALL = "setAll";
    public readonly static string SEND = "send";
    public readonly static string ITEM_HIT = "createItem";
    public readonly static string TRANSACTION_HIT = "createTransaction";
    public readonly static string SOCIAL_HIT = "createSocial";
    public readonly static string TIMING_HIT = "createTiming";
    public readonly static string EXCEPTION_HIT = "createException";

    private GoogleAnalyticsMPV3 mpTracker = new GoogleAnalyticsMPV3();

    void Awake()
    {
        InitializeTracker();
        if (sendLaunchEvent)
            LogEvent("Google Analytics", "Auto Instrumentation", "Game Launch", 0);

        if (UncaughtExceptionReporting)
        {
            Application.logMessageReceived += HandleException;

            if (belowThreshold(logLevel, DebugMode.VERBOSE))
                Debug.Log("Enabling uncaught exception reporting.");
        }
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(uncaughtExceptionStackTrace))
        {
            LogException(uncaughtExceptionStackTrace, true);
            uncaughtExceptionStackTrace = null;
        }
    }

    private void HandleException(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
            uncaughtExceptionStackTrace = condition + "\n" + stackTrace + StackTraceUtility.ExtractStackTrace();
    }

    private void InitializeTracker()
    {
        if (!initialized)
        {
            instance = this;

            DontDestroyOnLoad(instance);

            // automatically set app parameters from player settings if they are left empty
            if (string.IsNullOrEmpty(productName))
                productName = Application.productName;

            if (string.IsNullOrEmpty(bundleVersion))
                bundleVersion = Application.version;

            Debug.Log("Initializing Google Analytics 0.2.");

            mpTracker.SetTrackingCode(trackingCode);
            mpTracker.SetBundleIdentifier(bundleIdentifier);
            mpTracker.SetAppName(productName);
            mpTracker.SetAppVersion(bundleVersion);
            mpTracker.SetLogLevelValue(logLevel);
            mpTracker.SetAnonymizeIP(anonymizeIP);
            mpTracker.SetDryRun(dryRun);
            mpTracker.InitializeTracker();

            initialized = true;
            SetOnTracker(Fields.DEVELOPER_ID, "GbOCSs");
        }
    }

    public void SetAppLevelOptOut(bool optOut)
    {
        InitializeTracker();

        mpTracker.SetOptOut(optOut);
    }

    public void SetUserIDOverride(string userID)
    {
        SetOnTracker(Fields.USER_ID, userID);
    }

    public void ClearUserIDOverride()
    {
        InitializeTracker();

        mpTracker.ClearUserIDOverride();

    }

    public void DispatchHits()
    {
        InitializeTracker();
    }

    public void StartSession()
    {
        InitializeTracker();

        mpTracker.StartSession();
    }

    public void StopSession()
    {
        InitializeTracker();

        mpTracker.StopSession();
    }

    // Use values from Fields for the fieldName parameter ie. Fields.SCREEN_NAME
    public void SetOnTracker(Field fieldName, object value)
    {
        InitializeTracker();

        mpTracker.SetTrackerVal(fieldName, value);
    }

    public void LogScreen(string title)
    {
        AppViewHitBuilder builder = new AppViewHitBuilder().SetScreenName(title);
        LogScreen(builder);
    }

    public void LogScreen(AppViewHitBuilder builder)
    {
        InitializeTracker();
        if (builder.Validate() == null)
            return;

        if (belowThreshold(logLevel, DebugMode.VERBOSE))
            Debug.Log("Logging screen.");

        mpTracker.LogScreen(builder);
    }

    public void LogEvent(string eventCategory, string eventAction, string eventLabel, long value)
    {
        EventHitBuilder builder = new EventHitBuilder()
            .SetEventCategory(eventCategory)
            .SetEventAction(eventAction)
            .SetEventLabel(eventLabel)
            .SetEventValue(value);

        LogEvent(builder);
    }

    public void LogEvent(EventHitBuilder builder)
    {
        InitializeTracker();
        if (builder.Validate() == null)
            return;

        if (belowThreshold(logLevel, DebugMode.VERBOSE))
            Debug.Log("Logging event.");

        mpTracker.LogEvent(builder);
    }

    public void LogTransaction(string transID, string affiliation, double revenue, double tax, double shipping)
    {
        LogTransaction(transID, affiliation, revenue, tax, shipping, "");
    }

    public void LogTransaction(string transID, string affiliation, double revenue, double tax, double shipping, string currencyCode)
    {
        TransactionHitBuilder builder = new TransactionHitBuilder()
            .SetTransactionID(transID)
            .SetAffiliation(affiliation)
            .SetRevenue(revenue)
            .SetTax(tax)
            .SetShipping(shipping)
            .SetCurrencyCode(currencyCode);

        LogTransaction(builder);
    }

    public void LogTransaction(TransactionHitBuilder builder)
    {
        InitializeTracker();
        if (builder.Validate() == null)
            return;

        if (belowThreshold(logLevel, DebugMode.VERBOSE))
            Debug.Log("Logging transaction.");

        mpTracker.LogTransaction(builder);
    }

    public void LogItem(string transID, string name, string sku, string category, double price, long quantity)
    {
        LogItem(transID, name, sku, category, price, quantity, null);
    }

    public void LogItem(string transID, string name, string sku, string category, double price, long quantity, string currencyCode)
    {
        ItemHitBuilder builder = new ItemHitBuilder()
            .SetTransactionID(transID)
            .SetName(name)
            .SetSKU(sku)
            .SetCategory(category)
            .SetPrice(price)
            .SetQuantity(quantity)
            .SetCurrencyCode(currencyCode);

        LogItem(builder);
    }

    public void LogItem(ItemHitBuilder builder)
    {
        InitializeTracker();
        if (builder.Validate() == null)
            return;

        if (belowThreshold(logLevel, DebugMode.VERBOSE))
            Debug.Log("Logging item.");

        mpTracker.LogItem(builder);
    }

    public void LogException(string exceptionDescription, bool isFatal)
    {
        ExceptionHitBuilder builder = new ExceptionHitBuilder()
            .SetExceptionDescription(exceptionDescription)
            .SetFatal(isFatal);

        LogException(builder);
    }

    public void LogException(ExceptionHitBuilder builder)
    {
        InitializeTracker();
        if (builder.Validate() == null)
            return;

        if (belowThreshold(logLevel, DebugMode.VERBOSE))
            Debug.Log("Logging exception.");

        mpTracker.LogException(builder);
    }

    public void LogSocial(string socialNetwork, string socialAction, string socialTarget)
    {
        SocialHitBuilder builder = new SocialHitBuilder()
            .SetSocialNetwork(socialNetwork)
            .SetSocialAction(socialAction)
            .SetSocialTarget(socialTarget);

        LogSocial(builder);
    }

    public void LogSocial(SocialHitBuilder builder)
    {
        InitializeTracker();
        if (builder.Validate() == null)
            return;

        if (belowThreshold(logLevel, DebugMode.VERBOSE))
            Debug.Log("Logging social.");

        mpTracker.LogSocial(builder);
    }

    public void LogTiming(string timingCategory, long timingInterval, string timingName, string timingLabel)
    {
        TimingHitBuilder builder = new TimingHitBuilder()
            .SetTimingCategory(timingCategory)
            .SetTimingInterval(timingInterval)
            .SetTimingName(timingName)
            .SetTimingLabel(timingLabel);

        LogTiming(builder);
    }

    public void LogTiming(TimingHitBuilder builder)
    {
        InitializeTracker();
        if (builder.Validate() == null)
            return;

        if (belowThreshold(logLevel, DebugMode.VERBOSE))
            Debug.Log("Logging timing.");

        mpTracker.LogTiming(builder);
    }

    public void Dispose()
    {
        initialized = false;
    }

    public static bool belowThreshold(DebugMode userLogLevel, DebugMode comparelogLevel)
    {
        if (comparelogLevel == userLogLevel)
            return true;
        else if (userLogLevel == DebugMode.ERROR)
            return false;
        else if (userLogLevel == DebugMode.VERBOSE)
            return true;
        else if (userLogLevel == DebugMode.WARNING && (comparelogLevel == DebugMode.INFO || comparelogLevel == DebugMode.VERBOSE))
            return false;
        else if (userLogLevel == DebugMode.INFO && (comparelogLevel == DebugMode.VERBOSE))
            return false;

        return true;
    }

    public static GoogleAnalyticsV4 getInstance()
    {
        return instance;
    }
}
