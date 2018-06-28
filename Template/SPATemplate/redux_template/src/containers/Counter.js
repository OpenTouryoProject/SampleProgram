import { connect } from 'react-redux';
import * as actions from '../actions/Counter';
import Counter from '../components/Counter';

const mapStateToProps = state => {
  return {
    counter: state.CounterReducer.counter
  }
}

const mapDispatchToProps = dispatch => {
  return {
    ADD_VALUE: (amount) => dispatch(actions.ADD_VALUE(amount)),
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Counter)