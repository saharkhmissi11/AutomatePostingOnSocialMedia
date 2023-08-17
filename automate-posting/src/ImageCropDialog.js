import React, { useState, useEffect } from "react";
import Cropper from "react-easy-crop";
import getCroppedImg from "./CropImage";
import Button from 'react-bootstrap/Button';
import cropLogo from "./Images/crop-logo.png"
import zoomLogo from './Images/zoom.png'
import facebook from './Images/facebook.png'
import instagram from './Images/instagram.png'
import twitter from './Images/twitter.avif'
import Form from 'react-bootstrap/Form';

import "./App.css"
import "./ImageGallery/ImageGallery.css"
import Modal from 'react-bootstrap/Modal';
import { Col, Row, Container } from "react-bootstrap";
import client from "./utils";

const p={
  facebook:facebook,
  instagram:instagram,
  twitter:twitter,
}
const ImageCropDialog = ({
  selectedPlatform, open, handleClose
}) => {
  const aspectRatios = {
    facebook: 1200 / 1400,
    instagram: 1080 / 1080,
    twitter: 1600 / 900,
  };
  const aspectRatio = aspectRatios[selectedPlatform];

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


  const [croppedImageUrls, setCroppedImageUrls] = useState([]);
  const initialCropState = [];
  const initialZoomState = [];
  for (let i = 0; i < imageLinks.length; i++) {
    initialCropState.push({ x: 0, y: 0 });
    initialZoomState.push(1);
  }
  const [cropperCrop, setCropperCrop] = useState([]);
  const [cropperZoom, setCropperZoom] = useState(new Array(imageLinks.length).fill(1));
  for (let i = 0; i < imageLinks.length; i++) {
    cropperCrop.push({ x: 0, y: 0 });
    cropperZoom.push(1);
  }

  const VerticalLineWithShadow = () => {
    return (
      <div className="vertical-line"></div>
    );
  };



  const [croppedAreaPixels, setCroppedAreaPixels] = useState(new Array(imageLinks.length).fill(null));





  const onCropChange = (crop, index) => {
    setCropperCrop(prevCrop => {
      const newCrop = [...prevCrop];
      newCrop[index] = { ...newCrop[index], x: crop.x, y: crop.y }; // Update the x property
      return newCrop;
    });
  };
  const onZoomChange = (zoom, index) => {
    setCropperZoom(prevZoom => {
      const newZoom = [...prevZoom];
      newZoom[index] = zoom;
      return newZoom;
    });
  };


  const onCropComplete = (croppedArea, croppedAreaPixels, index) => {
    setCroppedAreaPixels((prevCroppedAreaPixels) => {
      const newCroppedAreaPixels = [...prevCroppedAreaPixels];
      newCroppedAreaPixels[index] = croppedAreaPixels; // Update the cropped area pixels for the specific image
      return newCroppedAreaPixels;
    });
  };


  // encodeURIComponent(croppedImageUrl)
  const onBeginProcess = async (imageUrl) => {

    const requestBody = {
      primaryProduct: "primaryProduct",
      secondaryProducts: "secondaryProducts",
      imagePathFacebook: await getCroppedImg(imageUrl, croppedAreaPixels, selectedPlatform),
      imagePathInstagram: await getCroppedImg(imageUrl, croppedAreaPixels, selectedPlatform),
      imagePathTwitter: await getCroppedImg(imageUrl, croppedAreaPixels, selectedPlatform),

    };
    client.post("Process/begin", requestBody, {
      headers: {
        'Content-Type': 'application/json', // Use 'application/json' for JSON data
      },
    })
      .then(response => console.log("result put ", response))
      .catch(error => console.error("Error:", error)); // Use error parameter to catch errors
  };
  return (
    <Modal show={open} fullscreen={true} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>
          <Row>
            <Col xs={2}> <img style={{ width: '30px' }} src={cropLogo} /></Col>
            <Col xs={10}>  <h2> Crop Image</h2></Col>
          </Row>
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>

        <Row>
          <Col xs={1}><img style={{ width: '35px' }} src={p[selectedPlatform]} /></Col> {'\u00a0'}

          <Col xs={10}>  <h4 >{selectedPlatform}</h4></Col>
        </Row><br></br>

        <Container>
                  <Col xs={"auto"}>

                  </Col>
                  <Col>    
          <Row>

           
            {
              imageLinks.map((link, index) => (

                <>

                  <Col xs="auto">
                    <Row>
                      <Cropper
                        key={index}
                        image={link}
                        zoom={cropperZoom[index]}
                        crop={cropperCrop[index]}
                        aspect={aspectRatio}
                        onCropChange={crop => onCropChange(crop, index)}
                        onZoomChange={zoom => onZoomChange(zoom, index)}
                        onCropComplete={(croppedArea, croppedAreaPixels) => onCropComplete(croppedArea, croppedAreaPixels[index], index)}
                        style={
                          {
                            containerStyle: {
                              position: 'relative',
                              width: '250px', // Set the width to 500px
                              height: '250px', // Set the height to 500px
                            },

                            cropGuideStyle: {}, // You can customize the crop guide style if needed
                            mediaStyle: {},
                          }
                        }
                      />  </Row><br></br>
                    <Row>

                      <input
                        type="range"
                        min={1}
                        max={3}
                        step={0.1}
                        value={cropperZoom[index]}
                        onInput={(e) => {
                          onZoomChange(e.target.value, index); // Pass index to onZoomChange
                        }}
                        className="slider"
                      ></input><br></br><br></br>
                    </Row>

                  </Col>
                  <Col xs={"auto"}>

                  </Col>


                </>
              )
              )
            }

          </Row> 
          </Col>
        </Container>

      </Modal.Body>

      <Modal.Footer >
        <Button style={{ width: '100px' }} variant="primary" onClick={() => { handleClose(); onBeginProcess() }}>
          Crop
        </Button>
        <Button style={{ width: '100px' }} variant="secondary" onClick={() => { handleClose(); }}>
          Cancel
        </Button>
      </Modal.Footer>
    </Modal>
  );



};

export default ImageCropDialog;