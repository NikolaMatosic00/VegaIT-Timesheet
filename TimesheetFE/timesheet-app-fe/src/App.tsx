import React from 'react';
import './App.css';
import NavbarComponent from './common/components/NavbarComponent';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import ClientsPage from './features/clients/components/ClientsPage';
import FooterComponent from './common/components/FooterComponent';
import ProjectsPage from './features/projects/components/ProjectsPage';
import CategoriesPage from './features/categories/components/CategoryPage';

function App() {
  return (
    <BrowserRouter>
    <NavbarComponent/>
    <Routes>
      <Route path="/clients" element={<ClientsPage/>} />
      <Route path="/projects" element={<ProjectsPage/>} />
      <Route path="/categories" element={<CategoriesPage/>} />
      </Routes>
      <FooterComponent/>
    </BrowserRouter>
  );
}

export default App;
