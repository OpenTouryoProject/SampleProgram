import 'package:flutter/material.dart';
import 'package:english_words/english_words.dart';
import 'package:url_launcher/url_launcher.dart';

import 'dart:io';
import 'dart:async';
import 'dart:convert' as convert;

// Platform呼出
import 'package:flutter/services.dart';

// WebAPI呼出
import 'package:http/http.dart' as http;

// AppAuth呼出
import 'package:flutter_appauth/flutter_appauth.dart';

// プッシュ通知
import 'package:flutter/foundation.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart';

import 'message.dart';
import 'message_list.dart';
import 'permissions.dart';
import 'token_monitor.dart';
import 'meta_card.dart';

/// Define a top-level named handler which background/terminated messages will call.
/// バックグラウンド / ターミネーテッド・メッセージが呼び出すトップレベルの名前付きハンドラーを定義。
/// To verify things are working, check out the native platform logs.
/// 動作を確認するには、ネイティブプラットフォームのログを確認します。
Future<void> _firebaseMessagingBackgroundHandler(RemoteMessage message) async {
  // If you're going to use other Firebase services in the background, such as Firestore,
  // make sure you call `initializeApp` before using other Firebase services.
  // Firestoreなど、他のFirebaseサービスをバックグラウンドで使用する場合
  // 他のFirebaseサービスを使用する前に、必ず`initializeApp`を呼び出す。
  await Firebase.initializeApp();
  print('Handling a background message ${message.messageId}');
}

/// Create a [AndroidNotificationChannel] for heads up notifications
/// ヘッドアップ通知用の[AndroidNotificationChannel]の作成
const AndroidNotificationChannel channel = AndroidNotificationChannel(
  'high_importance_channel', // id
  'High Importance Notifications', // title
  'This channel is used for important notifications.', // description
  importance: Importance.high,
);

/// Initialize the [FlutterLocalNotificationsPlugin] package.
/// FlutterLocalNotificationsPlugin]パッケージを初期化します。
FlutterLocalNotificationsPlugin? flutterLocalNotificationsPlugin;

Future<void> main() async {
  // Flutter Engine を使う準備の呪文
  WidgetsFlutterBinding.ensureInitialized();
  // Firebase を初期化
  await Firebase.initializeApp();

  // Set the background messaging handler early on, as a named top-level function
  // バックグラウンド・メッセージング・ハンドラを早い段階で、名前付きのトップレベル関数として設定。
  FirebaseMessaging.onBackgroundMessage(_firebaseMessagingBackgroundHandler);

  if (!kIsWeb) {
    flutterLocalNotificationsPlugin = FlutterLocalNotificationsPlugin();

    /// Create an Android Notification Channel.
    /// We use this channel in the `AndroidManifest.xml` file
    /// to override the default FCM channel to enable heads up notifications.
    /// Android Notification Channelを作成します。
    /// このチャンネルを `AndroidManifest.xml` ファイルで使用して、
    /// デフォルトの FCM チャンネルをオーバーライドし、ヘッドアップ通知を有効化。
    await flutterLocalNotificationsPlugin
        ?.resolvePlatformSpecificImplementation<
        AndroidFlutterLocalNotificationsPlugin>()
        ?.createNotificationChannel(channel);

    /// Update the iOS foreground notification presentation options to allow heads up notifications.
    /// iOSの前景通知の表示オプションを更新し、ヘッドアップ通知を有効化。
    await FirebaseMessaging.instance
        .setForegroundNotificationPresentationOptions(
      alert: true,
      badge: true,
      sound: true,
    );
  }

  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or simply save your changes to "hot reload" in a Flutter IDE).
        // Notice that the counter didn't reset back to zero; the application
        // is not restarted.
        primarySwatch: Colors.blue,
      ),
      routes: {
        '/': (context) => MyHomePage(title: 'Flutter Demo Home Page'),
        '/message': (context) => MessageView(),
      },
    );
  }
}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key? key, required this.title}) : super(key: key);

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  int _counter = 0;
  String _display = "hoge";
  String? _token;

  // Platform呼出
  static const platform = const MethodChannel('flutter_template.opentouryo.com/battery');

  // FlutterAppAuth
  final FlutterAppAuth _appAuth = FlutterAppAuth();

  String? _codeVerifier;
  String? _authorizationCode;
  String? _refreshToken;
  String? _accessToken;

  // final String _clientId = 'interactive.public';
  // final String _redirectUrl = 'io.identityserver.demo:/oauthredirect';
  // final String _issuer = 'https://demo.identityserver.io';
  // final String _discoveryUrl =
  //     'https://demo.identityserver.io/.well-known/openid-configuration';

  final String _clientId = '40319c0100f94ff3aab3004c8bdb5e52';
  final String _redirectUrl = 'com.opentouryo:/oauthredirect';
  final String _issuer = 'https://ssoauth.opentouryo.com';
  final String _discoveryUrl =
      'https://mpos-opentouryo.ddo.jp/MultiPurposeAuthSite/.well-known/openid-configuration';

  final List<String> _scopes = <String>[
    'openid',
    'email'
  ];

  // final AuthorizationServiceConfiguration _serviceConfiguration =
  //   const AuthorizationServiceConfiguration(
  //       'https://demo.identityserver.io/connect/authorize',
  //       'https://demo.identityserver.io/connect/token');

  final AuthorizationServiceConfiguration _serviceConfiguration =
  const AuthorizationServiceConfiguration(
      'https://mpos-opentouryo.ddo.jp/MultiPurposeAuthSite/authorize',
      'https://mpos-opentouryo.ddo.jp/MultiPurposeAuthSite/token');

  @override
  void initState() {
    super.initState();

    // ターミネーテッド状態でプッシュ通知からアプリを起動した時のアクションを実装
    FirebaseMessaging.instance
        .getInitialMessage()
        .then((RemoteMessage? message) {
      if (message != null) {
        // メッセージ詳細画面へ遷移
        Navigator.pushNamed(context, '/message',
            arguments: MessageArguments(message, true));
      }
    });

    // Android のフォアグラウンドプッシュ通知受信時アクションを設定
    //   (iOSと異なり、)Androidではアプリがフォアグラウンド状態で
    //   画面上部にプッシュ通知メッセージを表示することができない為、
    //   ローカル通知で擬似的に通知メッセージを表示する。
    FirebaseMessaging.onMessage.listen((RemoteMessage? message) {
      print("ローカル通知で擬似的に通知メッセージを表示");
      RemoteNotification? notification = message?.notification;
      AndroidNotification? android = message?.notification?.android;
      if (channel != null && flutterLocalNotificationsPlugin != null
          && notification != null && android != null && !kIsWeb) {

        flutterLocalNotificationsPlugin?.show(
            notification.hashCode,
            notification.title,
            notification.body,
            NotificationDetails(
              android: AndroidNotificationDetails(
                channel.id,
                channel.name,
                channel.description,
                // TODO add a proper drawable resource to android, for now using
                //      one that already exists in example app.
                icon: 'notification_icon',
              ),
            ));
      }
    });

    // バックグラウンド状態でプッシュ通知からアプリを起動した時のアクションを実装する
    FirebaseMessaging.onMessageOpenedApp.listen((RemoteMessage message) {
      print('A new onMessageOpenedApp event was published!');
      // メッセージ詳細画面へ遷移
      Navigator.pushNamed(context, '/message',
          arguments: MessageArguments(message, true));
    });
  }

  void _incrementCounter() {
    setState(() {
      // This call to setState tells the Flutter framework that something has
      // changed in this State, which causes it to rerun the build method below
      // so that the display can reflect the updated values. If we changed
      // _counter without calling setState(), then the build method would not be
      // called again, and so nothing would appear to happen.
      this._display = (this._counter++).toString();
    });
  }

  void _englishWords() {
    setState(() {
      this._display = WordPair.random().asPascalCase;
    });
  }

  void _urlLauncher() async {
    const url = "https://www.osscons.jp/jo5v2ne7n-537/";
    if (await canLaunch(url)) {
      await launch(url);
    } else {
      throw 'Could not Launch $url';
    }
  }

  Future<void> _getBatteryLevel() async {
    String batteryLevel;
    try {
      final int result = await platform.invokeMethod('getBatteryLevel');
      batteryLevel = 'Battery level at $result % .';
    } on PlatformException catch (e) {
      batteryLevel = "Failed to get battery level: '${e.message}'.";
    }
    setState(() {
      this._display = batteryLevel;
    });
  }

  Future<void> _getBooks() async {
    var url =
    Uri.https('www.googleapis.com', '/books/v1/volumes', {'q': '{http}'});

    // Await the http get response, then decode the json-formatted response.
    var response = await http.get(url);
    if (response.statusCode == 200) {
      var jsonResponse =
      convert.jsonDecode(response.body) as Map<String, dynamic>;
      var itemCount = jsonResponse['totalItems'];
      setState(() {
        this._display = itemCount.toString();
      });
    } else {
      print('Request failed with status: ${response.statusCode}.');
    }
  }

  Future<void> _signInWithNoCodeExchange() async {
    try {
      final AuthorizationResponse? result
        = await this._appAuth.authorize(AuthorizationRequest(
            this._clientId, this._redirectUrl,
            discoveryUrl: this._discoveryUrl, scopes: this._scopes),
      );
      if (result != null) {
        print("AuthorizationRequest was returned the response.");
        print("authorizationCode: " + result.authorizationCode!.toString());
        this._codeVerifier = result.codeVerifier;
        this._authorizationCode = result.authorizationCode!;
        await this._exchangeCode();
      }
      else {
        print("AuthorizationResponse is null");
      }
    } catch (e) {
      print(e);
    }
  }

  Future<void> _exchangeCode() async {
    try {
      final TokenResponse? result = await this._appAuth.token(TokenRequest(
          this._clientId, this._redirectUrl,
          authorizationCode: this._authorizationCode,
          discoveryUrl: this._discoveryUrl,
          codeVerifier: this._codeVerifier,
          scopes: this._scopes));
      if (result != null) {
        print("TokenRequest was returned the response.");
        print("accessToken: " + result.accessToken!.toString());
        this._accessToken = result.accessToken!;
        await this._testApi();
      }
      else {
        print("TokenResponse is null");
      }
    } catch (e) {
      print(e);
    }
  }

  Future<void> _testApi() async {
    final http.Response httpResponse = await http.get(
        Uri.parse('http://mpos-opentouryo.ddo.jp/MultiPurposeAuthSite/userinfo'),
        headers: <String, String>{'Authorization': 'Bearer ' + this._accessToken!});
    setState(() {
      this._display = httpResponse.statusCode == 200 ?
        httpResponse.body : httpResponse.statusCode.toString();
    });
  }

  Future<void> _sendPushMessage() async {
    if (_token == null) {
      print('Unable to send FCM message, no token exists.');
      return;
    }

    try {
      await http.post(
        Uri.parse('https://api.rnfirebase.io/messaging/send'),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: constructFCMPayload(_token),
      );
      print('FCM request for device sent!');
    } catch (e) {
      print(e);
    }
  }

  Future<void> _onActionSelected(String value) async {
    switch (value) {
      case 'subscribe':
        {
          print('FlutterFire Messaging Example: Subscribing to topic "fcm_test".');
          await FirebaseMessaging.instance.subscribeToTopic('fcm_test');
          print('FlutterFire Messaging Example: Subscribing to topic "fcm_test" successful.');
        }
        break;
      case 'unsubscribe':
        {
          print('FlutterFire Messaging Example: Unsubscribing from topic "fcm_test".');
          await FirebaseMessaging.instance.unsubscribeFromTopic('fcm_test');
          print('FlutterFire Messaging Example: Unsubscribing from topic "fcm_test" successful.');
        }
        break;
      case 'get_apns_token':
        {
          if (defaultTargetPlatform == TargetPlatform.iOS ||
              defaultTargetPlatform == TargetPlatform.macOS) {
            print('FlutterFire Messaging Example: Getting APNs token...');
            String? token = await FirebaseMessaging.instance.getAPNSToken();
            print('FlutterFire Messaging Example: Got APNs token: $token');
          } else {
            print('FlutterFire Messaging Example: Getting an APNs token is only supported on iOS and macOS platforms.');
          }
        }
        break;
      default:
        break;
    }
  }

  @override
  Widget build(BuildContext context) {
    // This method is rerun every time setState is called, for instance as done
    // by the _incrementCounter method above.
    //
    // The Flutter framework has been optimized to make rerunning build methods
    // fast, so that you can just rebuild anything that needs updating rather
    // than having to individually change instances of widgets.
    return Scaffold(
      appBar: AppBar(
        // Here we take the value from the MyHomePage object that was created by
        // the App.build method, and use it to set our appbar title.
        title: Text(widget.title),
        actions: <Widget>[
          PopupMenuButton(
            onSelected: _onActionSelected,
            itemBuilder: (BuildContext context) {
              return [
                const PopupMenuItem(
                  value: 'subscribe',
                  child: Text('Subscribe to topic'),
                ),
                const PopupMenuItem(
                  value: 'unsubscribe',
                  child: Text('Unsubscribe to topic'),
                ),
                const PopupMenuItem(
                  value: 'get_apns_token',
                  child: Text('Get APNs token (Apple only)'),
                ),
              ];
            },
          ),
        ],
      ),
      body: SingleChildScrollView(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: <Widget>[
            Column(
              crossAxisAlignment: CrossAxisAlignment.center,
              children: <Widget>[
                Text(
                  'You have pushed the button this many times:',
                ),
                Text(
                 '$_display',
                  style: Theme.of(context).textTheme.headline4,
                ),
              ],
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: <Widget>[
                ElevatedButton(
                  child: const Text('EnglishWords Button'),
                  style: ElevatedButton.styleFrom(
                    primary: Colors.orange,
                    onPrimary: Colors.white,
                  ),
                  onPressed: this._englishWords,
                ),
                ElevatedButton(
                  child: const Text('UrlLauncher Button'),
                  style: ElevatedButton.styleFrom(
                    primary: Colors.orange,
                    onPrimary: Colors.white,
                  ),
                  onPressed: this._urlLauncher,
                ),
              ]
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: <Widget>[
                ElevatedButton(
                  child: const Text('BatteryLevel Button'),
                  style: ElevatedButton.styleFrom(
                    primary: Colors.orange,
                    onPrimary: Colors.white,
                  ),
                  onPressed: this._getBatteryLevel,
                ),
                ElevatedButton(
                  child: const Text('GetBooks Button'),
                  style: ElevatedButton.styleFrom(
                    primary: Colors.orange,
                    onPrimary: Colors.white,
                  ),
                  onPressed: this._getBooks,
                ),
              ]
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: <Widget>[
                ElevatedButton(
                  child: const Text('SignIn Button'),
                  style: ElevatedButton.styleFrom(
                    primary: Colors.orange,
                    onPrimary: Colors.white,
                  ),
                  onPressed: this._signInWithNoCodeExchange,
                ),
                ElevatedButton(
                  child: const Text('SendPushMessage Button'),
                  style: ElevatedButton.styleFrom(
                    primary: Colors.orange,
                    onPrimary: Colors.white,
                  ),
                  onPressed: this._sendPushMessage,
                ),
              ]
            ),
            Column(children: [
              MetaCard('Permissions', Permissions()),
              MetaCard('FCM Token', TokenMonitor((token) {
                _token = token;
                return token == null
                    ? const CircularProgressIndicator()
                    : Text(token, style: const TextStyle(fontSize: 12));
              })),
              MetaCard('Message Stream', MessageList()),
            ])
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _incrementCounter,
        tooltip: 'Increment',
        child: Icon(Icons.add),
      ),
      // This trailing comma makes auto-formatting nicer for build methods.
      drawer: Drawer(
        child: ListView(
          children: <Widget>[
            DrawerHeader(
              child: Text('Drawer Header'),
              decoration: BoxDecoration(
                color: Colors.blue,
              ),
            ),
            ListTile(
              title: Text("Item 1"),
              trailing: Icon(Icons.arrow_forward),
              onTap: () {
                Navigator.of(context).pop();
                Navigator.of(context).push(
                  MaterialPageRoute(
                    builder: (context) {
                      return MyHomePage(title: 'Flutter Demo Home PageA');
                    },
                  ),
                );
              },
            ),
            ListTile(
              title: Text("Item 2"),
              trailing: Icon(Icons.arrow_forward),
              onTap: () {
                Navigator.of(context).pop();
                Navigator.of(context).push(
                  MaterialPageRoute(
                    builder: (context) {
                      return MyHomePage(title: 'Flutter Demo Home PageB');
                    },
                  ),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}

// Crude counter to make messages unique
int _messageCount = 0;

/// The API endpoint here accepts a raw FCM payload for demonstration purposes.
String constructFCMPayload(String? token) {
  _messageCount++;
  return convert.jsonEncode({
    'token': token,
    'data': {
      'via': 'FlutterFire Cloud Messaging!!!',
      'count': _messageCount.toString(),
    },
    'notification': {
      'title': 'Hello FlutterFire!',
      'body': 'This notification (#$_messageCount) was created via FCM!',
    },
  });
}
