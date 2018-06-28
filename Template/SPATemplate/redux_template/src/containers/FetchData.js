import { connect } from 'react-redux';
import * as actions from '../actions/FetchData';
import FetchData from '../components/FetchData';

const mapStateToProps = state => {
  return {
    counter: state.FetchDataReducer.counter
  }
}

const mapDispatchToProps = dispatch => {
  return {
    GET_DATA: (amount) => dispatch(actions.GET_DATA(amount)),
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(FetchData)