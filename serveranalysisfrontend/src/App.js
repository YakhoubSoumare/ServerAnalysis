import {React, useRef, useState, useEffect} from 'react';
import { BrowserRouter as Router, Link, Routes, Route, useLocation } from "react-router-dom";
import './App.css';
import Home from './components/Home';
import About from './components/About';
import useScreenResize from './customHooks/useScreenResize';
import useFetchData from './customHooks/useFetchData';
import useClickOutside from './customHooks/useClickOutside';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faRightLong, faLeftLong } from '@fortawesome/free-solid-svg-icons';
import {Puff} from 'react-loader-spinner';

let baseUrl;

if (window.location.hostname === 'localhost' || window.location.hostname === '127.0.0.1') {
  const useLocalApi = window.confirm('You want to use local api?');
  baseUrl = useLocalApi ? 'http://localhost:8000/api' : 'https://server-analysis-api-development.azurewebsites.net/api';
} else {
  baseUrl = 'https://server-analysis-api-development.azurewebsites.net/api';
}

const endpoints = [
    'topics', 
    'sources',
    'images',
    'abouts'
];

function App() {
    const {data, loading} = useFetchData(baseUrl, endpoints);

    const ref = useRef();
    useClickOutside(ref, () => setIsOpen(false));
    const [currentRoute, setCurrentRoute] = useState('/');
    const [isOpen, setIsOpen] = useScreenResize(false, loading, currentRoute);
    

    if (loading) {
       return displayAccordion();
    }

    return (
        <Router>
            <div className="App">
                <nav className="navbar transparent" ref={ref}>
                    <ul className={isOpen ? 'dropdown' : ''}>
                        <Link to="/" onClick={() => setIsOpen(false)}>
                            <li className="home">
                                Home
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
                <MainBody data={data} currentRoute={currentRoute} setCurrentRoute={setCurrentRoute}/>
                <footer className="app-footer transparent">
                    <p>© 2024 - Developed by Yakhoub Soumare, IT-Högskolan</p>
                </footer>
            </div>
        </Router>
    );
}
export default App;

function MainBody({ data, currentRoute, setCurrentRoute  }) {

    const location = useLocation();
    const [sidebarOpen, setSidebarOpen] = useState(true);

    useEffect(() => {
        setCurrentRoute(location.pathname);
    }, [location, setCurrentRoute]);

    useEffect(() => {
        const handleResize = () => {
            setSidebarOpen(window.innerWidth >= 600);
        };

        window.addEventListener('resize', handleResize);

        return () => {
            window.removeEventListener('resize', handleResize);
        };
    }, []);
  
    return (
      <div className='main-body'> 
        {currentRoute === '/' &&
          <aside className= {sidebarOpen? 'sidebar-container transparent' : 'sidebar-container hidden'}>
            <div className="sidebar-toggler" onClick={() => setSidebarOpen(!sidebarOpen)}>
                {sidebarOpen ?
                <FontAwesomeIcon icon={faLeftLong} /> 
                :
                <FontAwesomeIcon icon={faRightLong} />
                }
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
            <Route path="/about" element={<About data={data} />} />
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
                <p>Initial load may take few</p>
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