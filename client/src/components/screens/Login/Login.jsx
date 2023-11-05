import { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../../../providers/AuthProvider';
import styles from './Login.module.css';
import { loginInAccount } from '../../../services/AuthService';

const Login = () => {
  const [error, setError] = useState('');
  const nav = useNavigate();
  const { setUser } = useContext(AuthContext);

  const loginSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData(e.currentTarget);

    const { response, data } = await loginInAccount(
      Object.fromEntries(formData)
    );

    if (!response.ok) {
      setError(data.message);
      return;
    }
    setError('');

    localStorage.setItem('user', JSON.stringify(data));

    setUser(data);
    e.target.reset();

    nav('/');
  };

  return (
    <div className={styles.wrapper}>
      <form onSubmit={loginSubmit} className={styles.form}>
        <div style={{ marginBottom: '35px', fontSize: '20px' }}>
          Login in account
        </div>
        <div>
          <div>
            <div className={styles.inputTitle}>Login: </div>
            <input type="text" name="login" className={styles.input} />
          </div>
          <div>
            <div className={styles.inputTitle}>Password: </div>
            <input type="password" name="password" className={styles.input} />
          </div>
          <div className="error-block">{error}</div>
          <button type="submit" className={styles.button}>
            login
          </button>
        </div>
      </form>
    </div>
  );
};

export default Login;
