export const refreshToken = async (payload, token, request) => {
  // token = { accessToken, refreshToken }
  // request => fn
  // console.log('token: ', token);
  try {
    const response = await fetch('https://localhost:7777/api/token/refresh', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Accept: 'application/json',
      },
      body: JSON.stringify(token),
    });

    const data = await response.json();
    // if (response.status == 400) localStorage.removeItem('user');
    if (!response.ok) return { response, data };

    const user = JSON.parse(localStorage.getItem('user'));
    user.token = data;

    localStorage.setItem('user', JSON.stringify(user));

    return await request(payload, user); //TODO!!!!!!!!!!!!!!!!!!!!!!!!
  } catch (e) {
    console.log(e);
  }
};

export const loginInAccount = async (loginUser) => {
  // loginUser => { login, password }

  const response = await fetch('https://localhost:7777/api/users/login', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
    },
    body: JSON.stringify(loginUser),
  });

  const data = await response.json();

  return { response, data };
};
export const registerAccount = async (registeredUser) => {
  const response = await fetch('https://localhost:7777/api/users/register', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
    },
    body: JSON.stringify(registeredUser),
  });

  const data = await response.json();

  return { response, data };
};
