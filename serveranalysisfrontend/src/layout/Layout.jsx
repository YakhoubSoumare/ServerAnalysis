import { useState, useEffect } from 'react';
import Sidebar from '../components/Sidebar';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';
import './Layout.css';

export default function Layout({ children }) {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [isMobile, setIsMobile] = useState(window.innerWidth < 768);

  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth < 768);
      if (window.innerWidth >= 768) {
        setSidebarOpen(false);
      }
    };

    window.addEventListener('resize', handleResize);
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  return (
    <div className="layout">
      <Navbar toggleSidebar={() => setSidebarOpen(!sidebarOpen)} />
      <div className="layout-body">
        {isMobile && sidebarOpen && (
          <Sidebar onClose={() => setSidebarOpen(false)} />
        )}
        <main className="main-content">
          {children}
        </main>
      </div>
      <Footer />
    </div>
  );
}