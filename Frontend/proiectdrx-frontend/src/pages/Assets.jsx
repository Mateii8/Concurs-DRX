import { useApi } from '../hooks/useApi';

function Assets() {
  const { data } = useApi('/Assets');

  return (
    <main>
      <h1>Asset-uri</h1>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nume</th>
          </tr>
        </thead>

        <tbody>
          {data.map(asset => (
            <tr key={asset.assetId}>
              <td>{asset.assetId}</td>
              <td>{asset.name}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </main>
  );
}

export default Assets;