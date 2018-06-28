import { createStore as reduxCreateStore, applyMiddleware, combineReducers } from "redux";
import logger from "redux-logger";
//import Menu1Reducer from './reducers/Menu1Reducer';
//import Menu2Reducer from './reducers/Menu2Reducer';
import Menu12Reducer from './reducers/Menu12Reducer';

export default function createStore() {
  const store = reduxCreateStore(
    combineReducers({
      /*Menu1: Menu1Reducer,
      Menu2: Menu2Reducer */
      Menu12: Menu12Reducer
    }),
    applyMiddleware(
      logger,
    )
  );

  return store;
}