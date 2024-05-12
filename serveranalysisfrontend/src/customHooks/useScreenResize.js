import { useState, useEffect } from 'react';

function useScreenResize(initialState) {
  const [isOpen, setIsOpen] = useState(initialState);

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

  return [isOpen, setIsOpen];
}

export default useScreenResize;