import SectionCard from "../cards/SectionCard";

const Home = ({ data }) => {

    const handled_data = handleData(data);
    console.log(handled_data);

    return (
        <>
            <div className="page-body">
                <article className="page-content">
                    <h1 className="page-title">Serververless Functions vs. Server-based Applications</h1>
                        {renderSectionCards('Introduction', handled_data.introductions)}
                        {renderSectionCards('Approach', handled_data.approaches)}
                        {renderSectionCards('Use Cases', handled_data.useCases)}
                        {renderSectionCards('Advantages', handled_data.advantages)}
                        {renderSectionCards('Limitations', handled_data.limitations)}
                        {renderSectionCards('Comparison', handled_data.comparisons)}
                        {renderSectionCards('Industry Insights', handled_data.industryInsights)}
                        {renderSectionCards('Beneficiaries', handled_data.beneficiaries)}
                </article>
            </div>
        </>
    );
}

export default Home;

function handleData(data) {
    const topicsData = data.find(item => item[0] === 'topics')[1];
    const sourcesData = data.find(item => item[0] === 'sources')[1];
    const imagesData = data.find(item => item[0] === 'images')[1];

    const ids = topicsData.map(item => item.id);
    const titles = topicsData.map(item => item.title);

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
        return topicsData.map(item => {
            let text = item[field];
            const sources = extractSources(text);
            text = text.replace(/\nSource links: (\[\d+\])+/, '');
            const image = imagesData.find(image => image.title.toLowerCase() === field.toLowerCase() && image.topicId === item.id);
            return { text, sources, image: image ? image.url : null };
        });
    };

    const introductions = createSectionData('introductions');
    const approaches = createSectionData('approaches');
    const useCases = createSectionData('useCases');
    const limitations = createSectionData('limitations');
    const advantages = createSectionData('advantages');
    const comparisons = createSectionData('comparisons');
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

function renderSectionCards(sectionName, data) {
    return (
        <>
            <div className="sub-heading"><h2>{sectionName}</h2></div>
            <section id={sectionName.toLowerCase().replace(' ', '-')}>
                {data.map((item, index) => (
                    <SectionCard 
                        key={index}
                        title={item.title} 
                        text={item.text} 
                        sources={item.sources} 
                        image={item.image}
                    />
                ))}
            </section>
        </>
    );
}