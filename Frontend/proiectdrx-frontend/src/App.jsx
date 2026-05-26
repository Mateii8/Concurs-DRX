import { useState } from 'react';

import Sidebar from './components/Sidebar';

import Login from './pages/Login';
import Register from './pages/Register';
import Dashboard from './pages/Dashboard';
import Complaints from './pages/Complaints';
import Assets from './pages/Assets';
import Employees from './pages/Employees';
import Departments from './pages/Departments';

function App() {

  const savedUser =
    localStorage.getItem('user');

  const [user, setUser] = useState(
    savedUser
      ? JSON.parse(savedUser)
      : null
  );

  const [page, setPage] = useState(
    savedUser
      ? 'dashboard'
      : 'login'
  );

  const isAdmin =
    user?.role === 'Admin' ||
    user?.role === 'ADMIN';

  return (
    <div className="app">

      {page === 'login' && (

        <Login
          setPage={setPage}
          setUser={setUser}
        />

      )}

      {page === 'register' && (

        <Register
          setPage={setPage}
        />

      )}

      {page !== 'login' &&
        page !== 'register' && (

        <>

          <Sidebar
            setPage={setPage}
            user={user}

            onLogout={() => {

              localStorage.removeItem(
                'user'
              );

              setUser(null);

              setPage('login');
            }}
          />

          <div className="content">

            {page === 'dashboard' && (
              <Dashboard />
            )}

            {page === 'complaints' && (
              <Complaints />
            )}

            {page === 'assets' && (
              <Assets />
            )}

            {page === 'employees' && (

              isAdmin
                ? <Employees />
                : <Dashboard />

            )}

            {page === 'departments' && (

              isAdmin
                ? <Departments />
                : <Dashboard />

            )}

          </div>

        </>

      )}

    </div>
  );
}

export default App;