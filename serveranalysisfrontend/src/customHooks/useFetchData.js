import { useState, useEffect } from 'react';

function useFetchData(baseUrl, endpoints) {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            let results = [];
            try {
                setLoading(true);
                console.log('Fetching data...');
                results = await Promise.all(
                    endpoints.map(async endpoint => {
                        const response = await fetch(`${baseUrl}/${endpoint}`);
                        if (!response.ok) {
                            throw new Error(`HTTP error! status: ${response.status}`);
                        }
                        const data = await response.json();
                        return [endpoint, data];
                    })
                );
            } catch (error) {
                console.error(error);
            } finally {
                setData(results);
                setLoading(false);
            }
        };

        fetchData().catch(error => console.error(error));
    }, [baseUrl, endpoints]);

    return { data, loading };
}

export default useFetchData;