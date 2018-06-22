import { createStore as reduxCreateStore, applyMiddleware, combineReducers } from "redux";
import logger from "redux-logger";
import Menu1 from './reducers/Menu1';
import Menu2 from './reducers/Menu2';

export default function createStore() {
  const store = reduxCreateStore(
    combineReducers({
        Menu1,
        Menu2
    })/*,
    applyMiddleware(
      logger,
    )*/
  );

  return store;
}