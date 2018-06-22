import React, { Component } from 'react';
import PropTypes from 'prop-types'

export default class Menu1 extends Component {
  state = {
    name: "芥川龍之介",
    book: "杜子春",
    counter: 0
  };

  // 描画メソッド
  render() {
    console.log("components/Menu1.props: " + JSON.stringify(this.props));
    console.log("components/Menu1.state: " + JSON.stringify(this.state));
    
    // Storeからcounterを取り出す。
    const counter = this.props.counter;

    return (
      <div>
        <h1><font color={this.props.color}>ここはMenu1コンポーネントですよ！</font></h1>
        著者: {this.state.name}<br/>
        作品: {this.state.book}<br/>
        カウンタ: {counter}<br/>
        <button onClick={e => this.props.ADD_MENU1(1)}>PUSH + 1</button>
        <button onClick={e => this.props.ADD_MENU1(2)}>PUSH + 2</button>
      </div>
    );
  }
}

Menu1.propTypes = {
  counter: PropTypes.number.isRequired
}