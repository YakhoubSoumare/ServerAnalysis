import React from "react";

export default function SectionCard({ image, title, text, sources }) {
  return (
    <div className="card">
      <img src={image} alt={title} className="card-img-top"/>
      <div className="card-body">
        <hr/>
        <h5 className="card-title">{title}</h5>
        <p className="card-text">{text}</p>
      </div>
      <div className="card-footer">
        <hr/>
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