import "./App.css";
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { useState } from "react";
import ImageCropDialog from "./ImageCropDialog";
import Projects from "./Projects/Projects";
import Medias from "./Medias/Medias";
function App() {
  return (
    <div>
      <BrowserRouter>
        <div className="App">
          <Routes>
            <Route path="/projects" element={<Projects />} />
            <Route path="/medias" element={<Medias />} />
          </Routes>
        </div>
      </BrowserRouter>
    </div>
  );
}
export default App;