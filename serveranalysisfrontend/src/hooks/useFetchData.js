import { useState, useEffect } from 'react';

export default function useFetchData(baseUrl, endpoints) {
  // Säkra defaults: varje endpoint → tom array
  const initialData = Object.fromEntries(endpoints.map((e) => [e, []]));

  const [data, setData] = useState(initialData);
  const [loading, setLoading] = useState(true);

  // Gör en stabil nyckel av endpoints så vi inte triggar på ny arrayreferens
  const endpointsKey = endpoints.join(',');

  // eslint-disable-next-line react-hooks/exhaustive-deps
  useEffect(() => {
    const fetchAll = async () => {
      try {
        const results = await Promise.all(
          endpoints.map(async (endpoint) => {
            const res = await fetch(`${baseUrl}/${endpoint}`);
            if (!res.ok) {
              throw new Error(`Failed to fetch ${endpoint}: ${res.status}`);
            }
            const json = await res.json();
            return [endpoint, json];
          })
        );

        setData(Object.fromEntries(results));
      } catch (err) {
        console.error('API error:', err);
        // fallback till tomma arrayer så UI inte kraschar
        setData(initialData);
      } finally {
        setLoading(false);
      }
    };

    fetchAll();
    // Viktigt: bero på baseUrl + "stabil" endpointsKey, inte själva array-referensen
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [baseUrl, endpointsKey]);

  return { data, loading };
}
