import React from 'react'
import { useSelector } from 'react-redux';
import { RootState } from 'src/store/CombinedReducers';
import AddTodo from './AddTodo';
import TodoListItem from './TodoListItem';
import { todosSelectors } from './todoSlice';

export default function TodoList(): JSX.Element {
  const todos = useSelector(
      (state: RootState) => todosSelectors.selectAll(state)
  );
  
  return (
    <div>
    <AddTodo />
    <ul>
      {todos.map(todo => (
        <TodoListItem key={todo.id} {...todo} />
      ))}
    </ul>
    </div>
  );
}