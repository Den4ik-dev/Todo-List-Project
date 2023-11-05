import deleteIcon from '/Todo/delete-icon.svg';

const RemoveButton = ({ style, onClick }) => {
  return (
    <button
      className="icon-btn"
      style={{ padding: '10px', ...style }}
      onClick={(e) => onClick(e)}
    >
      <img src={deleteIcon} alt="delete" />
    </button>
  );
};

export default RemoveButton;
