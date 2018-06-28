const initialState = {
  counter1: 0,
  counter2: 0
};

const Menu12Reducer = (state = initialState, action) => {
  
  var newState = null;

  switch (action.type) {
    case 'ADD_MENU1':
      console.log("reducers/Menu12.ADD_MENU1.state: " + JSON.stringify(state));

      // stateを複製してpayloadを処理・追加
      newState = Object.assign({}, state);
      newState.counter1 = state.counter1 + action.payload.amount;

      console.log("reducers/Menu12.ADD_MENU1.newState: " + JSON.stringify(newState));

      return newState;
    
    case 'ADD_MENU2':
      console.log("reducers/Menu12.ADD_MENU2.state: " + JSON.stringify(state));

      // stateを複製してpayloadを処理・追加
      newState = Object.assign({}, state);
      newState.counter2 = state.counter2 + action.payload.amount;

      console.log("reducers/Menu12Reducer.ADD_MENU2.newState: " + JSON.stringify(newState));
      
      return newState;

    case 'ADD_MENU2TO1':
      console.log("reducers/Menu12Reducer.ADD_MENU2TO1.state: " + JSON.stringify(state));

      // stateを複製してpayloadを処理・追加
      newState = Object.assign({}, state);
      newState.counter1 = state.counter2 + action.payload.amount;

      console.log("reducers/Menu12Reducer.ADD_MENU2TO1.newState: " + JSON.stringify(newState));
      
      return newState;
    
    default:
      return state;
  }
};
  
export default Menu12Reducer;