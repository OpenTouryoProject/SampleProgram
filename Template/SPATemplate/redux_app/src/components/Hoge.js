import React, { Component } from 'react';

export default class Hoge extends Component {
  // 描画メソッド
  render() {
    console.log("components/Hoge.props: " + JSON.stringify(this.props));
    console.log("components/Hoge.state: " + JSON.stringify(this.state));

    return (
      <p>{this.props.fuga}</p>
    );
  }
}