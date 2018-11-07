# 概要
このアプリケーションは、

（コンソール）<---> Parent.exe <--->（匿名パイプの入出力）<---> Child.exe

間で匿名パイプを使用したプロセス間通信をします。

- StdIOAndPipe  
RedirectStandardXXXXをtrueに設定し、  
子プロセスではConsole経由で使用。
- AnonymousPipe  
AnonymousPipeXXXXStreamを直接使用する。

## デバッグ
子プロセス（Child.exe）にデバッガをアタッチ（Debug）する場合は下記を参照。

- 方法 : デバッガを自動的に起動する  
http://msdn.microsoft.com/ja-jp/library/a329t4ed(v=vs.80).aspx
- 備忘録 Image File Execution Options  
http://limejuicer.blog66.fc2.com/blog-entry-18.html

## 用語
### シェル
CUIのコマンドは、コンソールと標準入力経由でやり取りする。
　
### ３つの入出力先
プロセスに標準的に用意される３つの入出力先
- 標準入力（C言語のscanf、Win32のReadFileなどで「読み取り」）
- 標準出力（C言語のprintf、Win32のWriteFileなどで「書き込み」）
- 標準エラー出力（標準出力と同じ、ハンドルだけ異なる）

### リダイレクト
標準入出力の付け替えを指す。  
（下記のハンドルをコンソール ---> ファイル、匿名パイプなどと変更すること）

### 標準入出力関連の識別子

#### GetStdHandle 関数  
http://msdn.microsoft.com/ja-jp/library/cc429347.aspx

コンソールと入出力が必要なアプリケーションで利用。

- STD_INPUT_HANDLE：標準入力ハンドル
- STD_OUTPUT_HANDLE：標準出力ハンドル
- STD_ERROR_HANDLE：標準エラーハンドル
　
上記を、子プロセス起動時、STARTUPINFO構造体に設定する。
　
#### STARTUPINFO structure (Windows)
http://msdn.microsoft.com/ja-jp/library/ms686331.aspx
- HANDLE hStdInput;
- HANDLE hStdOutput;
- HANDLE hStdError;
　　
#### PROCESS_INFORMATION structure (Windows)
http://msdn.microsoft.com/ja-jp/library/ms684873.aspx

#### CreatePipe 関数
http://msdn.microsoft.com/ja-jp/library/cc429801.aspx

名前なしパイプを作成し、そのパイプの読取側と書込側の両方のハンドルを取得する（入力側と出力側がのハンドルがある）
　
#### DuplicateHandle 関数
http://msdn.microsoft.com/ja-jp/library/cc429766.aspx

オブジェクトハンドルの複製を作成する。
- ハンドルをプロセス間で共有させる場合
- アクセス許可の違うハンドルを作成する場合

に利用することができる。
 
## 参考（C言語での実装）
コンソール プロセスを生成して標準ハンドルをリダイレクトする方法
http://support.microsoft.com/kb/190351/ja

### 親側
- hOutputRead：標準入力（C言語のscanf、Win32のReadFileなど）
- hInputWrite：標準出力（C言語のprintf、Win32のWriteFileなど）

#### 子プロセスにデータを出力可能な匿名パイプ（継承可能）  
- hOutputReadTmp（親プロセスで使うTmp）
- hOutputWrite（子プロセスに渡す）

#### 子プロセスからデータを入力可能な匿名パイプ（継承可能）  
- hInputRead（子プロセスに渡す）
- hInputWriteTmp（親プロセスで使うTmp）
　　　
#### 親プロセスで使う匿名パイプ・ハンドル
- 継承不可のコピーを作成（DuplicateHandle）。
  - hOutputRead
  - hInputWrite
- Tmpをクローズする（CloseHandle）。
  - hOutputReadTmp
  - hInputWriteTmp
　　　
#### 子プロセスに渡す匿名パイプ・ハンドル
- STARTUPINFO構造体に設定して子プロセスを起動。
- ハンドルをクローズする。
  - hOutputWrite
  - hInputRead
　　
### 子側
- hOutputWrite：標準入力（C言語のscanf、Win32のReadFileなど）
- hInputRead：標準出力（C言語のprintf、Win32のWriteFileなど）
