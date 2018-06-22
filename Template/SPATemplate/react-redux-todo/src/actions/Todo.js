export const addTodo = (todo) => {
    return { 
      type: 'ADD_TODO',
      payload: { todo: todo }
    };
  }