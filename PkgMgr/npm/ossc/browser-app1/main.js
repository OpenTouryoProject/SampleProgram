var $ = require('jquery');
var util = require('ossc-test-library1');


$(function(){
    var $msg = $("#msg");
    $msg.fadeOut("slow", function(){
        $msg.text("jQuery")
            .css("color", "red")
            .fadeIn("slow");
    });

    util.sayAdd(1, 2); // 3
    util.saySubtract(2, 1); //1
    util.say // error. util.say is not a function
});