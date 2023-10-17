import React from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route  } from 'react-router-dom';
import Izbornik from './components/izbornik.components';
import Pocetna from './components/pocetna.components';
import NadzornaPloca from './components/nadzornaploca.components';
import Planinari from './components/planinar/planinari.components';
import Planine from './components/planina/planine.components';
import DodajPlanina from './components/planina/dodajPlanina.components';
import PromjeniPlanina from './components/planina/promjeniPlanina.components';
import DodajPlaninar from './components/planinar/dodajPlaninara.components';
import PromjeniPlaninar from './components/planinar/promjeniPlaninar.components';

export default function App() {
  return (
    <Router>
      <Izbornik />
      <Routes>
        <Route path='/' element={<Pocetna />} />
        <Route path='/nadzornaploca' element={<NadzornaPloca />} />
      
        <Route path="/planinar" element={<Planinari />} />
        <Route path="/planinari/dodaj" element={<DodajPlaninar/>} />
        <Route path="/planinari/:sifra" element={<PromjeniPlaninar />} />

        <Route path="/planina" element={<Planine />} />
        <Route path="/planine/dodaj" element={<DodajPlanina />} />
        <Route path="/planina/:sifra" element={<PromjeniPlanina />} />

      </Routes>
     
    </Router>
  );
}
