import SectionCard from "../cards/SectionCard";
// import bad from "../images/bad.jpg";

const Home = ({ data }) => {

    const handled_data = handleData(data);

    return (
        <>
            <div className="page-body">
                <div className="page-content">
                    <h1 className="page-title">Serververless Functions vs. Server-based Applications</h1>
                    <div className="sub-heading"><h2>Introduction</h2></div>
                    <section id="introduction">
                        <SectionCard /*image={bad}*/ title={handled_data.titles[0]} text={handled_data.introductions[0]}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.introductions[1]}/>
                    </section>
                    <div className="sub-heading"><h2>Approach</h2></div>
                    <section id="approach">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.approaches[0]}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.approaches[1]}/>
                    </section>
                    <div className="sub-heading"><h2>Use Cases</h2></div>
                    <section id="use-cases">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.useCases[0]}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.useCases[1]}/>
                    </section>
                    <div className="sub-heading"><h2>Advantages</h2></div>
                    <section id="advantages">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.advantages[0]}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.advantages[1]}/>
                    </section>
                    <div className="sub-heading"><h2>Limitations</h2></div>
                    <section id="limitations">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.limitations[0]}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.limitations[1]}/>
                    </section>
                    <div className="sub-heading"><h2>Comparison</h2></div>
                    <section id="comparisons">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.comparisons[0]}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.comparisons[1]}/>
                    </section>
                    <div className="sub-heading"><h2>Industry Insights</h2></div>
                    <section id="industry-insights">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.industryInsights[0]}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.industryInsights[1]}/>
                    </section>
                    <div className="sub-heading"><h2>Beneficiaries</h2></div>
                    <section id="beneficiaries">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.beneficiaries[0]}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.beneficiaries[1]}/>
                    </section>
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