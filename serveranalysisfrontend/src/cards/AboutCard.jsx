import './card.css';

export default function AboutCard({ title, text, sources }) {
  const paragraphs = text?.split('\n').filter(p => p.trim() !== '');

  return (
    <div className="card">
      <h3>{title}</h3>

      {paragraphs.map((para, index) => (
        <p key={index}>{para}</p>
      ))}

      {sources.length > 0 && (
        <div className="card-sources">
          <strong>Sources</strong>
          <ul>
            {sources.map((source, index) => (
              <li key={index}>
                <a href={source.link} target="_blank" rel="noopener noreferrer">
                  {source.title}
                </a>
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}