var moji = require('moji');

exports.sayAdd = function(a, b) {
    say(a + b);
  }
  
  exports.saySubtract = function(a, b) {
    say(a - b);
  }
  
  say = function(word) {
    word = moji(word.toString()).convert('HE', 'ZE').toString();
    console.log(word);
  }