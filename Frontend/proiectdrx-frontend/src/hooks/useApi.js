import { useEffect, useState } from 'react';
import { api } from '../api/api';

export function useApi(path) {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);

  async function load() {
    setLoading(true);

    try {
      const result = await api(path);
      setData(result);
    } catch (err) {
      console.log(err.message);
    }

    setLoading(false);
  }

  useEffect(() => {
    load();
  }, [path]);

  return {
    data,
    loading,
    reload: load
  };
}