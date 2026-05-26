const API_BASE = 'http://localhost:5147/api';

export async function api(path, options = {}) {
  const response = await fetch(`${API_BASE}${path}`, {
    headers: {
      'Content-Type': 'application/json'
    },
    ...options
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText || 'Eroare API');
  }

  if (response.status === 204) {
    return null;
  }

  return response.json();
}