import './card.css';

export default function SectionCard({ title, text, image, sources }) {
  return (
    <div className="card">
      <div className="card-image">
        <img src={image} alt={title} />
      </div>
      <div className="card-content">
        <h3>{title}</h3>
        <p>{text}</p>
        {sources && sources.length > 0 && (
          <div className="card-sources">
            <h4>Sources</h4>
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
    </div>
  );
}