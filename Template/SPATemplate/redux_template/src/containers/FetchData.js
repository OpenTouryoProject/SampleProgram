import { connect } from 'react-redux';
import * as actions from '../actions/FetchData';
import FetchData from '../components/FetchData';

const mapStateToProps = state => {
  return {
    isLoading: state.FetchDataReducer.isLoading,
    forecasts: state.FetchDataReducer.forecasts,
    startDateIndex: state.FetchDataReducer.startDateIndex
  }
}

const mapDispatchToProps = dispatch => {
  return {
    GET_DATA_ASYNC: (startDateIndex) => dispatch(actions.GET_DATA_ASYNC(startDateIndex)),
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(FetchData)