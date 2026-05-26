import { Eye, EyeOff } from 'lucide-react';

function PasswordInput({
  placeholder,
  value,
  onChange,
  show,
  setShow
}) {
  return (
    <div className="password-field">

      <input
        type={show ? 'text' : 'password'}
        placeholder={placeholder}
        value={value}
        onChange={onChange}
      />

      <button
        type="button"
        className="eye-btn"
        onClick={() => setShow(!show)}
      >
        {show
          ? <EyeOff size={20} />
          : <Eye size={20} />}
      </button>

    </div>
  );
}

export default PasswordInput;