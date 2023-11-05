import { useContext, useEffect, useState } from 'react';
import { AuthContext } from '../../../providers/AuthProvider';
import { useNavigate } from 'react-router-dom';
import {
  addTodo,
  editTodo,
  getTodoes,
  removeTodo,
} from '../../../services/TodoesService';
import CreateTodoForm from '../CreateTodoForm/CreateTodoForm';
import Todo from '../Todo/Todo';
import EditTodoForm from '../EditTodoForm/EditTodoForm';

const Home = () => {
  const nav = useNavigate();

  const { user, setUser } = useContext(AuthContext);
  const [todoList, setTodoList] = useState([]);

  useEffect(() => {
    const loginUser = JSON.parse(localStorage.getItem('user'));

    setUser(loginUser);

    if (!loginUser?.login) nav('/login');

    getTodoes(setTodoList, loginUser);
  }, []);

  const add = async (todo) => {
    const { response, data } = await addTodo(todo, user);

    if (!response.ok) return;

    setTodoList([...todoList, data.todo]);
  };

  const remove = async (id) => {
    const { response, data } = await removeTodo(id, user);

    if (!response.ok) return;

    const newTodoList = todoList.filter((t) => t.id !== id);
    setTodoList(newTodoList);
  };
  const edit = async (todo) => {
    const { response, data } = await editTodo(todo, user);

    if (!response.ok) return;

    const newTodoList = todoList.map((t) =>
      t.id == todo.id ? { ...todo, isChanging: false } : t
    );
    setTodoList(newTodoList);
  };
  const editTask = async (id) => {
    const newTodoList = todoList.map((t) =>
      t.id == id ? { ...t, isChanging: !t.isChanging } : t
    );
    setTodoList(newTodoList);
  };

  return (
    <div>
      <div style={{ margin: '10px 0 0 8px', fontSize: '16px' }}>
        Hello {user?.login}!
      </div>
      <div>
        <CreateTodoForm add={add} />
        {todoList.map((todo) =>
          todo.isChanging ? (
            <EditTodoForm todo={todo} edit={edit} key={todo.id} />
          ) : (
            <Todo
              editTask={editTask}
              edit={edit}
              remove={remove}
              key={todo.id}
              task={todo}
            />
          )
        )}
      </div>
    </div>
  );
};

export default Home;
