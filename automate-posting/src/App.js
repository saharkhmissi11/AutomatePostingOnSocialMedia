import "./App.css";
import { BrowserRouter , Routes, Route } from 'react-router-dom';

import { useState } from "react";
import ImageCropDialog from "./ImageCropDialog";
import c0 from "./Images/10000.jpg";
import c1 from "./Images/10001.jpg";
import c2 from "./Images/10002.jpg";
import ImageGallery from "./ImageGallery/ImageGallery";
import SideBar from "./SideBar/SideBar";
import Example from "./ImageCropDialogg";
import Facebook from "./Facebook/Facebook";
import Products from "./Projects/Projects";
import Projects from "./Projects/Projects";
import Medias from "./Medias/Medias";
const initData = [
  {
    id: 1,
    imageUrl: c0,
    croppedImageUrl: null,
  },
  {
    id: 2,
    imageUrl: c1,
    croppedImageUrl: null,
  },
  {
    id: 3,
    imageUrl: c2,
    croppedImageUrl: null,
  },
 
];

function App() {
  const [cars, setCars] = useState(initData);
  const [selectedCar, setSelectedCar] = useState(null);

  const onCancel = () => {
    setSelectedCar(null);
  };

  const setCroppedImageFor = (id, crop, zoom, aspect, croppedImageUrl) => {
    const newCarsList = [...cars];
    const carIndex = cars.findIndex((x) => x.id === id);
    const car = cars[carIndex];
    const newCar = { ...car, croppedImageUrl, crop, zoom, aspect };
    newCarsList[carIndex] = newCar;
    setCars(newCarsList);
    console.log("new car");
    setSelectedCar(null);

  };

  const resetImage = (id) => {
    setCroppedImageFor(id);
  };

  return (
    <div>
       
      <BrowserRouter>
    <div className="App">
     <Routes>
     <Route path="/ImageGallery" element={<ImageGallery/>} />
     <Route path="/SideBar" element={<SideBar/>} />
     <Route path="/dialogbox" element={<Example/>} />
     <Route path="/facebook" element={<Facebook/>} />
     <Route path="/projects" element={<Projects/>} />
     <Route path="/medias" element={<Medias/>} />
    </Routes>
      
    </div>
   
    </BrowserRouter>
    </div>
  );
}

export default App;