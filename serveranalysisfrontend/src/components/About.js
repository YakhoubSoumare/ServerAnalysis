import React from "react";
import AboutCard from "../cards/AboutCard";

const About = ({ data }) => {
    // const handled_data = handleData(data);
    console.log(data);

    return (
        <div>
            <h1 className="page-title">About</h1>
        </div>
    );
}

export default About;

function handleData(data) {
    const aboutsData = data.find(item => item[0] === 'abouts')[1];
    const sourcesData = data.find(item => item[0] === 'sources')[1];

    const ids = aboutsData.map(item => item.id);
    const titles = aboutsData.map(item => item.title);

    const extractSources = (text) => {
        if (typeof text !== 'string') {
            console.error('extractSources was called with a non-string argument:', text);
            return [];
        }
        const matches = text.matchAll(/\[(\d+)\]/g);
        if (matches) {
            const refs = Array.from(matches, m => Number(m[1]));
            const sources = refs.map(ref => sourcesData.find(source => source.referenceNumber === ref));
            return sources;
        }
        return [];
    };

    const createSectionData = (field) => {
        return aboutsData.map(item => {
            let text = item[field];
            const sources = extractSources(text);
            text = text.replace(/\nSource links: (\[\d+\])+/, '');
            return { text, sources };
        });
    };

    const overview = createSectionData('overview');
    const language = createSectionData('language');
    const framework = createSectionData('framework');
    const api = createSectionData('api');
    const database = createSectionData('database');
    const security = createSectionData('security');
    const frontEnd = createSectionData('frontEnd');
    const test = createSectionData('test');
    const versionControl = createSectionData('versionControl');

    return {
        ids,
        titles,
        overview,
        language,
        framework,
        api,
        database,
        security,
        frontEnd,
        test,
        versionControl
    };
}

// function renderSectionCards(sectionName, data) {
//     return (
//         <>
//             <div className="sub-heading"><h2>{sectionName}</h2></div>
//             <section id={sectionName.toLowerCase().replace(' ', '-')}>
//                 {data.map((item, index) => (
//                     <AboutCard 
//                         key={index}
//                         title={item.title} 
//                         text={item.text} 
//                         sources={item.sources} 
//                     />
//                 ))}
//             </section>
//         </>
//     );
// }