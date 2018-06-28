const initialState = {
  isLoading: false,
}

const FetchDataReducer = (state = initialState, action) => {

  var newState = null;
  
  switch (action.type) {
    
    case 'GET_POSTS_REQUEST':
      // stateを複製して
      newState = Object.assign({}, state);
      // isLoadingをtrueにする
      newState = {
        isLoading: true,
      };

      return newState;
      
    case 'GET_POSTS_SUCCESS':
    
      console.log("GET_POSTS_SUCCESS: " + JSON.stringify(action.forecasts));

      // stateを複製して
      newState = Object.assign({}, state);
      // isLoadingをfalseにして、値をセット。
      newState = {
        isLoading: false,
        forecasts: action.forecasts,
        startDateIndex: action.startDateIndex
      };

      return newState;

    case 'GET_POSTS_FAILURE':
      // stateを複製して
      newState = Object.assign({}, state);
      // isLoadingをfalseにして、値をセット。
      newState = {
        isLoading: false,
        error: action.error
      };

      return newState;

    default:
      return state
  }
};
    
export default FetchDataReducer;