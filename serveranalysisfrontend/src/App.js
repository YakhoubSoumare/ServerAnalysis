import React , { useState } from 'react';
import './App.css';

function App() {

    const [isOpen, setIsOpen] = useState(false);

    return (
        <div className="App">
            <header className="app-header">
                <div className="non-mobile-header">
                    <h1>Serververless Functions vs. Server-based Applications</h1>
                </div>
                <nav className="navbar">
                    <ul className={isOpen ? '' : 'dropdown'}>
                        <li className="home">Home</li>
                        <li>About</li>
                        <li>Thesis</li>
                    </ul>
                    <ul>  
                      <li className="hamburger" onClick={() => {setIsOpen(!isOpen)}}>☰</li>
                    </ul>
                </nav>
                <h1 className="mobile-header">Serververless Functions vs. Server-based Applications</h1>
            </header>
        </div>
    );
}
export default App;
