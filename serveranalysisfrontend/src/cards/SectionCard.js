import React from "react";

export default function SectionCard({ image, title, text, sources }) {
  return (
    <div className="card">
      <div className="img-container">
        <img src={image} alt={title} className="card-img-top"/>
      </div>
      <div className="card-body">
        <hr/>
        <h5 className="card-title">{title}</h5>
        {text.split('\n').map((item, key) => {
          return <p key={key} className="card-text pre-line">{item}</p>
        })}
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