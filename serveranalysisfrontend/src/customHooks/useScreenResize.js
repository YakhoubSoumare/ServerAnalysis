import { useState, useEffect } from 'react';

function useScreenResize(initialState) {
  const [isOpen, setIsOpen] = useState(initialState);

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth > 600) {
        setIsOpen(false);
      }
    };

    const setElementStyles = () => {
      const navbarElement = document.querySelector('.navbar');
      const footerElement = document.querySelector('.app-footer');
      const mainElement = document.querySelector('.main-body');
      const sidebarElement = document.querySelector('.sidebar-container');

      if (!navbarElement || !footerElement || !mainElement || !sidebarElement) {
        return;
      }

      const navbarHeight = navbarElement.offsetHeight;
      const footerHeight = footerElement.offsetHeight;

      const newHeight = `calc(100vh - ${navbarHeight + footerHeight}px)`;

      mainElement.style.height = newHeight;
      // sidebarElement.style.height = newHeight;
      // sidebarElement.style.top = `${navbarHeight}px`;
      // sidebarElement.style.bottom = `${footerHeight}px`;

      // // Add these lines
      // sidebarElement.style.position = 'fixed'; // or 'absolute'
      // sidebarElement.style.overflow = 'auto'; // or 'scroll'
      // mainElement.style.overflow = 'hidden'; // prevent outer scroll
    };

    setElementStyles();

    window.addEventListener('resize', handleResize);

    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);

  return [isOpen, setIsOpen];
}

export default useScreenResize;