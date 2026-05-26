import { useState } from 'react';
import { api } from '../api/api';
import { useApi } from '../hooks/useApi';
import PasswordInput from '../components/PasswordInput';

function Register({ setPage }) {
  const departments = useApi('/Departments');

  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const [form, setForm] = useState({
    name: '',
    email: '',
    password: '',
    confirmPassword: '',
    role: 'User',
    deptId: ''
  });

  async function handleRegister(e) {
    e.preventDefault();

    if (form.password !== form.confirmPassword) {
      alert('Parolele nu coincid.');
      return;
    }

    try {
      await api('/Auth/register', {
        method: 'POST',
        body: JSON.stringify({
          name: form.name,
          email: form.email,
          password: form.password,
          role: form.role,
          deptId: Number(form.deptId)
        })
      });

      alert('Cont creat cu succes');
      setPage('login');
    } catch (err) {
      alert(err.message);
    }
  }

  return (
    <main className="auth-page">
      <form className="auth-card" onSubmit={handleRegister}>
        <h1>Register</h1>

        <input
          placeholder="Nume"
          value={form.name}
          onChange={e => setForm({ ...form, name: e.target.value })}
        />

        <input
          type="email"
          placeholder="Email"
          value={form.email}
          onChange={e => setForm({ ...form, email: e.target.value })}
        />

        <PasswordInput
          placeholder="Parolă"
          value={form.password}
          show={showPassword}
          setShow={setShowPassword}
          onChange={e => setForm({ ...form, password: e.target.value })}
        />

        <PasswordInput
          placeholder="Confirmare parolă"
          value={form.confirmPassword}
          show={showConfirmPassword}
          setShow={setShowConfirmPassword}
          onChange={e =>
            setForm({ ...form, confirmPassword: e.target.value })
          }
        />

        <select
          value={form.role}
          onChange={e => setForm({ ...form, role: e.target.value })}
        >
          <option value="User">User</option>
          <option value="Admin">Admin</option>
        </select>

        <select
          value={form.deptId}
          onChange={e => setForm({ ...form, deptId: e.target.value })}
        >
          <option value="">Alege departament</option>

          {departments.data.map(dep => (
            <option key={dep.deptId} value={dep.deptId}>
              {dep.name}
            </option>
          ))}
        </select>

        <button>Register</button>

        <p>
          Ai deja cont?{' '}
          <span onClick={() => setPage('login')}>Login</span>
        </p>
      </form>
    </main>
  );
}

export default Register;