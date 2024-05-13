import React from "react";

export default function SectionCard() {
  return (
    <div className="card" style={{width: '18rem'}}>
        <img />
        <div className="card-body">
            <p className="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
            <a href="https://www.lipsum.com/" target="_blank" rel="noopener noreferrer" className="card-link">Card link</a>
            <a href="https://www.lipsum.com/" target="_blank" rel="noopener noreferrer" className="card-link">Another link</a>
        </div>
    </div>
  );
}