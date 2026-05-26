import { useState } from 'react';
 
import { api } from '../api/api';
import { useApi } from '../hooks/useApi';
 
function Complaints() {
  const savedUser = JSON.parse(localStorage.getItem('user'));
 
  const isAdmin =
    savedUser?.role === 'Admin' ||
    savedUser?.role === 'ADMIN';
 
  const { data, loading, reload } = useApi('/Complaints');
 
  const assets = useApi('/Assets');
 
  const employees = useApi('/Employees');
 
  const [form, setForm] = useState({
    title: '',
    description: '',
    assetId: '',
    emplId: ''
  });
 
  const [adminActions, setAdminActions] = useState({});
 
  async function addComplaint(e) {
    e.preventDefault();
 
    if (!form.title || !form.description || !form.assetId || !form.emplId) {
      alert('Completează toate câmpurile.');
      return;
    }
 
    try {
      await api('/Complaints', {
        method: 'POST',
        body: JSON.stringify({
          title: form.title,
          description: form.description,
          assetId: Number(form.assetId),
          emplId: Number(form.emplId)
        })
      });
 
      alert('Reclamație adăugată.');
 
      setForm({
        title: '',
        description: '',
        assetId: '',
        emplId: ''
      });
 
      reload();
    } catch (err) {
      alert(err.message);
    }
  }
 
  async function handleAdminUpdate(complaintId) {
    const action = adminActions[complaintId];
 
    if (!action) {
      alert('Alege status sau scrie un răspuns.');
      return;
    }
 
    try {
      if (action.status) {
        await api(`/Complaints/${complaintId}/status`, {
          method: 'PUT',
          body: JSON.stringify({
            status: action.status,
            emplId: savedUser.emplId
          })
        });
      }
 
      if (action.reply && action.reply.trim() !== '') {
        await api('/ComplaintComments', {
          method: 'POST',
          body: JSON.stringify({
            complaintId: complaintId,
            emplId: savedUser.emplId,
            message: action.reply
          })
        });
      }
 
      alert('Actualizare trimisă.');
 
      setAdminActions({
        ...adminActions,
        [complaintId]: {
          status: '',
          reply: ''
        }
      });
 
      reload();
    } catch (err) {
      alert(err.message);
    }
  }
 
  return (
    <main>
      <h1>Reclamații</h1>
 
      {!isAdmin && (
        <form className="form" onSubmit={addComplaint}>
          <input
            placeholder="Titlu reclamație"
            value={form.title}
            onChange={e =>
              setForm({
                ...form,
                title: e.target.value
              })
            }
          />
 
          <textarea
            placeholder="Descriere reclamație"
            value={form.description}
            onChange={e =>
              setForm({
                ...form,
                description: e.target.value
              })
            }
          />
 
          <select
            value={form.assetId}
            onChange={e =>
              setForm({
                ...form,
                assetId: e.target.value
              })
            }
          >
            <option value="">Alege asset</option>
 
            {assets.data.map(asset => (
              <option key={asset.assetId} value={asset.assetId}>
                {asset.name}
              </option>
            ))}
          </select>
 
          <select
            value={form.emplId}
            onChange={e =>
              setForm({
                ...form,
                emplId: e.target.value
              })
            }
          >
            <option value="">Alege angajat</option>
 
            {employees.data.map(emp => (
              <option key={emp.emplId} value={emp.emplId}>
                {emp.name}
              </option>
            ))}
          </select>
 
          <button>Adaugă reclamație</button>
        </form>
      )}
 
      {isAdmin && (
        loading ? (
          <p>Se încarcă...</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>ID</th>
                <th>Titlu</th>
                <th>Status</th>
                <th>Acțiuni admin</th>
              </tr>
            </thead>
 
            <tbody>
              {data.map(c => (
                <tr key={c.complaintId}>
                  <td>{c.complaintId}</td>
 
                  <td>{c.title}</td>
 
                  <td>{c.status}</td>
 
                  <td>
                    <div className="admin-actions">
                      <select
                        value={
                          adminActions[c.complaintId]?.status ||
                          c.status ||
                          ''
                        }
                        onChange={e =>
                          setAdminActions({
                            ...adminActions,
                            [c.complaintId]: {
                              ...adminActions[c.complaintId],
                              status: e.target.value
                            }
                          })
                        }
                      >
                        <option value="NEW">NEW</option>
                        <option value="IN_PROGRESS">IN_PROGRESS</option>
                        <option value="RESOLVED">RESOLVED</option>
                        <option value="CLOSED">CLOSED</option>
                      </select>
 
                      <textarea
                        placeholder="Răspuns către user"
                        value={adminActions[c.complaintId]?.reply || ''}
                        onChange={e =>
                          setAdminActions({
                            ...adminActions,
                            [c.complaintId]: {
                              ...adminActions[c.complaintId],
                              reply: e.target.value
                            }
                          })
                        }
                      />
 
                      <button
                        type="button"
                        onClick={() => handleAdminUpdate(c.complaintId)}
                      >
                        Trimite
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )
      )}
    </main>
  );
}
 
export default Complaints;