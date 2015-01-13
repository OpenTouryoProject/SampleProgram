using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace jqGridandWCF
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コードと config ファイルの両方で同時にインターフェイス名 "IJSONService" を変更できます。
    [ServiceContract]
    public interface IJSONService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetJson")]
        JQGridDataClass GetJson();
    }
}
