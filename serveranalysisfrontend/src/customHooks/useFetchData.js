import { useState, useEffect } from 'react';

function useFetchData(baseUrl, endpoints) {
    const abortController = new AbortController();
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [fetch, setFetch] = useState(false);

    useEffect(() => {
        return () => {
            abortController.abort();
        };
    }, [baseUrl, endpoints]);

    useEffect(() => {
        if (!fetch) return;

        const fetchData = async () => {
            let results = [];
            try {
                setLoading(true);
                results = await Promise.all(
                    endpoints.map(async endpoint => {
                        const fetchData = await fetchEndpoint(baseUrl, endpoint, abortController.signal)
                        return [endpoint, fetchData];
                    })
                );
            } catch (error) {
                if (!abortController.signal.aborted) {
                    console.error(error);
                } else {
                    console.log('Fetch aborted');
                }
            } finally {
                setData(results);
                setLoading(false);
            }
        };

        fetchData().catch(error => console.error(error));
        setFetch(false);
    }, [fetch, baseUrl, endpoints]);

    useEffect(() => {
        setFetch(true);
    }, [baseUrl, endpoints]);

    return { data, loading, setFetch };
}

export default useFetchData;



async function fetchEndpoint(baseUrl, endpoint, signal) {
    const response = await fetch(`${baseUrl}/${endpoint}`, { signal });

    if (!response.ok) {
        const error = new Error(`HTTP error! status: ${response.status}`);
        error.response = response;
        throw error;
    }

    const data = await response.json();
    console.log(`Data from ${endpoint}:`, data); // test
    return data;
}