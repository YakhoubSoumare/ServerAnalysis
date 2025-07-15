import useFetchData from '../hooks/useFetchData';
import AboutCard from '../cards/AboutCard';
import '../App.css';

export default function About() {
  const { data, loading } = useFetchData(
    'https://server-analysis-api-development.azurewebsites.net/api',
    ['abouts', 'sources']
  );

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

  const aboutItems = handleAboutData(data);

  return (
    <div className="page-body">
      <article className="page-content">
        <h1 className="page-title">About This Project</h1>
        {aboutItems.map((item, index) => (
          <AboutCard
            key={index}
            title={item.title}
            text={item.text}
            sources={item.sources}
          />
        ))}
      </article>
    </div>
  );
}

function handleAboutData({ abouts, sources }) {
  const extractSources = (text) => {
    if (typeof text !== 'string') return [];
    const matches = text.matchAll(/\[(\d+)\]/g);
    const refs = Array.from(matches, m => Number(m[1]));
    return refs
      .map((ref) => sources.find((s) => s.referenceNumber === ref))
      .filter(Boolean);
  };

  const sectionFields = [
    { key: 'overview', label: 'Overview' },
    { key: 'language', label: 'Language' },
    { key: 'framework', label: 'Framework' },
    { key: 'api', label: 'API' },
    { key: 'database', label: 'Database' },
    { key: 'security', label: 'Security' },
    { key: 'frontend', label: 'Front-End' },
    { key: 'test', label: 'Test' },
    { key: 'versionControl', label: 'Version Control' },
    { key: 'challenges', label: 'Challenges' },
    { key: 'improvements', label: 'Improvements' },
  ];

  return sectionFields
    .map(({ key, label }) => {
      const sectionText = abouts[0]?.[key];
      if (!sectionText) return null;
      const cleanedText = sectionText.replace(/\nSource links: (\[\d+\])+/, '');
      const sourcesForText = extractSources(sectionText);
      return {
        title: label,
        text: cleanedText,
        sources: sourcesForText,
      };
    })
    .filter(Boolean);
}