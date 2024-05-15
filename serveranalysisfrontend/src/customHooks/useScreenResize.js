import { useState, useEffect } from 'react';

function useScreenResize(initialState, loading) {
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
      const bodyContent = document.querySelector('.app-body');

      if (!navbarElement || !footerElement || !mainElement) {
        return;
      }

      const navbarHeight = navbarElement.offsetHeight;
      const footerHeight = footerElement.offsetHeight;

      const newHeight = `calc(100vh - ${navbarHeight + footerHeight}px)`;

      mainElement.style.minHeight = newHeight;
      mainElement.style.paddingBottom = `${footerHeight}px`;

      if (sidebarElement && getComputedStyle(sidebarElement).display !== 'none') {
        const sidebarWidth = sidebarElement.offsetWidth;
        bodyContent.style.paddingLeft = `${sidebarWidth}px`;
      } else {
        bodyContent.style.paddingLeft = '0px';
      }
    };

    if (!loading) {
      setElementStyles();
    }

    const footerObserver = new MutationObserver(setElementStyles);
    const footerElement = document.querySelector('.app-footer');
    if (footerElement) {
      footerObserver.observe(footerElement, { attributes: true, childList: true, subtree: true });
    }

    const sidebarObserver = new MutationObserver(setElementStyles);
    const sidebarElement = document.querySelector('.sidebar-container');
    if (sidebarElement) {
      sidebarObserver.observe(sidebarElement, { attributes: true, childList: true, subtree: true });
    }

    window.addEventListener('resize', handleResize);

    return () => {
      window.removeEventListener('resize', handleResize);
      if (footerElement) {
        footerObserver.disconnect();
      }
      if (sidebarElement) {
        sidebarObserver.disconnect();
      }
    };
  }, [loading]);

  return [isOpen, setIsOpen];
}

export default useScreenResize;