import { useContext, useState } from 'react';
import styles from './CreateTodoForm.module.css';

const CreateTodoForm = ({ add }) => {
  const [todo, setTodo] = useState('');

  const formSubmit = async (e) => {
    e.preventDefault();

    add(todo);
  };
  return (
    <form onSubmit={formSubmit} className={styles.form}>
      <input
        type="text"
        value={todo}
        onChange={(e) => setTodo(e.target.value)}
        className={styles.input}
      />
      <button type="submit" className={styles.button}>
        Add
      </button>
    </form>
  );
};
export default CreateTodoForm;
