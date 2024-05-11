import React , { useState, useEffect } from 'react';
import { BrowserRouter as Router, Link, Routes, Route } from "react-router-dom";
import './App.css';
import Home from './components/Home';
import Thesis from './components/Thesis';
import About from './components/About';

function App() {

    const [isOpen, setIsOpen] = useState(false);

    useEffect(() => {
        const handleResize = () => {
            if (window.innerWidth > 600) {
                setIsOpen(false);
            }
        };

        window.addEventListener('resize', handleResize);

        return () => {
            window.removeEventListener('resize', handleResize);
        };
    }, []);

    return (
        <Router>
            <div className="App">
                <header className="app-header">
                    <div className="non-mobile-header">
                        <h1>Serververless Functions vs. Server-based Applications</h1>
                    </div>
                    <nav className="navbar">
                        <ul className={isOpen ? 'dropdown' : ''}>
                            <li className="home">
                                <Link to="/">Home</Link>
                            </li>
                            <li>
                            <Link to="/thesis">Thesis</Link>
                            </li>
                            <li>
                            <Link to="/about">About</Link>
                            </li>
                        </ul>
                        <ul>  
                        <li className="hamburger" onClick={() => {setIsOpen(!isOpen)}}>☰</li>
                        </ul>
                    </nav>
                </header>
                <body className="app-body">
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/thesis" element={<Thesis />} />
                        <Route path="/about" element={<About />} />
                    </Routes>
                </body>
                <footer className="app-footer">
                    <p>© 2024 - Author Yakhoub Soumare</p>
                </footer>
            </div>
        </Router>
    );
}
export default App;
