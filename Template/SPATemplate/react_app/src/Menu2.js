import React, { Component } from 'react';

class Menu2 extends Component {

  // コンストラクタ
  constructor(props) {
    super(props);
    // State定義
    this.state = {
      name: "芥川龍之介",
      book: "杜子春",
      count: props.count
    };
    // Stateにアクセスするのでthis を指定のオブジェクトに束縛しておく。
    this.onClickButton1 = this.onClickButton1.bind(this);
    this.onClickButton2 = this.onClickButton2.bind(this);
  }

  // 描画メソッド
  render() {
    return (
      <div>
        <h1><font color={this.props.color}>ここはMenu2コンポーネントですよ！</font></h1>
        著者: {this.state.name}<br/>
        作品: {this.state.book}<br/>
        カウンタ: {this.state.count}<br/>
        <button onClick={this.onClickButton1}>PUSH1</button>
        <button onClick={this.onClickButton2}>PUSH2</button>
      </div>
    );
  }

  // ボタン押下時の処理
  onClickButton1() {
    // Steteを更新(カウンタを加算)
    this.setState({
      count: this.state.count + 1
    });
  }

  // ボタン押下時の処理
  onClickButton2() {
    // 親のメソッドを呼び出す。
    this.props.onEventCallback({
      name: 'child',
      type: 'click',
      value: this.state.count
    });
  }
}

export default Menu2;
