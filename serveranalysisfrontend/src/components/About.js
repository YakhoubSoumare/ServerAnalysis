import React, { useState, useEffect } from "react";
import AboutCard from "../cards/AboutCard";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUpLong } from '@fortawesome/free-solid-svg-icons';

const About = ({ data }) => {
    const handled_data = handleData(data);

    const [isVisible, setIsVisible] = useState(false);
    useEffect(() => {
        const toggleVisibility = () => {
          if (window.scrollY > 300) {
            setIsVisible(true);
          } else {
            setIsVisible(false);
          }
        };
      
        window.addEventListener("scroll", toggleVisibility);
      
        return () => window.removeEventListener("scroll", toggleVisibility);
      }, []);

      const scrollToTop = () => {
        window.scrollTo({
          top: 0,
          behavior: "smooth"
        });
      };

    return (
        <div className="page-body">
            <article className="page-content">
                <h1 className="page-title">About</h1>
                    {renderSectionCards('Overview', handled_data.overview)}
                    {renderSectionCards('Language', handled_data.language)}
                    {renderSectionCards('Framework', handled_data.framework)}
                    {renderSectionCards('Api', handled_data.api)}
                    {renderSectionCards('Database', handled_data.database)}
                    {renderSectionCards('Security', handled_data.security)}
                    {renderSectionCards('Front-End', handled_data.frontEnd)}
                    {renderSectionCards('Test', handled_data.test)}
                    {renderSectionCards('Version Control', handled_data.versionControl)}
                    {isVisible && (
                        <div className="up-top-icon" onClick={scrollToTop}>
                            <FontAwesomeIcon icon={faUpLong} size="2x" />
                        </div>
                    )}
            </article>
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

function renderSectionCards(sectionName, data) {
    return (
        <>
            <section id={sectionName.toLowerCase().replace(' ', '-')}>
                {data.map((item, index) => (
                    <AboutCard 
                        key={index}
                        title={sectionName}
                        text={item.text} 
                        sources={item.sources} 
                    />
                ))}
            </section>
        </>
    );
}