import { useState } from 'react';
import styles from './EditTodoForm.module.css';

const EditTodoForm = ({ todo, edit }) => {
  const [todoName, setTodoName] = useState(todo.name);

  return (
    <div className={styles.item}>
      <input
        type="text"
        value={todoName}
        onChange={(e) => setTodoName(e.target.value)}
        style={{ border: 'none' }}
      />
      <button onClick={() => edit({ ...todo, name: todoName })}>save</button>
    </div>
  );
};

export default EditTodoForm;
