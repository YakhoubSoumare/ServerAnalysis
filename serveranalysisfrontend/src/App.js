import {React, useRef, useState} from 'react';
import { BrowserRouter as Router, Link, Routes, Route, useLocation } from "react-router-dom";
import './App.css';
import Home from './components/Home';
import Thesis from './components/Thesis';
import About from './components/About';
import useScreenResize from './customHooks/useScreenResize';
import useFetchData from './customHooks/useFetchData';
import useTransparentOnScroll from './customHooks/useTransparentOnScroll';
import useClickOutside from './customHooks/useClickOutside';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPersonBooth } from '@fortawesome/free-solid-svg-icons';
import {Puff} from 'react-loader-spinner';

const baseUrl = 'https://serveranalysisapi.onrender.com/api';
const endpoints =[
    'topics', 
    'sources'
];

function App() {
    const [isOpen, setIsOpen] = useScreenResize(false);
    const {data, loading} = useFetchData(baseUrl, endpoints);

    useTransparentOnScroll();
    const ref = useRef();
    useClickOutside(ref, () => setIsOpen(false));

    if (loading) {
       return displayAccordion();
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
                <MainBody data={data}/>
                <footer className="app-footer">
                    <p>© 2024 - Developed by Yakhoub Soumare, IT-Högskolan & Meta Bytes</p>
                </footer>
            </div>
        </Router>
    );
}
export default App;

function MainBody({ data }) {
    console.log(data);
    const location = useLocation();
    const [sidebarOpen, setSidebarOpen] = useState(false);
  
    return (
      <div className='main-body'> 
        {location.pathname === '/' &&
          <aside className= {sidebarOpen? 'sidebar-container'
          : 'sidebar-container hidden'}>
            <div className="sidebar-toggler" onClick={() => setSidebarOpen(!sidebarOpen)}>
              <FontAwesomeIcon icon={faPersonBooth} />
            </div>
            <div className={sidebarOpen ? 'sidebar' : 'sidebar hide'}>
              <ul>
                {renderSidebarList()}
              </ul> 
            </div>
          </aside>
        }
        <div className="app-body">
          <Routes>
            <Route path="/" element={<Home data={data} />} />
            <Route path="/thesis" element={<Thesis />} />
            <Route path="/about" element={<About />} />
          </Routes>
        </div>
      </div>
    );
  }

const displayAccordion = () => {
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

const renderSidebarList = () => {
    const listItems = [
        { href: "#introduction", text: "Introduction" },
        { href: "#approach", text: "Approach" },
        { href: "#use-cases", text: "Use Cases" },
        { href: "#advantages", text: "Advantages" },
        { href: "#limitations", text: "Limitations" },
        { href: "#comparisons", text: "Comparison" },
        { href: "#industry-insights", text: "Industry Insights" },
        { href: "#beneficiaries", text: "Beneficiaries" },
    ];

    return (
        <ul>
            {listItems.map((item, index) => (
                <li key={index}>
                    <a href={item.href}>{item.text}</a>
                </li>
            ))}
        </ul>
    );
};