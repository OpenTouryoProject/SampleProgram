import React, { Component } from 'react';

class Menu extends Component {

  // コンストラクタ
  constructor(props) {
    super(props);
    // State定義
    this.state = {
      name: "芥川龍之介",
      book: "杜子春",
      count: 0
    };
    // "this"にアクセスするメソッドをBindする
    this.onClickButton = this.onClickButton.bind(this);
  }

  // 描画メソッド
  render() {
    return (
      <div>
        <h1>ここはMenuコンポーネントですよ！</h1>
        著者: {this.state.name}<br/>
        作品: {this.state.book}<br/>
        カウンタ: {this.state.count}<br/>
        <button onClick={this.onClickButton}>PUSH</button>
      </div>
    );
  }

  // ボタン押下時の処理
  onClickButton() {
    // Steteを更新(カウンタを加算)
    this.setState({
      count: this.state.count + 1
    });
  }
}

export default Menu;
