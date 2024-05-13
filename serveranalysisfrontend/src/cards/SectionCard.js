import React from "react";
import bad from "../images/bad.jpg";

export default function SectionCard({ title, text }) {
  return (
    <div className="card" style={{width: '18rem'}}>
        <img src={bad} alt="Crazy Server" />
        <h5 className="card-title">{title}</h5>
        <div className="card-body">
            <p className="card-text">{text}</p>
            <a href="https://www.lipsum.com/" target="_blank" rel="noopener noreferrer" className="card-link">Card link</a>
            <a href="https://www.lipsum.com/" target="_blank" rel="noopener noreferrer" className="card-link">Another link</a>
        </div>
    </div>
  );
}