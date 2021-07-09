# How to use
Add the necessary information for push notifications.

- /lib/token_monitor.dart
- /android/app/google-services.json

To change the authentication server to be used,  
change the URL and parameters in the following file.

- /lib/main.dart

For the settings of Private-Use URI Scheme Redirection and  
Claimed Https Scheme URI Redirection, please set them in the following files.

- /android/app/src/main/AndroidManifest.xml
- /android/app/build.gradle

If the authentication server uses a self-signed certificate,  
add the certificate to the following location to allow for self-signed certificates.  
This file can be exported as a CER in DER from the location bar of the browser.

- /android/app/src/debug/res/raw/my_ca.cer
