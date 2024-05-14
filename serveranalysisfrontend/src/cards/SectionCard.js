import React from "react";
// import bad from "../images/bad.jpg";

export default function SectionCard({ title, text }) {
  return (
    <div className="card">
      <img className="card-img-top" alt="Card cap"/>
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