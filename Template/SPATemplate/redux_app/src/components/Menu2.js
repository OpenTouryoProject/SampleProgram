import React, { Component } from 'react';
import PropTypes from 'prop-types'

export default class Menu2 extends Component {

  state = {
    name: "芥川龍之介",
    book: "杜子春",
    counter: 0
  };

  // 描画メソッド
  render() {
    console.log("components/Menu2.props: " + JSON.stringify(this.props));
    console.log("components/Menu2.state: " + JSON.stringify(this.state));

    // Storeからcounterを取り出す。
    const counter = this.props.counter;

    return (
      <div>
        <h1><font color={this.props.color}>ここはMenu2コンポーネントですよ！</font></h1>
        著者: {this.state.name}<br/>
        作品: {this.state.book}<br/>
        カウンタ: {counter}<br/>
        <button onClick={e => this.props.ADD_MENU2(1)}>PUSH + 1</button>
        <button onClick={e => this.props.ADD_MENU2(2)}>PUSH + 2</button>
      </div>
    );
  }
}

Menu2.propTypes = {
  counter: PropTypes.number.isRequired
}