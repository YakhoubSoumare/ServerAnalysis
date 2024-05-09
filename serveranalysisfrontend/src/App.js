import './App.css';
import { useState } from 'react';

function App() {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div className="App">
      <header className="App-header">
        <h1>Serververless Functions vs. Server-based Applications</h1>
        <nav className="navbar">
          <div className="hamburger-menu" onClick={() => setIsOpen(!isOpen)}>☰</div>
          <div className={isOpen ? 'menu' : 'menu open'}>
            <ul>
              <li>Home</li>
              <li>Thesis</li>
            </ul>
          </div>
        </nav>
      </header>
    </div>
  );
}

export default App;
