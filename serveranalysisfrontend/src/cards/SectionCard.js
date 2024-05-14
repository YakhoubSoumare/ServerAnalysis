import React from "react";
// import bad from "../images/bad.jpg";

export default function SectionCard({ image, title, text }) {
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
          <a href="https://www.lipsum.com/" target="_blank" rel="noopener noreferrer" className="card-link">Card link</a>
          <a href="https://www.lipsum.com/" target="_blank" rel="noopener noreferrer" className="card-link">Another link</a>
        </small>
      </div>
  </div>
  );
}