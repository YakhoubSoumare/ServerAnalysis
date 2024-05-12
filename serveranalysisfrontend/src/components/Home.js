import React from "react";

const Home = ({ data }) => {
    return (
        <>
            <h1 className="page-title">Serververless Functions vs. Server-based Applications</h1>
            <>
                {data.map(([endpoint, data], index) => (
                    <div key={index}>
                        <h2>{endpoint}</h2>
                        <pre>{JSON.stringify(data, null, 2)}</pre>
                    </div>
                ))}
            </>
        </>
    );
}

export default Home;