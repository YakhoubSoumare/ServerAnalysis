import React from 'react';
import { BrowserRouter as Router, Link, Routes, Route } from "react-router-dom";
import './App.css';
import Home from './components/Home';
import Thesis from './components/Thesis';
import About from './components/About';
import useScreenResize from './customHooks/useScreenResize';
import useFetchData from './customHooks/useFetchData';

const baseUrl = 'https://serveranalysisapi.onrender.com/api';
const endpoints = [
    'topics',
    'serverbasedapplications', 
    'serverlessfunctions',  
    'benefits', 
    'sources'
];

function App() {
    const [isOpen, setIsOpen] = useScreenResize(false);
    // const {data, error} = useFetchData(baseUrl, endpoints);
    const data = useFetchData(baseUrl, endpoints);

    console.log("Fetched data: ", data); // test

    // if (error) {
    //     return <div>Error: {error}</div>; // Add this line
    // }

    return (
        <Router>
            <div className="App">
                <div className="app-header">
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
                </div>
                <div className="app-body">
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/thesis" element={<Thesis />} />
                        <Route path="/about" element={<About />} />
                    </Routes>
                </div>
                <div className="app-footer">
                    <p>© 2024 - Author Yakhoub Soumare</p>
                </div>
            </div>
        </Router>
    );
}
export default App;
