import SectionCard from '../cards/SectionCard';

export default function Home({ data, loading }) {
  if (loading) {
    return (
      <div style={{ textAlign: 'center', paddingTop: '2rem' }}>
        <p>Loading...</p>
        <p style={{ fontSize: '0.9rem', color: '#666' }}>
          First load may take longer due to cold start (free-tier hosting).
        </p>
      </div>
    );
  }

  const handledData = handleData(data);

  return (
    <div className="page-body">
      <article className="page-content">
        <h1 className="page-title">Serverless Functions vs. Server-based Applications</h1>

        {renderSectionCards('Introduction', handledData.introductions)}
        {renderSectionCards('Approach', handledData.approaches)}
        {renderSectionCards('Use Cases', handledData.useCases)}
        {renderSectionCards('Advantages', handledData.advantages)}
        {renderSectionCards('Limitations', handledData.limitations)}
        {renderSectionCards('Comparison', handledData.comparisons)}
        {renderSectionCards('Industry Insights', handledData.industryInsights)}
        {renderSectionCards('Beneficiaries', handledData.beneficiaries)}
      </article>
    </div>
  );
}

function handleData(all) {
  const topicsData = all.topics;
  const sourcesData = all.sources;
  const imagesData = all.images;

  const extractSources = (text) => {
    if (typeof text !== 'string') return [];
    const matches = text.matchAll(/\[(\d+)\]/g);
    const refs = Array.from(matches, m => Number(m[1]));
    return refs.map(ref => sourcesData.find(src => src.referenceNumber === ref)).filter(Boolean);
  };

  const createSectionData = (field) =>
    topicsData.map((item) => {
      let text = item[field];
      const sources = extractSources(text);
      text = text.replace(/\nSource links: (\[\d+\])+/, '');
      const image = imagesData.find(
        img => img.title.toLowerCase() === field.toLowerCase() && img.topicId === item.id
      );
      return {
        title: item.title,
        text,
        sources,
        image: image ? image.url : null,
      };
    });

  return {
    introductions: createSectionData('introductions'),
    approaches: createSectionData('approaches'),
    useCases: createSectionData('useCases'),
    limitations: createSectionData('limitations'),
    advantages: createSectionData('advantages'),
    comparisons: createSectionData('comparisons'),
    industryInsights: createSectionData('industryInsights'),
    beneficiaries: createSectionData('beneficiaries'),
  };
}

function renderSectionCards(sectionName, sectionData) {
  return (
    <>
      <div className="sub-heading"><h2>{sectionName}</h2></div>
      <section id={sectionName.toLowerCase().replace(' ', '-')}>
        <div className="card-row">
          {sectionData.map((item, index) => (
            <SectionCard
              key={index}
              title={item.title}
              text={item.text}
              sources={item.sources}
              image={item.image}
            />
          ))}
        </div>
      </section>
    </>
  );
}