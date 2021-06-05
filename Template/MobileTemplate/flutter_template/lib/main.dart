import 'package:flutter/material.dart';
import 'package:english_words/english_words.dart';
import 'package:url_launcher/url_launcher.dart';

// Platform呼出
import 'package:flutter/services.dart';

// WebAPI呼出
import 'dart:convert' as convert;
import 'package:http/http.dart' as http;

// AppAuth呼出
import 'package:flutter_appauth/flutter_appauth.dart';

void main() {
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
      home: MyHomePage(title: 'Flutter Demo Home Page'),
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

  @override
  void initState() {
    super.initState();
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
      ),
      body: Column(
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
              ]
          ),
        ],
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
