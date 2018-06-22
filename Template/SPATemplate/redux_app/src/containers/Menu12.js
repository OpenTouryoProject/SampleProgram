import React, { Component } from 'react';
import { connect } from 'react-redux';
import * as actions from '../actions/Menu12';
import Menu1 from '../components/Menu1';
import Menu2 from '../components/Menu2';

class Menu12 extends Component {

  render() {
    console.log("containers/Menu12.props: " + JSON.stringify(this.props));
    console.log("containers/Menu12.state: " + JSON.stringify(this.state));

     return (
        <div>
           <Menu1 color={this.props.color.color1}
              counter={this.props.counter1}
              ADD_MENU1={this.props.ADD_MENU1} />
           <Menu2 color={this.props.color.color2}
              counter={this.props.counter2} 
              ADD_MENU2={this.props.ADD_MENU2} 
              ADD_MENU2TO1={this.props.ADD_MENU2TO1} />
        </div>
     )
  }
}

const mapStateToProps = state => {
  return {
    counter1: state.Menu12.counter1,
    counter2: state.Menu12.counter2
  }
}

const mapDispatchToProps = dispatch => {
  return {
    ADD_MENU1: (amount) => dispatch(actions.ADD_MENU1(amount)),
    ADD_MENU2: (amount) => dispatch(actions.ADD_MENU2(amount)),
    ADD_MENU2TO1: (amount) => dispatch(actions.ADD_MENU2TO1(amount)),
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Menu12)