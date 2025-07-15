import { NavLink } from 'react-router-dom';
import './Sidebar.css';

export default function Sidebar({ onClose }) {
  const links = [
    { to: '/', text: 'Home' },
    { to: '/about', text: 'About' },
  ];

  return (
    <aside className="sidebar">
      <button className="close-btn" onClick={onClose}>
        ✕
      </button>
      <nav>
        <ul>
          {links.map((link, index) => (
            <li key={index}>
              <NavLink
                to={link.to}
                className={({ isActive }) => (isActive ? 'active' : '')}
              >
                {link.text}
              </NavLink>
            </li>
          ))}
        </ul>
      </nav>
    </aside>
  );
}