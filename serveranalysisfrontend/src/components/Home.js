import SectionCard from "../cards/SectionCard";
import * as serverbasedImages from "../images/serverbased";
import * as serverlessImages from "../images/serverless";

const Home = ({ data }) => {

    const handled_data = handleData(data);

    return (
        <>
            <div className="page-body">
                <div className="page-content">
                    <h1 className="page-title">Serververless Functions vs. Server-based Applications</h1>
                    <div className="sub-heading"><h2>Introduction</h2></div>
                    <section id="introduction">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.introductions[0].text} sources={handled_data.introductions[0].sources} image={serverbasedImages.serverbasedIntroduction}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.introductions[1].text} sources={handled_data.introductions[1].sources} image={serverlessImages.serverlessIntroduction}/>
                    </section>
                    <div className="sub-heading"><h2>Approach</h2></div>
                    <section id="approach">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.approaches[0].text} sources={handled_data.approaches[0].sources} image={serverbasedImages.serverbasedApproach}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.approaches[1].text} sources={handled_data.approaches[1].sources} image={serverlessImages.serverlessApproach}/>
                    </section>
                    <div className="sub-heading"><h2>Use Cases</h2></div>
                    <section id="use-cases">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.useCases[0].text} sources={handled_data.useCases[0].sources} image={serverbasedImages.serverbasedUseCases}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.useCases[1].text} sources={handled_data.useCases[1].sources} image={serverlessImages.serverlessUseCases}/>
                    </section>
                    <div className="sub-heading"><h2>Advantages</h2></div>
                    <section id="advantages">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.advantages[0].text} sources={handled_data.advantages[0].sources} image={serverbasedImages.serverbasedAdvantages}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.advantages[1].text} sources={handled_data.advantages[1].sources} image={serverlessImages.serverlessAdvantages}/>
                    </section>
                    <div className="sub-heading"><h2>Limitations</h2></div>
                    <section id="limitations">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.limitations[0].text} sources={handled_data.limitations[0].sources} image={serverbasedImages.serverbasedLimitations}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.limitations[1].text} sources={handled_data.limitations[1].sources} image={serverlessImages.serverlessLimitations}/>
                    </section>
                    <div className="sub-heading"><h2>Comparison</h2></div>
                    <section id="comparisons">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.comparisons[0].text} sources={handled_data.comparisons[0].sources} image={serverbasedImages.serverbasedComparison}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.comparisons[1].text} sources={handled_data.comparisons[1].sources} image={serverlessImages.serverlessComparison}/>
                    </section>
                    <div className="sub-heading"><h2>Industry Insights</h2></div>
                    <section id="industry-insights">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.industryInsights[0].text} sources={handled_data.industryInsights[0].sources} image={serverbasedImages.serverbasedIndustryInsights}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.industryInsights[1].text} sources={handled_data.industryInsights[1].sources} image={serverlessImages.serverlessIndustryInsights}/>
                    </section>
                    <div className="sub-heading"><h2>Beneficiaries</h2></div>
                    <section id="beneficiaries">
                        <SectionCard title={handled_data.titles[0]} text={handled_data.beneficiaries[0].text} sources={handled_data.beneficiaries[0].sources} image={serverbasedImages.serverbasedBeneficiaries}/>
                        <SectionCard title={handled_data.titles[1]} text={handled_data.beneficiaries[1].text} sources={handled_data.beneficiaries[1].sources} image={serverlessImages.serverlessBeneficiaries}/>
                    </section>
                </div>
            </div>
        </>
    );
}

export default Home;

function handleData(data) {
    const topicsData = data.find(item => item[0] === 'topics')[1];
    const sourcesData = data.find(item => item[0] === 'sources')[1];

    const ids = topicsData.map(item => item.id);
    const titles = topicsData.map(item => item.title);

    const extractSources = (text) => {
        const matches = text.matchAll(/\[(\d+)\]/g);
        if (matches) {
            const refs = Array.from(matches, m => Number(m[1]));
            const sources = refs.map(ref => sourcesData.find(source => source.referenceNumber === ref));
            return sources;
        }
        return [];
    };

    const createSectionData = (field) => {
        return topicsData.map(item => {
            let text = item[field];
            const sources = extractSources(text);
            text = text.replace(/\nSource links: (\[\d+\])+/, '');
            return { text, sources };
        });
    };

    const introductions = createSectionData('introduction');
    const approaches = createSectionData('approach');
    const useCases = createSectionData('useCases');
    const limitations = createSectionData('limitations');
    const advantages = createSectionData('advantages');
    const comparisons = createSectionData('comparison');
    const industryInsights = createSectionData('industryInsights');
    const beneficiaries = createSectionData('beneficiaries');

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
        beneficiaries
    };
}