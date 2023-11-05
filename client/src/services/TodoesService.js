import { refreshToken } from './AuthService';

export const getTodoes = async (setTodoList, loginUser) => {
  try {
    const response = await fetch('https://localhost:7777/api/todoes', {
      credentials: 'include',
      method: 'GET',
      headers: {
        Authorization: `Bearer ${loginUser?.token?.accessToken}`,
      },
    });

    if (response?.status == 401) {
      return await refreshToken(setTodoList, loginUser?.token, getTodoes);
    }

    const data = await response?.json();
    setTodoList(data);
  } catch (e) {
    console.log(e);
  }
};

export const addTodo = async (todo, loginUser) => {
  // todo = { name }
  const response = await fetch('https://localhost:7777/api/todoes', {
    credentials: 'include',
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Authorization: `Bearer ${loginUser?.token?.accessToken}`,
    },
    body: JSON.stringify({ name: todo }),
  });

  if (response.status == 401) {
    return await refreshToken(todo, loginUser?.token, addTodo);
  }

  const data = await response.json();
  return { response, data };
};

export const removeTodo = async (todoId, loginUser) => {
  const response = await fetch(`https://localhost:7777/api/todoes/${todoId}`, {
    credentials: 'include',
    method: 'DELETE',
    headers: {
      Accept: 'application/json',
      Authorization: `Bearer ${loginUser?.token?.accessToken}`,
    },
  });

  if (response.status == 401) {
    return await refreshToken(todoId, loginUser?.token, removeTodo);
  }

  const data = await response.json();
  return { response, data };
};

export const editTodo = async (todo, loginUser) => {
  const response = await fetch(
    `https://localhost:7777/api/todoes/${todo?.id}`,
    {
      credentials: 'include',
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        Accept: 'application/json',
        Authorization: `Bearer ${loginUser?.token?.accessToken}`,
      },
      body: JSON.stringify({ name: todo.name, complete: todo.complete }),
    }
  );

  if (response.status == 401) {
    return await refreshToken(todo, loginUser?.token, editTodo);
  }

  const data = await response.json();
  return { response, data };
};
