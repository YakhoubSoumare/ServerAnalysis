import { NavLink } from 'react-router-dom';
import './Navbar.css';

export default function Navbar({ toggleSidebar }) {
  return (
    <header className="navbar">
      <h1 className="logo">Server Analysis</h1>
      <nav>
        <ul>
          <li>
            <NavLink to="/" end>
              Home
            </NavLink>
          </li>
          <li>
            <NavLink to="/about">
              About
            </NavLink>
          </li>
        </ul>
      </nav>
      <button className="toggle-btn" onClick={toggleSidebar}>
        ☰
      </button>
    </header>
  );
}