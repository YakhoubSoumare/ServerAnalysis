import React from "react";

export default function About({ title, text, sources }) {
  return (
    <div className="card">
      <div className="about-title-container">
        <h5 className="card-title">{title}</h5>
      </div>
      <div className="card-body">
        <hr/>
        <p className="card-text">{text}</p>
      </div>
      <div className="card-footer">
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
  </div>
  );
}