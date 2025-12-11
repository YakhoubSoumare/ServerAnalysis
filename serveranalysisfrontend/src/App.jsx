import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './layout/Layout';
import Home from './pages/Home';
import About from './pages/About';
import useFetchData from './hooks/useFetchData';

// Manuell switch
const USE_AZURE = false;

const API_BASE_URL = USE_AZURE
  ? 'https://server-analysis-api-development.azurewebsites.net/api'
  : 'http://localhost:8080/api';

export default function App() {
  // const { data, loading } = useFetchData(
  //   'https://server-analysis-api-development.azurewebsites.net/api',
  //   ['topics', 'sources', 'images', 'abouts']
  // );

  const { data, loading } = useFetchData(
    API_BASE_URL,
    ['topics', 'sources', 'images', 'abouts']
  );

  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/" element={<Home data={data} loading={loading} />} />
          <Route path="/about" element={<About data={data} loading={loading} />} />
        </Routes>
      </Layout>
    </Router>
  );
}