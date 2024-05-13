import React, { useState } from "react";
import SectionCard from "../cards/SectionCard";

const Home = ({ data }) => {
    const [isOpen, setIsOpen] = useState(true);

    console.log(data);
    const handled_data = handleData(data);
    // console.log(handled_data.comparisons);

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
                        <li><a href="#comparisons">Comparison</a></li>
                        <li><a href="#industry-insights">Industry Insights</a></li>
                        <li><a href="#beneficiaries">Beneficiaries</a></li>
                    </ul> 
                </aside>
                <div className="page-content">
                    <div className="display-data">
                        <div className="sub-heading">Introduction</div>
                        <section id="introduction">
                            <SectionCard title={handled_data.titles[0]} text={handled_data.introductions[0]}/>
                            <SectionCard title={handled_data.titles[1]} text={handled_data.introductions[1]}/>
                        </section>
                        <div className="sub-heading">Approach</div>
                        <section id="approach">
                            <SectionCard title={handled_data.titles[0]} text={handled_data.approaches[0]}/>
                            <SectionCard title={handled_data.titles[1]} text={handled_data.approaches[1]}/>
                        </section>
                        <div className="sub-heading">Use Cases</div>
                        <section id="use-cases">
                            <SectionCard title={handled_data.titles[0]} text={handled_data.useCases[0]}/>
                            <SectionCard title={handled_data.titles[1]} text={handled_data.useCases[1]}/>
                        </section>
                        <div className="sub-heading">Advantages</div>
                        <section id="advantages">
                            <SectionCard title={handled_data.titles[0]} text={handled_data.advantages[0]}/>
                            <SectionCard title={handled_data.titles[1]} text={handled_data.advantages[1]}/>
                        </section>
                        <div className="sub-heading">Limitations</div>
                        <section id="limitations">
                            <SectionCard title={handled_data.titles[0]} text={handled_data.limitations[0]}/>
                            <SectionCard title={handled_data.titles[1]} text={handled_data.limitations[1]}/>
                        </section>
                        <div className="sub-heading">Comparison</div>
                        <section id="comparisons">
                            <SectionCard title={handled_data.titles[0]} text={handled_data.comparisons[0]}/>
                            <SectionCard title={handled_data.titles[1]} text={handled_data.comparisons[1]}/>
                        </section>
                        <div className="sub-heading">Industry Insights</div>
                        <section id="industry-insights">
                            <SectionCard title={handled_data.titles[0]} text={handled_data.industryInsights[0]}/>
                            <SectionCard title={handled_data.titles[1]} text={handled_data.industryInsights[1]}/>
                        </section>
                        <div className="sub-heading">Beneficiaries</div>
                        <section id="beneficiaries">
                            <SectionCard title={handled_data.titles[0]} text={handled_data.beneficiaries[0]}/>
                            <SectionCard title={handled_data.titles[1]} text={handled_data.beneficiaries[1]}/>
                        </section>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Home;

function handleData(data){    
    
    const topicsData = data.find(item => item[0] === 'topics')[1];
    const sourcesData = data.find(item => item[0] === 'sources')[1];
    
    const ids = topicsData.map(item => item.id);
    const titles = topicsData.map(item => item.title);
    const introductions = topicsData.map(item => item.introduction);
    const approaches = topicsData.map(item => item.approach);
    const useCases = topicsData.map(item => item.useCases);
    const limitations = topicsData.map(item => item.limitations);
    const advantages = topicsData.map(item => item.advantages);
    const comparisons = topicsData.map(item => item.comparison);
    const industryInsights = topicsData.map(item => item.industryInsights);
    const beneficiaries = topicsData.map(item => item.beneficiaries);

    const sources_titles = sourcesData.map(item => item.title);
    const sources_links = sourcesData.map(item => item.link);
    const sources_referenceNumbers = sourcesData.map(item => item.referenceNumber);

    return {
        ids,
        titles,
        introductions,
        approaches,
        useCases,
        limitations,
        advantages,
        comparisons,
        industryInsights,
        beneficiaries,
        sources_titles,
        sources_links,
        sources_referenceNumbers
    };
}