const initialState = {counter: 0};

const CounterReducer = (state = initialState, action) => {

  var newState = null;
  
  switch (action.type) {
    
    case 'ADD_VALUE':
      console.log("reducers/CounterReducer.ADD_VALUE.state: " + JSON.stringify(state));

      // stateを複製してpayloadを処理・追加
      newState = Object.assign({}, state);
      newState = { counter: state.counter + action.payload.amount };

      console.log("reducers/CounterReducer.ADD_VALUE.newState: " + JSON.stringify(newState));

      return newState;

    default:
      return state;
  }
};
    
export default CounterReducer;