import { useState } from 'react';
import { api } from '../api/api';
import PasswordInput from '../components/PasswordInput';

function Login({ setPage, setUser }) {
  const [showPassword, setShowPassword] = useState(false);

  const [form, setForm] = useState({
    email: '',
    password: ''
  });

  async function handleLogin(e) {
    e.preventDefault();

    try {
      const result = await api('/Auth/login', {
        method: 'POST',
        body: JSON.stringify(form)
      });

      localStorage.setItem('user', JSON.stringify(result));
      setUser(result);
      setPage('dashboard');
    } catch (err) {
      alert(err.message);
    }
  }

  return (
    <main className="auth-page">
      <form className="auth-card" onSubmit={handleLogin}>
        <h1>Login</h1>

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

        <button>Login</button>

        <p>
          Nu ai cont?{' '}
          <span onClick={() => setPage('register')}>Register</span>
        </p>
      </form>
    </main>
  );
}

export default Login;