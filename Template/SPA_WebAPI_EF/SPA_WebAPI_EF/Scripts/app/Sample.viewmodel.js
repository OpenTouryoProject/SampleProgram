function SampleViewModel() {
    // shippers テーブルのレコードリスト (JSON 形式)
    this.dataLists = ko.observableArray();

    // データアクセス制御クラス
    this.ddlDapItems = ko.observableArray([
        { displayText: "SQL Server / SQL Client", value: "SQL" }, 
        { displayText: "Multi-DB / OLEDB.NET", value: "OLE" },
        { displayText: "Multi-DB / ODBC.NET", value: "ODB" },
        { displayText: "Oracle / ODP.NET", value: "ODP" },
        { displayText: "DB2 / DB2.NET", value: "DB2" },
        { displayText: "HiRDB / HiRDB-DP", value: "HIR" },
        { displayText: "MySQL Cnn/NET", value: "MCN" },
        { displayText: "PostgreSQL / Npgsql", value: "NPS" }
    ]);
    this.ddlDap = ko.observable("SQL");

    // 静的、動的のクエリ モード
    this.ddlMode2Items = ko.observableArray([
        { displayText: "静的クエリ", value: "static" },
        { displayText: "動的クエリ", value: "dynamic" }
    ]);
    this.ddlMode2 = ko.observable("static");

    // 並び替え対象列
    this.ddlOrderColumnItems = ko.observableArray([
        { displayText: "c1", value: "c1" },
        { displayText: "c2", value: "c2" },
        { displayText: "c3", value: "c3" }
    ]);
    this.ddlOrderColumn = ko.observable("c1");

    // 昇順・降順
    this.ddlOrderSequenceItems = ko.observableArray([
        { displayText: "ASC", value: "A" },
        { displayText: "DESC", value: "D" }
    ]);
    this.ddlOrderSequence = ko.observable("A");

    // Shipper テーブルの各項目
    this.shipperId = ko.observable("");
    this.companyName = ko.observable("");
    this.phone = ko.observable("");    

    // 処理結果 (正常系)
    this.Result = ko.observable("");

    // 処理結果 (異常系)
    this.ErrorMessage = ko.observable("");

    this.isVisibletblShippers = ko.observable(false);
    this.isMessage = ko.observable(false);
    this.isProgressLoading = ko.observable(false);    

    // ボタンコマンド (件数取得)
    this.GetCount = function () {
        var self = this;
        this.isProgressLoading(true);
        this.isVisibletblShippers(false);        
        this.isMessage(true);

        // エラーメッセージをクリアする
        this.ErrorMessage("");

        // パラメタを JSON 形式で纏める
        var param = {
            ddlDap: this.ddlDap(),
            ddlMode2: this.ddlMode2()
        }

        // Ajax でリクエストを送信
        $.ajax({
            type: 'POST',
            url: '/api/GetCountEF',
            data: param,
            dataType: 'json',
            success: function (data, dataType) {
                if (data.error) {
                    // エラーメッセージ格納
                    self.ErrorMessage(data.error);                    
                }
                else {
                    // 結果格納
                    self.Result(data.message);
                }
                self.isProgressLoading(false);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // エラーメッセージ格納
                self.ErrorMessage(XMLHttpRequest.responseText);
                self.isProgressLoading(false);
            }
        });
    };

    // ボタンコマンド (一覧取得（dt）)
    this.GetList = function () {       
        var self = this;
        this.isProgressLoading(true);
        this.isVisibletblShippers(true);       
        this.isMessage(false);

        // エラーメッセージをクリアする
        this.ErrorMessage("");

        // パラメタを JSON 形式で纏める
        var param = {
            ddlDap: this.ddlDap(),
            ddlMode2: this.ddlMode2()
        }

        // Ajax でリクエストを送信
        $.ajax({
            type: 'POST',
            url: '/api/SelectListEF',
            data: param,
            dataType: 'json',
            success: function (data, dataType) {
                if (data.error) {
                    // エラーメッセージ格納
                    self.ErrorMessage(data.error);
                }
                else {
                    // 一旦レコードリストをクリアする
                    self.ClearList();

                    // 結果格納
                    self.dataLists(data);
                }
                self.isProgressLoading(false);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // エラーメッセージ格納
                self.ErrorMessage(XMLHttpRequest.responseText);
                self.isProgressLoading(false);
            }
        });
    };

    this.GetDetail = function () {
        var self = this;
        this.isProgressLoading(true);
        this.isVisibletblShippers(true);
        this.isMessage(false);

        // エラーメッセージをクリアする
        this.ErrorMessage("");

        // パラメタを JSON 形式で纏める
        var param = {
            ddlDap: this.ddlDap(),
            ddlMode2: this.ddlMode2(),
            shipperId: this.shipperId(),
            companyName: this.companyName(),
            phone:this.phone()
        }

        // Ajax でリクエストを送信
        $.ajax({
            type: 'POST',
            url: '/api/SearchEF',
            data: param,
            dataType: 'json',
            success: function (data, dataType) {
                if (data.error) {
                    // エラーメッセージ格納
                    self.ErrorMessage(data.error);
                }
                else {
                    // 一旦レコードリストをクリアする
                    self.ClearList();

                    // 結果格納
                    
                    if (data.noRecords) {
                        self.Result(data.noRecords);
                        self.isMessage(true);
                    }
                    
                    else {
                        self.isMessage(false);
                        self.dataLists(data);
                    }
                }
                self.isProgressLoading(false);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // エラーメッセージ格納
                self.ErrorMessage(XMLHttpRequest.responseText);
                self.isProgressLoading(false);
            }
        });
    };

    this.InsertRecord = function () {
        var self = this;
        this.isProgressLoading(true);
        this.isVisibletblShippers(false);
        this.isMessage(true);

        // エラーメッセージをクリアする
        this.ErrorMessage("");

        // パラメタを JSON 形式で纏める
        var param = {
            ddlDap: this.ddlDap(),
            ddlMode2: this.ddlMode2(),
            companyName: this.companyName(),
            phone: this.phone()
        }

        // Ajax でリクエストを送信
        $.ajax({
            type: 'POST',
            url: '/api/InsertEF',
            data: param,
            dataType: 'json',
            success: function (data, dataType) {
                if (data.error) {
                    // エラーメッセージ格納
                    self.ErrorMessage(data.error);
                }
                else {
                    // 結果格納
                    self.Result(data.message);
                }
                self.isProgressLoading(false);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // エラーメッセージ格納
                self.ErrorMessage(XMLHttpRequest.responseText);
                self.isProgressLoading(false);
            }
        });
    };

    this.UpdateRecord = function () {
        var self = this;
        this.isProgressLoading(true);
        this.isVisibletblShippers(false);
        this.isMessage(true);

        // エラーメッセージをクリアする
        this.ErrorMessage("");

        var param = {
            ddlDap: this.ddlDap(),
            ddlMode2: this.ddlMode2(),
            shipperId: this.shipperId(),
            companyName: this.companyName(),
            phone: this.phone()
        }

        // Ajax でリクエストを送信
        $.ajax({
            type: 'POST',
            url: '/api/UpdateEF',
            data: param,
            dataType: 'json',
            success: function (data, dataType) {
                if (data.error) {
                    // エラーダイアログを表示する
                    self.ErrorMessage(data.error);
                }
                else {
                    // 結果格納
                    self.Result(data.message);
                }
                self.isProgressLoading(false);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // エラーメッセージ格納
                self.ErrorMessage(XMLHttpRequest.responseText);
            }
        });
    };

    this.DeleteRecord = function () {
        var self = this;
        this.isProgressLoading(true);
        this.isVisibletblShippers(false);
        this.isMessage(true);

        // エラーメッセージをクリアする
        this.ErrorMessage("");

        // パラメタを JSON 形式で纏める
        var param = {
            ddlDap: this.ddlDap(),
            ddlMode2: this.ddlMode2(),
            shipperId: this.shipperId()
        }

        // Ajax でリクエストを送信
        $.ajax({
            type: 'POST',
            url: '/api/DeleteEF',
            data: param,
            dataType: 'json',
            success: function (data, dataType) {
                if (data.error) {
                    // エラーメッセージ格納
                    self.ErrorMessage(data.error);
                }
                else {
                    // 結果格納
                    self.Result(data.message);
                }
                self.isProgressLoading(false);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // エラーメッセージ格納
                self.ErrorMessage(XMLHttpRequest.responseText);
                self.isProgressLoading(false);
            }
        });
    };

    this.ClearList = function () {
        // レコードリストをクリアする
        this.dataLists([]);
    }
}

// ViewModel を作成
var model = new SampleViewModel();

// エラーメッセージが格納されたら、ダイアログを出す
model.ErrorMessage.subscribe(function (newValue) {
    if (newValue != '') {
        $('<div>' + newValue + '</div>').dialog({
            title: 'エラーが発生しました',
            modal: true,
            resizable: false,
            height: 600,
            width: 800,
            buttons: {
                'OK': function (event) {
                    $(this).dialog('close');
                }
            }
        });
    }
});
ko.applyBindings(model);
