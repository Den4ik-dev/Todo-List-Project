import { memo, useContext, useState } from 'react';
import styles from './Todo.module.css';
import TodoOptions from '../TodoOptions/TodoOptions';

const Todo = ({ task, remove, edit, editTask }) => {
  const [todo, setTodo] = useState(task);

  return (
    <div className={styles.item}>
      <div className={styles.item__body}>{todo.name}</div>
      <TodoOptions
        todo={todo}
        setTodo={setTodo}
        edit={edit}
        editTask={editTask}
        remove={remove}
      />
    </div>
  );
};
export default memo(Todo);
