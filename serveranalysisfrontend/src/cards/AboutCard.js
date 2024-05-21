import React from "react";

export default function About({ title, text, sources }) {
  return (
    <div className="about-card">
      <div className="about-title-container">
        <h3 className="about-card-title">{title}</h3>
      </div>
      <div className="about-card-body">
        <hr/>
        {text.split('\n').map((item, key) => {
          return <p key={key} className="card-text pre-line">{item}</p>
        })}
      </div>
      {sources.length > 0 && 
        <div className="about-card-footer">
          <hr/>
          <h6>Sources</h6>
          <small className="text-muted">
            {sources.map((source, index) => (
              <p key={index}>
                  <a href={source.link} target="_blank" rel="noopener noreferrer">
                      {source.title}
                  </a>
              </p>
            ))}
          </small>
        </div>
      }
  </div>
  );
}