import React from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route  } from 'react-router-dom';
import Izbornik from './components/izbornik.components';
import Pocetna from './components/pocetna.components';
import NadzornaPloca from './components/nadzornaploca.components';
import Planinari from './components/planinar/planinari.components';

export default function App() {
  return (
    <Router>
      <Izbornik />
      <Routes>
        <Route path='/' element={<Pocetna />} />
        <Route path='/nadzornaploca' element={<NadzornaPloca />} />
        <Route path='/planinar' element={<Planinari />} />
      </Routes>
     
    </Router>
  );
}
