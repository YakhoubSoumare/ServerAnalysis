import { useEffect } from 'react';

export default function useTransparentOnScroll() {
    useEffect(() => {
        const handleScroll = () => {
            const header = document.querySelector('.navbar');
            const footer = document.querySelector('.app-footer');
            if (window.scrollY > 0) {
                header.classList.add('transparent');
                footer.classList.add('transparent');
            } else {
                header.classList.remove('transparent');
                footer.classList.remove('transparent');
            }
        };

        window.addEventListener('scroll', handleScroll);

        return () => {
            window.removeEventListener('scroll', handleScroll);
        };
    }, []);
}