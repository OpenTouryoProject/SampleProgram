import 'package:flutter/material.dart';
import 'package:firebase_messaging/firebase_messaging.dart';

/// Manages & returns the users FCM token.
/// Also monitors token refreshes and updates state.
class TokenMonitor extends StatefulWidget {
  // ignore: public_member_api_docs
  TokenMonitor(this._builder);
  // ...
  final Widget Function(String token) _builder;

  @override
  State<StatefulWidget> createState() => _TokenMonitor();
}

class _TokenMonitor extends State<TokenMonitor> {
  String? _token;
  Stream<String>? _tokenStream;

  void setToken(String? token) {
    print('FCM Token: $token');
    setState(() {
      this._token = token;
    });
  }

  @override
  void initState() {
    super.initState();
    FirebaseMessaging.instance
        .getToken(
          vapidKey:
          '<YOUR_PUBLIC_VAPID_KEY_HERE>')
        .then(setToken);
    this._tokenStream = FirebaseMessaging.instance.onTokenRefresh;
    this._tokenStream?.listen(setToken);
  }

  @override
  Widget build(BuildContext context) {
    return widget._builder(this._token ?? "");
  }
}