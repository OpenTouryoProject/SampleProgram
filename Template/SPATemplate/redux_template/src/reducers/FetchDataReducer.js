const initialState = {counter: 0};

const FetchDataReducer = (state = initialState, action) => {

  var newState = null;
  
  switch (action.type) {
    
    case 'GET_DATA':
      console.log("reducers/FetchDataReducer.GET_DATA.state: " + JSON.stringify(state));

      // stateを複製してpayloadを処理・追加
      newState = Object.assign({}, state);
      newState = { counter: state.counter + action.payload.amount };

      console.log("reducers/FetchDataReducer.GET_DATA.newState: " + JSON.stringify(newState));

      return newState;

    default:
      return state;
  }
};
    
export default FetchDataReducer;