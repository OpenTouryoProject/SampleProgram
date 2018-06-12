// expressモジュールをロードし、インスタンス化してappに代入。
var express = require("express");
var app = express();

// CORSを許可する
app.use(function(req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    next();
  });

// listen()メソッドを実行して5000番ポートで待ち受け。
var server = app.listen(5000, function(){
    console.log("Node.js is listening to PORT:" + server.address().port);
});

// *.json
// http://localhost:5000/hoge1.json
app.get("/hoge1.json", function(req, res, next){
    var fs = require('fs');
    var json = JSON.parse(fs.readFileSync('./server/hoge1.json', 'utf8'));
    res.json(json);
});
// http://localhost:5000/hoge2.json
app.get("/hoge2.json", function(req, res, next){
    var fs = require('fs');
    var json = JSON.parse(fs.readFileSync('./server/hoge2.json', 'utf8'));
    res.json(json);
});