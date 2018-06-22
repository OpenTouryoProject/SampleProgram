import { connect } from 'react-redux';
import * as actions from '../actions/Menu2';
import Menu2 from '../components/Menu2';

const mapStateToProps = state => {
  return {
    counter: state.counter,
  }
}

const mapDispatchToProps = dispatch => {
  return {
    ADD_MENU1: (amount) => dispatch(actions.ADD_MENU2(amount)),
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Menu2)