import editIcon from '/Todo/edit-icon.svg';

const EditButton = ({ style, onClick }) => {
  return (
    <button
      className="icon-btn"
      style={{ padding: '11px', ...style }}
      onClick={(e) => onClick(e)}
    >
      <img src={editIcon} alt="edit" />
    </button>
  );
};
export default EditButton;
