import React, { useState, useEffect } from 'react';
import SideBar from '../SideBar/SideBar';
import client, { config } from '../utils';
import ImageCropDialog from "../ImageCropDialog";
import "../App.css";
import 'bootstrap/dist/css/bootstrap.css';
import './ImageGallery.css'

function ImageGallery() {
  const [imageLinks, setImageLinks] = useState([]);
  const [selectedPlatform, setSelectedPlatform] = useState(null);
  
  const handlePlatformSelection = (platform) => {
    setSelectedPlatform(platform);
  };
  useEffect(() => {
    // Fetch image links from your API endpoint    
    client.get('DropBox/GetImagesLinks', { headers: { 'Content-Type': 'application/json', } })
      .then(response => {
        setImageLinks(response.data);
      })
      .catch(error => {
        console.error('Error fetching image links:', error);
      });
  }, []);

  return (
    <div>
      {selectedPlatform && (
      <ImageCropDialog
        selectedPlatform={selectedPlatform}
        open={true} // Show the dialog directly
        handleClose={() => setSelectedPlatform(null)} // Close the dialog when needed
      />
    )}
    <div >
      <button onClick={() => handlePlatformSelection('facebook')}>Facebook</button>
      <button onClick={() => handlePlatformSelection('twitter')}>Twitter</button>
      <button onClick={() => handlePlatformSelection('instagram')}>Instagram</button>
    </div>

  
    </div>
     

  );
}

export default ImageGallery;