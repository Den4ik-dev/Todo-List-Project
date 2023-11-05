import { useNavigate } from 'react-router-dom';
import styles from './Registration.module.css';
import { registerAccount } from '../../../services/AuthService';
import { useState } from 'react';

const Registration = () => {
  const [error, setError] = useState('');
  const nav = useNavigate();

  const registerSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData(e.currentTarget);

    const { response, data } = await registerAccount(
      Object.fromEntries(formData)
    );

    if (!response.ok) {
      setError(data.message);
      return;
    }
    setError('');

    e.target.reset();

    nav('/login');
  };

  return (
    <div className={styles.wrapper}>
      <form className={styles.form} onSubmit={registerSubmit}>
        <div style={{ marginBottom: '35px', fontSize: '20px' }}>
          Registration
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

export default Registration;
