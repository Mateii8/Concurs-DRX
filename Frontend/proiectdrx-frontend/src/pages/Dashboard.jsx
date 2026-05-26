import { useEffect, useState } from 'react';
import { api } from '../api/api';
import { useApi } from '../hooks/useApi';
 
function Dashboard() {
  const savedUser = JSON.parse(localStorage.getItem('user'));
 
  const isAdmin =
    savedUser?.role === 'Admin' ||
    savedUser?.role === 'ADMIN';
 
  const complaints = useApi('/Complaints');
  const employees = useApi('/Employees');
  const departments = useApi('/Departments');
  const assets = useApi('/Assets');
 
  const [notifications, setNotifications] = useState([]);
 
  const myComplaints = complaints.data.filter(
    c => c.emplId === savedUser?.emplId
  );
 
  const myOpenComplaints = myComplaints.filter(
    c => c.status === 'NEW' || c.status === 'IN_PROGRESS'
  );
 
  const myResolvedComplaints = myComplaints.filter(
    c => c.status === 'RESOLVED'
  );
 
  const newComplaints = complaints.data.filter(
    c => c.status === 'NEW'
  );
 
  const inProgressComplaints = complaints.data.filter(
    c => c.status === 'IN_PROGRESS'
  );
 
  const resolvedComplaints = complaints.data.filter(
    c => c.status === 'RESOLVED'
  );
 
  useEffect(() => {
    async function loadNotifications() {
      if (isAdmin || myComplaints.length === 0) {
        return;
      }
 
      try {
        const allComments = [];
 
        for (const complaint of myComplaints) {
          const comments = await api(
            `/ComplaintComments/${complaint.complaintId}`
          );
 
          comments.forEach(comment => {
            allComments.push({
              ...comment,
              complaintTitle: complaint.title
            });
          });
        }
 
        setNotifications(
          allComments
            .sort(
              (a, b) =>
                new Date(b.createdAt) -
                new Date(a.createdAt)
            )
            .slice(0, 5)
        );
      } catch (err) {
        console.log(err.message);
      }
    }
 
    loadNotifications();
  }, [complaints.data.length]);
 
  function exportCSV() {
    const rows = [
      ['ID', 'Titlu', 'Status', 'ID Angajat', 'ID Asset'],
      ...complaints.data.map(c => [
        c.complaintId,
        c.title,
        c.status,
        c.emplId,
        c.assetId
      ])
    ];
 
    const csv = rows.map(r => r.join(',')).join('\n');
    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = `raport_${new Date().toISOString().slice(0, 10)}.csv`;
    link.click();
    URL.revokeObjectURL(url);
  }
 
  return (
    <main>
      <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
        <h1>Dashboard</h1>
        {isAdmin && (
          <button onClick={exportCSV} className="btn-export">
            ⬇ Exportă Raport CSV
          </button>
        )}
      </div>
 
      <div className="cards">
        <div className="card">
          <h3>Reclamații</h3>
          <p>{isAdmin ? complaints.data.length : myComplaints.length}</p>
        </div>
 
        <div className="card">
          <h3>Asset-uri</h3>
          <p>{assets.data.length}</p>
        </div>
 
        {isAdmin && (
          <>
            <div className="card">
              <h3>Angajați</h3>
              <p>{employees.data.length}</p>
            </div>
 
            <div className="card">
              <h3>Departamente</h3>
              <p>{departments.data.length}</p>
            </div>
          </>
        )}
 
        {!isAdmin && (
          <>
            <div className="card">
              <h3>Deschise</h3>
              <p>{myOpenComplaints.length}</p>
            </div>
 
            <div className="card">
              <h3>Rezolvate</h3>
              <p>{myResolvedComplaints.length}</p>
            </div>
          </>
        )}
      </div>
 
      {isAdmin && (
        <div className="dashboard-grid">
          <div className="dashboard-box">
            <h2>Reclamații urgente / deschise</h2>
 
            {complaints.data
              .slice(-4)
              .reverse()
              .map(c => (
                <div key={c.complaintId} className="complaint-item">
                  <div>
                    <h3>#{c.complaintId} - {c.title}</h3>
                    <p>Status: {c.status}</p>
                  </div>
 
                  <span className={`status-badge ${c.status}`}>
                    {c.status}
                  </span>
                </div>
              ))}
          </div>
 
          <div className="dashboard-box">
            <h2>Volum reclamații</h2>
 
            <div className="status-row">
              <span>NEW</span>
              <div className="status-bar">
                <div
                  className="fill blue"
                  style={{
                    width: `${Math.max(
                      newComplaints.length * 25,
                      8
                    )}px`
                  }}
                ></div>
              </div>
              <span>{newComplaints.length}</span>
            </div>
 
            <div className="status-row">
              <span>IN_PROGRESS</span>
              <div className="status-bar">
                <div
                  className="fill orange"
                  style={{
                    width: `${Math.max(
                      inProgressComplaints.length * 25,
                      8
                    )}px`
                  }}
                ></div>
              </div>
              <span>{inProgressComplaints.length}</span>
            </div>
 
            <div className="status-row">
              <span>RESOLVED</span>
              <div className="status-bar">
                <div
                  className="fill green"
                  style={{
                    width: `${Math.max(
                      resolvedComplaints.length * 25,
                      8
                    )}px`
                  }}
                ></div>
              </div>
              <span>{resolvedComplaints.length}</span>
            </div>
          </div>
        </div>
      )}
 
      {!isAdmin && (
        <div className="dashboard-grid">
          <div className="dashboard-box">
            <h2>Reclamațiile mele recente</h2>
 
            {myComplaints.length === 0 ? (
              <p>Nu ai reclamații.</p>
            ) : (
              myComplaints
                .slice(-3)
                .reverse()
                .map(c => (
                  <div key={c.complaintId} className="complaint-item">
                    <div>
                      <h3>#{c.complaintId} - {c.title}</h3>
                      <p>Status: {c.status}</p>
                    </div>
 
                    <span className={`status-badge ${c.status}`}>
                      {c.status}
                    </span>
                  </div>
                ))
            )}
          </div>
 
          <div className="dashboard-box">
            <h2>Notificări recente</h2>
 
            {notifications.length === 0 ? (
              <p>Nu există notificări momentan.</p>
            ) : (
              notifications.map(n => (
                <div key={n.commentId} className="notification">
                  <span className="dot"></span>
 
                  <div>
                    <strong>
                      Răspuns nou la reclamația #{n.complaintId}
                    </strong>
 
                    <p>{n.message}</p>
                  </div>
                </div>
              ))
            )}
          </div>
        </div>
      )}
    </main>
  );
}
 
export default Dashboard;