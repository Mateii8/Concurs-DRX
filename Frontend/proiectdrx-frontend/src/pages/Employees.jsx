import { useApi } from '../hooks/useApi';

function Employees() {
  const { data } = useApi('/Employees');

  return (
    <main>
      <h1>Angajați</h1>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nume</th>
            <th>Email</th>
          </tr>
        </thead>

        <tbody>
          {data.map(emp => (
            <tr key={emp.emplId}>
              <td>{emp.emplId}</td>
              <td>{emp.name}</td>
              <td>{emp.email}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </main>
  );
}

export default Employees;