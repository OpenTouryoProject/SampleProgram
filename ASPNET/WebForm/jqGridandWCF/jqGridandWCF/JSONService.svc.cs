using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.ServiceModel;
using System.Web;

namespace jqGridandWCF
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "JSONService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで JSONService.svc または JSONService.svc.cs を選択し、デバッグを開始してください。
    public class JSONService : IJSONService
    {
        public JQGridDataClass GetJson()
        {
            // サンプルデータ取得
            DataTable dt = this.CreateSampleData();

            // クエリ文字列から、グリッドの表示に関する情報を取得
            NameValueCollection queryStrings = HttpContext.Current.Request.QueryString;
            string page = queryStrings["page"]; // クエリの現在のページ
            string rows = queryStrings["rows"]; // 1 ページあたりの行数
            string sidx = queryStrings["sidx"]; // ソート列
            string sord = queryStrings["sord"]; // ソートの方向
            int intPage = int.Parse(page);
            int intRows = Math.Min(int.Parse(rows), dt.Rows.Count);

            // jqGrid に渡すデータを格納
            JQGridDataClass data = new JQGridDataClass();
            data.page = page;
            data.total = (int)Math.Ceiling((double)dt.Rows.Count / (double)intRows);
            data.records = dt.Rows.Count;

            // ソート
            DataRow[] dataRows;
            if (!string.IsNullOrEmpty(sidx))
            {
                // ソートあり
                dataRows = dt.Select(null, sidx + " " + sord);
            }
            else
            {
                // ソートなし
                dataRows = dt.Select(null);
            }

            // jqGrid の各セルに表示するデータを格納
            data.rows = new List<JQGridDataRowClass>();
            for (int rIndex = 0; rIndex < intRows; rIndex++)
            {
                // ページなどを考慮し、DataTable から取得する行インデックスを取得
                int actualRowIndex = ((intPage - 1) * intRows) + rIndex;

                if ((actualRowIndex + 1) > dataRows.Length)
                {
                    // 行インデックスが、実際の行数を超えた場合は、ループを抜ける
                    // (最終ページの行数が端数の場合)
                    break;
                }

                // jqGrid に表示する、1 行分のデータを格納
                JQGridDataRowClass subData = new JQGridDataRowClass();
                subData.id = dataRows[actualRowIndex][0].ToString();
                subData.cell = new List<string>();

                for (int cIndex = 0; cIndex < dt.Columns.Count; cIndex++)
                {
                    // 列のデータを、DataTable から取得
                    subData.cell.Add(dataRows[actualRowIndex][cIndex].ToString());
                }
                data.rows.Add(subData);
            }

            // jqGrid にデータを返す
            return data;
        }

        #region サンプルデータ作成
        private DataTable CreateSampleData()
        {
            DataTable dt = new DataTable("dummy");
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("phone", typeof(string));

            DataRow row;
            for (int index = 0; index < 105; index++)
            {
                row = dt.NewRow();
                row["id"] = index + 1;
                row["name"] = "xxx";
                row["email"] = "xxx.xxx.xx@hitachi-solutions.com";
                row["phone"] = "xxxx-xxxx";
                dt.Rows.Add(row);
            }

            return dt;
        }
        #endregion

    }

    /// <summary>
    /// jqGrid に渡すデータを格納するクラス
    /// </summary>
    public class JQGridDataClass
    {
        /// <summary>
        /// クエリの現在のページ
        /// </summary>
        public string page;

        /// <summary>
        /// クエリの総ページ数
        /// </summary>
        public int total;

        /// <summary>
        /// クエリに対するレコードの総数
        /// </summary>
        public int records;

        /// <summary>
        /// 実際のデータを含むリスト
        /// </summary>
        public List<JQGridDataRowClass> rows;
    }

    /// <summary>
    /// jqGrid に表示する、1 行分のデータを格納するクラス
    /// </summary>
    public class JQGridDataRowClass
    {
        /// <summary>
        /// 列の固有 ID
        /// </summary>
        public string id;

        /// <summary>
        /// 列のデータを含むリスト
        /// </summary>
        public List<string> cell;
    }
}
