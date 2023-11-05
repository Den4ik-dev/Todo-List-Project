import { useState } from 'react';
import styles from './Checkbox.module.css';
import checkedIcon from '/Todo/checked-icon.svg';

const Checkbox = ({ value = false, onChange }) => {
  const [checked, setChecked] = useState(value);

  return (
    <div
      className={styles.checkbox}
      onClick={(e) => {
        setChecked(!checked);
        onChange(e);
      }}
    >
      {checked ? (
        <div
          className={styles.checkbox__body}
          style={{
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
          }}
        >
          <img style={{ width: '75%' }} src={checkedIcon} alt="checked" />
        </div>
      ) : (
        <div className={styles.checkbox__body}></div>
      )}
    </div>
  );
};
export default Checkbox;
