// Android 2.3 における Function.prototype.bind() サポート用のポリフィル
(function () {
    if (!Function.prototype.bind) {
        Function.prototype.bind = function (thisValue) {
            if (typeof this !== "function") {
                throw new TypeError(this + " cannot be bound as it is not a function");
            }

            // bind() では、呼び出しに引数を付けることもできます
            var preArgs = Array.prototype.slice.call(arguments, 1);

            // "this" 値をビルドする実際の関数と引数
            var functionToBind = this;
            var noOpFunction = function () { };

            // 使用する "this" 引数
            var thisArg = this instanceof noOpFunction && thisValue ? this : thisValue;

            // 結果のバインドされた関数
            var boundFunction = function () {
                return functionToBind.apply(thisArg, preArgs.concat(Array.prototype.slice.call(arguments)));
            };

            noOpFunction.prototype = this.prototype;
            boundFunction.prototype = new noOpFunction();

            return boundFunction;
        };
    }
}());
