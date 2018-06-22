import { connect } from 'react-redux';
import * as actions from '../actions/Menu1';
import Menu1 from '../components/Menu1';

const mapStateToProps = state => {
  return {
    counter: state.Menu1.counter
  }
}

const mapDispatchToProps = dispatch => {
  return {
    ADD_MENU1: (amount) => dispatch(actions.ADD_MENU1(amount)),
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Menu1)