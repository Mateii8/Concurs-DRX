import { useApi } from '../hooks/useApi';

function Departments() {
  const { data } = useApi('/Departments');

  return (
    <main>
      <h1>Departamente</h1>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nume</th>
          </tr>
        </thead>

        <tbody>
          {data.map(dep => (
            <tr key={dep.deptId}>
              <td>{dep.deptId}</td>
              <td>{dep.name}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </main>
  );
}

export default Departments;