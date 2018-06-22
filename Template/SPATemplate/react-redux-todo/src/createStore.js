import { createStore as reduxCreateStore, applyMiddleware, combineReducers } from "redux";
import logger from "redux-logger";
import { todoReducer } from "./reducers/Todo";

export default function createStore() {
  const store = reduxCreateStore(
    combineReducers({
      todo: todoReducer,
    }),
    applyMiddleware(
      logger,
    )
  );

  return store;
}