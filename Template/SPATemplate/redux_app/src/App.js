import React, { Component } from 'react';
import logo from './logo.svg';
import Menu1 from './containers/Menu1';
import Menu2 from './containers/Menu2';
import './App.css';

class App extends Component {
  // コンストラクタ
  constructor(props) {
    super(props);
    this.state = {
      color1: 'red',
      color2: 'blue'
    };
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1 className="App-title">Welcome to React</h1>
        </header>
        <h1>ここはAppコンポーネントですよ！</h1>
        <Menu1 color={this.state.color1} />
        <Menu2 color={this.state.color2} />
      </div>
    );
  }
}

export default App;
