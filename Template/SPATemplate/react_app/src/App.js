import React, { Component } from 'react';
import logo from './logo.svg';
import Menu1 from './Menu1';
import Menu2 from './Menu2';
import './App.css';

class App extends Component {
  // コンストラクタ
  constructor(props) {
    super(props);
    this.state = {
      count: 0,
      color1: 'red',
      color2: 'blue'
    };
  }

  // 子（Menu2）のイベントを受ける
  receive (data) {
    //alert(data.name + ' :: ' + data.type + ' :: ' + data.value)
    // this.state.countを更新すると、
    // 子（Menu1）のcomponentWillReceivePropsが実行される。
    this.setState({count: data.value});
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1 className="App-title">Welcome to React</h1>
        </header>
        <h1>ここはAppコンポーネントですよ！</h1>
        <Menu1 count={this.state.count} color={this.state.color1} />
        <Menu2 count={this.state.count} color={this.state.color2} onEventCallback={e => this.receive(e)}/>
      </div>
    );
  }
}

export default App;
