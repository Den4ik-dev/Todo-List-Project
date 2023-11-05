import styles from './TodoOptions.module.css';
import Checkbox from '../Checkbox/Checkbox';
import EditButton from './EditButton/EditButton';
import RemoveButton from './RemoveButton/RemoveButton';

const TodoOptions = ({ todo, edit, editTask, remove }) => {
  return (
    <div className={styles.item}>
      <Checkbox
        value={todo.complete}
        onChange={async (e) =>
          await edit({ ...todo, complete: !todo.complete })
        }
      />
      <EditButton
        onClick={() => editTask(todo.id)}
        style={{ margin: '0 5px 0 30px' }}
      />
      <RemoveButton onClick={() => remove(todo.id)} />
    </div>
  );
};

export default TodoOptions;
