const initialState = {counter: 0};

const Menu2 = (state = initialState, action) => {
  
  var newState = null;

  switch (action.type) {

    case 'ADD_MENU2':
      console.log("reducers/Menu2.ADD_MENU2.state: " + JSON.stringify(state));

      // stateを複製してpayloadを処理・追加
      newState = Object.assign({}, state);
      newState = { counter: state.counter + action.payload.amount };

      console.log("reducers/Menu2.ADD_MENU2.newState: " + JSON.stringify(newState));
      
      return newState;

    case 'ADD_MENU2TO1':
      console.log("reducers/Menu2.ADD_MENU2TO1.state: " + JSON.stringify(state));

      // stateを複製してpayloadを処理・追加
      newState = Object.assign({}, state);
      newState = { counter: state.counter + action.payload.amount };

      console.log("reducers/Menu2.ADD_MENU2TO1.newState: " + JSON.stringify(newState));

      return newState;

    default:
      return state;
  }
};
  
  export default Menu2;