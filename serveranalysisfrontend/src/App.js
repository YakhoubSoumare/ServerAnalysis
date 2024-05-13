import {React, useRef} from 'react';
import { BrowserRouter as Router, Link, Routes, Route } from "react-router-dom";
import './App.css';
import Home from './components/Home';
import Thesis from './components/Thesis';
import About from './components/About';
import useScreenResize from './customHooks/useScreenResize';
import useFetchData from './customHooks/useFetchData';
import useTransparentOnScroll from './customHooks/useTransparentOnScroll';
import useClickOutside from './customHooks/useClickOutside';
import SectionCard from './cards/SectionCard';
import {Puff} from 'react-loader-spinner';

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
    const {data, loading} = useFetchData(baseUrl, endpoints);

    useTransparentOnScroll();
    const ref = useRef();
    useClickOutside(ref, () => setIsOpen(false));

    if (loading) {
        return (
            <div className='accordion'>
                <Puff
                    color="#00BFFF"
                    height={100}
                    width={100}
                />
                <div>
                    <p><strong>Please wait,</strong></p>
                    <p>Initial load may take up to 50 seconds</p>
                    <p><strong>due to free tier hosting plan.</strong></p>
                    <p>Thank you for your patience.</p>
                </div>
            </div>
        );
    }

    return (
        <Router>
            <div className="App">
                <nav className="navbar" ref={ref}>
                    <ul className={isOpen ? 'dropdown' : ''}>
                        <Link to="/" onClick={() => setIsOpen(false)}>
                            <li className="home">
                                Home
                            </li>
                        </Link>
                        <Link to="/thesis" onClick={() => setIsOpen(false)}>
                            <li>
                                Thesis
                            </li>
                        </Link>
                        <Link to="/about" onClick={() => setIsOpen(false)}>
                            <li>
                                About
                            </li>
                        </Link>
                    </ul>
                    <ul>  
                        <li className="hamburger" onClick={() => {setIsOpen(!isOpen)}}>☰</li>
                    </ul>
                </nav>
                <div className="app-body">
                    <Routes>
                        <Route path="/" element={<Home data={data} />} />
                        <Route path="/thesis" element={<Thesis />} />
                        <Route path="/about" element={<About />} />
                    </Routes>
                </div>
                <footer className="app-footer">
                    <p>© 2024 - Developed by Yakhoub Soumare, IT-Högskolan & Meta Bytes</p>
                </footer>
            </div>
        </Router>
    );
}
export default App;
