import {
  LayoutDashboard,
  MessageSquare,
  Archive,
  Users,
  Building2,
  LogOut
} from 'lucide-react';

function Sidebar({ setPage, user, onLogout }) {
  const isAdmin =
    user?.role === 'Admin' ||
    user?.role === 'ADMIN';

  return (
    <aside className="sidebar">
      <h2>DRX Helpdesk</h2>

      <button onClick={() => setPage('dashboard')}>
        <LayoutDashboard size={18} />
        Dashboard
      </button>

      <button onClick={() => setPage('complaints')}>
        <MessageSquare size={18} />
        Reclamații
      </button>

      <button onClick={() => setPage('assets')}>
        <Archive size={18} />
        Asset-uri
      </button>

      {isAdmin && (
        <>
          <button onClick={() => setPage('employees')}>
            <Users size={18} />
            Angajați
          </button>

          <button onClick={() => setPage('departments')}>
            <Building2 size={18} />
            Departamente
          </button>
        </>
      )}

      <button className="logout-sidebar" onClick={onLogout}>
        <LogOut size={18} />
        Deconectare
      </button>
    </aside>
  );
}

export default Sidebar;