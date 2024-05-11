import { useState, useEffect } from 'react';

async function fetchEndpoint(baseUrl, endpoint, signal) {
    const response = await fetch(`${baseUrl}/${endpoint}`, { signal });

    if (!response.ok) {
        // throw new Error(`HTTP error! status: ${response.status}`);
        const error = new Error(`HTTP error! status: ${response.status}`);
        error.response = response;
        throw error;
    }

    const data = await response.json();
    console.log(`Data from ${endpoint}:`, data); // test
    return data;
}

function useFetchData(baseUrl, endpoints) {

    const abortController = new AbortController();
    const [data, setData] = useState([]);
    // const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {

                const results = await Promise.all(
                    endpoints.map(async endpoint => {
                        const fetchData = await fetchEndpoint(baseUrl, endpoint, abortController.signal)
                        return [endpoint, fetchData];
                    })
                );

                console.log('Results:', results); // test
                setData(results);

            } catch (error) {
                if (!abortController.signal.aborted) {
                    // setError(error);
                    console.error(error);
                } else {
                    console.log('Fetch aborted');
                }
            }
        };

        fetchData().catch(error => console.error(error));

        return () => {
            abortController.abort();
        };

    }, [baseUrl, endpoints]);

    // return {data, error};
    return data;
}


export default useFetchData;