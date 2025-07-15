import { useState, useEffect } from 'react';

export default function useFetchData(baseUrl, endpoints) {
  const [data, setData] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchAll = async () => {
      try {
        const results = await Promise.all(
          endpoints.map(async (endpoint) => {
            const res = await fetch(`${baseUrl}/${endpoint}`);
            const json = await res.json();
            return [endpoint, json];
          })
        );
        setData(Object.fromEntries(results));
      } catch (err) {
        console.error('API error:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchAll();
  }, [baseUrl, endpoints]);

  return { data, loading };
}