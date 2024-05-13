import React, { useState } from "react";
import SectionCard from "../cards/SectionCard";

const Home = ({ data }) => {
    const [isOpen, setIsOpen] = useState(true);

    let definitions = getDefinitions(data, ['serverbasedapplications', 'serverlessfunctions']);
    console.log(definitions);

    return (
        <>
            <h1 className="page-title">Serververless Functions vs. Server-based Applications</h1>
            <div className="page-body">
                <div className="current-section" onClick={() => setIsOpen(!isOpen)}>
                    
                </div>
                <div className={isOpen ? 'open-curtain hide' : 'open-curtain'}>
                    <strong  onClick={() => setIsOpen(!isOpen)}>
                        {isOpen ? '' : '>>>'}
                    </strong>
                </div>
                <aside className={isOpen ? 'sidebar' : 'sidebar hide'}>
                    <ul>
                        <li className="close-curtain">
                            <strong  onClick={() => setIsOpen(!isOpen)}>
                                {isOpen ? '<<<' : ''}
                            </strong>
                        </li>
                        <li><a href="#introduction">Introduction</a></li>
                        <li><a href="#approach">Approach</a></li>
                        <li><a href="#use-cases">Use Cases</a></li>
                        <li><a href="#advantages">Advantages</a></li>
                        <li><a href="#limitations">Limitations</a></li>
                        <li><a href="#comparison">Comparison</a></li>
                        <li><a href="#beneficiaries">Beneficiaries</a></li>
                    </ul> 
                </aside>
                <div className="page-content">
                    <section id="introduction">
                        <h2>Introduction</h2>
                        <SectionCard/>
                    </section>
                    <section id="approach">
                        <h2>Approach</h2>
                    </section>
                    <section id="use-cases">
                        <h2>Use Cases</h2>
                    </section>
                    <section id="advantages">
                        <h2>Advantages</h2>
                    </section>
                    <section id="limitations">
                        <h2>Limitations</h2>
                    </section>
                    <section id="comparison">
                        <h2>Comparison</h2>
                    </section>
                    <section id="beneficiaries">
                        <h2>Beneficiaries</h2>
                    </section>

                    {/* {data.map(([endpoint, data], index) => (
                        <div key={index}>
                            <h2>{endpoint}</h2>
                            <pre>{JSON.stringify(data, null, 2)}</pre>
                        </div>
                    ))} */}
                </div>
            </div>
        </>
    );
}

export default Home;

function getDefinitions(data, topics){    
    const serverBasedApplications = data.find(item => item[0] === topics[0]);
    const serverlessFunctions = data.find(item => item[0] === topics[1]);

    if (serverBasedApplications && serverlessFunctions) {
        return {
            serverBasedApplications: {
                approach: serverBasedApplications[1][0].approach,
                benefitId: serverBasedApplications[1][0].benefitId,
                id: serverBasedApplications[1][0].id,
                introduction: serverBasedApplications[1][0].introduction,
                limitations: serverBasedApplications[1][0].limitations,
                useCases: serverBasedApplications[1][0].useCases,
            },
            serverlessFunctions: {
                approach: serverlessFunctions[1][0].approach,
                benefitId: serverlessFunctions[1][0].benefitId,
                id: serverlessFunctions[1][0].id,
                introduction: serverlessFunctions[1][0].introduction,
                limitations: serverlessFunctions[1][0].limitations,
                useCases: serverlessFunctions[1][0].useCases,
            }
        };
    }

    return;
}