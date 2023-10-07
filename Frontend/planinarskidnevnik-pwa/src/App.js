import React from 'react';

import './App.css';
import { Route } from 'react-router-dom';
import Pocetna from './components/pocetna.components';

export default function App(){

  return(

    <Routes>
      <Route path='/' element={Pocetna/>}/>
      </Route>

    </Routes>

  );

}


