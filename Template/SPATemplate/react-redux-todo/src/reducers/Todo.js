const initialState = {
  todoList: []
}

export const todoReducer = (state = initialState, action) => {
  switch (action.type) {
    case 'ADD_TODO':
      console.log("reducers/Todo.ADD_TODO.state: " + JSON.stringify(state));

      // 新しく追加するTODO
      const todo = action.payload.todo;
      // stateを複製して追加
      const newState = Object.assign({}, state);
      newState.todoList.push(todo);

      console.log("reducers/Todo.ADD_TODO.newState: " + JSON.stringify(newState));

      return newState;
    default:
      return state;
  }
};