/// --------------------------------------------------
/// mainScreen object
/// --------------------------------------------------
var mainScreen = 
{
    resultDivId : "resultDiv",
    resultDiv : null
}

mainScreen.Init = function() {
    /// <summary>
    /// Initializes mainScreen variables
    /// </summary>
    this.resultDiv = $get(this.resultDivId);
};
mainScreen.Translate = function () {
    //                var s = document.createElement("script");
    //                s.src = "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=mycallback&appId=myclientid_123 " + encodeURIComponent(result.access_token) + "&from=" + languageFrom + "&to=" + languageTo + "&text=" + text;
    //                document.getElementsByTagName("head")[0].appendChild(s);
    PageMethods.GetAccessToken(mainScreen.OnSucceeded, mainScreen.OnFailed);
};
//mainScreen.OnSucceeded = function (result, usercontext, methodName) {
mainScreen.OnSucceeded = function (result) {

if(result) {
        mainScreen.resultDiv.innerHTML = result;
    }
    
};
mainScreen.OnFailed = function (error, userContext, methodName) {
    if (error) {
        // TODO: add your error handling
        mainScreen.resultDiv.innerHTML = error.get_message();
        //    alert("Error");
    };
    mainScreen.GetTime = function () {
        /// <summary>
        /// Loads rendered server control from server
        /// </summary>
        PageMethods.GetTime(mainScreen.GetTimeCallback, mainScreen.GetTimeFailed);
    };
    mainScreen.GetTimeCallback = function (result) {
        /// <summary>
        /// Is called when server sent result back
        /// </summary>
        /// <param name="result">
        /// Result of calling server method, 
        /// string - server time 
        /// </param>
        if (result) {
            mainScreen.resultDiv.innerHTML = result;
        }
    };
    mainScreen.GetTimeFailed = function (error, userContext, methodName) {
        /// <summary>
        /// Callback function invoked on failure of the page method 
        /// </summary>
        /// <param name="error">error object containing error</param>
        /// <param name="userContext">userContext object</param>
        /// <param name="methodName">methodName object</param>
        if (error) {
            // TODO: add your error handling
            mainScreen.resultDiv.innerHTML = error.get_message();
        }
    };



    /// --------------------------------------------------
    /// Page events processing
    /// --------------------------------------------------

    Sys.Application.add_load(applicationLoadHandler);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler);
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler);

    function applicationLoadHandler() {
        /// <summary>
        /// Raised after all scripts have been loaded and 
        /// the objects in the application have been created 
        /// and initialized.
        /// </summary>
        mainScreen.Init()
    }

    function endRequestHandler() {
        /// <summary>
        /// Raised before processing of an asynchronous 
        /// postback starts and the postback request is 
        /// sent to the server.
        /// </summary>

        // TODO: Add your custom processing for event
    }

    function beginRequestHandler() {
        /// <summary>
        /// Raised after an asynchronous postback is 
        /// finished and control has been returned 
        /// to the browser.
        /// </summary>

        // TODO: Add your custom processing for event
    }
}