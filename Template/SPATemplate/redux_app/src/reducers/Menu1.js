const initialState = {counter: 0};

const Menu1 = (state = initialState, action) => {

  var newState = null;
  
  switch (action.type) {
    
    case 'ADD_MENU1':
      console.log("reducers/Menu1.ADD_MENU1.state: " + JSON.stringify(state));

      // stateを複製してpayloadを処理・追加
      newState = Object.assign({}, state);
      newState = { counter: state.counter + action.payload.amount };

      console.log("reducers/Menu1.ADD_MENU1.newState: " + JSON.stringify(newState));

      return newState;

    default:
      return state;
  }
};
    
export default Menu1;