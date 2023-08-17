import React, { useState, useEffect } from 'react';
import Facebook from '../Facebook/Facebook';
import ImageCropDialog from '../ImageCropDialog';
import client, { config } from '../utils';

import './SideBar.css'
function SideBar(){
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const [imageLinks, setImageLinks] = useState([]);
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
    return(<>
     {
        show ?
          <Facebook
          imageUrl
            open={show}
            handleClose={handleClose}
           
          />
          : <></>

      }
        <div className="sidebar">
    <h1>Automate Posting</h1>
    <ul>
      <li><a onClick={() => {setShow(true)} } >View Images</a></li>
      <li><a  href="">History</a></li>
    </ul>
  </div></>
    );
}




export default SideBar;
